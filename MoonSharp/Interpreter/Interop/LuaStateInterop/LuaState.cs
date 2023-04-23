using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000886 RID: 2182
	public class LuaState
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x00024F2F File Offset: 0x0002312F
		// (set) Token: 0x060036A4 RID: 13988 RVA: 0x00024F37 File Offset: 0x00023137
		public ScriptExecutionContext ExecutionContext { get; private set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x00024F40 File Offset: 0x00023140
		// (set) Token: 0x060036A6 RID: 13990 RVA: 0x00024F48 File Offset: 0x00023148
		public string FunctionName { get; private set; }

		// Token: 0x060036A7 RID: 13991 RVA: 0x0011D440 File Offset: 0x0011B640
		internal LuaState(ScriptExecutionContext executionContext, CallbackArguments args, string functionName)
		{
			this.ExecutionContext = executionContext;
			this.m_Stack = new List<DynValue>(16);
			for (int i = 0; i < args.Count; i++)
			{
				this.m_Stack.Add(args[i]);
			}
			this.FunctionName = functionName;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00024F51 File Offset: 0x00023151
		public DynValue Top(int pos = 0)
		{
			return this.m_Stack[this.m_Stack.Count - 1 - pos];
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00024F6D File Offset: 0x0002316D
		public DynValue At(int pos)
		{
			if (pos < 0)
			{
				pos = this.m_Stack.Count + pos + 1;
			}
			if (pos > this.m_Stack.Count)
			{
				return DynValue.Void;
			}
			return this.m_Stack[pos - 1];
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x00024FA6 File Offset: 0x000231A6
		public int Count
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00024FB3 File Offset: 0x000231B3
		public void Push(DynValue v)
		{
			this.m_Stack.Add(v);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00024FC1 File Offset: 0x000231C1
		public DynValue Pop()
		{
			DynValue result = this.Top(0);
			this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			return result;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x0011D494 File Offset: 0x0011B694
		public DynValue[] GetTopArray(int num)
		{
			DynValue[] array = new DynValue[num];
			for (int i = 0; i < num; i++)
			{
				array[num - i - 1] = this.Top(i);
			}
			return array;
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x00024FE2 File Offset: 0x000231E2
		public DynValue GetReturnValue(int retvals)
		{
			if (retvals == 0)
			{
				return DynValue.Nil;
			}
			if (retvals == 1)
			{
				return this.Top(0);
			}
			return DynValue.NewTupleNested(this.GetTopArray(retvals));
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x0011D4C4 File Offset: 0x0011B6C4
		public void Discard(int nargs)
		{
			for (int i = 0; i < nargs; i++)
			{
				this.m_Stack.RemoveAt(this.m_Stack.Count - 1);
			}
		}

		// Token: 0x04002E90 RID: 11920
		private List<DynValue> m_Stack;
	}
}
