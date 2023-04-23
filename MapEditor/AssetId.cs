using System;
using System.Collections.Generic;

namespace MapEditor
{
	// Token: 0x020005C0 RID: 1472
	public struct AssetId : IEquatable<AssetId>
	{
		// Token: 0x06002624 RID: 9764 RVA: 0x0001A50B File Offset: 0x0001870B
		public AssetId(string guid)
		{
			this.guid = guid;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0001A514 File Offset: 0x00018714
		public bool HasValue()
		{
			return this != AssetId.empty;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x0001A526 File Offset: 0x00018726
		public override bool Equals(object obj)
		{
			return obj is AssetId && this.Equals((AssetId)obj);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0001A53E File Offset: 0x0001873E
		public bool Equals(AssetId other)
		{
			return this.guid == other.guid;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0001A551 File Offset: 0x00018751
		public static bool operator ==(AssetId lhs, AssetId rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0001A55B File Offset: 0x0001875B
		public static bool operator !=(AssetId lhs, AssetId rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0001A568 File Offset: 0x00018768
		public override int GetHashCode()
		{
			return -1324198676 + EqualityComparer<string>.Default.GetHashCode(this.guid);
		}

		// Token: 0x040024A4 RID: 9380
		public static readonly AssetId empty;

		// Token: 0x040024A5 RID: 9381
		public readonly string guid;
	}
}
