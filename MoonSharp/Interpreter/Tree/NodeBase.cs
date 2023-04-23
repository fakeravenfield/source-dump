using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020007EC RID: 2028
	internal abstract class NodeBase
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x00022F6B File Offset: 0x0002116B
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x00022F73 File Offset: 0x00021173
		public Script Script { get; private set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x00022F7C File Offset: 0x0002117C
		// (set) Token: 0x060032AF RID: 12975 RVA: 0x00022F84 File Offset: 0x00021184
		private protected ScriptLoadingContext LoadingContext { protected get; private set; }

		// Token: 0x060032B0 RID: 12976 RVA: 0x00022F8D File Offset: 0x0002118D
		public NodeBase(ScriptLoadingContext lcontext)
		{
			this.Script = lcontext.Script;
		}

		// Token: 0x060032B1 RID: 12977
		public abstract void Compile(ByteCode bc);

		// Token: 0x060032B2 RID: 12978 RVA: 0x00022FA1 File Offset: 0x000211A1
		protected static Token UnexpectedTokenType(Token t)
		{
			throw new SyntaxErrorException(t, "unexpected symbol near '{0}'", new object[]
			{
				t.Text
			})
			{
				IsPrematureStreamTermination = (t.Type == TokenType.Eof)
			};
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x00112810 File Offset: 0x00110A10
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x00112848 File Offset: 0x00110A48
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType1, TokenType tokenType2)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType1 && token.Type != tokenType2)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x00112888 File Offset: 0x00110A88
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType1, TokenType tokenType2, TokenType tokenType3)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType1 && token.Type != tokenType2 && token.Type != tokenType3)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x001128D0 File Offset: 0x00110AD0
		protected static void CheckTokenTypeNotNext(ScriptLoadingContext lcontext, TokenType tokenType)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType)
			{
				NodeBase.UnexpectedTokenType(token);
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x001128FC File Offset: 0x00110AFC
		protected static Token CheckMatch(ScriptLoadingContext lcontext, Token originalToken, TokenType expectedTokenType, string expectedTokenText)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != expectedTokenType)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "'{0}' expected (to close '{1}' at line {2}) near '{3}'", new object[]
				{
					expectedTokenText,
					originalToken.Text,
					originalToken.FromLine,
					token.Text
				})
				{
					IsPrematureStreamTermination = (token.Type == TokenType.Eof)
				};
			}
			lcontext.Lexer.Next();
			return token;
		}
	}
}
