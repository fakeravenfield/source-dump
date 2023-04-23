using System;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000837 RID: 2103
	public static class InteropRegistrationPolicy
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x00023AF5 File Offset: 0x00021CF5
		public static IRegistrationPolicy Default
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00023AF5 File Offset: 0x00021CF5
		[Obsolete("Please use InteropRegistrationPolicy.Default instead.")]
		public static IRegistrationPolicy Explicit
		{
			get
			{
				return new DefaultRegistrationPolicy();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x00023AFC File Offset: 0x00021CFC
		public static IRegistrationPolicy Automatic
		{
			get
			{
				return new AutomaticRegistrationPolicy();
			}
		}
	}
}
