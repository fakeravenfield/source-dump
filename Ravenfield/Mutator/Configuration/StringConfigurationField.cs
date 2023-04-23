using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000388 RID: 904
	[Serializable]
	public class StringConfigurationField : MutatorConfigurationFieldGeneric<string>
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x00011E72 File Offset: 0x00010072
		public override void DeserializeValue(string serializedValue)
		{
			this.value = serializedValue;
		}
	}
}
