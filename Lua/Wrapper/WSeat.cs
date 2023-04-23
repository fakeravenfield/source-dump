using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000991 RID: 2449
	[Wrapper(typeof(Seat))]
	public static class WSeat
	{
		// Token: 0x06003E33 RID: 15923 RVA: 0x0002A0A6 File Offset: 0x000282A6
		[Getter]
		public static Vehicle GetVehicle(Seat self)
		{
			return self.vehicle;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x0002A0AE File Offset: 0x000282AE
		[Getter]
		public static Actor GetOccupant(Seat self)
		{
			return self.occupant;
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x0002A0B6 File Offset: 0x000282B6
		[Getter]
		public static bool IsDriverSeat(Seat self)
		{
			return self.IsDriverSeat();
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x0002A0BE File Offset: 0x000282BE
		[Getter]
		public static bool IsOccupied(Seat self)
		{
			return self.IsOccupied();
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0002A0C6 File Offset: 0x000282C6
		[Getter]
		public static bool IsEnclosed(Seat self)
		{
			return self.enclosed;
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x0002A0CE File Offset: 0x000282CE
		[Getter]
		public static bool HasWeapons(Seat self)
		{
			return self.HasAnyMountedWeapons();
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x0002A0D6 File Offset: 0x000282D6
		[Getter]
		public static bool HasActiveWeapon(Seat self)
		{
			return self.HasActiveWeapon();
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x0002A0DE File Offset: 0x000282DE
		[Getter]
		public static MountedWeapon GetActiveWeapon(Seat self)
		{
			return self.activeWeapon;
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x0002A0E6 File Offset: 0x000282E6
		[Getter]
		public static MountedWeapon[] GetWeapons(Seat self)
		{
			return self.weapons;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x0002A0EE File Offset: 0x000282EE
		[Getter]
		public static Seat.Type GetCameraType(Seat self)
		{
			return self.type;
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x0002A0F6 File Offset: 0x000282F6
		[Setter]
		public static void SetCameraType(Seat self, Seat.Type type)
		{
			self.type = type;
			if (self.IsOccupied() && !self.occupant.aiControlled)
			{
				FpsActorController.instance.OnSeatCameraUpdated(self);
			}
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x0002A11F File Offset: 0x0002831F
		[Getter]
		public static Camera GetFirstPersonCamera(Seat self)
		{
			return self.camera;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0002A127 File Offset: 0x00028327
		[Getter]
		public static Camera GetThirdPersonCamera(Seat self)
		{
			return self.thirdPersonCamera;
		}
	}
}
