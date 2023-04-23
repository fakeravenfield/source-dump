using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000927 RID: 2343
	public class RavenscriptManager : MonoBehaviour
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x00028256 File Offset: 0x00026456
		public static ScriptEngine engine
		{
			get
			{
				return RavenscriptManager.instance._engine;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x00028262 File Offset: 0x00026462
		public static RavenscriptEvents events
		{
			get
			{
				return RavenscriptManager.instance._events;
			}
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0012CB2C File Offset: 0x0012AD2C
		private void Awake()
		{
			RavenscriptManager.instance = this;
			this.console.Initialize();
			this._engine = base.gameObject.AddComponent<ScriptEngine>();
			this._events = base.gameObject.AddComponent<RavenscriptEvents>();
			this._engine.Set(NameAttribute.GetName(typeof(RavenscriptEvents)), this._events);
			this.PrintStartupMessage();
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x0002826E File Offset: 0x0002646E
		private void Start()
		{
			this.PrintKeybindMessage();
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x0012CB94 File Offset: 0x0012AD94
		private void OnGUI()
		{
			if (!this.console.IsVisible())
			{
				return;
			}
			ProcessorStats processorStats = this._engine.GetProcessorStats();
			GUI.color = Color.red;
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.Scale(new Vector3(2f, 2f, 2f));
			float x = (float)(Screen.width / 4);
			this.LogProcessorStat("ValueStack", x, 40f, processorStats.valueStack);
			this.LogProcessorStat("ExecutionStack", x, 60f, processorStats.executionStack);
			GUI.Label(new Rect(x, 80f, 300f, 20f), string.Format("LUA ValueStack clears: {0}", processorStats.valueStackClears));
			GUI.matrix = matrix;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00028276 File Offset: 0x00026476
		private void LogProcessorStat(string name, float x, float y, ProcessorStats.StackSize stack)
		{
			GUI.Label(new Rect(x, y, 300f, 20f), string.Format("LUA {0}: current={1}, max={2}", name, stack.current, stack.maxSize));
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000282B1 File Offset: 0x000264B1
		public void ResetLog()
		{
			this._engine.ResetProcessorStats();
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x0012CC54 File Offset: 0x0012AE54
		private void PrintStartupMessage()
		{
			if (ScriptConsole.instance != null)
			{
				ScriptConsole.instance.LogInfo("Ravenfield Console");
				ScriptConsole.instance.LogInfo("Ravenfield is ® and © SteelRaven7 AB. All rights reserved.");
				ScriptConsole.instance.LogInfo("");
				string message = GameManager.GenerateVersionString();
				Debug.Log(message);
				ScriptConsole.instance.LogInfo(message);
				ScriptConsole.instance.LogInfo("");
			}
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x0012CCC4 File Offset: 0x0012AEC4
		private void PrintKeybindMessage()
		{
			ScriptConsole.instance.LogInfo("");
			ScriptConsole.instance.LogInfo("Use {0} to toggle console", new object[]
			{
				SteelInput.GetInput(SteelInput.KeyBinds.Console).PositiveLabel()
			});
			ScriptConsole.instance.LogInfo("Use {0} to reload scripts (Only available in test mode)", new object[]
			{
				SteelInput.GetInput(SteelInput.KeyBinds.ReloadScripts).PositiveLabel()
			});
			ScriptConsole.instance.LogInfo("");
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000282BE File Offset: 0x000264BE
		private static IEnumerable<Type> GetTypesWithHelpAttribute(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				if (type.GetCustomAttributes(typeof(GlobalInstanceAttribute), true).Length != 0)
				{
					yield return type;
				}
			}
			Type[] array = null;
			yield break;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000282CE File Offset: 0x000264CE
		public void OnStartTestSession(string testSessionId)
		{
			if (this.ParseReloadableJson())
			{
				this._engine.StartDebugServer();
			}
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000282E3 File Offset: 0x000264E3
		public static string StripLocalPathString(string localPath)
		{
			return localPath.Replace("\\", "/").Replace("..", "").Replace(":", "");
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x00028313 File Offset: 0x00026513
		public static string ResolveInvokingSourceModContentPath(string localPath)
		{
			string text = RavenscriptManager.ResolveInvokingSourceModPath();
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("Could not resolve script mod path");
			}
			return text + "/" + RavenscriptManager.StripLocalPathString(localPath);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x0002833D File Offset: 0x0002653D
		public static string ResolveInvokingSourceModPath()
		{
			return RavenscriptManager.ResolveCurrentlyInvokingSourceScript().sourceModPath;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x0012CD40 File Offset: 0x0012AF40
		public static ScriptedBehaviour ResolveCurrentlyInvokingSourceScript()
		{
			if (!RavenscriptManager.instance._events.IsCallStackEmpty())
			{
				Debug.Log("Event");
				ScriptedBehaviour currentInvokingListenerScript = RavenscriptManager.instance._events.GetCurrentEvent().GetCurrentInvokingListenerScript();
				if (currentInvokingListenerScript != null)
				{
					return currentInvokingListenerScript;
				}
			}
			if (ValueMonitor.invokingMonitor != null)
			{
				Debug.Log("Monitor");
				return ValueMonitor.invokingMonitor.sourceScript;
			}
			if (ScriptedBehaviour.currentInvokingBuiltInMethodSource != null)
			{
				Debug.Log("Method");
				return ScriptedBehaviour.currentInvokingBuiltInMethodSource;
			}
			throw new ScriptRuntimeException("Could not resolve currently invoking script source");
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x0012CDCC File Offset: 0x0012AFCC
		private void Update()
		{
			if (ScriptConsole.instance != null && SteelInput.GetButtonDown(SteelInput.KeyBinds.Console))
			{
				ScriptConsole.instance.Toggle();
			}
			if (SteelInput.GetButtonDown(SteelInput.KeyBinds.ReloadScripts))
			{
				if (!GameManager.IsTestingContentMod())
				{
					ScriptConsole.instance.LogInfo("Script reloading is only available in Test mode");
					return;
				}
				this._engine.ReloadScripts();
			}
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x0012CE24 File Offset: 0x0012B024
		private bool ParseReloadableJson()
		{
			string text = Path.Combine(ModManager.ModStagingPath(), "reloadable.json");
			Debug.Log("Parse reloadable.json file at " + text);
			if (!File.Exists(text))
			{
				Debug.LogError("No test session description file found.");
				return false;
			}
			ReloadableJson reloadableJson = JsonUtility.FromJson<ReloadableJson>(File.ReadAllText(text));
			Debug.Log("Loading reloadable.json file");
			Debug.Log("text assets: " + reloadableJson.textAssets.Length.ToString());
			if (reloadableJson.textAssets != null)
			{
				foreach (string text2 in reloadableJson.textAssets)
				{
					if (File.Exists(text2))
					{
						string extension = Path.GetExtension(text2);
						if (extension == ".txt" || extension == ".lua")
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
							this._engine.RegisterTextAssetPath(fileNameWithoutExtension, text2);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04003093 RID: 12435
		public ScriptConsole console;

		// Token: 0x04003094 RID: 12436
		private ScriptEngine _engine;

		// Token: 0x04003095 RID: 12437
		private RavenscriptEvents _events;

		// Token: 0x04003096 RID: 12438
		public static RavenscriptManager instance;
	}
}
