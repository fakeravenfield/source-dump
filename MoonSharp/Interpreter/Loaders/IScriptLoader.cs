using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000823 RID: 2083
	public interface IScriptLoader
	{
		// Token: 0x060033D3 RID: 13267
		object LoadFile(string file, Table globalContext);

		// Token: 0x060033D4 RID: 13268
		[Obsolete("This serves almost no purpose. Kept here just to preserve backward compatibility.")]
		string ResolveFileName(string filename, Table globalContext);

		// Token: 0x060033D5 RID: 13269
		string ResolveModuleName(string modname, Table globalContext);
	}
}
