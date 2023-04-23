using System;
using UnityEngine;

namespace MapMagicDemo
{
	// Token: 0x020004AB RID: 1195
	public class MoveSun : MonoBehaviour
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x000C4D94 File Offset: 0x000C2F94
		private void Update()
		{
			Vector3 position = base.transform.position;
			position.x += Time.deltaTime * this.speed;
			position.x %= this.max;
			base.transform.position = position;
		}

		// Token: 0x04001E74 RID: 7796
		public float speed = 100f;

		// Token: 0x04001E75 RID: 7797
		public float max = 1000f;
	}
}
