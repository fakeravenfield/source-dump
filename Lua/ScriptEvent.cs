using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua
{
	// Token: 0x02000934 RID: 2356
	[Ignore]
	public class ScriptEvent
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x0012DC34 File Offset: 0x0012BE34
		public void AddListener(ScriptedBehaviour script, string methodName, DynValue data)
		{
			if (!script.HasMethod(methodName))
			{
				throw new ScriptRuntimeException("cannot find method '{0}'", new object[]
				{
					methodName
				});
			}
			if (this.eventListeners.FindIndex((ScriptEvent.EventListener e) => e.script.IsAlive && (ScriptedBehaviour)e.script.Target == script && e.methodName == methodName) == -1)
			{
				this.eventListeners.Add(new ScriptEvent.EventListener
				{
					script = new WeakReference(script),
					methodName = methodName,
					data = data
				});
			}
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x0012DCD0 File Offset: 0x0012BED0
		public void RemoveListener(ScriptedBehaviour script, string methodName)
		{
			int num = this.eventListeners.FindIndex((ScriptEvent.EventListener e) => (ScriptedBehaviour)e.script.Target == script && e.methodName == methodName);
			if (num != -1)
			{
				this.eventListeners.RemoveAt(num);
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x0012DD1C File Offset: 0x0012BF1C
		public void UnsafeInvoke(params object[] args)
		{
			if (RavenscriptManager.events.IsCallStackFull())
			{
				StackTraceLogType stackTraceLogType = Application.GetStackTraceLogType(LogType.Error);
				Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.None);
				ScriptConsole.instance.LogError("Event Call Stack is full, escaping event callback. If you see this message, one or more scripts generated an event feedback loop.");
				Application.SetStackTraceLogType(LogType.Error, stackTraceLogType);
				return;
			}
			RavenscriptManager.events.PushCallStack(this);
			this.isConsumed = false;
			try
			{
				foreach (ScriptEvent.EventListener eventListener in this.eventListeners)
				{
					if (eventListener.script.IsAlive)
					{
						this.invokingListener = eventListener;
						((ScriptedBehaviour)eventListener.script.Target).Call(eventListener.methodName, args);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				RavenscriptManager.events.PopCallStack();
			}
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x0002872E File Offset: 0x0002692E
		public void Consume()
		{
			this.isConsumed = true;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00028737 File Offset: 0x00026937
		public void RemoveInvalidListeners()
		{
			this.eventListeners.RemoveAll((ScriptEvent.EventListener e) => !e.script.IsAlive || !(ScriptedBehaviour)e.script.Target);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00028764 File Offset: 0x00026964
		public ScriptedBehaviour GetCurrentInvokingListenerScript()
		{
			if (!this.invokingListener.script.IsAlive)
			{
				return null;
			}
			return this.invokingListener.script.Target as ScriptedBehaviour;
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x0002878F File Offset: 0x0002698F
		public DynValue GetCurrentInvokingListenerData()
		{
			return this.invokingListener.data;
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0002879C File Offset: 0x0002699C
		public static ScriptedBehaviour ScriptedBehaviourFromLuaValue(DynValue value)
		{
			if (value.Type != DataType.Table)
			{
				throw new ScriptRuntimeException("expected a table");
			}
			ScriptedBehaviour scriptedBehaviour = ScriptedBehaviour.FindInstance(value.Table);
			if (scriptedBehaviour == null)
			{
				throw new ScriptRuntimeException("cannot find associated ScriptedBehaviour");
			}
			return scriptedBehaviour;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000287D1 File Offset: 0x000269D1
		[Include]
		[Doc("Adds `script.methodName` as an event handler.[..]This handler is called every time the event occurs.")]
		public void AddListener(DynValue script, string methodName)
		{
			this.AddListener(ScriptEvent.ScriptedBehaviourFromLuaValue(script), methodName, null);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000287E1 File Offset: 0x000269E1
		[Include]
		[Doc("Adds `script.methodName` as an event handler with a data argument.[..]This handler is called every time the event occurs. In the event listener, access the data value with ``CurrentEvent.listenerData``.")]
		public void AddListener(DynValue script, string methodName, DynValue listenerData)
		{
			this.AddListener(ScriptEvent.ScriptedBehaviourFromLuaValue(script), methodName, listenerData);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000287F1 File Offset: 0x000269F1
		[Include]
		[Doc("Removes `script.methodName` from the list of event handlers.")]
		public void RemoveListener(DynValue script, string methodName)
		{
			this.RemoveListener(ScriptEvent.ScriptedBehaviourFromLuaValue(script), methodName);
		}

		// Token: 0x040030C8 RID: 12488
		private List<ScriptEvent.EventListener> eventListeners = new List<ScriptEvent.EventListener>();

		// Token: 0x040030C9 RID: 12489
		public bool isConsumed;

		// Token: 0x040030CA RID: 12490
		private ScriptEvent.EventListener invokingListener;

		// Token: 0x02000935 RID: 2357
		private class EventListener
		{
			// Token: 0x040030CB RID: 12491
			public WeakReference script;

			// Token: 0x040030CC RID: 12492
			public string methodName;

			// Token: 0x040030CD RID: 12493
			public DynValue data;
		}
	}
}
