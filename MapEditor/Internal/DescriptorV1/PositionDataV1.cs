using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000750 RID: 1872
	[Serializable]
	public struct PositionDataV1
	{
		// Token: 0x06002E96 RID: 11926 RVA: 0x00020062 File Offset: 0x0001E262
		public PositionDataV1(Transform transform)
		{
			this.position = transform.position;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00020070 File Offset: 0x0001E270
		public void CopyTo(Transform transform)
		{
			transform.position = this.position;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0002007E File Offset: 0x0001E27E
		public bool IsZero()
		{
			return this.position == Vector3.zero;
		}

		// Token: 0x04002AA3 RID: 10915
		public Vector3 position;
	}
}
