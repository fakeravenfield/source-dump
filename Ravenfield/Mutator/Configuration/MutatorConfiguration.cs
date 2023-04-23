using System;
using System.Collections.Generic;
using System.Linq;

namespace Ravenfield.Mutator.Configuration
{
	// Token: 0x02000380 RID: 896
	[Serializable]
	public class MutatorConfiguration
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x000A0E3C File Offset: 0x0009F03C
		public bool HasAnyFields()
		{
			using (IEnumerator<MutatorConfigurationSortableField> enumerator = this.GetAllFields().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					MutatorConfigurationSortableField mutatorConfigurationSortableField = enumerator.Current;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000A0E8C File Offset: 0x0009F08C
		public IEnumerable<MutatorConfigurationSortableField> GetAllFields()
		{
			IEnumerable<MutatorConfigurationSortableField> result;
			try
			{
				IEnumerable<MutatorConfigurationSortableField> enumerable = Enumerable.Empty<MutatorConfigurationSortableField>();
				MutatorConfigurationSortableField[] fields = this.labels;
				this.ConcatField(ref enumerable, fields);
				fields = this.integers;
				this.ConcatField(ref enumerable, fields);
				fields = this.floats;
				this.ConcatField(ref enumerable, fields);
				fields = this.ranges;
				this.ConcatField(ref enumerable, fields);
				fields = this.strings;
				this.ConcatField(ref enumerable, fields);
				fields = this.bools;
				this.ConcatField(ref enumerable, fields);
				fields = this.dropdowns;
				this.ConcatField(ref enumerable, fields);
				result = enumerable;
			}
			catch (Exception)
			{
				result = Enumerable.Empty<MutatorConfigurationSortableField>();
			}
			return result;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00011DE5 File Offset: 0x0000FFE5
		private void ConcatField(ref IEnumerable<MutatorConfigurationSortableField> allFields, MutatorConfigurationSortableField[] fields)
		{
			if (fields != null)
			{
				allFields = allFields.Concat(fields);
			}
		}

		// Token: 0x0400190A RID: 6410
		public MutatorConfigurationLabel[] labels;

		// Token: 0x0400190B RID: 6411
		public IntegerConfigurationField[] integers;

		// Token: 0x0400190C RID: 6412
		public FloatConfigurationField[] floats;

		// Token: 0x0400190D RID: 6413
		public RangeConfigurationField[] ranges;

		// Token: 0x0400190E RID: 6414
		public StringConfigurationField[] strings;

		// Token: 0x0400190F RID: 6415
		public BoolConfigurationField[] bools;

		// Token: 0x04001910 RID: 6416
		public DropdownConfigurationField[] dropdowns;
	}
}
