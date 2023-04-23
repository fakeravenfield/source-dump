using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007E4 RID: 2020
	internal abstract class Expression : NodeBase
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x00022D51 File Offset: 0x00020F51
		public Expression(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00002FD8 File Offset: 0x000011D8
		public virtual string GetFriendlyDebugName()
		{
			return null;
		}

		// Token: 0x0600326F RID: 12911
		public abstract DynValue Eval(ScriptExecutionContext context);

		// Token: 0x06003270 RID: 12912 RVA: 0x00002FD8 File Offset: 0x000011D8
		public virtual SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return null;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00110BAC File Offset: 0x0010EDAC
		internal static List<Expression> ExprListAfterFirstExpr(ScriptLoadingContext lcontext, Expression expr1)
		{
			List<Expression> list = new List<Expression>();
			list.Add(expr1);
			while (lcontext.Lexer.Current.Type == TokenType.Comma)
			{
				lcontext.Lexer.Next();
				list.Add(Expression.Expr(lcontext));
			}
			return list;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00110BF4 File Offset: 0x0010EDF4
		internal static List<Expression> ExprList(ScriptLoadingContext lcontext)
		{
			List<Expression> list = new List<Expression>();
			for (;;)
			{
				list.Add(Expression.Expr(lcontext));
				if (lcontext.Lexer.Current.Type != TokenType.Comma)
				{
					break;
				}
				lcontext.Lexer.Next();
			}
			return list;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x00022D5A File Offset: 0x00020F5A
		internal static Expression Expr(ScriptLoadingContext lcontext)
		{
			return Expression.SubExpr(lcontext, true);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00110C38 File Offset: 0x0010EE38
		internal static Expression SubExpr(ScriptLoadingContext lcontext, bool isPrimary)
		{
			Token token = lcontext.Lexer.Current;
			Expression expression;
			if (token.IsUnaryOperator())
			{
				lcontext.Lexer.Next();
				expression = Expression.SubExpr(lcontext, false);
				Token unaryOpToken = token;
				token = lcontext.Lexer.Current;
				if (isPrimary && token.Type == TokenType.Op_Pwr)
				{
					List<Expression> list = new List<Expression>();
					list.Add(expression);
					while (isPrimary && token.Type == TokenType.Op_Pwr)
					{
						lcontext.Lexer.Next();
						list.Add(Expression.SubExpr(lcontext, false));
						token = lcontext.Lexer.Current;
					}
					expression = list[list.Count - 1];
					for (int i = list.Count - 2; i >= 0; i--)
					{
						expression = BinaryOperatorExpression.CreatePowerExpression(list[i], expression, lcontext);
					}
				}
				expression = new UnaryOperatorExpression(lcontext, expression, unaryOpToken);
			}
			else
			{
				expression = Expression.SimpleExp(lcontext);
			}
			token = lcontext.Lexer.Current;
			if (isPrimary && token.IsBinaryOperator())
			{
				object chain = BinaryOperatorExpression.BeginOperatorChain();
				BinaryOperatorExpression.AddExpressionToChain(chain, expression);
				while (token.IsBinaryOperator())
				{
					BinaryOperatorExpression.AddOperatorToChain(chain, token);
					lcontext.Lexer.Next();
					Expression exp = Expression.SubExpr(lcontext, false);
					BinaryOperatorExpression.AddExpressionToChain(chain, exp);
					token = lcontext.Lexer.Current;
				}
				expression = BinaryOperatorExpression.CommitOperatorChain(chain, lcontext);
			}
			return expression;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00110D84 File Offset: 0x0010EF84
		internal static Expression SimpleExp(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			TokenType type = token.Type;
			if (type > TokenType.True)
			{
				if (type <= TokenType.Brk_Open_Curly)
				{
					if (type == TokenType.VarArgs)
					{
						return new SymbolRefExpression(token, lcontext);
					}
					if (type != TokenType.Brk_Open_Curly)
					{
						goto IL_9A;
					}
				}
				else
				{
					if (type - TokenType.String <= 4)
					{
						goto IL_5C;
					}
					if (type != TokenType.Brk_Open_Curly_Shared)
					{
						goto IL_9A;
					}
				}
				return new TableConstructor(lcontext, token.Type == TokenType.Brk_Open_Curly_Shared);
			}
			switch (type)
			{
			case TokenType.False:
				break;
			case TokenType.For:
				goto IL_9A;
			case TokenType.Function:
				lcontext.Lexer.Next();
				return new FunctionDefinitionExpression(lcontext, false, false);
			case TokenType.Lambda:
				return new FunctionDefinitionExpression(lcontext, false, true);
			default:
				if (type != TokenType.Nil && type != TokenType.True)
				{
					goto IL_9A;
				}
				break;
			}
			IL_5C:
			return new LiteralExpression(lcontext, token);
			IL_9A:
			return Expression.PrimaryExp(lcontext);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x00110E34 File Offset: 0x0010F034
		internal static Expression PrimaryExp(ScriptLoadingContext lcontext)
		{
			Expression expression = Expression.PrefixExp(lcontext);
			for (;;)
			{
				Token token = lcontext.Lexer.Current;
				Token thisCallName = null;
				TokenType type = token.Type;
				if (type <= TokenType.Colon)
				{
					if (type == TokenType.Dot)
					{
						lcontext.Lexer.Next();
						Token token2 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
						expression = new IndexExpression(expression, token2.Text, lcontext);
						continue;
					}
					if (type != TokenType.Colon)
					{
						break;
					}
					lcontext.Lexer.Next();
					thisCallName = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				}
				else
				{
					switch (type)
					{
					case TokenType.Brk_Open_Curly:
					case TokenType.Brk_Open_Round:
						break;
					case TokenType.Brk_Close_Round:
					case TokenType.Brk_Close_Square:
						return expression;
					case TokenType.Brk_Open_Square:
					{
						Token originalToken = lcontext.Lexer.Current;
						lcontext.Lexer.Next();
						Expression expression2 = Expression.Expr(lcontext);
						if (lcontext.Lexer.Current.Type == TokenType.Comma)
						{
							expression2 = new ExprListExpression(Expression.ExprListAfterFirstExpr(lcontext, expression2), lcontext);
						}
						NodeBase.CheckMatch(lcontext, originalToken, TokenType.Brk_Close_Square, "]");
						expression = new IndexExpression(expression, expression2, lcontext);
						continue;
					}
					default:
						if (type - TokenType.String > 1 && type != TokenType.Brk_Open_Curly_Shared)
						{
							return expression;
						}
						break;
					}
				}
				expression = new FunctionCallExpression(lcontext, expression, thisCallName);
			}
			return expression;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00110F54 File Offset: 0x0010F154
		private static Expression PrefixExp(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			TokenType type = token.Type;
			if (type == TokenType.Name)
			{
				return new SymbolRefExpression(token, lcontext);
			}
			if (type == TokenType.Brk_Open_Round)
			{
				lcontext.Lexer.Next();
				Expression expression = Expression.Expr(lcontext);
				expression = new AdjustmentExpression(lcontext, expression);
				NodeBase.CheckMatch(lcontext, token, TokenType.Brk_Close_Round, ")");
				return expression;
			}
			throw new SyntaxErrorException(token, "unexpected symbol near '{0}'", new object[]
			{
				token.Text
			})
			{
				IsPrematureStreamTermination = (token.Type == TokenType.Eof)
			};
		}
	}
}
