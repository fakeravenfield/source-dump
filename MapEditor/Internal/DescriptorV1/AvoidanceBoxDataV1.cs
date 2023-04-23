using System;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000765 RID: 1893
	[Serializable]
	public struct AvoidanceBoxDataV1
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x00109C88 File Offset: 0x00107E88
		public AvoidanceBoxDataV1(MeoAvoidanceBox box)
		{
			this.name = box.name;
			this.transform = new TransformDataV1(box.transform);
			this.applyToAllTypes = box.applyToAllTypes;
			this.type = AvoidanceBoxDataV1.TYPE.Encode(box.type);
			this.penalty = box.penalty;
			this.unwalkable = box.unwalkable;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00109CEC File Offset: 0x00107EEC
		public void CopyTo(MeoAvoidanceBox box)
		{
			box.name = this.name;
			this.transform.CopyTo(box.transform);
			box.applyToAllTypes = this.applyToAllTypes;
			box.type = this.GetAvoidanceType();
			box.penalty = this.penalty;
			box.unwalkable = this.unwalkable;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00109D48 File Offset: 0x00107F48
		public void CopyTo(AvoidanceBox box)
		{
			box.name = this.name;
			this.transform.CopyTo(box.transform);
			box.applyToAllTypes = this.applyToAllTypes;
			box.type = this.GetAvoidanceType();
			box.penalty = (uint)(this.penalty * 100f);
			box.unwalkable = this.unwalkable;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00020444 File Offset: 0x0001E644
		public PathfindingBox.Type GetAvoidanceType()
		{
			return AvoidanceBoxDataV1.TYPE.Decode(this.type);
		}

		// Token: 0x04002AFE RID: 11006
		private static readonly EnumEncoder<PathfindingBox.Type> TYPE = new EnumEncoder<PathfindingBox.Type>();

		// Token: 0x04002AFF RID: 11007
		public string name;

		// Token: 0x04002B00 RID: 11008
		public TransformDataV1 transform;

		// Token: 0x04002B01 RID: 11009
		public bool applyToAllTypes;

		// Token: 0x04002B02 RID: 11010
		public string type;

		// Token: 0x04002B03 RID: 11011
		public float penalty;

		// Token: 0x04002B04 RID: 11012
		public bool unwalkable;
	}
}
