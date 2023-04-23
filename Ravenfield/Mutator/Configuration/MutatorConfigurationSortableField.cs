using System;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000381 RID: 897
	public abstract class MutatorConfigurationSortableField
	{
		// Token: 0x0600169C RID: 5788
		public abstract string SerializeValue();

		// Token: 0x0600169D RID: 5789
		public abstract void DeserializeValue(string serializedValue);

		// Token: 0x04001911 RID: 6417
		public string id;

		// Token: 0x04001912 RID: 6418
		public string displayName;

		// Token: 0x04001913 RID: 6419
		public int orderPriority;
	}
}
