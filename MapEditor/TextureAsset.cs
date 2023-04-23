using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005CC RID: 1484
	public struct TextureAsset : IEquatable<TextureAsset>
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x0001A9A1 File Offset: 0x00018BA1
		public TextureAsset(AssetId id, string path, Texture2D texture)
		{
			this.id = id;
			this.path = path;
			this.texture = texture;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		public bool HasValue()
		{
			return this != TextureAsset.empty;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0001A9CA File Offset: 0x00018BCA
		public override bool Equals(object obj)
		{
			return obj is TextureAsset && this.Equals((TextureAsset)obj);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0001A9E2 File Offset: 0x00018BE2
		public bool Equals(TextureAsset other)
		{
			return this.id == other.id && this.path == other.path && this.texture == other.texture;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000F4768 File Offset: 0x000F2968
		public override int GetHashCode()
		{
			return ((1120554560 * -1521134295 + EqualityComparer<AssetId>.Default.GetHashCode(this.id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.path)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0001AA1D File Offset: 0x00018C1D
		public static bool operator ==(TextureAsset lhs, TextureAsset rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0001AA27 File Offset: 0x00018C27
		public static bool operator !=(TextureAsset lhs, TextureAsset rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040024D3 RID: 9427
		public static readonly TextureAsset empty;

		// Token: 0x040024D4 RID: 9428
		public readonly AssetId id;

		// Token: 0x040024D5 RID: 9429
		public readonly string path;

		// Token: 0x040024D6 RID: 9430
		public readonly Texture2D texture;
	}
}
