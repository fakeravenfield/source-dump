using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000894 RID: 2196
	public interface IMemberDescriptor
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600370D RID: 14093
		bool IsStatic { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600370E RID: 14094
		string Name { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600370F RID: 14095
		MemberDescriptorAccess MemberAccess { get; }

		// Token: 0x06003710 RID: 14096
		DynValue GetValue(Script script, object obj);

		// Token: 0x06003711 RID: 14097
		void SetValue(Script script, object obj, DynValue value);
	}
}
