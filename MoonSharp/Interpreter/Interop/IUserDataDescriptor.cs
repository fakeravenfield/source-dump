using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000834 RID: 2100
	public interface IUserDataDescriptor
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06003425 RID: 13349
		string Name { get; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06003426 RID: 13350
		Type Type { get; }

		// Token: 0x06003427 RID: 13351
		DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing);

		// Token: 0x06003428 RID: 13352
		bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x06003429 RID: 13353
		string AsString(object obj);

		// Token: 0x0600342A RID: 13354
		DynValue MetaIndex(Script script, object obj, string metaname);

		// Token: 0x0600342B RID: 13355
		bool IsTypeCompatible(Type type, object obj);
	}
}
