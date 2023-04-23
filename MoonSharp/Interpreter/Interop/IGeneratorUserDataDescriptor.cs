using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000833 RID: 2099
	public interface IGeneratorUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06003424 RID: 13348
		IUserDataDescriptor Generate(Type type);
	}
}
