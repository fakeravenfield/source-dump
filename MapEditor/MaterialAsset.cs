using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005C8 RID: 1480
	public struct MaterialAsset : IEquatable<MaterialAsset>
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x0001A839 File Offset: 0x00018A39
		public MaterialAsset(AssetId id, string path, Material material)
		{
			this.id = id;
			this.path = path;
			this.material = material;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0001A850 File Offset: 0x00018A50
		public bool HasValue()
		{
			return this != MaterialAsset.empty;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0001A862 File Offset: 0x00018A62
		public override bool Equals(object obj)
		{
			return obj is MaterialAsset && this.Equals((MaterialAsset)obj);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0001A87A File Offset: 0x00018A7A
		public bool Equals(MaterialAsset other)
		{
			return this.id == other.id && this.path == other.path && this.material == other.material;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000F46B8 File Offset: 0x000F28B8
		public override int GetHashCode()
		{
			return ((1120554560 * -1521134295 + EqualityComparer<AssetId>.Default.GetHashCode(this.id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.path)) * -1521134295 + EqualityComparer<Material>.Default.GetHashCode(this.material);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0001A8B5 File Offset: 0x00018AB5
		public static bool operator ==(MaterialAsset lhs, MaterialAsset rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0001A8BF File Offset: 0x00018ABF
		public static bool operator !=(MaterialAsset lhs, MaterialAsset rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040024C7 RID: 9415
		public static readonly MaterialAsset empty;

		// Token: 0x040024C8 RID: 9416
		public readonly AssetId id;

		// Token: 0x040024C9 RID: 9417
		public readonly string path;

		// Token: 0x040024CA RID: 9418
		public readonly Material material;
	}
}
