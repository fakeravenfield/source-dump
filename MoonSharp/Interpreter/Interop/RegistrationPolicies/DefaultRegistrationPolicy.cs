using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000880 RID: 2176
	public class DefaultRegistrationPolicy : IRegistrationPolicy
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x000249FA File Offset: 0x00022BFA
		public IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			if (newDescriptor == null)
			{
				return null;
			}
			return oldDescriptor ?? newDescriptor;
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool AllowTypeAutoRegistration(Type type)
		{
			return false;
		}
	}
}
