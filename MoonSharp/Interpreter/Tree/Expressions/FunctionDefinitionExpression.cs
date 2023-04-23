using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x0200080A RID: 2058
	internal class FunctionDefinitionExpression : Expression, IClosureBuilder
	{
		// Token: 0x0600332E RID: 13102 RVA: 0x0002335A File Offset: 0x0002155A
		public FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool usesGlobalEnv) : this(lcontext, false, usesGlobalEnv, false)
		{
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x00023366 File Offset: 0x00021566
		public FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool pushSelfParam, bool isLambda) : this(lcontext, pushSelfParam, false, isLambda)
		{
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x001151F4 File Offset: 0x001133F4
		private FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool pushSelfParam, bool usesGlobalEnv, bool isLambda) : base(lcontext)
		{
			this.m_UsesGlobalEnv = usesGlobalEnv;
			if (usesGlobalEnv)
			{
				NodeBase.CheckTokenType(lcontext, TokenType.Function);
			}
			Token token = NodeBase.CheckTokenType(lcontext, isLambda ? TokenType.Lambda : TokenType.Brk_Open_Round);
			List<string> paramnames = this.BuildParamList(lcontext, pushSelfParam, token, isLambda);
			this.m_Begin = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			lcontext.Scope.PushFunction(this, this.m_HasVarArgs);
			if (this.m_UsesGlobalEnv)
			{
				this.m_Env = lcontext.Scope.DefineLocal("_ENV");
			}
			else
			{
				lcontext.Scope.ForceEnvUpValue();
			}
			this.m_ParamNames = this.DefineArguments(paramnames, lcontext);
			if (isLambda)
			{
				this.m_Statement = this.CreateLambdaBody(lcontext);
			}
			else
			{
				this.m_Statement = this.CreateBody(lcontext);
			}
			this.m_StackFrame = lcontext.Scope.PopFunction();
			lcontext.Source.Refs.Add(this.m_Begin);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x00115308 File Offset: 0x00113508
		private Statement CreateLambdaBody(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			Expression e = Expression.Expr(lcontext);
			Token to = lcontext.Lexer.Current;
			SourceRef sourceRefUpTo = token.GetSourceRefUpTo(to, true);
			return new ReturnStatement(lcontext, e, sourceRefUpTo);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x00115344 File Offset: 0x00113544
		private Statement CreateBody(ScriptLoadingContext lcontext)
		{
			Statement result = new CompositeStatement(lcontext);
			if (lcontext.Lexer.Current.Type != TokenType.End)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "'end' expected near '{0}'", new object[]
				{
					lcontext.Lexer.Current.Text
				})
				{
					IsPrematureStreamTermination = (lcontext.Lexer.Current.Type == TokenType.Eof)
				};
			}
			this.m_End = lcontext.Lexer.Current.GetSourceRef(true);
			lcontext.Lexer.Next();
			return result;
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x001153D8 File Offset: 0x001135D8
		private List<string> BuildParamList(ScriptLoadingContext lcontext, bool pushSelfParam, Token openBracketToken, bool isLambda)
		{
			TokenType tokenType = isLambda ? TokenType.Lambda : TokenType.Brk_Close_Round;
			List<string> list = new List<string>();
			if (pushSelfParam)
			{
				list.Add("self");
			}
			while (lcontext.Lexer.Current.Type != tokenType)
			{
				Token token = lcontext.Lexer.Current;
				if (token.Type == TokenType.Name)
				{
					list.Add(token.Text);
				}
				else if (token.Type == TokenType.VarArgs)
				{
					this.m_HasVarArgs = true;
					list.Add("...");
				}
				else
				{
					NodeBase.UnexpectedTokenType(token);
				}
				lcontext.Lexer.Next();
				token = lcontext.Lexer.Current;
				if (token.Type != TokenType.Comma)
				{
					NodeBase.CheckMatch(lcontext, openBracketToken, tokenType, isLambda ? "|" : ")");
					break;
				}
				lcontext.Lexer.Next();
			}
			if (lcontext.Lexer.Current.Type == tokenType)
			{
				lcontext.Lexer.Next();
			}
			return list;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x001154D4 File Offset: 0x001136D4
		private SymbolRef[] DefineArguments(List<string> paramnames, ScriptLoadingContext lcontext)
		{
			HashSet<string> hashSet = new HashSet<string>();
			SymbolRef[] array = new SymbolRef[paramnames.Count];
			for (int i = paramnames.Count - 1; i >= 0; i--)
			{
				if (!hashSet.Add(paramnames[i]))
				{
					paramnames[i] = paramnames[i] + "@" + i.ToString();
				}
				array[i] = lcontext.Scope.DefineLocal(paramnames[i]);
			}
			return array;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0011554C File Offset: 0x0011374C
		public SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol)
		{
			for (int i = 0; i < this.m_Closure.Count; i++)
			{
				if (this.m_Closure[i].i_Name == symbol.i_Name)
				{
					return SymbolRef.Upvalue(symbol.i_Name, i);
				}
			}
			this.m_Closure.Add(symbol);
			if (this.m_ClosureInstruction != null)
			{
				this.m_ClosureInstruction.SymbolList = this.m_Closure.ToArray();
			}
			return SymbolRef.Upvalue(symbol.i_Name, this.m_Closure.Count - 1);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x00023372 File Offset: 0x00021572
		public override DynValue Eval(ScriptExecutionContext context)
		{
			throw new DynamicExpressionException("Dynamic Expressions cannot define new functions.");
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x001155DC File Offset: 0x001137DC
		public int CompileBody(ByteCode bc, string friendlyName)
		{
			string funcName = friendlyName ?? ("<" + this.m_Begin.FormatLocation(bc.Script, true) + ">");
			bc.PushSourceRef(this.m_Begin);
			Instruction instruction = bc.Emit_Jump(OpCode.Jump, -1, 0);
			Instruction instruction2 = bc.Emit_Meta(funcName, OpCodeMetadataType.FunctionEntrypoint, null);
			int jumpPointForLastInstruction = bc.GetJumpPointForLastInstruction();
			bc.Emit_BeginFn(this.m_StackFrame);
			bc.LoopTracker.Loops.Push(new LoopBoundary());
			int jumpPointForLastInstruction2 = bc.GetJumpPointForLastInstruction();
			if (this.m_UsesGlobalEnv)
			{
				bc.Emit_Load(SymbolRef.Upvalue("_ENV", 0));
				bc.Emit_Store(this.m_Env, 0, 0);
				bc.Emit_Pop(1);
			}
			if (this.m_ParamNames.Length != 0)
			{
				bc.Emit_Args(this.m_ParamNames);
			}
			this.m_Statement.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_End);
			bc.Emit_Ret(0);
			bc.LoopTracker.Loops.Pop();
			instruction.NumVal = bc.GetJumpPointForNextInstruction();
			instruction2.NumVal = bc.GetJumpPointForLastInstruction() - jumpPointForLastInstruction;
			bc.PopSourceRef();
			return jumpPointForLastInstruction2;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x00115700 File Offset: 0x00113900
		public int Compile(ByteCode bc, Func<int> afterDecl, string friendlyName)
		{
			using (bc.EnterSource(this.m_Begin))
			{
				SymbolRef[] symbols = this.m_Closure.ToArray();
				this.m_ClosureInstruction = bc.Emit_Closure(symbols, bc.GetJumpPointForNextInstruction());
				int num = afterDecl();
				this.m_ClosureInstruction.NumVal += 2 + num;
			}
			return this.CompileBody(bc, friendlyName);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0002337E File Offset: 0x0002157E
		public override void Compile(ByteCode bc)
		{
			this.Compile(bc, () => 0, null);
		}

		// Token: 0x04002D5B RID: 11611
		private SymbolRef[] m_ParamNames;

		// Token: 0x04002D5C RID: 11612
		private Statement m_Statement;

		// Token: 0x04002D5D RID: 11613
		private RuntimeScopeFrame m_StackFrame;

		// Token: 0x04002D5E RID: 11614
		private List<SymbolRef> m_Closure = new List<SymbolRef>();

		// Token: 0x04002D5F RID: 11615
		private bool m_HasVarArgs;

		// Token: 0x04002D60 RID: 11616
		private Instruction m_ClosureInstruction;

		// Token: 0x04002D61 RID: 11617
		private bool m_UsesGlobalEnv;

		// Token: 0x04002D62 RID: 11618
		private SymbolRef m_Env;

		// Token: 0x04002D63 RID: 11619
		private SourceRef m_Begin;

		// Token: 0x04002D64 RID: 11620
		private SourceRef m_End;
	}
}
