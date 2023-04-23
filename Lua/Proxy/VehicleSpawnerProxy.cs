using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A13 RID: 2579
	[Proxy(typeof(VehicleSpawner))]
	public class VehicleSpawnerProxy : IProxy
	{
		// Token: 0x06005192 RID: 20882 RVA: 0x0003C283 File Offset: 0x0003A483
		[MoonSharpHidden]
		public VehicleSpawnerProxy(VehicleSpawner value)
		{
			this._value = value;
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06005193 RID: 20883 RVA: 0x0003C292 File Offset: 0x0003A492
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06005195 RID: 20885 RVA: 0x0003C2B6 File Offset: 0x0003A4B6
		public VehicleProxy lastSpawnedVehicle
		{
			get
			{
				return VehicleProxy.New(WVehicleSpawner.GetLastSpawnedVehicle(this._value));
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		public bool lastSpawnedVehicleHasBeenUsed
		{
			get
			{
				return WVehicleSpawner.GetLastSpawnedVehicleHasBeenUsed(this._value);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x0003C2D5 File Offset: 0x0003A4D5
		// (set) Token: 0x06005198 RID: 20888 RVA: 0x00138D30 File Offset: 0x00136F30
		public SpawnPointProxy parentSpawnPoint
		{
			get
			{
				return SpawnPointProxy.New(WVehicleSpawner.GetParentSpawnPoint(this._value));
			}
			set
			{
				SpawnPoint spawn = null;
				if (value != null)
				{
					spawn = value._value;
				}
				WVehicleSpawner.SetParentSpawnPoint(this._value, spawn);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x0003C2E7 File Offset: 0x0003A4E7
		public VehicleSpawner.VehicleSpawnType spawnType
		{
			get
			{
				return WVehicleSpawner.GetSpawnType(this._value);
			}
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0003C2F4 File Offset: 0x0003A4F4
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00138D58 File Offset: 0x00136F58
		[MoonSharpHidden]
		public static VehicleSpawnerProxy New(VehicleSpawner value)
		{
			if (value == null)
			{
				return null;
			}
			VehicleSpawnerProxy vehicleSpawnerProxy = (VehicleSpawnerProxy)ObjectCache.Get(typeof(VehicleSpawnerProxy), value);
			if (vehicleSpawnerProxy == null)
			{
				vehicleSpawnerProxy = new VehicleSpawnerProxy(value);
				ObjectCache.Add(typeof(VehicleSpawnerProxy), value, vehicleSpawnerProxy);
			}
			return vehicleSpawnerProxy;
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0003C2FC File Offset: 0x0003A4FC
		public GameObjectProxy GetPrefab()
		{
			return GameObjectProxy.New(WVehicleSpawner.GetPrefab(this._value));
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0003C30E File Offset: 0x0003A50E
		public static GameObjectProxy GetPrefab(WTeam team, VehicleSpawner.VehicleSpawnType type)
		{
			return GameObjectProxy.New(WVehicleSpawner.GetPrefab(team, type));
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0003C31C File Offset: 0x0003A51C
		public VehicleProxy GetPrefabVehicle()
		{
			return VehicleProxy.New(WVehicleSpawner.GetPrefabVehicle(this._value));
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0003C32E File Offset: 0x0003A52E
		public bool SpawnIsBlocked()
		{
			return WVehicleSpawner.SpawnIsBlocked(this._value);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0003C33B File Offset: 0x0003A53B
		public VehicleProxy SpawnNow()
		{
			return VehicleProxy.New(WVehicleSpawner.SpawnNow(this._value));
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0003C34D File Offset: 0x0003A54D
		public static VehicleProxy SpawnVehicle(WTeam team, VehicleSpawner.VehicleSpawnType type, Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			return VehicleProxy.New(WVehicleSpawner.SpawnVehicle(team, type, position._value, rotation._value));
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0003C383 File Offset: 0x0003A583
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A2 RID: 12962
		[MoonSharpHidden]
		public VehicleSpawner _value;
	}
}
