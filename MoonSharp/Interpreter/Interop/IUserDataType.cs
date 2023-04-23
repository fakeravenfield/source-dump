using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000836 RID: 2102
	public interface IUserDataType
	{
		// Token: 0x06003433 RID: 13363
		DynValue Index(Script script, DynValue index, bool isDirectIndexing);

		// Token: 0x06003434 RID: 13364
		bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x06003435 RID: 13365
		DynValue MetaIndex(Script script, string metaname);
	}
}
