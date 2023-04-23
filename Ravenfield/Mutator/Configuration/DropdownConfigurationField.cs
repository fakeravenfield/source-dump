using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x0200038A RID: 906
	[Serializable]
	public class DropdownConfigurationField : MutatorConfigurationFieldGeneric<DropdownConfigurationField.FieldData>
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x00011E9A File Offset: 0x0001009A
		public override string SerializeValue()
		{
			return this.value.index.ToString();
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00011EAC File Offset: 0x000100AC
		public override void DeserializeValue(string serializedValue)
		{
			int.TryParse(serializedValue, out this.value.index);
		}

		// Token: 0x0200038B RID: 907
		[Serializable]
		public class FieldData
		{
			// Token: 0x04001919 RID: 6425
			public int index;

			// Token: 0x0400191A RID: 6426
			public string[] labels;
		}
	}
}
