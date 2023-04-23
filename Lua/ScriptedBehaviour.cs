using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Events;

namespace Lua
{
	// Token: 0x02000913 RID: 2323
	[Ignore]
	[Doc("All Ravenscript's are executed and managed by a ScriptedBehaviour component. This component is accessable in Ravenscript through ``self.script``.")]
	public class ScriptedBehaviour : MonoBehaviour, IAutoUpgradeReference
	{
		// Token: 0x06003AF9 RID: 15097 RVA: 0x0012BE0C File Offset: 0x0012A00C
		public void UpgradeReference(Component previous, Component upgraded)
		{
			for (int i = 0; i < this.targets.Length; i++)
			{
				if (this.targets[i].value == previous)
				{
					this.targets[i].value = upgraded;
				}
			}
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x0012BE50 File Offset: 0x0012A050
		protected void Initialize()
		{
			try
			{
				string name = this.behaviour;
				if (string.IsNullOrEmpty(name))
				{
					name = this.source.name;
				}
				Dictionary<string, UnityEngine.Object> dictionary = new Dictionary<string, UnityEngine.Object>();
				if (this.targets != null && this.targets.Length != 0)
				{
					foreach (NamedTarget namedTarget in this.targets)
					{
						if (!string.IsNullOrEmpty(namedTarget.name) && namedTarget.value)
						{
							dictionary.Add(namedTarget.name, namedTarget.value);
						}
					}
				}
				this.engine = RavenscriptManager.engine;
				ScriptEngine.IncludeTextAssetResult includeTextAssetResult = this.engine.IncludeTextAsset(this.source, true);
				DynValue dynValue = this.engine.CreateTable(Array.Empty<DynValue>());
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, UnityEngine.Object> keyValuePair in dictionary)
					{
						if (!string.IsNullOrEmpty(keyValuePair.Key) && keyValuePair.Value)
						{
							DynValue dynValue2 = this.engine.CreateDynValue(keyValuePair.Value);
							if (dynValue2.IsNotNil())
							{
								dynValue.Table.Set(keyValuePair.Key, dynValue2);
							}
						}
					}
				}
				this.luaClass = new LuaClass(this.engine, includeTextAssetResult.environment, name);
				this.methods = new Dictionary<string, LuaClass.Method>();
				this.monitors = new List<ValueMonitor>(16);
				this.luaClass.Set("gameObject", base.gameObject);
				this.luaClass.Set("transform", base.transform);
				this.luaClass.Set("script", this);
				this.luaClass.Set("targets", dynValue);
				this.engine.onScriptReloaded.AddListener(new UnityAction(this.GetMethodPointers));
				this.GetMethodPointers();
				this.Initialized();
			}
			catch (Exception e)
			{
				base.enabled = false;
				ModManager.HandleModException(e);
			}
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00027B8F File Offset: 0x00025D8F
		private void Initialized()
		{
			this.isInitialized = true;
			this.AwakeAndInitialized();
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00027B9E File Offset: 0x00025D9E
		private void Awake()
		{
			this.Initialize();
			this.isAwake = true;
			this.AwakeAndInitialized();
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00027BB3 File Offset: 0x00025DB3
		private void CallBuiltInMethod(LuaClass.Method method)
		{
			ScriptedBehaviour.currentInvokingBuiltInMethodSource = this;
			method.Call();
			ScriptedBehaviour.currentInvokingBuiltInMethodSource = null;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00027BC8 File Offset: 0x00025DC8
		private void AwakeAndInitialized()
		{
			if (this.isAwake && this.isInitialized)
			{
				this.CallBuiltInMethod(this.awake);
			}
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00027BE6 File Offset: 0x00025DE6
		private void OnEnable()
		{
			if (this.isAwake && this.isInitialized)
			{
				this.CallBuiltInMethod(this.onEnable);
			}
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00027C04 File Offset: 0x00025E04
		private void OnDisable()
		{
			if (this.isAwake && this.isInitialized)
			{
				this.CallBuiltInMethod(this.onDisable);
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x0012C078 File Offset: 0x0012A278
		private void GetMethodPointers()
		{
			this.methods.Clear();
			this.awake = this.GetMethod("awake");
			this.start = this.GetMethod("start");
			this.update = this.GetMethod("update");
			this.lateUpdate = this.GetMethod("late_update");
			this.fixedUpdate = this.GetMethod("fixed_update");
			this.onEnable = this.GetMethod("on_enable");
			this.onDisable = this.GetMethod("on_disable");
			this.onAnimatorIK = this.GetMethod("OnAnimatorIK");
			foreach (ValueMonitor valueMonitor in this.monitors)
			{
				valueMonitor.UpdateMethodPointers(this);
			}
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00027C22 File Offset: 0x00025E22
		private void OnDestroy()
		{
			if (this.engine && this.engine.onScriptReloaded != null)
			{
				this.engine.onScriptReloaded.RemoveListener(new UnityAction(this.GetMethodPointers));
			}
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00027C5A File Offset: 0x00025E5A
		private void Start()
		{
			if (!this.isInitialized || !this.isAwake)
			{
				throw new Exception("ScriptedBehaviour started but not initialized.");
			}
			this.CallBuiltInMethod(this.start);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00027C83 File Offset: 0x00025E83
		private void Update()
		{
			this.UpdateMonitors();
			this.CallBuiltInMethod(this.update);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x00027C97 File Offset: 0x00025E97
		private void LateUpdate()
		{
			this.CallBuiltInMethod(this.lateUpdate);
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00027CA5 File Offset: 0x00025EA5
		private void FixedUpdate()
		{
			this.CallBuiltInMethod(this.fixedUpdate);
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00027CB3 File Offset: 0x00025EB3
		private void OnAnimatorIK()
		{
			this.CallBuiltInMethod(this.onAnimatorIK);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x0012C15C File Offset: 0x0012A35C
		private void UpdateMonitors()
		{
			foreach (ValueMonitor valueMonitor in this.monitors)
			{
				valueMonitor.Update(this);
			}
			ValueMonitor.invokingMonitor = null;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00027CC1 File Offset: 0x00025EC1
		public LuaClass.Method GetMethod(string name)
		{
			if (!this.methods.ContainsKey(name))
			{
				this.methods[name] = this.luaClass.GetMethod(name);
			}
			return this.methods[name];
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x00027CF5 File Offset: 0x00025EF5
		public DynValue Call(string name, params object[] args)
		{
			return this.GetMethod(name).Call(args);
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x00027D04 File Offset: 0x00025F04
		public bool HasMethod(string name)
		{
			return this.GetMethod(name);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00027D12 File Offset: 0x00025F12
		public DynValue GetSelf()
		{
			return this.luaClass.self;
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x00027D1F File Offset: 0x00025F1F
		public void Set(string name, object value)
		{
			this.luaClass.Set(name, value);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0012C1B4 File Offset: 0x0012A3B4
		[Include]
		[Doc("Create a value monitor that notifies the script when a value changes.[..] Each frame before Update() the return value of ``monitorMethod`` is evaluated and compared to the previous value. If the value has changed, the ``onChangeMethod`` is invoked with the new value as the argument.")]
		public void AddValueMonitor(string monitorMethodName, string onChangeMethodName)
		{
			ValueMonitor valueMonitor = new ValueMonitor(monitorMethodName, onChangeMethodName, null, this);
			valueMonitor.UpdateMethodPointers(this);
			this.monitors.Add(valueMonitor);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0012C1E0 File Offset: 0x0012A3E0
		[Include]
		[Doc("Create a value monitor that notifies the script when a value changes, with a data argument.[..] In the monitor/callback functions you can access the data value with ``CurrentEvent.listenerData``.")]
		public void AddValueMonitor(string monitorMethodName, string onChangeMethodName, DynValue monitorData)
		{
			ValueMonitor valueMonitor = new ValueMonitor(monitorMethodName, onChangeMethodName, monitorData, this);
			valueMonitor.UpdateMethodPointers(this);
			this.monitors.Add(valueMonitor);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x0012C20C File Offset: 0x0012A40C
		[Include]
		[Doc("Create a value monitor that prints the current value to the screen.[..] The value is only printed to screen when the mod is run in test content mod mode.")]
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel)
		{
			if (!GameManager.IsTestingContentMod() || !ValueMonitorCanvas.IsAvailable())
			{
				return;
			}
			ValueMonitorPanel valueMonitorPanel = ValueMonitorCanvas.CreateValueMonitor();
			valueMonitorPanel.Initialize(valueLabel, "");
			ValueMonitor valueMonitor = new ValueMonitor(monitorMethodName, valueMonitorPanel, null, this);
			valueMonitor.UpdateMethodPointers(this);
			this.monitors.Add(valueMonitor);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x0012C258 File Offset: 0x0012A458
		[Include]
		[Doc("Create a value monitor that prints the current value to the screen.[..] The value is only printed to screen when the mod is run in test content mod mode.")]
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel, Color color)
		{
			if (!GameManager.IsTestingContentMod() || !ValueMonitorCanvas.IsAvailable())
			{
				return;
			}
			ValueMonitorPanel valueMonitorPanel = ValueMonitorCanvas.CreateValueMonitor();
			valueMonitorPanel.Initialize(valueLabel, "");
			valueMonitorPanel.changeBlink.color = color;
			ValueMonitor valueMonitor = new ValueMonitor(monitorMethodName, valueMonitorPanel, null, this);
			valueMonitor.UpdateMethodPointers(this);
			this.monitors.Add(valueMonitor);
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0012C2B0 File Offset: 0x0012A4B0
		[Include]
		[Doc("Create a value monitor that prints the current value to the screen, with a data argument.[..] In the monitor/callback functions you can access the data value with ``CurrentEvent.listenerData``.")]
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel, Color color, DynValue monitorData)
		{
			if (!GameManager.IsTestingContentMod() || !ValueMonitorCanvas.IsAvailable())
			{
				return;
			}
			ValueMonitorPanel valueMonitorPanel = ValueMonitorCanvas.CreateValueMonitor();
			valueMonitorPanel.Initialize(valueLabel, "");
			valueMonitorPanel.changeBlink.color = color;
			ValueMonitor valueMonitor = new ValueMonitor(monitorMethodName, valueMonitorPanel, monitorData, this);
			valueMonitor.UpdateMethodPointers(this);
			this.monitors.Add(valueMonitor);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x0012C308 File Offset: 0x0012A508
		[Include]
		[Doc("Removes all value monitors using the specified ``monitorMethod``.")]
		public void RemoveValueMonitor(string monitorMethodName)
		{
			foreach (ValueMonitor valueMonitor in this.monitors.FindAll((ValueMonitor m) => m.monitorMethodName.Equals(monitorMethodName)))
			{
				valueMonitor.OnRemoved();
				this.monitors.Remove(valueMonitor);
			}
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x0012C388 File Offset: 0x0012A588
		[Include]
		[Doc("Starts a coroutine that is executed once every frame until done.[..] Use ``coroutine.yield()`` to pause the coroutine. See the Unity manual for details about Coroutines and their use.")]
		public void StartCoroutine([Doc("A closure; or the name of a member function as a string. A closure is executed without arguments while ``self`` is automatically passed to member functions.")] DynValue coroutine)
		{
			DynValue[] args = null;
			if (coroutine == null || coroutine.IsNil())
			{
				throw new ScriptRuntimeException("argument 'value' is nil");
			}
			if (coroutine.Type == DataType.String)
			{
				string text = coroutine.CastToString();
				coroutine = this.luaClass.Get(text);
				args = new DynValue[]
				{
					this.luaClass.self
				};
				if (coroutine == null || coroutine.IsNil())
				{
					throw new ScriptRuntimeException("no function named '" + text + "'");
				}
			}
			if (coroutine.Type != DataType.ClrFunction && coroutine.Type != DataType.Function)
			{
				throw new ScriptRuntimeException("expected a function");
			}
			LuaCoroutine routine = this.engine.CreateCoroutine(coroutine, args);
			base.StartCoroutine(routine);
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0012C438 File Offset: 0x0012A638
		public static ScriptedBehaviour FindInstance(Table obj)
		{
			foreach (ScriptedBehaviour scriptedBehaviour in UnityEngine.Object.FindObjectsOfType<ScriptedBehaviour>())
			{
				if (scriptedBehaviour.isInitialized && scriptedBehaviour.luaClass.self.Table == obj)
				{
					return scriptedBehaviour;
				}
			}
			return null;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0012C47C File Offset: 0x0012A67C
		public static ScriptedBehaviour FindOfType(Table prototype)
		{
			return (from s in UnityEngine.Object.FindObjectsOfType<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).FirstOrDefault<ScriptedBehaviour>();
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0012C4B4 File Offset: 0x0012A6B4
		public static ScriptedBehaviour[] FindAllOfType(Table prototype)
		{
			return (from s in UnityEngine.Object.FindObjectsOfType<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).ToArray<ScriptedBehaviour>();
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00027D2E File Offset: 0x00025F2E
		public static ScriptedBehaviour GetScript(GameObject go)
		{
			return go.GetComponent<ScriptedBehaviour>();
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x0012C4EC File Offset: 0x0012A6EC
		public static ScriptedBehaviour GetScript(GameObject go, Table prototype)
		{
			return (from s in go.GetComponents<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).FirstOrDefault<ScriptedBehaviour>();
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x0012C524 File Offset: 0x0012A724
		public static ScriptedBehaviour GetScriptInChildren(GameObject go, Table prototype)
		{
			return (from s in go.GetComponentsInChildren<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).FirstOrDefault<ScriptedBehaviour>();
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x0012C55C File Offset: 0x0012A75C
		public static ScriptedBehaviour GetScriptInParent(GameObject go, Table prototype)
		{
			return (from s in go.GetComponentsInParent<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).FirstOrDefault<ScriptedBehaviour>();
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0012C594 File Offset: 0x0012A794
		public static ScriptedBehaviour[] GetScripts(GameObject go, Table prototype)
		{
			return (from s in go.GetComponents<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).ToArray<ScriptedBehaviour>();
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x0012C5CC File Offset: 0x0012A7CC
		public static ScriptedBehaviour[] GetScriptsInChildren(GameObject go, Table prototype)
		{
			return (from s in go.GetComponentsInChildren<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).ToArray<ScriptedBehaviour>();
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0012C604 File Offset: 0x0012A804
		public static ScriptedBehaviour[] GetScriptsInParent(GameObject go, Table prototype)
		{
			return (from s in go.GetComponentsInParent<ScriptedBehaviour>()
			where s.isInitialized && s.luaClass.prototype.Table == prototype
			select s).ToArray<ScriptedBehaviour>();
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0012C63C File Offset: 0x0012A83C
		public static void DestroyAll(GameObject go)
		{
			ScriptedBehaviour[] componentsInChildren = go.GetComponentsInChildren<ScriptedBehaviour>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				UnityEngine.Object.Destroy(componentsInChildren[i]);
			}
		}

		// Token: 0x0400304A RID: 12362
		public TextAsset source;

		// Token: 0x0400304B RID: 12363
		public string behaviour;

		// Token: 0x0400304C RID: 12364
		public NamedTarget[] targets;

		// Token: 0x0400304D RID: 12365
		public static ScriptedBehaviour currentInvokingBuiltInMethodSource;

		// Token: 0x0400304E RID: 12366
		private bool isInitialized;

		// Token: 0x0400304F RID: 12367
		private bool isAwake;

		// Token: 0x04003050 RID: 12368
		private ScriptEngine engine;

		// Token: 0x04003051 RID: 12369
		private LuaClass luaClass;

		// Token: 0x04003052 RID: 12370
		private Dictionary<string, LuaClass.Method> methods;

		// Token: 0x04003053 RID: 12371
		private List<ValueMonitor> monitors;

		// Token: 0x04003054 RID: 12372
		[HideInInspector]
		public string sourceModPath;

		// Token: 0x04003055 RID: 12373
		[HideInInspector]
		public MutatorEntry sourceMutator;

		// Token: 0x04003056 RID: 12374
		private LuaClass.Method awake;

		// Token: 0x04003057 RID: 12375
		private LuaClass.Method start;

		// Token: 0x04003058 RID: 12376
		private LuaClass.Method update;

		// Token: 0x04003059 RID: 12377
		private LuaClass.Method lateUpdate;

		// Token: 0x0400305A RID: 12378
		private LuaClass.Method fixedUpdate;

		// Token: 0x0400305B RID: 12379
		private LuaClass.Method onEnable;

		// Token: 0x0400305C RID: 12380
		private LuaClass.Method onDisable;

		// Token: 0x0400305D RID: 12381
		private LuaClass.Method onAnimatorIK;
	}
}
