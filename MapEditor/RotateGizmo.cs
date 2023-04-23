using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000601 RID: 1537
	public class RotateGizmo : TransformGizmo
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x0001B3B9 File Offset: 0x000195B9
		public void Reset()
		{
			this.accumulatedRotation = Quaternion.identity;
		}

		// Token: 0x04002582 RID: 9602
		public Quaternion accumulatedRotation;
	}
}
