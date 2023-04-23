using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua
{
	// Token: 0x0200094A RID: 2378
	public class ValueMonitor
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x00028CE1 File Offset: 0x00026EE1
		public static bool IsInvokingCallbackEvent()
		{
			return ValueMonitor.invokingMonitor != null;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00028CEB File Offset: 0x00026EEB
		public ValueMonitor(string methodName, string callbackMethodName, DynValue monitorData, ScriptedBehaviour sourceScript)
		{
			this.monitorMethodName = methodName;
			this.callbackMethodName = callbackMethodName;
			this.monitorData = monitorData;
			this.sourceScript = sourceScript;
			this.isDebugMonitor = false;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00028D17 File Offset: 0x00026F17
		public ValueMonitor(string methodName, ValueMonitorPanel panel, DynValue monitorData, ScriptedBehaviour sourceScript)
		{
			this.monitorMethodName = methodName;
			this.panel = panel;
			this.monitorData = monitorData;
			this.sourceScript = sourceScript;
			this.isDebugMonitor = true;
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x0012E1C4 File Offset: 0x0012C3C4
		public void UpdateMethodPointers(ScriptedBehaviour script)
		{
			this.monitor = script.GetMethod(this.monitorMethodName);
			if (!this.isDebugMonitor)
			{
				this.callback = script.GetMethod(this.callbackMethodName);
			}
			ValueMonitor.invokingMonitor = this;
			try
			{
				this.lastValue = this.monitor.Call();
				if (this.isDebugMonitor)
				{
					this.panel.UpdateValueText(this.lastValue.ToString());
				}
			}
			catch (Exception ex)
			{
				ScriptConsole.instance.LogException(ex);
			}
			finally
			{
				ValueMonitor.invokingMonitor = null;
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00028D43 File Offset: 0x00026F43
		public void OnRemoved()
		{
			if (this.panel != null)
			{
				UnityEngine.Object.Destroy(this.panel.gameObject);
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0012E268 File Offset: 0x0012C468
		public void Update(ScriptedBehaviour script)
		{
			ValueMonitor.invokingMonitor = this;
			try
			{
				DynValue dynValue = this.monitor.Call();
				if (!dynValue.Equals(this.lastValue))
				{
					if (this.isDebugMonitor)
					{
						this.panel.UpdateValueText(dynValue.ToString());
					}
					else
					{
						this.callback.Call(new DynValue[]
						{
							dynValue
						});
					}
					this.lastValue = dynValue;
				}
			}
			catch (Exception ex)
			{
				ScriptConsole.instance.LogException(ex);
			}
			finally
			{
				ValueMonitor.invokingMonitor = null;
			}
		}

		// Token: 0x040030F5 RID: 12533
		public static ValueMonitor invokingMonitor;

		// Token: 0x040030F6 RID: 12534
		public ScriptedBehaviour sourceScript;

		// Token: 0x040030F7 RID: 12535
		public DynValue lastValue;

		// Token: 0x040030F8 RID: 12536
		public DynValue monitorData;

		// Token: 0x040030F9 RID: 12537
		public string monitorMethodName;

		// Token: 0x040030FA RID: 12538
		public string callbackMethodName;

		// Token: 0x040030FB RID: 12539
		public bool isDebugMonitor;

		// Token: 0x040030FC RID: 12540
		public ValueMonitorPanel panel;

		// Token: 0x040030FD RID: 12541
		private LuaClass.Method monitor;

		// Token: 0x040030FE RID: 12542
		private LuaClass.Method callback;
	}
}
