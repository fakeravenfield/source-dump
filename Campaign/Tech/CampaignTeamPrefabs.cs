using System;
using UnityEngine;

namespace Campaign.Tech
{
	// Token: 0x02000410 RID: 1040
	[CreateAssetMenu(fileName = "New Team Prefab List", menuName = "Campaign/Team Prefabs")]
	public class CampaignTeamPrefabs : ScriptableObject
	{
		// Token: 0x04001BC2 RID: 7106
		public CampaignTeamPrefabs.WeaponGroup[] weaponGroups;

		// Token: 0x04001BC3 RID: 7107
		public CampaignTeamPrefabs.VehicleGroup[] vehicleGroups;

		// Token: 0x04001BC4 RID: 7108
		public CampaignTeamPrefabs.TurretGroup[] turretGroups;

		// Token: 0x02000411 RID: 1041
		[Serializable]
		public struct WeaponGroup
		{
			// Token: 0x04001BC5 RID: 7109
			public string techId;

			// Token: 0x04001BC6 RID: 7110
			public string[] weaponEntryNames;
		}

		// Token: 0x02000412 RID: 1042
		[Serializable]
		public struct VehicleGroup
		{
			// Token: 0x04001BC7 RID: 7111
			public string techId;

			// Token: 0x04001BC8 RID: 7112
			public CampaignTeamPrefabs.VehicleGroup.VehicleEntry[] entries;

			// Token: 0x02000413 RID: 1043
			[Serializable]
			public struct VehicleEntry
			{
				// Token: 0x04001BC9 RID: 7113
				public string vehicleName;

				// Token: 0x04001BCA RID: 7114
				public VehicleSpawner.VehicleSpawnType slot;

				// Token: 0x04001BCB RID: 7115
				public int priority;
			}
		}

		// Token: 0x02000414 RID: 1044
		[Serializable]
		public struct TurretGroup
		{
			// Token: 0x04001BCC RID: 7116
			public string techId;

			// Token: 0x04001BCD RID: 7117
			public CampaignTeamPrefabs.TurretGroup.TurretEntry[] entries;

			// Token: 0x02000415 RID: 1045
			[Serializable]
			public struct TurretEntry
			{
				// Token: 0x04001BCE RID: 7118
				public string turretName;

				// Token: 0x04001BCF RID: 7119
				public TurretSpawner.TurretSpawnType slot;

				// Token: 0x04001BD0 RID: 7120
				public int priority;
			}
		}
	}
}
