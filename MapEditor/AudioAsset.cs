using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005C6 RID: 1478
	public struct AudioAsset : IEquatable<AudioAsset>
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x0001A74E File Offset: 0x0001894E
		public AudioAsset(AssetId id, string path, AudioClip audioClip)
		{
			this.id = id;
			this.path = path;
			this.audioClip = audioClip;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0001A765 File Offset: 0x00018965
		public bool HasValue()
		{
			return this != AudioAsset.empty;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000F45B8 File Offset: 0x000F27B8
		public bool Equals(AudioAsset other)
		{
			return this.id.Equals(other.id) && this.path == other.path && EqualityComparer<AudioClip>.Default.Equals(this.audioClip, other.audioClip);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000F4608 File Offset: 0x000F2808
		public override int GetHashCode()
		{
			return ((876241793 * -1521134295 + EqualityComparer<AssetId>.Default.GetHashCode(this.id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.path)) * -1521134295 + EqualityComparer<AudioClip>.Default.GetHashCode(this.audioClip);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0001A777 File Offset: 0x00018977
		public override bool Equals(object obj)
		{
			return obj is AudioAsset && this.Equals((AudioAsset)obj);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0001A78F File Offset: 0x0001898F
		public static bool operator ==(AudioAsset lhs, AudioAsset rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0001A799 File Offset: 0x00018999
		public static bool operator !=(AudioAsset lhs, AudioAsset rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040024BF RID: 9407
		public static readonly AudioAsset empty;

		// Token: 0x040024C0 RID: 9408
		public readonly AssetId id;

		// Token: 0x040024C1 RID: 9409
		public readonly string path;

		// Token: 0x040024C2 RID: 9410
		public readonly AudioClip audioClip;
	}
}
