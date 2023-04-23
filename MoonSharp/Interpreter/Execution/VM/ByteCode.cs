using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x020008AA RID: 2218
	internal class ByteCode : RefIdObject
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000258AB File Offset: 0x00023AAB
		// (set) Token: 0x06003796 RID: 14230 RVA: 0x000258B3 File Offset: 0x00023AB3
		public Script Script { get; private set; }

		// Token: 0x06003797 RID: 14231 RVA: 0x000258BC File Offset: 0x00023ABC
		public ByteCode(Script script)
		{
			this.Script = script;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000258EC File Offset: 0x00023AEC
		public IDisposable EnterSource(SourceRef sref)
		{
			return new ByteCode.SourceCodeStackGuard(sref, this);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000258F5 File Offset: 0x00023AF5
		public void PushSourceRef(SourceRef sref)
		{
			this.m_SourceRefStack.Add(sref);
			this.m_CurrentSourceRef = sref;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x00120DAC File Offset: 0x0011EFAC
		public void PopSourceRef()
		{
			this.m_SourceRefStack.RemoveAt(this.m_SourceRefStack.Count - 1);
			this.m_CurrentSourceRef = ((this.m_SourceRefStack.Count > 0) ? this.m_SourceRefStack[this.m_SourceRefStack.Count - 1] : null);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x00120E00 File Offset: 0x0011F000
		public void Dump(string file)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Code.Count; i++)
			{
				if (this.Code[i].OpCode == OpCode.Debug)
				{
					stringBuilder.AppendFormat("    {0}\n", this.Code[i]);
				}
				else
				{
					stringBuilder.AppendFormat("{0:X8}  {1}\n", i, this.Code[i]);
				}
			}
			File.WriteAllText(file, stringBuilder.ToString());
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0002590A File Offset: 0x00023B0A
		public int GetJumpPointForNextInstruction()
		{
			return this.Code.Count;
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x00025917 File Offset: 0x00023B17
		public int GetJumpPointForLastInstruction()
		{
			return this.Code.Count - 1;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x00025926 File Offset: 0x00023B26
		public Instruction GetLastInstruction()
		{
			return this.Code[this.Code.Count - 1];
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x00025940 File Offset: 0x00023B40
		private Instruction AppendInstruction(Instruction c)
		{
			this.Code.Add(c);
			return c;
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x0002594F File Offset: 0x00023B4F
		public Instruction Emit_Nop(string comment)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Nop,
				Name = comment
			});
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x00025970 File Offset: 0x00023B70
		public Instruction Emit_Invalid(string type)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Invalid,
				Name = type
			});
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x00025992 File Offset: 0x00023B92
		public Instruction Emit_Pop(int num = 1)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Pop,
				NumVal = num
			});
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000259B3 File Offset: 0x00023BB3
		public void Emit_Call(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Call,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000259DD File Offset: 0x00023BDD
		public void Emit_ThisCall(int argCount, string debugName)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ThisCall,
				NumVal = argCount,
				Name = debugName
			});
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x00025A07 File Offset: 0x00023C07
		public Instruction Emit_Literal(DynValue value)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Literal,
				Value = value
			});
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x00025A28 File Offset: 0x00023C28
		public Instruction Emit_Jump(OpCode jumpOpCode, int idx, int optPar = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = jumpOpCode,
				NumVal = idx,
				NumVal2 = optPar
			});
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x00025A50 File Offset: 0x00023C50
		public Instruction Emit_MkTuple(int cnt)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.MkTuple,
				NumVal = cnt
			});
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x00120E84 File Offset: 0x0011F084
		public Instruction Emit_Operator(OpCode opcode)
		{
			Instruction result = this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opcode
			});
			if (opcode == OpCode.LessEq)
			{
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.CNot
				});
			}
			if (opcode == OpCode.Eq || opcode == OpCode.Less)
			{
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.ToBool
				});
			}
			return result;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x00025A72 File Offset: 0x00023C72
		[Conditional("EMIT_DEBUG_OPS")]
		public void Emit_Debug(string str)
		{
			this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Debug,
				Name = str.Substring(0, Math.Min(32, str.Length))
			});
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x00025AA7 File Offset: 0x00023CA7
		public Instruction Emit_Enter(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x00025ADA File Offset: 0x00023CDA
		public Instruction Emit_Leave(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.To
			});
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x00025AA7 File Offset: 0x00023CA7
		public Instruction Emit_Exit(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.From,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x00025B0D File Offset: 0x00023D0D
		public Instruction Emit_Clean(RuntimeScopeBlock runtimeScopeBlock)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Clean,
				NumVal = runtimeScopeBlock.To + 1,
				NumVal2 = runtimeScopeBlock.ToInclusive
			});
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x00025B42 File Offset: 0x00023D42
		public Instruction Emit_Closure(SymbolRef[] symbols, int jmpnum)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Closure,
				SymbolList = symbols,
				NumVal = jmpnum
			});
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x00025B6A File Offset: 0x00023D6A
		public Instruction Emit_Args(params SymbolRef[] symbols)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Args,
				SymbolList = symbols
			});
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x00025B8C File Offset: 0x00023D8C
		public Instruction Emit_Ret(int retvals)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Ret,
				NumVal = retvals
			});
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x00025BAE File Offset: 0x00023DAE
		public Instruction Emit_ToNum(int stage = 0)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ToNum,
				NumVal = stage
			});
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x00025BD0 File Offset: 0x00023DD0
		public Instruction Emit_Incr(int i)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Incr,
				NumVal = i
			});
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x00025BF2 File Offset: 0x00023DF2
		public Instruction Emit_NewTable(bool shared)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.NewTable,
				NumVal = (shared ? 1 : 0)
			});
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x00025C19 File Offset: 0x00023E19
		public Instruction Emit_IterPrep()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterPrep
			});
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x00025C34 File Offset: 0x00023E34
		public Instruction Emit_ExpTuple(int stackOffset)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.ExpTuple,
				NumVal = stackOffset
			});
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x00025C56 File Offset: 0x00023E56
		public Instruction Emit_IterUpd()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.IterUpd
			});
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x00025C71 File Offset: 0x00023E71
		public Instruction Emit_Meta(string funcName, OpCodeMetadataType metaType, DynValue value = null)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Meta,
				Name = funcName,
				NumVal2 = (int)metaType,
				Value = value
			});
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x00120EEC File Offset: 0x0011F0EC
		public Instruction Emit_BeginFn(RuntimeScopeFrame stackFrame)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.BeginFn,
				SymbolList = stackFrame.DebugSymbols.ToArray(),
				NumVal = stackFrame.Count,
				NumVal2 = stackFrame.ToFirstBlock
			});
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x00025CA1 File Offset: 0x00023EA1
		public Instruction Emit_Scalar()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Scalar
			});
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00120F3C File Offset: 0x0011F13C
		public int Emit_Load(SymbolRef sym)
		{
			switch (sym.Type)
			{
			case SymbolRefType.Local:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Local,
					Symbol = sym
				});
				return 1;
			case SymbolRefType.Upvalue:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Upvalue,
					Symbol = sym
				});
				return 1;
			case SymbolRefType.Global:
				this.Emit_Load(sym.i_Env);
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.Index,
					Value = DynValue.NewString(sym.i_Name)
				});
				return 2;
			default:
				throw new InternalErrorException("Unexpected symbol type : {0}", new object[]
				{
					sym
				});
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x00120FFC File Offset: 0x0011F1FC
		public int Emit_Store(SymbolRef sym, int stackofs, int tupleidx)
		{
			switch (sym.Type)
			{
			case SymbolRefType.Local:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.StoreLcl,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx
				});
				return 1;
			case SymbolRefType.Upvalue:
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.StoreUpv,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx
				});
				return 1;
			case SymbolRefType.Global:
				this.Emit_Load(sym.i_Env);
				this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
				{
					OpCode = OpCode.IndexSet,
					Symbol = sym,
					NumVal = stackofs,
					NumVal2 = tupleidx,
					Value = DynValue.NewString(sym.i_Name)
				});
				return 2;
			default:
				throw new InternalErrorException("Unexpected symbol type : {0}", new object[]
				{
					sym
				});
			}
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x00025CBC File Offset: 0x00023EBC
		public Instruction Emit_TblInitN()
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitN
			});
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x00025CD6 File Offset: 0x00023ED6
		public Instruction Emit_TblInitI(bool lastpos)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.TblInitI,
				NumVal = (lastpos ? 1 : 0)
			});
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x001210EC File Offset: 0x0011F2EC
		public Instruction Emit_Index(DynValue index = null, bool isNameIndex = false, bool isExpList = false)
		{
			OpCode opCode;
			if (isNameIndex)
			{
				opCode = OpCode.IndexN;
			}
			else if (isExpList)
			{
				opCode = OpCode.IndexL;
			}
			else
			{
				opCode = OpCode.Index;
			}
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opCode,
				Value = index
			});
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x0012112C File Offset: 0x0011F32C
		public Instruction Emit_IndexSet(int stackofs, int tupleidx, DynValue index = null, bool isNameIndex = false, bool isExpList = false)
		{
			OpCode opCode;
			if (isNameIndex)
			{
				opCode = OpCode.IndexSetN;
			}
			else if (isExpList)
			{
				opCode = OpCode.IndexSetL;
			}
			else
			{
				opCode = OpCode.IndexSet;
			}
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = opCode,
				NumVal = stackofs,
				NumVal2 = tupleidx,
				Value = index
			});
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x00025CFE File Offset: 0x00023EFE
		public Instruction Emit_Copy(int numval)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Copy,
				NumVal = numval
			});
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x00025D1F File Offset: 0x00023F1F
		public Instruction Emit_Swap(int p1, int p2)
		{
			return this.AppendInstruction(new Instruction(this.m_CurrentSourceRef)
			{
				OpCode = OpCode.Swap,
				NumVal = p1,
				NumVal2 = p2
			});
		}

		// Token: 0x04002EF8 RID: 12024
		public List<Instruction> Code = new List<Instruction>();

		// Token: 0x04002EFA RID: 12026
		private List<SourceRef> m_SourceRefStack = new List<SourceRef>();

		// Token: 0x04002EFB RID: 12027
		private SourceRef m_CurrentSourceRef;

		// Token: 0x04002EFC RID: 12028
		internal LoopTracker LoopTracker = new LoopTracker();

		// Token: 0x020008AB RID: 2219
		private class SourceCodeStackGuard : IDisposable
		{
			// Token: 0x060037C2 RID: 14274 RVA: 0x00025D47 File Offset: 0x00023F47
			public SourceCodeStackGuard(SourceRef sref, ByteCode bc)
			{
				this.m_Bc = bc;
				this.m_Bc.PushSourceRef(sref);
			}

			// Token: 0x060037C3 RID: 14275 RVA: 0x00025D62 File Offset: 0x00023F62
			public void Dispose()
			{
				this.m_Bc.PopSourceRef();
			}

			// Token: 0x04002EFD RID: 12029
			private ByteCode m_Bc;
		}
	}
}
