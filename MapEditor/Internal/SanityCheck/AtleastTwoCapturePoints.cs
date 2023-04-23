using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000746 RID: 1862
	public class AtleastTwoCapturePoints : ValidationRule
	{
		// Token: 0x06002E81 RID: 11905 RVA: 0x0001FFD3 File Offset: 0x0001E1D3
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			if (MapEditor.instance.FindObjectsToSave<MeoCapturePoint>().Length >= 2)
			{
				return true;
			}
			result = new ValidationResult("The level must contain at-least two capture points", null);
			return false;
		}
	}
}
