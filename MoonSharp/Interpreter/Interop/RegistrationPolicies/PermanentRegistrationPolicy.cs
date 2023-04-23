using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000882 RID: 2178
	public class PermanentRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x0600362A RID: 13866 RVA: 0x00024A07 File Offset: 0x00022C07
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x0000257D File Offset: 0x0000077D
		public bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
