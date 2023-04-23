using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009B9 RID: 2489
	[Proxy(typeof(CanvasGroup))]
	public class CanvasGroupProxy : IProxy
	{
		// Token: 0x060042F6 RID: 17142 RVA: 0x0002E1E7 File Offset: 0x0002C3E7
		[MoonSharpHidden]
		public CanvasGroupProxy(CanvasGroup value)
		{
			this._value = value;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x0002E1F6 File Offset: 0x0002C3F6
		public CanvasGroupProxy()
		{
			this._value = new CanvasGroup();
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x0002E209 File Offset: 0x0002C409
		// (set) Token: 0x060042F9 RID: 17145 RVA: 0x0002E216 File Offset: 0x0002C416
		public float alpha
		{
			get
			{
				return this._value.alpha;
			}
			set
			{
				this._value.alpha = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x0002E224 File Offset: 0x0002C424
		// (set) Token: 0x060042FB RID: 17147 RVA: 0x0002E231 File Offset: 0x0002C431
		public bool blocksRaycasts
		{
			get
			{
				return this._value.blocksRaycasts;
			}
			set
			{
				this._value.blocksRaycasts = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x0002E23F File Offset: 0x0002C43F
		// (set) Token: 0x060042FD RID: 17149 RVA: 0x0002E24C File Offset: 0x0002C44C
		public bool ignoreParentGroups
		{
			get
			{
				return this._value.ignoreParentGroups;
			}
			set
			{
				this._value.ignoreParentGroups = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x0002E25A File Offset: 0x0002C45A
		// (set) Token: 0x060042FF RID: 17151 RVA: 0x0002E267 File Offset: 0x0002C467
		public bool interactable
		{
			get
			{
				return this._value.interactable;
			}
			set
			{
				this._value.interactable = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x0002E275 File Offset: 0x0002C475
		// (set) Token: 0x06004301 RID: 17153 RVA: 0x0002E282 File Offset: 0x0002C482
		public bool enabled
		{
			get
			{
				return this._value.enabled;
			}
			set
			{
				this._value.enabled = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x0002E290 File Offset: 0x0002C490
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x0002E29D File Offset: 0x0002C49D
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x0002E2AF File Offset: 0x0002C4AF
		// (set) Token: 0x06004305 RID: 17157 RVA: 0x0002E2BC File Offset: 0x0002C4BC
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x0002E2CA File Offset: 0x0002C4CA
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x0002E2E9 File Offset: 0x0002C4E9
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0002E2F7 File Offset: 0x0002C4F7
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x00130180 File Offset: 0x0012E380
		[MoonSharpHidden]
		public static CanvasGroupProxy New(CanvasGroup value)
		{
			if (value == null)
			{
				return null;
			}
			CanvasGroupProxy canvasGroupProxy = (CanvasGroupProxy)ObjectCache.Get(typeof(CanvasGroupProxy), value);
			if (canvasGroupProxy == null)
			{
				canvasGroupProxy = new CanvasGroupProxy(value);
				ObjectCache.Add(typeof(CanvasGroupProxy), value, canvasGroupProxy);
			}
			return canvasGroupProxy;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x0002E2FF File Offset: 0x0002C4FF
		[MoonSharpUserDataMetamethod("__call")]
		public static CanvasGroupProxy Call(DynValue _)
		{
			return new CanvasGroupProxy();
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x001301CC File Offset: 0x0012E3CC
		public bool IsRaycastLocationValid(Vector2Proxy sp, CameraProxy eventCamera)
		{
			if (sp == null)
			{
				throw new ScriptRuntimeException("argument 'sp' is nil");
			}
			Camera eventCamera2 = null;
			if (eventCamera != null)
			{
				eventCamera2 = eventCamera._value;
			}
			return this._value.IsRaycastLocationValid(sp._value, eventCamera2);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0002E306 File Offset: 0x0002C506
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0002E314 File Offset: 0x0002C514
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x0002E321 File Offset: 0x0002C521
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003152 RID: 12626
		[MoonSharpHidden]
		public CanvasGroup _value;
	}
}
