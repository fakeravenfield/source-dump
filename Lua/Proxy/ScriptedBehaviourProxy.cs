using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009F8 RID: 2552
	[Proxy(typeof(ScriptedBehaviour))]
	public class ScriptedBehaviourProxy : IProxy
	{
		// Token: 0x06004E31 RID: 20017 RVA: 0x000389FB File Offset: 0x00036BFB
		[MoonSharpHidden]
		public ScriptedBehaviourProxy(ScriptedBehaviour value)
		{
			this._value = value;
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x00038A0A File Offset: 0x00036C0A
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x00038A1C File Offset: 0x00036C1C
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x00038A2E File Offset: 0x00036C2E
		public MutatorEntryProxy mutator
		{
			get
			{
				return MutatorEntryProxy.New(WScriptedBehaviour.GetMutator(this._value));
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06004E35 RID: 20021 RVA: 0x00038A40 File Offset: 0x00036C40
		public DynValue self
		{
			get
			{
				return WScriptedBehaviour.GetSelf(this._value);
			}
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00038A4D File Offset: 0x00036C4D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00137D80 File Offset: 0x00135F80
		[MoonSharpHidden]
		public static ScriptedBehaviourProxy New(ScriptedBehaviour value)
		{
			if (value == null)
			{
				return null;
			}
			ScriptedBehaviourProxy scriptedBehaviourProxy = (ScriptedBehaviourProxy)ObjectCache.Get(typeof(ScriptedBehaviourProxy), value);
			if (scriptedBehaviourProxy == null)
			{
				scriptedBehaviourProxy = new ScriptedBehaviourProxy(value);
				ObjectCache.Add(typeof(ScriptedBehaviourProxy), value, scriptedBehaviourProxy);
			}
			return scriptedBehaviourProxy;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00038A55 File Offset: 0x00036C55
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel)
		{
			this._value.AddDebugValueMonitor(monitorMethodName, valueLabel);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00038A64 File Offset: 0x00036C64
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel, ColorProxy color)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			this._value.AddDebugValueMonitor(monitorMethodName, valueLabel, color._value);
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00038A87 File Offset: 0x00036C87
		public void AddDebugValueMonitor(string monitorMethodName, string valueLabel, ColorProxy color, DynValue monitorData)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			this._value.AddDebugValueMonitor(monitorMethodName, valueLabel, color._value, monitorData);
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00038AAC File Offset: 0x00036CAC
		public void AddValueMonitor(string monitorMethodName, string onChangeMethodName)
		{
			this._value.AddValueMonitor(monitorMethodName, onChangeMethodName);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00038ABB File Offset: 0x00036CBB
		public void AddValueMonitor(string monitorMethodName, string onChangeMethodName, DynValue monitorData)
		{
			this._value.AddValueMonitor(monitorMethodName, onChangeMethodName, monitorData);
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00038ACB File Offset: 0x00036CCB
		public void RemoveValueMonitor(string monitorMethodName)
		{
			this._value.RemoveValueMonitor(monitorMethodName);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00038AD9 File Offset: 0x00036CD9
		public void StartCoroutine(DynValue coroutine)
		{
			this._value.StartCoroutine(coroutine);
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00137DCC File Offset: 0x00135FCC
		public static DynValue GetScript(GameObjectProxy go)
		{
			GameObject go2 = null;
			if (go != null)
			{
				go2 = go._value;
			}
			return WScriptedBehaviour.GetScript(go2);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00038AE7 File Offset: 0x00036CE7
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003287 RID: 12935
		[MoonSharpHidden]
		public ScriptedBehaviour _value;
	}
}
