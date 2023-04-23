using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000849 RID: 2121
	public class ObjectCallbackMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x060034A1 RID: 13473 RVA: 0x00023F5C File Offset: 0x0002215C
		public ObjectCallbackMemberDescriptor(string funcName) : this(funcName, (object o, ScriptExecutionContext c, CallbackArguments a) => DynValue.Void, new ParameterDescriptor[0])
		{
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x00023F8A File Offset: 0x0002218A
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack) : this(funcName, callBack, new ParameterDescriptor[0])
		{
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x00023F9A File Offset: 0x0002219A
		public ObjectCallbackMemberDescriptor(string funcName, Func<object, ScriptExecutionContext, CallbackArguments, object> callBack, ParameterDescriptor[] parameters)
		{
			this.m_CallbackFunc = callBack;
			base.Initialize(funcName, false, parameters, false);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x00118CF8 File Offset: 0x00116EF8
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			if (this.m_CallbackFunc != null)
			{
				object obj2 = this.m_CallbackFunc(obj, context, args);
				return ClrToScriptConversions.ObjectToDynValue(script, obj2, null);
			}
			return DynValue.Void;
		}

		// Token: 0x04002DE7 RID: 11751
		private Func<object, ScriptExecutionContext, CallbackArguments, object> m_CallbackFunc;
	}
}
