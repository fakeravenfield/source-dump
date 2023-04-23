using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000747 RID: 1863
	public class RedCapturePoint : ValidationRule
	{
		// Token: 0x06002E83 RID: 11907 RVA: 0x00108F20 File Offset: 0x00107120
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			MeoCapturePoint[] array = MapEditor.instance.FindObjectsToSave<MeoCapturePoint>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].owner == SpawnPoint.Team.Red)
				{
					return true;
				}
			}
			result = new ValidationResult("A capture point must belong to the red team", null);
			return false;
		}
	}
}
