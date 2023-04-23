using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x0200087F RID: 2175
	public class AutomaticRegistrationPolicy : DefaultRegistrationPolicy
	{
		// Token: 0x06003623 RID: 13859 RVA: 0x0000476F File Offset: 0x0000296F
		public override bool AllowTypeAutoRegistration(Type type)
		{
			return true;
		}
	}
}
