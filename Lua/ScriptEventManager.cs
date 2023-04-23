using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Events;

namespace Lua
{
	// Token: 0x0200093C RID: 2364
	[Ignore(ignoreBase = true)]
	public abstract class ScriptEventManager : MonoBehaviour
	{
		// Token: 0x06003C08 RID: 15368 RVA: 0x00028921 File Offset: 0x00026B21
		protected void Awake()
		{
			this.InitializeEvents();
			this.StartGarbageCollection();
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0012DE04 File Offset: 0x0012C004
		public void InitializeEvents()
		{
			this.events.Clear();
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.PropertyType.IsGenericType && !(propertyInfo.PropertyType.GetGenericTypeDefinition().BaseType != typeof(ScriptEvent)) && (ScriptEvent)propertyInfo.GetValue(this, null) == null)
				{
					ScriptEvent scriptEvent = (ScriptEvent)Activator.CreateInstance(propertyInfo.PropertyType);
					propertyInfo.SetValue(this, scriptEvent, null);
					this.RegisterScriptEvent(scriptEvent);
				}
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x0002892F File Offset: 0x00026B2F
		private void RegisterScriptEvent(ScriptEvent scriptEvent)
		{
			this.events.Add(new WeakReference(scriptEvent));
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x0012DE9C File Offset: 0x0012C09C
		public ScriptEvent CreateEvent()
		{
			ScriptEvent scriptEvent = new ScriptEvent();
			this.RegisterScriptEvent(scriptEvent);
			return scriptEvent;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00028942 File Offset: 0x00026B42
		public void PushCallStack(ScriptEvent calledEvent)
		{
			this.eventCallStack.Push(calledEvent);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00028950 File Offset: 0x00026B50
		public void PopCallStack()
		{
			this.eventCallStack.Pop();
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0002895E File Offset: 0x00026B5E
		public bool IsCallStackFull()
		{
			return this.eventCallStack.Count > 100;
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x0002896F File Offset: 0x00026B6F
		public bool IsCallStackEmpty()
		{
			return this.eventCallStack.Count == 0;
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0002897F File Offset: 0x00026B7F
		public ScriptEvent GetCurrentEvent()
		{
			if (this.IsCallStackEmpty())
			{
				throw new ScriptRuntimeException("The ScriptEvent callstack is empty");
			}
			return this.eventCallStack.Peek();
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x0002899F File Offset: 0x00026B9F
		private ScriptEventCache.GetOrCreateResult GetOrCreateEvent(Component target, UnityEventBase unityEvent)
		{
			return (target.gameObject.GetComponent<ScriptEventCache>() ?? target.gameObject.AddComponent<ScriptEventCache>()).GetOrCreateEvent(unityEvent);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000289C1 File Offset: 0x00026BC1
		private ScriptEventCache.GetOrCreateResult GetOrCreateEvent(Component target, Action action)
		{
			return (target.gameObject.GetComponent<ScriptEventCache>() ?? target.gameObject.AddComponent<ScriptEventCache>()).GetOrCreateAction(action);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x0012DEB8 File Offset: 0x0012C0B8
		public ScriptEvent WrapAction(Component target, Action action)
		{
			ScriptEventCache.GetOrCreateResult r = this.GetOrCreateEvent(target, action);
			if (r.wasCreated)
			{
				action = (Action)Delegate.Combine(action, new Action(delegate()
				{
					r.scriptEvent.UnsafeInvoke(Array.Empty<object>());
				}));
			}
			return r.scriptEvent;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x0012DF0C File Offset: 0x0012C10C
		public ScriptEvent WrapUnityEvent(Component target, UnityEvent unityEvent)
		{
			ScriptEventCache.GetOrCreateResult r = this.GetOrCreateEvent(target, unityEvent);
			if (r.wasCreated)
			{
				unityEvent.AddListener(delegate()
				{
					r.scriptEvent.UnsafeInvoke(Array.Empty<object>());
				});
			}
			return r.scriptEvent;
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0012DF58 File Offset: 0x0012C158
		public ScriptEvent WrapUnityEvent<T0>(Component target, UnityEvent<T0> unityEvent)
		{
			ScriptEventCache.GetOrCreateResult r = this.GetOrCreateEvent(target, unityEvent);
			if (r.wasCreated)
			{
				unityEvent.AddListener(delegate(T0 a0)
				{
					r.scriptEvent.UnsafeInvoke(new object[]
					{
						a0
					});
				});
			}
			return r.scriptEvent;
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x0012DFA4 File Offset: 0x0012C1A4
		public ScriptEvent WrapUnityEvent<T0, T1>(Component target, UnityEvent<T0, T1> unityEvent)
		{
			ScriptEventCache.GetOrCreateResult r = this.GetOrCreateEvent(target, unityEvent);
			if (r.wasCreated)
			{
				unityEvent.AddListener(delegate(T0 a0, T1 a1)
				{
					r.scriptEvent.UnsafeInvoke(new object[]
					{
						a0,
						a1
					});
				});
			}
			return r.scriptEvent;
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0012DFF0 File Offset: 0x0012C1F0
		public ScriptEvent WrapUnityEvent<T0, T1, T2>(Component target, UnityEvent<T0, T1, T2> unityEvent)
		{
			ScriptEventCache.GetOrCreateResult r = this.GetOrCreateEvent(target, unityEvent);
			if (r.wasCreated)
			{
				unityEvent.AddListener(delegate(T0 a0, T1 a1, T2 a2)
				{
					r.scriptEvent.UnsafeInvoke(new object[]
					{
						a0,
						a1,
						a2
					});
				});
			}
			return r.scriptEvent;
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000289E3 File Offset: 0x00026BE3
		private void StartGarbageCollection()
		{
			if (!this.isCollectingGarbage)
			{
				this.isCollectingGarbage = true;
				base.StartCoroutine(this.CollectGarbage());
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00028A01 File Offset: 0x00026C01
		private IEnumerator CollectGarbage()
		{
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				int count = this.events.Count;
				this.events.RemoveAll((WeakReference e) => !e.IsAlive);
				using (List<WeakReference>.Enumerator enumerator = this.events.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WeakReference weakReference = enumerator.Current;
						((ScriptEvent)weakReference.Target).RemoveInvalidListeners();
					}
					continue;
				}
				yield break;
			}
		}

		// Token: 0x040030D4 RID: 12500
		private const int MAX_CALL_STACK_SIZE = 100;

		// Token: 0x040030D5 RID: 12501
		private List<WeakReference> events = new List<WeakReference>();

		// Token: 0x040030D6 RID: 12502
		private bool isCollectingGarbage;

		// Token: 0x040030D7 RID: 12503
		private Stack<ScriptEvent> eventCallStack = new Stack<ScriptEvent>();
	}
}
