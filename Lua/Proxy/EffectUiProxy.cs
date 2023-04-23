using System;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009C2 RID: 2498
	[Proxy(typeof(EffectUi))]
	public class EffectUiProxy : IProxy
	{
		// Token: 0x06004449 RID: 17481 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x0002F64D File Offset: 0x0002D84D
		public static void ChangeFadeColor(ColorProxy color, float changeTime)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			EffectUi.ChangeFadeColor(color._value, changeTime);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x0002F669 File Offset: 0x0002D869
		public static void Clear()
		{
			EffectUi.Clear();
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x0002F670 File Offset: 0x0002D870
		public static void FadeIn(EffectUi.FadeType type, float duration, ColorProxy color)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			EffectUi.FadeIn(type, duration, color._value);
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x0002F68D File Offset: 0x0002D88D
		public static void FadeOut(EffectUi.FadeType type, float duration, ColorProxy color)
		{
			if (color == null)
			{
				throw new ScriptRuntimeException("argument 'color' is nil");
			}
			EffectUi.FadeOut(type, duration, color._value);
		}
	}
}
