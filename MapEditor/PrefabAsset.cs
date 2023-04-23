using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005C9 RID: 1481
	public struct PrefabAsset : IEquatable<PrefabAsset>
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x0001A8CC File Offset: 0x00018ACC
		public PrefabAsset(AssetId id, string path, GameObject gameObject)
		{
			this.id = id;
			this.path = path;
			this.gameObject = gameObject;
			this.canBakeMesh = PrefabAsset.CanBakeMeshOfPrefab(gameObject);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0001A8EF File Offset: 0x00018AEF
		private static bool CanBakeMeshOfPrefab(GameObject gameObject)
		{
			return !(gameObject.GetComponent<LODGroup>() != null) && !gameObject.CompareTag("Ingame Editor Dont Bake");
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0001A911 File Offset: 0x00018B11
		public bool HasValue()
		{
			return this != PrefabAsset.empty;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0001A923 File Offset: 0x00018B23
		public override bool Equals(object obj)
		{
			return obj is PrefabAsset && this.Equals((PrefabAsset)obj);
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0001A93B File Offset: 0x00018B3B
		public bool Equals(PrefabAsset other)
		{
			return this.id == other.id && this.path == other.path && this.gameObject == other.gameObject;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000F4710 File Offset: 0x000F2910
		public override int GetHashCode()
		{
			return ((-710470044 * -1521134295 + EqualityComparer<AssetId>.Default.GetHashCode(this.id)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.path)) * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(this.gameObject);
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0001A976 File Offset: 0x00018B76
		public static bool operator ==(PrefabAsset lhs, PrefabAsset rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0001A980 File Offset: 0x00018B80
		public static bool operator !=(PrefabAsset lhs, PrefabAsset rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x040024CB RID: 9419
		public static readonly PrefabAsset empty;

		// Token: 0x040024CC RID: 9420
		public readonly AssetId id;

		// Token: 0x040024CD RID: 9421
		public readonly string path;

		// Token: 0x040024CE RID: 9422
		public readonly GameObject gameObject;

		// Token: 0x040024CF RID: 9423
		public readonly bool canBakeMesh;
	}
}
