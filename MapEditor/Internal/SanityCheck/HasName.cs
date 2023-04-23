using System;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000742 RID: 1858
	public class HasName : ValidationRule
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x00108E78 File Offset: 0x00107078
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			if (MapEditor.HasDescriptorFilePath())
			{
				return true;
			}
			result = new ValidationResult("A name is required (save level to disk)", delegate()
			{
				MapEditor.instance.GetEditorUI().ShowOnlySaveDialog();
			});
			return false;
		}
	}
}
