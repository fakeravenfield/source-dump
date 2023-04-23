using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.REPL;
using UnityEngine;

namespace Lua
{
	// Token: 0x0200092B RID: 2347
	public class ScriptConsole : MonoBehaviour
	{
		// Token: 0x06003BB2 RID: 15282 RVA: 0x00028501 File Offset: 0x00026701
		public virtual void Initialize()
		{
			ScriptConsole.instance = this;
			this.Hide();
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnScriptEngineReady(ReplInterpreter repl)
		{
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x0000257D File Offset: 0x0000077D
		public virtual bool IsVisible()
		{
			return false;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void Show()
		{
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void Hide()
		{
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x0002850F File Offset: 0x0002670F
		public void Toggle()
		{
			if (this.IsVisible())
			{
				this.Hide();
				return;
			}
			this.Show();
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x00028526 File Offset: 0x00026726
		public virtual void LogInfo(string message)
		{
			Debug.Log("Lua: " + message);
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x00028538 File Offset: 0x00026738
		public virtual void LogInfo(string format, params object[] args)
		{
			this.LogInfo(string.Format(format, args));
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x00028547 File Offset: 0x00026747
		public virtual void LogError(string message)
		{
			Debug.LogError("Lua Error: " + message);
			throw new Exception(message);
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x0002855F File Offset: 0x0002675F
		public virtual void LogError(string message, params object[] args)
		{
			this.LogError(string.Format(message, args));
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x0002856E File Offset: 0x0002676E
		public virtual void LogException(Exception ex)
		{
			this.LogError(ScriptConsole.DecorateExceptionMessage(ex));
			Debug.LogException(ex);
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00028582 File Offset: 0x00026782
		public virtual void LogException(Exception ex, string format, params object[] args)
		{
			this.LogError(string.Format(format, args) + ": " + ScriptConsole.DecorateExceptionMessage(ex));
			Debug.LogException(ex);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000285A7 File Offset: 0x000267A7
		public virtual void LogException(InterpreterException ex)
		{
			Debug.LogException(ex);
			this.LogError(ScriptConsole.DecorateExceptionMessage(ex));
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000285BB File Offset: 0x000267BB
		public virtual void LogException(InterpreterException ex, string format, params object[] args)
		{
			Debug.LogException(ex);
			this.LogError(string.Format(format, args) + ": " + ScriptConsole.DecorateExceptionMessage(ex));
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x0012D32C File Offset: 0x0012B52C
		private static string DecorateExceptionMessage(Exception ex)
		{
			InterpreterException ex2 = ex as InterpreterException;
			Exception innerException = ex.InnerException;
			string text = (ex2 != null) ? ex2.DecoratedMessage : ex.Message;
			bool flag = ex2 != null && innerException != null;
			if (flag)
			{
				ex = innerException;
			}
			if (flag && ex.TargetSite != null)
			{
				string name = ex.TargetSite.Name;
				string name2 = ex.TargetSite.DeclaringType.Name;
				string name3 = ex.GetType().Name;
				text = string.Concat(new string[]
				{
					text,
					" [originated as ",
					name3,
					" in ",
					name2,
					".",
					name,
					"]"
				});
			}
			return text;
		}

		// Token: 0x040030AE RID: 12462
		public static ScriptConsole instance;
	}
}
