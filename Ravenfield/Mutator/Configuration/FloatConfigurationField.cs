using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000385 RID: 901
	[Serializable]
	public class FloatConfigurationField : MutatorConfigurationFieldGeneric<float>
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x00011E2D File Offset: 0x0001002D
		public override void DeserializeValue(string serializedValue)
		{
			float.TryParse(serializedValue, out this.value);
		}
	}
}
