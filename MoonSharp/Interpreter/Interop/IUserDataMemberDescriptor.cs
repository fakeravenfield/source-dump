using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000835 RID: 2101
	public interface IUserDataMemberDescriptor
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600342C RID: 13356
		string Name { get; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600342D RID: 13357
		Type Type { get; }

		// Token: 0x0600342E RID: 13358
		DynValue GetValue(Script script, object obj);

		// Token: 0x0600342F RID: 13359
		bool SetValue(Script script, object obj, DynValue value);

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06003430 RID: 13360
		UserDataMemberType MemberType { get; }

		// Token: 0x06003431 RID: 13361
		void Optimize();

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06003432 RID: 13362
		bool IsStatic { get; }
	}
}
