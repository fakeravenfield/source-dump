using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x0200087D RID: 2173
	public abstract class HardwiredMethodMemberDescriptor : FunctionMemberDescriptorBase
	{
		// Token: 0x0600361E RID: 13854 RVA: 0x0011CDF8 File Offset: 0x0011AFF8
		public override DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanExecute, obj);
			List<int> list = null;
			object[] pars = base.BuildArgumentList(script, obj, context, args, out list);
			object obj2 = this.Invoke(script, obj, pars, this.CalcArgsCount(pars));
			return DynValue.FromObject(script, obj2);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0011CE38 File Offset: 0x0011B038
		private int CalcArgsCount(object[] pars)
		{
			int num = pars.Length;
			for (int i = 0; i < pars.Length; i++)
			{
				if (base.Parameters[i].HasDefaultValue && pars[i] is DefaultValue)
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x06003620 RID: 13856
		protected abstract object Invoke(Script script, object obj, object[] pars, int argscount);
	}
}
