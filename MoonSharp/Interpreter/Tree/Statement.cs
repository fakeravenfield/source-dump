using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Tree.Expressions;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007ED RID: 2029
	internal abstract class Statement : NodeBase
	{
		// Token: 0x060032B8 RID: 12984 RVA: 0x00022D51 File Offset: 0x00020F51
		public Statement(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0011297C File Offset: 0x00110B7C
		protected static Statement CreateStatement(ScriptLoadingContext lcontext, out bool forceLast)
		{
			Token token = lcontext.Lexer.Current;
			forceLast = false;
			TokenType type = token.Type;
			if (type <= TokenType.Return)
			{
				switch (type)
				{
				case TokenType.Break:
					return new BreakStatement(lcontext);
				case TokenType.Do:
					return new ScopeBlockStatement(lcontext);
				case TokenType.Else:
				case TokenType.ElseIf:
				case TokenType.End:
				case TokenType.False:
				case TokenType.Lambda:
				case TokenType.In:
					break;
				case TokenType.For:
					return Statement.DispatchForLoopStatement(lcontext);
				case TokenType.Function:
					return new FunctionDefinitionStatement(lcontext, false, null);
				case TokenType.Goto:
					return new GotoStatement(lcontext);
				case TokenType.If:
					return new IfStatement(lcontext);
				case TokenType.Local:
				{
					Token token2 = lcontext.Lexer.Current;
					lcontext.Lexer.Next();
					if (lcontext.Lexer.Current.Type == TokenType.Function)
					{
						return new FunctionDefinitionStatement(lcontext, true, token2);
					}
					return new AssignmentStatement(lcontext, token2);
				}
				default:
					if (type == TokenType.Repeat)
					{
						return new RepeatStatement(lcontext);
					}
					if (type == TokenType.Return)
					{
						forceLast = true;
						return new ReturnStatement(lcontext);
					}
					break;
				}
			}
			else
			{
				if (type == TokenType.While)
				{
					return new WhileStatement(lcontext);
				}
				if (type == TokenType.DoubleColon)
				{
					return new LabelStatement(lcontext);
				}
				if (type == TokenType.SemiColon)
				{
					lcontext.Lexer.Next();
					return new EmptyStatement(lcontext);
				}
			}
			Token first = lcontext.Lexer.Current;
			Expression expression = Expression.PrimaryExp(lcontext);
			FunctionCallExpression functionCallExpression = expression as FunctionCallExpression;
			if (functionCallExpression != null)
			{
				return new FunctionCallStatement(lcontext, functionCallExpression);
			}
			return new AssignmentStatement(lcontext, expression, first);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x00112AD0 File Offset: 0x00110CD0
		private static Statement DispatchForLoopStatement(ScriptLoadingContext lcontext)
		{
			Token forToken = NodeBase.CheckTokenType(lcontext, TokenType.For);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			if (lcontext.Lexer.Current.Type == TokenType.Op_Assignment)
			{
				return new ForLoopStatement(lcontext, token, forToken);
			}
			return new ForEachLoopStatement(lcontext, token, forToken);
		}
	}
}
