using System;
using System.Collections;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200083A RID: 2106
	internal class EnumerableWrapper : IUserDataType
	{
		// Token: 0x0600343E RID: 13374 RVA: 0x00023B2B File Offset: 0x00021D2B
		private EnumerableWrapper(Script script, IEnumerator enumerator, Type elementType = null)
		{
			this.m_Script = script;
			this.m_Enumerator = enumerator;
			this.m_ElementType = elementType;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00023B53 File Offset: 0x00021D53
		public void Reset()
		{
			if (this.m_HasTurnOnce)
			{
				this.m_Enumerator.Reset();
			}
			this.m_HasTurnOnce = true;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00117A18 File Offset: 0x00115C18
		private DynValue GetNext(DynValue prev)
		{
			if (prev.IsNil())
			{
				this.Reset();
			}
			while (this.m_Enumerator.MoveNext())
			{
				DynValue dynValue = ClrToScriptConversions.ObjectToDynValue(this.m_Script, this.m_Enumerator.Current, this.m_ElementType);
				if (!dynValue.IsNil())
				{
					return dynValue;
				}
			}
			return DynValue.Nil;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00023B6F File Offset: 0x00021D6F
		private DynValue LuaIteratorCallback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			this.m_Prev = this.GetNext(this.m_Prev);
			return this.m_Prev;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00117A70 File Offset: 0x00115C70
		internal static DynValue ConvertIterator(Script script, IEnumerator enumerator, Type elementType = null)
		{
			EnumerableWrapper o = new EnumerableWrapper(script, enumerator, elementType);
			return DynValue.NewTuple(new DynValue[]
			{
				UserData.Create(o),
				DynValue.Nil,
				DynValue.Nil
			});
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00023B89 File Offset: 0x00021D89
		internal static DynValue ConvertTable(Table table)
		{
			return EnumerableWrapper.ConvertIterator(table.OwnerScript, table.Values.GetEnumerator(), null);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x00117AAC File Offset: 0x00115CAC
		public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
		{
			if (index.Type == DataType.String)
			{
				string @string = index.String;
				if (@string == "Current" || @string == "current")
				{
					return DynValue.FromObject(script, this.m_Enumerator.Current);
				}
				if (@string == "MoveNext" || @string == "moveNext" || @string == "move_next")
				{
					return DynValue.NewCallback((ScriptExecutionContext ctx, CallbackArguments args) => DynValue.NewBoolean(this.m_Enumerator.MoveNext()), null);
				}
				if (@string == "Reset" || @string == "reset")
				{
					return DynValue.NewCallback(delegate(ScriptExecutionContext ctx, CallbackArguments args)
					{
						this.Reset();
						return DynValue.Nil;
					}, null);
				}
			}
			return null;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x0000257D File Offset: 0x0000077D
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x00023BA2 File Offset: 0x00021DA2
		public DynValue MetaIndex(Script script, string metaname)
		{
			if (metaname == "__call")
			{
				return DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(this.LuaIteratorCallback), null);
			}
			return null;
		}

		// Token: 0x04002DAA RID: 11690
		private IEnumerator m_Enumerator;

		// Token: 0x04002DAB RID: 11691
		private Script m_Script;

		// Token: 0x04002DAC RID: 11692
		private DynValue m_Prev = DynValue.Nil;

		// Token: 0x04002DAD RID: 11693
		private bool m_HasTurnOnce;

		// Token: 0x04002DAE RID: 11694
		private Type m_ElementType;
	}
}
