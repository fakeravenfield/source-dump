using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000752 RID: 1874
	[Serializable]
	public struct TransformDataV1
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x00109210 File Offset: 0x00107410
		public TransformDataV1(Transform transform)
		{
			this.position = transform.position;
			this.rotation = transform.rotation.eulerAngles;
			this.scale = transform.localScale;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000200CF File Offset: 0x0001E2CF
		public void CopyTo(Transform transform)
		{
			transform.SetPositionAndRotation(this.position, Quaternion.Euler(this.rotation));
			transform.localScale = this.scale;
		}

		// Token: 0x04002AA6 RID: 10918
		public Vector3 position;

		// Token: 0x04002AA7 RID: 10919
		public Vector3 rotation;

		// Token: 0x04002AA8 RID: 10920
		public Vector3 scale;
	}
}
