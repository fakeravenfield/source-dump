using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A19 RID: 2585
	[Proxy(typeof(WDebug))]
	public class WDebugProxy : IProxy
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06005238 RID: 21048 RVA: 0x0003CA65 File Offset: 0x0003AC65
		public static bool isTestMode
		{
			get
			{
				return WDebug.isTestMode;
			}
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00138FB4 File Offset: 0x001371B4
		public static void DrawLine(Vector3Proxy from, Vector3Proxy to, ColorProxy color)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawLine(from._value, to._value, color._value);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00139004 File Offset: 0x00137204
		public static void DrawLine(Vector3Proxy from, Vector3Proxy to, ColorProxy color, float duration)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawLine(from._value, to._value, color._value, duration);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00139054 File Offset: 0x00137254
		public static void DrawLine(Vector3Proxy from, Vector3Proxy to, ColorProxy color, float duration, Matrix4x4Proxy localToWorldMatrix)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (to == null)
			{
				throw new ScriptRuntimeException("argument 'to' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			if (localToWorldMatrix == null)
			{
				throw new ScriptRuntimeException("argument 'localToWorldMatrix' is nil");
			}
			WDebug.DrawLine(from._value, to._value, color._value, duration, localToWorldMatrix._value);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0003CA6C File Offset: 0x0003AC6C
		public static void DrawPath(Vector3[] vertices, ColorProxy color)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawPath(vertices, color._value);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0003CA88 File Offset: 0x0003AC88
		public static void DrawPath(Vector3[] vertices, ColorProxy color, float duration)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawPath(vertices, color._value, duration);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0003CAA5 File Offset: 0x0003ACA5
		public static void DrawPath(Vector3[] vertices, ColorProxy color, float duration, Matrix4x4Proxy localToWorldMatrix)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			if (localToWorldMatrix == null)
			{
				throw new ScriptRuntimeException("argument 'localToWorldMatrix' is nil");
			}
			WDebug.DrawPath(vertices, color._value, duration, localToWorldMatrix._value);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x001390BC File Offset: 0x001372BC
		public static void DrawRay(Vector3Proxy from, Vector3Proxy direction, ColorProxy color)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawRay(from._value, direction._value, color._value);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0013910C File Offset: 0x0013730C
		public static void DrawRay(Vector3Proxy from, Vector3Proxy direction, ColorProxy color, float duration)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			WDebug.DrawRay(from._value, direction._value, color._value, duration);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0013915C File Offset: 0x0013735C
		public static void DrawRay(Vector3Proxy from, Vector3Proxy direction, ColorProxy color, float duration, Matrix4x4Proxy localToWorldMatrix)
		{
			if (from == null)
			{
				throw new ScriptRuntimeException("argument 'from' is nil");
			}
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			if (localToWorldMatrix == null)
			{
				throw new ScriptRuntimeException("argument 'localToWorldMatrix' is nil");
			}
			WDebug.DrawRay(from._value, direction._value, color._value, duration, localToWorldMatrix._value);
		}
	}
}
