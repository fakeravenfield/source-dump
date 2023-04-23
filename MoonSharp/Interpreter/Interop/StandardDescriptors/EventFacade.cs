using System;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors
{
	// Token: 0x0200087A RID: 2170
	internal class EventFacade : IUserDataType
	{
		// Token: 0x06003608 RID: 13832 RVA: 0x000248A8 File Offset: 0x00022AA8
		public EventFacade(EventMemberDescriptor parent, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.AddCallback);
			this.m_RemoveCallback = new Func<object, ScriptExecutionContext, CallbackArguments, DynValue>(parent.RemoveCallback);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000248DB File Offset: 0x00022ADB
		public EventFacade(Func<object, ScriptExecutionContext, CallbackArguments, DynValue> addCallback, Func<object, ScriptExecutionContext, CallbackArguments, DynValue> removeCallback, object obj)
		{
			this.m_Object = obj;
			this.m_AddCallback = addCallback;
			this.m_RemoveCallback = removeCallback;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0011CD34 File Offset: 0x0011AF34
		public DynValue Index(Script script, DynValue index, bool isDirectIndexing)
		{
			if (index.Type == DataType.String)
			{
				if (index.String == "add")
				{
					return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => this.m_AddCallback(this.m_Object, c, a), null);
				}
				if (index.String == "remove")
				{
					return DynValue.NewCallback((ScriptExecutionContext c, CallbackArguments a) => this.m_RemoveCallback(this.m_Object, c, a), null);
				}
			}
			throw new ScriptRuntimeException("Events only support add and remove methods");
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000248F8 File Offset: 0x00022AF8
		public bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing)
		{
			throw new ScriptRuntimeException("Events do not have settable fields");
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x00002FD8 File Offset: 0x000011D8
		public DynValue MetaIndex(Script script, string metaname)
		{
			return null;
		}

		// Token: 0x04002E78 RID: 11896
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_AddCallback;

		// Token: 0x04002E79 RID: 11897
		private Func<object, ScriptExecutionContext, CallbackArguments, DynValue> m_RemoveCallback;

		// Token: 0x04002E7A RID: 11898
		private object m_Object;
	}
}
