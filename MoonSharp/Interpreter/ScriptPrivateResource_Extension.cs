using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007B4 RID: 1972
	internal static class ScriptPrivateResource_Extension
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x0010EBFC File Offset: 0x0010CDFC
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, DynValue[] values)
		{
			foreach (DynValue value in values)
			{
				containingResource.CheckScriptOwnership(value);
			}
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0010EC24 File Offset: 0x0010CE24
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, DynValue value)
		{
			if (value != null)
			{
				IScriptPrivateResource asPrivateResource = value.GetAsPrivateResource();
				if (asPrivateResource != null)
				{
					containingResource.CheckScriptOwnership(asPrivateResource);
				}
			}
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x00021B2B File Offset: 0x0001FD2B
		public static void CheckScriptOwnership(this IScriptPrivateResource resource, Script script)
		{
			if (resource.OwnerScript != null && resource.OwnerScript != script && script != null)
			{
				throw new ScriptRuntimeException("Attempt to access a resource owned by a script, from another script");
			}
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x0010EC48 File Offset: 0x0010CE48
		public static void CheckScriptOwnership(this IScriptPrivateResource containingResource, IScriptPrivateResource itemResource)
		{
			if (itemResource != null)
			{
				if (containingResource.OwnerScript != null && containingResource.OwnerScript != itemResource.OwnerScript && itemResource.OwnerScript != null)
				{
					throw new ScriptRuntimeException("Attempt to perform operations with resources owned by different scripts.");
				}
				if (containingResource.OwnerScript == null && itemResource.OwnerScript != null)
				{
					throw new ScriptRuntimeException("Attempt to perform operations with a script private resource on a shared resource.");
				}
			}
		}
	}
}
