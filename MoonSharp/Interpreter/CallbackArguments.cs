using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007A4 RID: 1956
	public class CallbackArguments
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x0010D8D8 File Offset: 0x0010BAD8
		public CallbackArguments(IList<DynValue> args, bool isMethodCall)
		{
			this.m_Args = args;
			if (this.m_Args.Count > 0)
			{
				DynValue dynValue = this.m_Args[this.m_Args.Count - 1];
				if (dynValue.Type == DataType.Tuple)
				{
					this.m_Count = dynValue.Tuple.Length - 1 + this.m_Args.Count;
					this.m_LastIsTuple = true;
				}
				else if (dynValue.Type == DataType.Void)
				{
					this.m_Count = this.m_Args.Count - 1;
				}
				else
				{
					this.m_Count = this.m_Args.Count;
				}
			}
			else
			{
				this.m_Count = 0;
			}
			this.IsMethodCall = isMethodCall;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000211E8 File Offset: 0x0001F3E8
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000211F0 File Offset: 0x0001F3F0
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x000211F8 File Offset: 0x0001F3F8
		public bool IsMethodCall { get; private set; }

		// Token: 0x170003BA RID: 954
		public DynValue this[int index]
		{
			get
			{
				return this.RawGet(index, true) ?? DynValue.Void;
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0010D988 File Offset: 0x0010BB88
		public DynValue RawGet(int index, bool translateVoids)
		{
			if (index >= this.m_Count)
			{
				return null;
			}
			DynValue dynValue;
			if (!this.m_LastIsTuple || index < this.m_Args.Count - 1)
			{
				dynValue = this.m_Args[index];
			}
			else
			{
				dynValue = this.m_Args[this.m_Args.Count - 1].Tuple[index - (this.m_Args.Count - 1)];
			}
			if (dynValue.Type == DataType.Tuple)
			{
				if (dynValue.Tuple.Length != 0)
				{
					dynValue = dynValue.Tuple[0];
				}
				else
				{
					dynValue = DynValue.Nil;
				}
			}
			if (translateVoids && dynValue.Type == DataType.Void)
			{
				dynValue = DynValue.Nil;
			}
			return dynValue;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0010DA2C File Offset: 0x0010BC2C
		public DynValue[] GetArray(int skip = 0)
		{
			if (skip >= this.m_Count)
			{
				return new DynValue[0];
			}
			DynValue[] array = new DynValue[this.m_Count - skip];
			for (int i = skip; i < this.m_Count; i++)
			{
				array[i - skip] = this[i];
			}
			return array;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00021214 File Offset: 0x0001F414
		public DynValue AsType(int argNum, string funcName, DataType type, bool allowNil = false)
		{
			return this[argNum].CheckType(funcName, type, argNum, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0002122D File Offset: 0x0001F42D
		public T AsUserData<T>(int argNum, string funcName, bool allowNil = false)
		{
			return this[argNum].CheckUserDataType<T>(funcName, argNum, allowNil ? TypeValidationFlags.AllowNil : TypeValidationFlags.None);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x00021244 File Offset: 0x0001F444
		public int AsInt(int argNum, string funcName)
		{
			return (int)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x00021256 File Offset: 0x0001F456
		public long AsLong(int argNum, string funcName)
		{
			return (long)this.AsType(argNum, funcName, DataType.Number, false).Number;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0010DA78 File Offset: 0x0010BC78
		public string AsStringUsingMeta(ScriptExecutionContext executionContext, int argNum, string funcName)
		{
			if (this[argNum].Type != DataType.Table || this[argNum].Table.MetaTable == null || this[argNum].Table.MetaTable.RawGet("__tostring") == null)
			{
				return this[argNum].ToPrintString();
			}
			DynValue dynValue = executionContext.GetScript().Call(this[argNum].Table.MetaTable.RawGet("__tostring"), new DynValue[]
			{
				this[argNum]
			});
			if (dynValue.Type != DataType.String)
			{
				throw new ScriptRuntimeException("'tostring' must return a string to '{0}'", new object[]
				{
					funcName
				});
			}
			return dynValue.ToPrintString();
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x00021268 File Offset: 0x0001F468
		public CallbackArguments SkipMethodCall()
		{
			if (this.IsMethodCall)
			{
				return new CallbackArguments(new Slice<DynValue>(this.m_Args, 1, this.m_Args.Count - 1, false), false);
			}
			return this;
		}

		// Token: 0x04002BAC RID: 11180
		private IList<DynValue> m_Args;

		// Token: 0x04002BAD RID: 11181
		private int m_Count;

		// Token: 0x04002BAE RID: 11182
		private bool m_LastIsTuple;
	}
}
