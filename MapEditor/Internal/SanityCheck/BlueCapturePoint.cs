using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000748 RID: 1864
	public class BlueCapturePoint : ValidationRule
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x00108F70 File Offset: 0x00107170
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			MeoCapturePoint[] array = MapEditor.instance.FindObjectsToSave<MeoCapturePoint>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].owner == SpawnPoint.Team.Blue)
				{
					return true;
				}
			}
			result = new ValidationResult("A capture point must belong to the blue team", null);
			return false;
		}
	}
}
