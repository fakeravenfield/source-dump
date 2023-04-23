using System;

namespace MoonSharp.Interpreter.Interop.RegistrationPolicies
{
	// Token: 0x02000881 RID: 2177
	public interface IRegistrationPolicy
	{
		// Token: 0x06003628 RID: 13864
		IUserDataDescriptor HandleRegistration(IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor);

		// Token: 0x06003629 RID: 13865
		bool AllowTypeAutoRegistration(Type type);
	}
}
