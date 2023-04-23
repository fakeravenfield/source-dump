using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000992 RID: 2450
	[Wrapper(typeof(SpawnPoint))]
	public static class WSpawnPoint
	{
		// Token: 0x06003E40 RID: 15936 RVA: 0x0002A12F File Offset: 0x0002832F
		[Getter]
		public static WTeam GetOwner(SpawnPoint self)
		{
			return (WTeam)self.owner;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0002A137 File Offset: 0x00028337
		[Getter]
		public static WTeam GetDefaultOwner(SpawnPoint self)
		{
			return (WTeam)self.defaultOwner;
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0002A13F File Offset: 0x0002833F
		[Getter]
		[Doc("Gets all neighbors connected to this point, ignoring one way connections.")]
		public static IList<SpawnPoint> GetNeighours(SpawnPoint self)
		{
			return self.allNeighbors;
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0002A147 File Offset: 0x00028347
		[Getter]
		[Doc("Gets all neighbors that can attack this point, respecting one way connections.")]
		public static IList<SpawnPoint> GetNeighoursIncoming(SpawnPoint self)
		{
			return self.incomingNeighbors;
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0002A14F File Offset: 0x0002834F
		[Getter]
		[Doc("Gets all neighbors that can be attacked from point, respecting one way connections.")]
		public static IList<SpawnPoint> GetNeighoursOutgoing(SpawnPoint self)
		{
			return self.outgoingNeighbors;
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0002A157 File Offset: 0x00028357
		[Getter]
		public static IList<TurretSpawner> GetTurretSpawners(SpawnPoint self)
		{
			return self.turretSpawners;
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0002A15F File Offset: 0x0002835F
		[Getter]
		public static IList<VehicleSpawner> GetVehicleSpawners(SpawnPoint self)
		{
			return self.vehicleSpawners.ToArray();
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x0002A16C File Offset: 0x0002836C
		[Getter]
		public static IList<LandingZone> GetLandingZones(SpawnPoint self)
		{
			return self.landingZones.ToArray();
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x0002A179 File Offset: 0x00028379
		[Getter]
		public static Vector3 GetSpawnPosition(SpawnPoint self)
		{
			return self.GetSpawnPosition();
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0002A181 File Offset: 0x00028381
		[Getter]
		[Doc("Returns true if this is a CapturePoint[..]")]
		public static bool IsCapturePoint(SpawnPoint self)
		{
			return self is CapturePoint;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0002A18C File Offset: 0x0002838C
		[Getter]
		[Doc("Casts this SpawnPoint into a CapturePoint; if possible[..]")]
		public static CapturePoint GetCapturePoint(SpawnPoint self)
		{
			if (self is CapturePoint)
			{
				return (CapturePoint)self;
			}
			throw new ScriptRuntimeException("not a CapturePoint");
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0002A1A7 File Offset: 0x000283A7
		[Getter]
		public static string GetName(SpawnPoint self)
		{
			return self.shortName;
		}
	}
}
