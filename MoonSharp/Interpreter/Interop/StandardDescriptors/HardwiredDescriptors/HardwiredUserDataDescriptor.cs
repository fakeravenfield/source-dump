using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x0200087E RID: 2174
	public abstract class HardwiredUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x06003622 RID: 13858 RVA: 0x000249D9 File Offset: 0x00022BD9
		protected HardwiredUserDataDescriptor(Type T) : base(T, "::hardwired::" + T.Name)
		{
		}
	}
}
