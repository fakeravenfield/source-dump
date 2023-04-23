using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000382 RID: 898
	[Serializable]
	public class MutatorConfigurationFieldGeneric<T> : MutatorConfigurationSortableField
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		public override string SerializeValue()
		{
			return this.value.ToString();
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0000296E File Offset: 0x00000B6E
		public override void DeserializeValue(string serializedValue)
		{
		}

		// Token: 0x04001914 RID: 6420
		public T value;
	}
}
