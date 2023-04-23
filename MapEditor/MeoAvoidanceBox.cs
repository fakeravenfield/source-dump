using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200062E RID: 1582
	public class MeoAvoidanceBox : MapEditorObject
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x0001BF2E File Offset: 0x0001A12E
		public override string GetCategoryName()
		{
			return "Avoidance Box";
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000FAD40 File Offset: 0x000F8F40
		public override MapEditorObject Clone()
		{
			MeoAvoidanceBox meoAvoidanceBox = MeoAvoidanceBox.Create(base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoAvoidanceBox.transform);
			PropertyProvider.CopyProperties(this, meoAvoidanceBox, false);
			return meoAvoidanceBox;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0001BF35 File Offset: 0x0001A135
		public static MeoAvoidanceBox Create(Transform parent = null)
		{
			return MapEditorObject.Create<MeoAvoidanceBox>(MapEditorAssistant.instance.avoidanceBoxRenderingPrefab, null, parent, true);
		}

		// Token: 0x04002687 RID: 9863
		[ShowInMapEditor(2)]
		[NonSerialized]
		public bool applyToAllTypes = true;

		// Token: 0x04002688 RID: 9864
		[ShowInMapEditor(3)]
		[NonSerialized]
		public PathfindingBox.Type type;

		// Token: 0x04002689 RID: 9865
		[ShowInMapEditor(4)]
		[Range(0f, 100f)]
		[NonSerialized]
		public float penalty = 100f;

		// Token: 0x0400268A RID: 9866
		[ShowInMapEditor(5)]
		[NonSerialized]
		public bool unwalkable;
	}
}
