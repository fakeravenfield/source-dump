using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000744 RID: 1860
	public class HasPhoto : ValidationRule
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x00108ECC File Offset: 0x001070CC
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			if (PhotoTool.DescriptorHasIconFile())
			{
				return true;
			}
			result = new ValidationResult("A photo is required (snap one with the Photo tool)", delegate()
			{
				MapEditor.instance.GetEditorUI().toolsMenu.SwitchToPhotoMode(true);
			});
			return false;
		}
	}
}
