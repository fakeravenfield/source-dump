using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009BC RID: 2492
	[Proxy(typeof(CapturePoint))]
	public class CapturePointProxy : IProxy
	{
		// Token: 0x06004363 RID: 17251 RVA: 0x0002E7EE File Offset: 0x0002C9EE
		[MoonSharpHidden]
		public CapturePointProxy(CapturePoint value)
		{
			this._value = value;
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x0002E7FD File Offset: 0x0002C9FD
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x0002E80F File Offset: 0x0002CA0F
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x0002E821 File Offset: 0x0002CA21
		public CapturePointProxy capturePoint
		{
			get
			{
				return CapturePointProxy.New(WSpawnPoint.GetCapturePoint(this._value));
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x0002E833 File Offset: 0x0002CA33
		public WTeam defaultOwner
		{
			get
			{
				return WSpawnPoint.GetDefaultOwner(this._value);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x0002E840 File Offset: 0x0002CA40
		public string name
		{
			get
			{
				return WSpawnPoint.GetName(this._value);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x0002E84D File Offset: 0x0002CA4D
		public IList<SpawnPoint> neighours
		{
			get
			{
				return WSpawnPoint.GetNeighours(this._value);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x0002E85A File Offset: 0x0002CA5A
		public IList<SpawnPoint> neighoursIncoming
		{
			get
			{
				return WSpawnPoint.GetNeighoursIncoming(this._value);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x0002E867 File Offset: 0x0002CA67
		public IList<SpawnPoint> neighoursOutgoing
		{
			get
			{
				return WSpawnPoint.GetNeighoursOutgoing(this._value);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x0002E874 File Offset: 0x0002CA74
		public WTeam owner
		{
			get
			{
				return WSpawnPoint.GetOwner(this._value);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x0002E881 File Offset: 0x0002CA81
		public Vector3Proxy spawnPosition
		{
			get
			{
				return Vector3Proxy.New(WSpawnPoint.GetSpawnPosition(this._value));
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x0002E893 File Offset: 0x0002CA93
		public IList<TurretSpawner> turretSpawners
		{
			get
			{
				return WSpawnPoint.GetTurretSpawners(this._value);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		public IList<VehicleSpawner> vehicleSpawners
		{
			get
			{
				return WSpawnPoint.GetVehicleSpawners(this._value);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x0002E8AD File Offset: 0x0002CAAD
		public bool isCapturePoint
		{
			get
			{
				return WSpawnPoint.IsCapturePoint(this._value);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x0002E8BA File Offset: 0x0002CABA
		// (set) Token: 0x06004372 RID: 17266 RVA: 0x0002E8C7 File Offset: 0x0002CAC7
		public float captureCeiling
		{
			get
			{
				return WCapturePoint.GetCaptureCeiling(this._value);
			}
			set
			{
				WCapturePoint.SetCaptureCeiling(this._value, value);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x0002E8D5 File Offset: 0x0002CAD5
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x0002E8E2 File Offset: 0x0002CAE2
		public float captureFloor
		{
			get
			{
				return WCapturePoint.GetCaptureFloor(this._value);
			}
			set
			{
				WCapturePoint.SetCaptureFloor(this._value, value);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		public float captureProgress
		{
			get
			{
				return WCapturePoint.GetCaptureProgress(this._value);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x0002E8FD File Offset: 0x0002CAFD
		// (set) Token: 0x06004377 RID: 17271 RVA: 0x0002E90A File Offset: 0x0002CB0A
		public float captureRange
		{
			get
			{
				return WCapturePoint.GetCaptureRange(this._value);
			}
			set
			{
				WCapturePoint.SetCaptureRange(this._value, value);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x0002E918 File Offset: 0x0002CB18
		// (set) Token: 0x06004379 RID: 17273 RVA: 0x0002E925 File Offset: 0x0002CB25
		public float captureRate
		{
			get
			{
				return WCapturePoint.GetCaptureRate(this._value);
			}
			set
			{
				WCapturePoint.SetCaptureRate(this._value, value);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x0002E933 File Offset: 0x0002CB33
		public RendererProxy flagRenderer
		{
			get
			{
				return RendererProxy.New(WCapturePoint.GetFlagRenderer(this._value));
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x0002E945 File Offset: 0x0002CB45
		public bool isContested
		{
			get
			{
				return WCapturePoint.GetIsContested(this._value);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x0002E952 File Offset: 0x0002CB52
		public WTeam pendingOwner
		{
			get
			{
				return WCapturePoint.GetPendingOwner(this._value);
			}
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x0002E95F File Offset: 0x0002CB5F
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x001302C8 File Offset: 0x0012E4C8
		[MoonSharpHidden]
		public static CapturePointProxy New(CapturePoint value)
		{
			if (value == null)
			{
				return null;
			}
			CapturePointProxy capturePointProxy = (CapturePointProxy)ObjectCache.Get(typeof(CapturePointProxy), value);
			if (capturePointProxy == null)
			{
				capturePointProxy = new CapturePointProxy(value);
				ObjectCache.Add(typeof(CapturePointProxy), value, capturePointProxy);
			}
			return capturePointProxy;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x0002E967 File Offset: 0x0002CB67
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003155 RID: 12629
		[MoonSharpHidden]
		public CapturePoint _value;
	}
}
