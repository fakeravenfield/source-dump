using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000895 RID: 2197
	public static class MemberDescriptor
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x00022A05 File Offset: 0x00020C05
		public static bool HasAllFlags(this MemberDescriptorAccess access, MemberDescriptorAccess flag)
		{
			return (access & flag) == flag;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000252CF File Offset: 0x000234CF
		public static bool CanRead(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanRead);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000252DD File Offset: 0x000234DD
		public static bool CanWrite(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanWrite);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000252EB File Offset: 0x000234EB
		public static bool CanExecute(this IMemberDescriptor desc)
		{
			return desc.MemberAccess.HasAllFlags(MemberDescriptorAccess.CanExecute);
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000252F9 File Offset: 0x000234F9
		public static DynValue GetGetterCallbackAsDynValue(this IMemberDescriptor desc, Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext p1, CallbackArguments p2) => desc.GetValue(script, obj), null);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x00025326 File Offset: 0x00023526
		public static IMemberDescriptor WithAccessOrNull(this IMemberDescriptor desc, MemberDescriptorAccess access)
		{
			if (desc == null)
			{
				return null;
			}
			if (desc.MemberAccess.HasAllFlags(access))
			{
				return desc;
			}
			return null;
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x00120654 File Offset: 0x0011E854
		public static void CheckAccess(this IMemberDescriptor desc, MemberDescriptorAccess access, object obj)
		{
			if (!desc.IsStatic && obj == null)
			{
				throw ScriptRuntimeException.AccessInstanceMemberOnStatics(desc);
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanExecute) && !desc.CanExecute())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be called.", new object[]
				{
					desc.Name
				});
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanWrite) && !desc.CanWrite())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be assigned to.", new object[]
				{
					desc.Name
				});
			}
			if (access.HasAllFlags(MemberDescriptorAccess.CanRead) && !desc.CanRead())
			{
				throw new ScriptRuntimeException("userdata member {0} cannot be read from.", new object[]
				{
					desc.Name
				});
			}
		}
	}
}
