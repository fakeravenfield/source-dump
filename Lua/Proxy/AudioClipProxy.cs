using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B2 RID: 2482
	[Proxy(typeof(AudioClip))]
	public class AudioClipProxy : IProxy
	{
		// Token: 0x06004188 RID: 16776 RVA: 0x0002CB13 File Offset: 0x0002AD13
		[MoonSharpHidden]
		public AudioClipProxy(AudioClip value)
		{
			this._value = value;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x0002CB22 File Offset: 0x0002AD22
		public float length
		{
			get
			{
				return WAudioClip.GetLength(this._value);
			}
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x0002CB2F File Offset: 0x0002AD2F
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0012FE40 File Offset: 0x0012E040
		[MoonSharpHidden]
		public static AudioClipProxy New(AudioClip value)
		{
			if (value == null)
			{
				return null;
			}
			AudioClipProxy audioClipProxy = (AudioClipProxy)ObjectCache.Get(typeof(AudioClipProxy), value);
			if (audioClipProxy == null)
			{
				audioClipProxy = new AudioClipProxy(value);
				ObjectCache.Add(typeof(AudioClipProxy), value, audioClipProxy);
			}
			return audioClipProxy;
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0002CB37 File Offset: 0x0002AD37
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400314B RID: 12619
		[MoonSharpHidden]
		public AudioClip _value;
	}
}
