using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	public struct PositionRotationDataV1
	{
		// Token: 0x06002E99 RID: 11929 RVA: 0x001091E0 File Offset: 0x001073E0
		public PositionRotationDataV1(Transform transform)
		{
			this.position = transform.position;
			this.rotation = transform.rotation.eulerAngles;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x00020090 File Offset: 0x0001E290
		public void CopyTo(Transform transform)
		{
			transform.SetPositionAndRotation(this.position, Quaternion.Euler(this.rotation));
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000200A9 File Offset: 0x0001E2A9
		public bool IsZero()
		{
			return this.position == Vector3.zero && this.rotation == Vector3.zero;
		}

		// Token: 0x04002AA4 RID: 10916
		public Vector3 position;

		// Token: 0x04002AA5 RID: 10917
		public Vector3 rotation;
	}
}
