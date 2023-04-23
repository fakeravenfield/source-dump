using System;

namespace Ravenfield.Mods.Data
{
	// Token: 0x020003A0 RID: 928
	[Serializable]
	public abstract class DataEntryBase
	{
		// Token: 0x06001702 RID: 5890 RVA: 0x00012230 File Offset: 0x00010430
		public bool MatchesIdHash(int hash)
		{
			if (this.idHash == -1)
			{
				this.GenerateHash();
			}
			return hash == this.idHash;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0001224A File Offset: 0x0001044A
		public void GenerateHash()
		{
			this.idHash = DataEntryBase.IdToHash(this.id);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0001225D File Offset: 0x0001045D
		public static int IdToHash(string id)
		{
			return id.ToLowerInvariant().GetHashCode();
		}

		// Token: 0x0400192E RID: 6446
		public string id;

		// Token: 0x0400192F RID: 6447
		[NonSerialized]
		public int idHash = -1;
	}
}
