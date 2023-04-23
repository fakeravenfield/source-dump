using System;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000818 RID: 2072
	public class ReplInterpreterScriptLoader : FileSystemScriptLoader
	{
		// Token: 0x06003378 RID: 13176 RVA: 0x00116B00 File Offset: 0x00114D00
		public ReplInterpreterScriptLoader()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MOONSHARP_PATH");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
			}
			if (base.ModulePaths == null)
			{
				environmentVariable = Environment.GetEnvironmentVariable("LUA_PATH_5_2");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
			}
			if (base.ModulePaths == null)
			{
				environmentVariable = Environment.GetEnvironmentVariable("LUA_PATH");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
			}
			if (base.ModulePaths == null)
			{
				base.ModulePaths = ScriptLoaderBase.UnpackStringPaths("?;?.lua");
			}
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x00116B98 File Offset: 0x00114D98
		public override string ResolveModuleName(string modname, Table globalContext)
		{
			DynValue dynValue = globalContext.RawGet("LUA_PATH");
			if (dynValue != null && dynValue.Type == DataType.String)
			{
				return this.ResolveModuleName(modname, ScriptLoaderBase.UnpackStringPaths(dynValue.String));
			}
			return base.ResolveModuleName(modname, globalContext);
		}
	}
}
