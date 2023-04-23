using System;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x02000813 RID: 2067
	public sealed class JsonNull
	{
		// Token: 0x0600335C RID: 13148 RVA: 0x0000476F File Offset: 0x0000296F
		public static bool isNull()
		{
			return true;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x00023484 File Offset: 0x00021684
		[MoonSharpHidden]
		public static bool IsJsonNull(DynValue v)
		{
			return v.Type == DataType.UserData && v.UserData.Descriptor != null && v.UserData.Descriptor.Type == typeof(JsonNull);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000234BD File Offset: 0x000216BD
		[MoonSharpHidden]
		public static DynValue Create()
		{
			return UserData.CreateStatic<JsonNull>();
		}
	}
}
