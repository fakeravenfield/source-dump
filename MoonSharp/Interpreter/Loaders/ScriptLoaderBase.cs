using System;
using System.Linq;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000825 RID: 2085
	public abstract class ScriptLoaderBase : IScriptLoader
	{
		// Token: 0x060033DA RID: 13274
		public abstract bool ScriptFileExists(string name);

		// Token: 0x060033DB RID: 13275
		public abstract object LoadFile(string file, Table globalContext);

		// Token: 0x060033DC RID: 13276 RVA: 0x00116ECC File Offset: 0x001150CC
		protected virtual string ResolveModuleName(string modname, string[] paths)
		{
			if (paths == null)
			{
				return null;
			}
			modname = modname.Replace('.', '/');
			for (int i = 0; i < paths.Length; i++)
			{
				string text = paths[i].Replace("?", modname);
				if (this.ScriptFileExists(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x00116F18 File Offset: 0x00115118
		public virtual string ResolveModuleName(string modname, Table globalContext)
		{
			if (!this.IgnoreLuaPathGlobal)
			{
				DynValue dynValue = globalContext.RawGet("LUA_PATH");
				if (dynValue != null && dynValue.Type == DataType.String)
				{
					return this.ResolveModuleName(modname, ScriptLoaderBase.UnpackStringPaths(dynValue.String));
				}
			}
			return this.ResolveModuleName(modname, this.ModulePaths);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00023893 File Offset: 0x00021A93
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x0002389B File Offset: 0x00021A9B
		public string[] ModulePaths { get; set; }

		// Token: 0x060033E0 RID: 13280 RVA: 0x00116F68 File Offset: 0x00115168
		public static string[] UnpackStringPaths(string str)
		{
			return (from s in str.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries)
			select s.Trim() into s
			where !string.IsNullOrEmpty(s)
			select s).ToArray<string>();
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x00116FD4 File Offset: 0x001151D4
		public static string[] GetDefaultEnvironmentPaths()
		{
			string[] array = null;
			if (array == null)
			{
				string environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable("MOONSHARP_PATH");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					array = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
				if (array == null)
				{
					environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable("LUA_PATH");
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						array = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
					}
				}
				if (array == null)
				{
					array = ScriptLoaderBase.UnpackStringPaths("?;?.lua");
				}
			}
			return array;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000091D3 File Offset: 0x000073D3
		public virtual string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000238A4 File Offset: 0x00021AA4
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000238AC File Offset: 0x00021AAC
		public bool IgnoreLuaPathGlobal { get; set; }
	}
}
