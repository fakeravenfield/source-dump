using System;
using System.Collections.Generic;
using Lua;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000024 RID: 36
public class ScriptEventCache : MonoBehaviour
{
	// Token: 0x060000AB RID: 171 RVA: 0x0003F9C4 File Offset: 0x0003DBC4
	public ScriptEventCache.GetOrCreateResult GetOrCreateEvent(UnityEventBase unityEvent)
	{
		if (!this.events.ContainsKey(unityEvent))
		{
			ScriptEvent scriptEvent = RavenscriptManager.events.CreateEvent();
			this.events.Add(unityEvent, scriptEvent);
			return new ScriptEventCache.GetOrCreateResult(scriptEvent, true);
		}
		return new ScriptEventCache.GetOrCreateResult(this.events[unityEvent], false);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0003FA14 File Offset: 0x0003DC14
	public ScriptEventCache.GetOrCreateResult GetOrCreateAction(Action action)
	{
		if (!this.actions.ContainsKey(action))
		{
			ScriptEvent scriptEvent = RavenscriptManager.events.CreateEvent();
			this.actions.Add(action, scriptEvent);
			return new ScriptEventCache.GetOrCreateResult(scriptEvent, true);
		}
		return new ScriptEventCache.GetOrCreateResult(this.actions[action], false);
	}

	// Token: 0x04000066 RID: 102
	private Dictionary<UnityEventBase, ScriptEvent> events = new Dictionary<UnityEventBase, ScriptEvent>();

	// Token: 0x04000067 RID: 103
	private Dictionary<Action, ScriptEvent> actions = new Dictionary<Action, ScriptEvent>();

	// Token: 0x02000025 RID: 37
	public struct GetOrCreateResult
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00002B26 File Offset: 0x00000D26
		public GetOrCreateResult(ScriptEvent scriptEvent, bool wasCreated)
		{
			this.scriptEvent = scriptEvent;
			this.wasCreated = wasCreated;
		}

		// Token: 0x04000068 RID: 104
		public readonly ScriptEvent scriptEvent;

		// Token: 0x04000069 RID: 105
		public readonly bool wasCreated;
	}
}
