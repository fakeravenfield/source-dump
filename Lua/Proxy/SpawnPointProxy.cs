using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A03 RID: 2563
	[Proxy(typeof(SpawnPoint))]
	public class SpawnPointProxy : IProxy
	{
		// Token: 0x06004F3C RID: 20284 RVA: 0x000397B1 File Offset: 0x000379B1
		[MoonSharpHidden]
		public SpawnPointProxy(SpawnPoint value)
		{
			this._value = value;
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x000397C0 File Offset: 0x000379C0
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x000397D2 File Offset: 0x000379D2
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x000397E4 File Offset: 0x000379E4
		public CapturePointProxy capturePoint
		{
			get
			{
				return CapturePointProxy.New(WSpawnPoint.GetCapturePoint(this._value));
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x000397F6 File Offset: 0x000379F6
		public WTeam defaultOwner
		{
			get
			{
				return WSpawnPoint.GetDefaultOwner(this._value);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x00039803 File Offset: 0x00037A03
		public string name
		{
			get
			{
				return WSpawnPoint.GetName(this._value);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x00039810 File Offset: 0x00037A10
		public IList<SpawnPoint> neighours
		{
			get
			{
				return WSpawnPoint.GetNeighours(this._value);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x0003981D File Offset: 0x00037A1D
		public IList<SpawnPoint> neighoursIncoming
		{
			get
			{
				return WSpawnPoint.GetNeighoursIncoming(this._value);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x0003982A File Offset: 0x00037A2A
		public IList<SpawnPoint> neighoursOutgoing
		{
			get
			{
				return WSpawnPoint.GetNeighoursOutgoing(this._value);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x00039837 File Offset: 0x00037A37
		public WTeam owner
		{
			get
			{
				return WSpawnPoint.GetOwner(this._value);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x00039844 File Offset: 0x00037A44
		public Vector3Proxy spawnPosition
		{
			get
			{
				return Vector3Proxy.New(WSpawnPoint.GetSpawnPosition(this._value));
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x00039856 File Offset: 0x00037A56
		public IList<TurretSpawner> turretSpawners
		{
			get
			{
				return WSpawnPoint.GetTurretSpawners(this._value);
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x00039863 File Offset: 0x00037A63
		public IList<VehicleSpawner> vehicleSpawners
		{
			get
			{
				return WSpawnPoint.GetVehicleSpawners(this._value);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x00039870 File Offset: 0x00037A70
		public bool isCapturePoint
		{
			get
			{
				return WSpawnPoint.IsCapturePoint(this._value);
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0003987D File Offset: 0x00037A7D
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0013833C File Offset: 0x0013653C
		[MoonSharpHidden]
		public static SpawnPointProxy New(SpawnPoint value)
		{
			if (value == null)
			{
				return null;
			}
			SpawnPointProxy spawnPointProxy = (SpawnPointProxy)ObjectCache.Get(typeof(SpawnPointProxy), value);
			if (spawnPointProxy == null)
			{
				spawnPointProxy = new SpawnPointProxy(value);
				ObjectCache.Add(typeof(SpawnPointProxy), value, spawnPointProxy);
			}
			return spawnPointProxy;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x00039885 File Offset: 0x00037A85
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003292 RID: 12946
		[MoonSharpHidden]
		public SpawnPoint _value;
	}
}
