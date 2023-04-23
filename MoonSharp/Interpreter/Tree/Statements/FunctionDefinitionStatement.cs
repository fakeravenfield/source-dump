using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020007F7 RID: 2039
	internal class FunctionDefinitionStatement : Statement
	{
		// Token: 0x060032D2 RID: 13010 RVA: 0x001136B0 File Offset: 0x001118B0
		public FunctionDefinitionStatement(ScriptLoadingContext lcontext, bool local, Token localToken) : base(lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Function);
			token = (localToken ?? token);
			this.m_Local = local;
			if (this.m_Local)
			{
				Token token2 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				this.m_FuncSymbol = lcontext.Scope.TryDefineLocal(token2.Text);
				this.m_FriendlyName = string.Format("{0} (local)", token2.Text);
				this.m_SourceRef = token.GetSourceRef(token2, true);
			}
			else
			{
				Token token3 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				string text = token3.Text;
				this.m_SourceRef = token.GetSourceRef(token3, true);
				this.m_FuncSymbol = lcontext.Scope.Find(text);
				this.m_FriendlyName = text;
				if (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
				{
					this.m_TableAccessors = new List<string>();
					while (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
					{
						Token token4 = lcontext.Lexer.Current;
						if (token4.Type != TokenType.Colon && token4.Type != TokenType.Dot)
						{
							NodeBase.UnexpectedTokenType(token4);
						}
						lcontext.Lexer.Next();
						Token token5 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
						this.m_FriendlyName = this.m_FriendlyName + token4.Text + token5.Text;
						this.m_SourceRef = token.GetSourceRef(token5, true);
						if (token4.Type == TokenType.Colon)
						{
							this.m_MethodName = token5.Text;
							this.m_IsMethodCallingConvention = true;
							break;
						}
						this.m_TableAccessors.Add(token5.Text);
					}
					if (this.m_MethodName == null && this.m_TableAccessors.Count > 0)
					{
						this.m_MethodName = this.m_TableAccessors[this.m_TableAccessors.Count - 1];
						this.m_TableAccessors.RemoveAt(this.m_TableAccessors.Count - 1);
					}
				}
			}
			this.m_FuncDef = new FunctionDefinitionExpression(lcontext, this.m_IsMethodCallingConvention, false);
			lcontext.Source.Refs.Add(this.m_SourceRef);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x001138B4 File Offset: 0x00111AB4
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_SourceRef))
			{
				if (this.m_Local)
				{
					bc.Emit_Literal(DynValue.Nil);
					bc.Emit_Store(this.m_FuncSymbol, 0, 0);
					this.m_FuncDef.Compile(bc, () => this.SetFunction(bc, 2), this.m_FriendlyName);
				}
				else if (this.m_MethodName == null)
				{
					this.m_FuncDef.Compile(bc, () => this.SetFunction(bc, 1), this.m_FriendlyName);
				}
				else
				{
					this.m_FuncDef.Compile(bc, () => this.SetMethod(bc), this.m_FriendlyName);
				}
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x001139A8 File Offset: 0x00111BA8
		private int SetMethod(ByteCode bc)
		{
			int num = 0;
			num += bc.Emit_Load(this.m_FuncSymbol);
			foreach (string str in this.m_TableAccessors)
			{
				bc.Emit_Index(DynValue.NewString(str), true, false);
				num++;
			}
			bc.Emit_IndexSet(0, 0, DynValue.NewString(this.m_MethodName), true, false);
			return 1 + num;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0002305C File Offset: 0x0002125C
		private int SetFunction(ByteCode bc, int numPop)
		{
			int num = bc.Emit_Store(this.m_FuncSymbol, 0, 0);
			bc.Emit_Pop(numPop);
			return num + 1;
		}

		// Token: 0x04002D01 RID: 11521
		private SymbolRef m_FuncSymbol;

		// Token: 0x04002D02 RID: 11522
		private SourceRef m_SourceRef;

		// Token: 0x04002D03 RID: 11523
		private bool m_Local;

		// Token: 0x04002D04 RID: 11524
		private bool m_IsMethodCallingConvention;

		// Token: 0x04002D05 RID: 11525
		private string m_MethodName;

		// Token: 0x04002D06 RID: 11526
		private string m_FriendlyName;

		// Token: 0x04002D07 RID: 11527
		private List<string> m_TableAccessors;

		// Token: 0x04002D08 RID: 11528
		private FunctionDefinitionExpression m_FuncDef;
	}
}
