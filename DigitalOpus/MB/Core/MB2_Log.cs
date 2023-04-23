using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000441 RID: 1089
	public class MB2_Log
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x000B03B4 File Offset: 0x000AE5B4
		public static void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
		{
			if (l <= currentThreshold)
			{
				if (l == MB2_LogLevel.error)
				{
					Debug.LogError(msg);
				}
				if (l == MB2_LogLevel.warn)
				{
					Debug.LogWarning(string.Format("frm={0} WARN {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.info)
				{
					Debug.Log(string.Format("frm={0} INFO {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.debug)
				{
					Debug.Log(string.Format("frm={0} DEBUG {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("frm={0} TRACE {1}", Time.frameCount, msg));
				}
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000B044C File Offset: 0x000AE64C
		public static string Error(string msg, params object[] args)
		{
			string arg = string.Format(msg, args);
			string text = string.Format("f={0} ERROR {1}", Time.frameCount, arg);
			Debug.LogError(text);
			return text;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000B047C File Offset: 0x000AE67C
		public static string Warn(string msg, params object[] args)
		{
			string arg = string.Format(msg, args);
			string text = string.Format("f={0} WARN {1}", Time.frameCount, arg);
			Debug.LogWarning(text);
			return text;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000B04AC File Offset: 0x000AE6AC
		public static string Info(string msg, params object[] args)
		{
			string arg = string.Format(msg, args);
			string text = string.Format("f={0} INFO {1}", Time.frameCount, arg);
			Debug.Log(text);
			return text;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000B04DC File Offset: 0x000AE6DC
		public static string LogDebug(string msg, params object[] args)
		{
			string arg = string.Format(msg, args);
			string text = string.Format("f={0} DEBUG {1}", Time.frameCount, arg);
			Debug.Log(text);
			return text;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000B050C File Offset: 0x000AE70C
		public static string Trace(string msg, params object[] args)
		{
			string arg = string.Format(msg, args);
			string text = string.Format("f={0} TRACE {1}", Time.frameCount, arg);
			Debug.Log(text);
			return text;
		}
	}
}
