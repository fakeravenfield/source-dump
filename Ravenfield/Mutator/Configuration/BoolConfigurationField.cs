using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000389 RID: 905
	[Serializable]
	public class BoolConfigurationField : MutatorConfigurationFieldGeneric<bool>
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x00011E83 File Offset: 0x00010083
		public override void DeserializeValue(string serializedValue)
		{
			bool.TryParse(serializedValue, out this.value);
		}
	}
}
