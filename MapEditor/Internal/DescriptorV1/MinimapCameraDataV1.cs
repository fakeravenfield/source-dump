using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000755 RID: 1877
	[Serializable]
	public struct MinimapCameraDataV1
	{
		// Token: 0x06002EA1 RID: 11937 RVA: 0x0002011F File Offset: 0x0001E31F
		public MinimapCameraDataV1(MinimapCamera minimap, Camera camera)
		{
			this.position = new PositionDataV1(minimap.transform);
			this.fieldOfView = camera.fieldOfView;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0010924C File Offset: 0x0010744C
		public void CopyTo(MinimapCamera minimap, Camera camera)
		{
			if (!this.position.IsZero())
			{
				this.position.CopyTo(minimap.transform);
			}
			if (camera.transform.position.y < 300f)
			{
				Vector3 vector = camera.transform.position;
				vector = new Vector3(vector.x, 399f, vector.z);
				camera.transform.position = vector;
			}
			float y = camera.transform.position.y;
			camera.farClipPlane = y + 1f;
			camera.nearClipPlane = camera.farClipPlane - 200f;
			camera.fieldOfView = this.fieldOfView;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0002013E File Offset: 0x0001E33E
		public bool IsValid()
		{
			return this.fieldOfView > 0f;
		}

		// Token: 0x04002AAC RID: 10924
		public PositionDataV1 position;

		// Token: 0x04002AAD RID: 10925
		public float fieldOfView;
	}
}
