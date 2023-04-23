using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A0E RID: 2574
	[Proxy(typeof(TurretSpawner))]
	public class TurretSpawnerProxy : IProxy
	{
		// Token: 0x060050A6 RID: 20646 RVA: 0x0003AD50 File Offset: 0x00038F50
		[MoonSharpHidden]
		public TurretSpawnerProxy(TurretSpawner value)
		{
			this._value = value;
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x0003AD5F File Offset: 0x00038F5F
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x0003AD71 File Offset: 0x00038F71
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060050A9 RID: 20649 RVA: 0x0003AD83 File Offset: 0x00038F83
		public VehicleProxy activeTurret
		{
			get
			{
				return VehicleProxy.New(WTurretSpawner.GetActiveTurret(this._value));
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x0003AD95 File Offset: 0x00038F95
		// (set) Token: 0x060050AB RID: 20651 RVA: 0x0013898C File Offset: 0x00136B8C
		public SpawnPointProxy parentSpawnPoint
		{
			get
			{
				return SpawnPointProxy.New(WTurretSpawner.GetParentSpawnPoint(this._value));
			}
			set
			{
				SpawnPoint spawn = null;
				if (value != null)
				{
					spawn = value._value;
				}
				WTurretSpawner.SetParentSpawnPoint(this._value, spawn);
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x0003ADA7 File Offset: 0x00038FA7
		public TurretSpawner.TurretSpawnType spawnType
		{
			get
			{
				return WTurretSpawner.GetSpawnType(this._value);
			}
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x0003ADB4 File Offset: 0x00038FB4
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x001389B4 File Offset: 0x00136BB4
		[MoonSharpHidden]
		public static TurretSpawnerProxy New(TurretSpawner value)
		{
			if (value == null)
			{
				return null;
			}
			TurretSpawnerProxy turretSpawnerProxy = (TurretSpawnerProxy)ObjectCache.Get(typeof(TurretSpawnerProxy), value);
			if (turretSpawnerProxy == null)
			{
				turretSpawnerProxy = new TurretSpawnerProxy(value);
				ObjectCache.Add(typeof(TurretSpawnerProxy), value, turretSpawnerProxy);
			}
			return turretSpawnerProxy;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0003ADBC File Offset: 0x00038FBC
		public static GameObjectProxy GetPrefab(WTeam team, TurretSpawner.TurretSpawnType type)
		{
			return GameObjectProxy.New(WTurretSpawner.GetPrefab(team, type));
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0003ADCA File Offset: 0x00038FCA
		public static VehicleProxy GetPrefabVehicle(WTeam team, TurretSpawner.TurretSpawnType type)
		{
			return VehicleProxy.New(WTurretSpawner.GetPrefabVehicle(team, type));
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0003ADD8 File Offset: 0x00038FD8
		public static VehicleProxy SpawnTurret(WTeam team, TurretSpawner.TurretSpawnType type, Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			return VehicleProxy.New(WTurretSpawner.SpawnTurret(team, type, position._value, rotation._value));
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0003AE0E File Offset: 0x0003900E
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400329D RID: 12957
		[MoonSharpHidden]
		public TurretSpawner _value;
	}
}
