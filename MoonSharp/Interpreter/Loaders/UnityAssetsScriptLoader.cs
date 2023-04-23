using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000827 RID: 2087
	public class UnityAssetsScriptLoader : ScriptLoaderBase
	{
		// Token: 0x060033EA RID: 13290 RVA: 0x000238D4 File Offset: 0x00021AD4
		public UnityAssetsScriptLoader(string assetsPath = null)
		{
			assetsPath = (assetsPath ?? "MoonSharp/Scripts");
			this.LoadResourcesWithReflection(assetsPath);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000238FA File Offset: 0x00021AFA
		public UnityAssetsScriptLoader(Dictionary<string, string> scriptToCodeMap)
		{
			this.m_Resources = scriptToCodeMap;
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00117040 File Offset: 0x00115240
		private void LoadResourcesWithReflection(string assetsPath)
		{
			try
			{
				Type type = Type.GetType("UnityEngine.Resources, UnityEngine");
				Type type2 = Type.GetType("UnityEngine.TextAsset, UnityEngine");
				MethodInfo getMethod = Framework.Do.GetGetMethod(Framework.Do.GetProperty(type2, "name"));
				MethodInfo getMethod2 = Framework.Do.GetGetMethod(Framework.Do.GetProperty(type2, "text"));
				Array array = (Array)Framework.Do.GetMethod(type, "LoadAll", new Type[]
				{
					typeof(string),
					typeof(Type)
				}).Invoke(null, new object[]
				{
					assetsPath,
					type2
				});
				for (int i = 0; i < array.Length; i++)
				{
					object value = array.GetValue(i);
					string key = getMethod.Invoke(value, null) as string;
					string value2 = getMethod2.Invoke(value, null) as string;
					this.m_Resources.Add(key, value2);
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error initializing UnityScriptLoader : {0}", arg);
			}
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x00117158 File Offset: 0x00115358
		private string GetFileName(string filename)
		{
			int num = Math.Max(filename.LastIndexOf('\\'), filename.LastIndexOf('/'));
			if (num > 0)
			{
				filename = filename.Substring(num + 1);
			}
			return filename;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x0011718C File Offset: 0x0011538C
		public override object LoadFile(string filePath, Table globalContext)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
			if (this.m_Resources.ContainsKey(fileNameWithoutExtension))
			{
				return this.m_Resources[fileNameWithoutExtension];
			}
			throw new UnityAssetsScriptLoader.ScriptAssetNotFoundException(string.Format("Cannot load script '{0}'. By default, scripts should be .txt files placed under a Assets/Resources/{1} directory.\r\nIf you want scripts to be put in another directory or another way, use a custom instance of UnityAssetsScriptLoader or implement\r\nyour own IScriptLoader (possibly extending ScriptLoaderBase).", fileNameWithoutExtension, "MoonSharp/Scripts"), filePath, fileNameWithoutExtension);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x00023914 File Offset: 0x00021B14
		public override bool ScriptFileExists(string file)
		{
			file = Path.GetFileNameWithoutExtension(file);
			return this.m_Resources.ContainsKey(file);
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x0002392A File Offset: 0x00021B2A
		public string[] GetLoadedScripts()
		{
			return this.m_Resources.Keys.ToArray<string>();
		}

		// Token: 0x04002D92 RID: 11666
		private Dictionary<string, string> m_Resources = new Dictionary<string, string>();

		// Token: 0x04002D93 RID: 11667
		public const string DEFAULT_PATH = "MoonSharp/Scripts";

		// Token: 0x02000828 RID: 2088
		public class ScriptAssetNotFoundException : Exception
		{
			// Token: 0x060033F1 RID: 13297 RVA: 0x0002393C File Offset: 0x00021B3C
			public ScriptAssetNotFoundException(string message, string filePath, string assetName) : base(message)
			{
				this.FilePath = filePath;
				this.AssetName = assetName;
			}

			// Token: 0x04002D94 RID: 11668
			public readonly string FilePath;

			// Token: 0x04002D95 RID: 11669
			public readonly string AssetName;
		}
	}
}
