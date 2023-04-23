using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lua.Proxy;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using MoonSharp.Interpreter.REPL;
using MoonSharp.VsCodeDebugger;
using UnityEngine;
using UnityEngine.Events;

namespace Lua
{
	// Token: 0x0200092C RID: 2348
	public class ScriptEngine : MonoBehaviour
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000285E0 File Offset: 0x000267E0
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x000285E8 File Offset: 0x000267E8
		public ScriptConsole console { get; private set; }

		// Token: 0x06003BC4 RID: 15300 RVA: 0x0012D3E4 File Offset: 0x0012B5E4
		private void Awake()
		{
			this.onScriptReloaded = new UnityEvent();
			this.console = ScriptConsole.instance;
			if (!this.console)
			{
				this.console = base.gameObject.AddComponent<ScriptConsole>();
			}
			base.StartCoroutine(this.CleanObjectCache());
			Script.GlobalOptions.Platform = new LimitedPlatformAccessor();
			Script.GlobalOptions.DecorateAllExceptions = true;
			Script.GlobalOptions.RethrowExceptionNested = true;
			Script.DefaultOptions.ScriptLoader = new UnityAssetsScriptLoader("Lua");
			Script.DefaultOptions.DebugPrint = delegate(string s)
			{
				this.console.LogInfo(s);
			};
			this.proxyTypes = Registrar.GetProxyTypes();
			Registrar.RegisterTypes();
			this.script = new Script(CoreModules.Ravenfield_Sandbox);
			Registrar.ExposeTypes(this.script);
			this.IncludeResource("class", true);
			this.IncludeResource("table", true);
			this.console.OnScriptEngineReady(new ReplInterpreter(this.script));
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000285F1 File Offset: 0x000267F1
		private void OnDestroy()
		{
			this.StopDebugServer();
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000285F9 File Offset: 0x000267F9
		private IEnumerator CleanObjectCache()
		{
			for (;;)
			{
				ObjectCache.Clean();
				yield return new WaitForSeconds(10f);
			}
			yield break;
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00028601 File Offset: 0x00026801
		public bool IsDebugServerRunning()
		{
			return ScriptEngine.server != null;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x0012D4DC File Offset: 0x0012B6DC
		public void StartDebugServer()
		{
			try
			{
				if (ScriptEngine.server == null)
				{
					ScriptEngine.server = new MoonSharpVsCodeDebugServer(41912);
					ScriptEngine.server.Start();
				}
				ScriptEngine.server.AttachToScript(this.script, "ScriptManager", (SourceCode sc) => this.GetScriptPath(sc));
				this.console.LogInfo("Ravenscript Debug Server listening to port " + ScriptEngine.server.Port.ToString());
			}
			catch (Exception ex)
			{
				this.console.LogException(ex, "Unable to start Ravenscript Debug Server", Array.Empty<object>());
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x0012D580 File Offset: 0x0012B780
		public void StopDebugServer()
		{
			if (ScriptEngine.server != null)
			{
				try
				{
					ScriptEngine.server.Detach(this.script);
				}
				catch (Exception ex)
				{
					this.console.LogException(ex, "Unable to detach script from Ravenscript Debug Server", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x0002860B File Offset: 0x0002680B
		public ReplInterpreter CreateRepl()
		{
			return new ReplInterpreter(this.script);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x0012D5D0 File Offset: 0x0012B7D0
		private string GetScriptPath(SourceCode sc)
		{
			if (this.sourcePaths.ContainsKey(sc.SourceID))
			{
				return this.sourcePaths[sc.SourceID];
			}
			Debug.LogFormat("Unknown source: ID={0}, Name={1}", new object[]
			{
				sc.SourceID,
				sc.Name
			});
			return sc.Name;
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x0012D630 File Offset: 0x0012B830
		private bool IncludeResource(string resourceName, bool useCache = true)
		{
			string text = Path.Combine("Lua", resourceName);
			string fullPath = Path.GetFullPath(Path.Combine("Assets\\Resources\\", text + ".txt"));
			TextAsset asset = Resources.Load<TextAsset>(text);
			if (!asset)
			{
				throw new FileNotFoundException(string.Format("Cannot find script '{0}'. It should be a TextAsset located in the Assets/Resources/{1} folder.", resourceName, text), resourceName);
			}
			return this.IncludeScript(fullPath, asset.name, () => asset.text, useCache, null, null);
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x0012D6B8 File Offset: 0x0012B8B8
		private Table GetEnvironmentTable(int? id)
		{
			int key = (id != null) ? id.Value : -1;
			if (!this.environments.ContainsKey(key))
			{
				Table envTable = DynValue.NewTable(this.script).Table;
				Table table = DynValue.NewTable(this.script).Table;
				table["__metatable"] = DynValue.NewBoolean(true);
				table["__index"] = this.script.Globals;
				envTable["_G"] = envTable;
				DynValue classFunc = this.script.Globals.Get("class");
				envTable["behaviour"] = new Func<DynValue, DynValue>(delegate(DynValue name)
				{
					DynValue dynValue = envTable.Get(name);
					if (dynValue.IsNil())
					{
						dynValue = this.Call(classFunc, new DynValue[]
						{
							name
						});
						envTable.Set(name, dynValue);
					}
					return dynValue;
				});
				envTable.MetaTable = table;
				this.environments.Add(key, envTable);
			}
			return this.environments[key];
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x0012D7BC File Offset: 0x0012B9BC
		public ScriptEngine.IncludeTextAssetResult IncludeTextAsset(TextAsset asset, bool useCache = true)
		{
			int? textAssetBundleId = ModManager.instance.GetTextAssetBundleId(asset);
			Table environmentTable;
			string cacheId;
			if (textAssetBundleId != null)
			{
				environmentTable = this.GetEnvironmentTable(textAssetBundleId);
				cacheId = asset.name + "#" + textAssetBundleId.Value.ToString();
			}
			else
			{
				environmentTable = this.GetEnvironmentTable(textAssetBundleId);
				cacheId = asset.name;
			}
			string filePath;
			Func<string> getSource;
			if (this.assetPaths.ContainsKey(asset.name))
			{
				filePath = this.assetPaths[asset.name];
				if (!this.reloadable.ContainsKey(asset.name))
				{
					this.reloadable.Add(asset.name, new ScriptEngine.ReloadInfo
					{
						filePath = filePath,
						cacheId = cacheId,
						environmentId = textAssetBundleId
					});
				}
				getSource = (() => File.ReadAllText(filePath));
			}
			else
			{
				filePath = asset.name;
				getSource = (() => asset.text);
			}
			bool success = this.IncludeScript(filePath, asset.name, getSource, useCache, environmentTable, cacheId);
			return new ScriptEngine.IncludeTextAssetResult
			{
				environment = environmentTable,
				success = success
			};
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x0012D92C File Offset: 0x0012BB2C
		private bool IncludeScript(string filePath, string scriptName, Func<string> getSource, bool useCache = true, Table envTable = null, string cacheId = null)
		{
			cacheId = (cacheId ?? scriptName);
			bool flag = this.loadedSources.Contains(cacheId);
			if (!useCache || !flag)
			{
				this.console.LogInfo("Loading script: {0} {1}", new object[]
				{
					filePath,
					flag ? "RELOAD" : ""
				});
				int sourceCodeCount = this.script.SourceCodeCount;
				this.sourcePaths[sourceCodeCount] = filePath;
				try
				{
					string code = getSource();
					this.script.DoString(code, envTable, scriptName);
					this.loadedSources.Add(cacheId);
					flag = true;
				}
				catch (Exception ex)
				{
					this.sourcePaths.Remove(sourceCodeCount);
					this.console.LogException(ex);
				}
			}
			return flag;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x00028618 File Offset: 0x00026818
		public void RegisterTextAssetPath(string assetName, string filePath)
		{
			this.assetPaths[assetName] = filePath;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x0012D9F8 File Offset: 0x0012BBF8
		public void ReloadScripts()
		{
			this.console.LogInfo("Reloading scripts");
			foreach (KeyValuePair<string, ScriptEngine.ReloadInfo> keyValuePair in this.reloadable)
			{
				string key = keyValuePair.Key;
				ScriptEngine.ReloadInfo reload = keyValuePair.Value;
				if (this.loadedSources.Contains(reload.cacheId))
				{
					Table environmentTable = this.GetEnvironmentTable(reload.environmentId);
					Func<string> getSource = () => File.ReadAllText(reload.filePath);
					this.IncludeScript(reload.filePath, key, getSource, false, environmentTable, reload.cacheId);
				}
			}
			this.onScriptReloaded.Invoke();
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x00028627 File Offset: 0x00026827
		public DynValue[] CreateDynValues(params object[] values)
		{
			return (from obj in values
			select this.CreateDynValue(obj)).ToArray<DynValue>();
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00028640 File Offset: 0x00026840
		public DynValue CreateDynValue(object value)
		{
			if (value is DynValue)
			{
				return (DynValue)value;
			}
			return DynValue.FromObject(this.script, value);
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x0002865D File Offset: 0x0002685D
		public DynValue CreateTable(params DynValue[] values)
		{
			return DynValue.NewTable(this.script, values);
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x0002866B File Offset: 0x0002686B
		public LuaCoroutine CreateCoroutine(DynValue function, DynValue[] args)
		{
			return new LuaCoroutine(this, this.script.CreateCoroutine(function), args);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x0012DADC File Offset: 0x0012BCDC
		public DynValue Call(DynValue func, params DynValue[] args)
		{
			DynValue result = DynValue.Nil;
			try
			{
				result = this.script.Call(func, args);
			}
			catch (InterpreterException ex)
			{
				this.console.LogException(ex);
			}
			catch (Exception ex2)
			{
				this.console.LogException(ex2, "An exception occured in a wrapper function", Array.Empty<object>());
			}
			return result;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x00028680 File Offset: 0x00026880
		public void Set(string key, DynValue value)
		{
			this.script.Globals.Set(key, value);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x00028694 File Offset: 0x00026894
		public void Set(string key, object value)
		{
			this.Set(key, this.CreateDynValue(value));
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000286A4 File Offset: 0x000268A4
		public ProcessorStats GetProcessorStats()
		{
			return this.script.GetProcessorStats();
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000286B1 File Offset: 0x000268B1
		public void ResetProcessorStats()
		{
			this.script.ResetProcessorStats();
		}

		// Token: 0x040030AF RID: 12463
		private const string RESOURCE_PATH = "Lua";

		// Token: 0x040030B0 RID: 12464
		public UnityEvent onScriptReloaded;

		// Token: 0x040030B2 RID: 12466
		private static MoonSharpVsCodeDebugServer server;

		// Token: 0x040030B3 RID: 12467
		private Script script;

		// Token: 0x040030B4 RID: 12468
		private Type[] proxyTypes = new Type[0];

		// Token: 0x040030B5 RID: 12469
		private Dictionary<int, Table> environments = new Dictionary<int, Table>();

		// Token: 0x040030B6 RID: 12470
		private HashSet<string> loadedSources = new HashSet<string>();

		// Token: 0x040030B7 RID: 12471
		private Dictionary<int, string> sourcePaths = new Dictionary<int, string>();

		// Token: 0x040030B8 RID: 12472
		private Dictionary<string, string> assetPaths = new Dictionary<string, string>();

		// Token: 0x040030B9 RID: 12473
		private Dictionary<string, ScriptEngine.ReloadInfo> reloadable = new Dictionary<string, ScriptEngine.ReloadInfo>();

		// Token: 0x0200092D RID: 2349
		private struct ReloadInfo
		{
			// Token: 0x040030BA RID: 12474
			public string filePath;

			// Token: 0x040030BB RID: 12475
			public string cacheId;

			// Token: 0x040030BC RID: 12476
			public int? environmentId;
		}

		// Token: 0x0200092E RID: 2350
		public struct IncludeTextAssetResult
		{
			// Token: 0x040030BD RID: 12477
			public Table environment;

			// Token: 0x040030BE RID: 12478
			public bool success;
		}
	}
}
