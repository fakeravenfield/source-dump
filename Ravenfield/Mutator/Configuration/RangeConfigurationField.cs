using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000386 RID: 902
	[Serializable]
	public class RangeConfigurationField : MutatorConfigurationFieldGeneric<RangeConfigurationField.FieldData>
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x00011E44 File Offset: 0x00010044
		public override string SerializeValue()
		{
			return this.value.value.ToString();
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00011E56 File Offset: 0x00010056
		public override void DeserializeValue(string serializedValue)
		{
			float.TryParse(serializedValue, out this.value.value);
		}

		// Token: 0x02000387 RID: 903
		[Serializable]
		public class FieldData
		{
			// Token: 0x04001915 RID: 6421
			public float value;

			// Token: 0x04001916 RID: 6422
			public float min;

			// Token: 0x04001917 RID: 6423
			public float max;

			// Token: 0x04001918 RID: 6424
			public bool wholeNumbers;
		}
	}
}
