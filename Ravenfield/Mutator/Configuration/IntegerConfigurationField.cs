using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000384 RID: 900
	[Serializable]
	public class IntegerConfigurationField : MutatorConfigurationFieldGeneric<int>
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x00011E16 File Offset: 0x00010016
		public override void DeserializeValue(string serializedValue)
		{
			int.TryParse(serializedValue, out this.value);
		}
	}
}
