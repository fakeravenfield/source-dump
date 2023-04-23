using System;
using System.Collections.Generic;
using System.Linq;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000749 RID: 1865
	public class AllNeighboursAssigned : ValidationRule
	{
		// Token: 0x06002E87 RID: 11911 RVA: 0x00108FC0 File Offset: 0x001071C0
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			List<MeoCapturePoint> list = MapEditor.instance.FindObjectsToSave<MeoCapturePoint>().ToList<MeoCapturePoint>();
			foreach (MeoCapturePoint.Neighbour neighbour in list.SelectMany((MeoCapturePoint cp) => cp.GetNeighbours()).ToArray<MeoCapturePoint.Neighbour>())
			{
				list.Remove(neighbour.capturePointA);
				list.Remove(neighbour.capturePointB);
			}
			if (!list.Any<MeoCapturePoint>())
			{
				return true;
			}
			result = new ValidationResult("Some neighbours are unassigned", delegate()
			{
				MapEditor.instance.GetEditorUI().ShowOnlyLevelDetails<NeighboursUI>();
			});
			return false;
		}
	}
}
