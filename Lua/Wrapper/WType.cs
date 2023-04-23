using System;

namespace Lua.Wrapper
{
	// Token: 0x0200099C RID: 2460
	[Name("Type")]
	public static class WType
	{
		// Token: 0x06003E7E RID: 15998 RVA: 0x0012F3D8 File Offset: 0x0012D5D8
		[Doc("Cast the object to the target type. Returns the cast object, or nil on failure.")]
		public static object Cast(object obj, object type)
		{
			try
			{
				Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
				if (typeFromUserData != null)
				{
					return Convert.ChangeType(obj, typeFromUserData);
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}
	}
}
