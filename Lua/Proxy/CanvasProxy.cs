using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009BA RID: 2490
	[Proxy(typeof(Canvas))]
	public class CanvasProxy : IProxy
	{
		// Token: 0x06004310 RID: 17168 RVA: 0x0002E32E File Offset: 0x0002C52E
		[MoonSharpHidden]
		public CanvasProxy(Canvas value)
		{
			this._value = value;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x0002E33D File Offset: 0x0002C53D
		public CanvasProxy()
		{
			this._value = new Canvas();
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x0002E350 File Offset: 0x0002C550
		public int cachedSortingLayerValue
		{
			get
			{
				return this._value.cachedSortingLayerValue;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x0002E35D File Offset: 0x0002C55D
		public bool isRootCanvas
		{
			get
			{
				return this._value.isRootCanvas;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x0002E36A File Offset: 0x0002C56A
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x0002E377 File Offset: 0x0002C577
		public float normalizedSortingGridSize
		{
			get
			{
				return this._value.normalizedSortingGridSize;
			}
			set
			{
				this._value.normalizedSortingGridSize = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x0002E385 File Offset: 0x0002C585
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x0002E392 File Offset: 0x0002C592
		public bool overridePixelPerfect
		{
			get
			{
				return this._value.overridePixelPerfect;
			}
			set
			{
				this._value.overridePixelPerfect = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x0002E3AD File Offset: 0x0002C5AD
		public bool overrideSorting
		{
			get
			{
				return this._value.overrideSorting;
			}
			set
			{
				this._value.overrideSorting = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x0002E3BB File Offset: 0x0002C5BB
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		public bool pixelPerfect
		{
			get
			{
				return this._value.pixelPerfect;
			}
			set
			{
				this._value.pixelPerfect = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x0002E3D6 File Offset: 0x0002C5D6
		public RectProxy pixelRect
		{
			get
			{
				return RectProxy.New(this._value.pixelRect);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		// (set) Token: 0x0600431E RID: 17182 RVA: 0x0002E3F5 File Offset: 0x0002C5F5
		public float planeDistance
		{
			get
			{
				return this._value.planeDistance;
			}
			set
			{
				this._value.planeDistance = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0002E403 File Offset: 0x0002C603
		// (set) Token: 0x06004320 RID: 17184 RVA: 0x0002E410 File Offset: 0x0002C610
		public float referencePixelsPerUnit
		{
			get
			{
				return this._value.referencePixelsPerUnit;
			}
			set
			{
				this._value.referencePixelsPerUnit = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x0002E41E File Offset: 0x0002C61E
		public Vector2Proxy renderingDisplaySize
		{
			get
			{
				return Vector2Proxy.New(this._value.renderingDisplaySize);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06004322 RID: 17186 RVA: 0x0002E430 File Offset: 0x0002C630
		public int renderOrder
		{
			get
			{
				return this._value.renderOrder;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x0002E43D File Offset: 0x0002C63D
		public CanvasProxy rootCanvas
		{
			get
			{
				return CanvasProxy.New(this._value.rootCanvas);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x0002E44F File Offset: 0x0002C64F
		// (set) Token: 0x06004325 RID: 17189 RVA: 0x0002E45C File Offset: 0x0002C65C
		public float scaleFactor
		{
			get
			{
				return this._value.scaleFactor;
			}
			set
			{
				this._value.scaleFactor = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x0002E46A File Offset: 0x0002C66A
		// (set) Token: 0x06004327 RID: 17191 RVA: 0x0002E477 File Offset: 0x0002C677
		public int sortingLayerID
		{
			get
			{
				return this._value.sortingLayerID;
			}
			set
			{
				this._value.sortingLayerID = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x0002E485 File Offset: 0x0002C685
		// (set) Token: 0x06004329 RID: 17193 RVA: 0x0002E492 File Offset: 0x0002C692
		public string sortingLayerName
		{
			get
			{
				return this._value.sortingLayerName;
			}
			set
			{
				this._value.sortingLayerName = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
		// (set) Token: 0x0600432B RID: 17195 RVA: 0x0002E4AD File Offset: 0x0002C6AD
		public int sortingOrder
		{
			get
			{
				return this._value.sortingOrder;
			}
			set
			{
				this._value.sortingOrder = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x0002E4BB File Offset: 0x0002C6BB
		// (set) Token: 0x0600432D RID: 17197 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		public int targetDisplay
		{
			get
			{
				return this._value.targetDisplay;
			}
			set
			{
				this._value.targetDisplay = value;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x0002E4D6 File Offset: 0x0002C6D6
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x00130208 File Offset: 0x0012E408
		public CameraProxy worldCamera
		{
			get
			{
				return CameraProxy.New(this._value.worldCamera);
			}
			set
			{
				Camera worldCamera = null;
				if (value != null)
				{
					worldCamera = value._value;
				}
				this._value.worldCamera = worldCamera;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x0002E4E8 File Offset: 0x0002C6E8
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x0002E4F5 File Offset: 0x0002C6F5
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

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x0002E503 File Offset: 0x0002C703
		public bool isActiveAndEnabled
		{
			get
			{
				return this._value.isActiveAndEnabled;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x0002E510 File Offset: 0x0002C710
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x0002E522 File Offset: 0x0002C722
		// (set) Token: 0x06004335 RID: 17205 RVA: 0x0002E52F File Offset: 0x0002C72F
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x0002E53D File Offset: 0x0002C73D
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x0002E54F File Offset: 0x0002C74F
		// (set) Token: 0x06004338 RID: 17208 RVA: 0x0002E55C File Offset: 0x0002C75C
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

		// Token: 0x06004339 RID: 17209 RVA: 0x0002E56A File Offset: 0x0002C76A
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x00130230 File Offset: 0x0012E430
		[MoonSharpHidden]
		public static CanvasProxy New(Canvas value)
		{
			if (value == null)
			{
				return null;
			}
			CanvasProxy canvasProxy = (CanvasProxy)ObjectCache.Get(typeof(CanvasProxy), value);
			if (canvasProxy == null)
			{
				canvasProxy = new CanvasProxy(value);
				ObjectCache.Add(typeof(CanvasProxy), value, canvasProxy);
			}
			return canvasProxy;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0002E572 File Offset: 0x0002C772
		[MoonSharpUserDataMetamethod("__call")]
		public static CanvasProxy Call(DynValue _)
		{
			return new CanvasProxy();
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0002E579 File Offset: 0x0002C779
		public static void ForceUpdateCanvases()
		{
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0002E580 File Offset: 0x0002C780
		public static MaterialProxy GetDefaultCanvasMaterial()
		{
			return MaterialProxy.New(Canvas.GetDefaultCanvasMaterial());
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0002E58C File Offset: 0x0002C78C
		public static MaterialProxy GetETC1SupportedCanvasMaterial()
		{
			return MaterialProxy.New(Canvas.GetETC1SupportedCanvasMaterial());
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0002E598 File Offset: 0x0002C798
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0002E5A6 File Offset: 0x0002C7A6
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003153 RID: 12627
		[MoonSharpHidden]
		public Canvas _value;
	}
}
