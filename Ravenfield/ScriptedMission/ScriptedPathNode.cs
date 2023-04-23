using System;
using UnityEngine;

namespace Ravenfield.ScriptedMission
{
	// Token: 0x02000379 RID: 889
	[Serializable]
	public struct ScriptedPathNode
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x000A035C File Offset: 0x0009E55C
		public ScriptedPathNode(Vector3 position, ScriptedPathNode basedOn)
		{
			this.localPosition = position;
			this.speed = basedOn.speed;
			this.slowDownForNextTurn = basedOn.slowDownForNextTurn;
			this.stance = basedOn.stance;
			this.synchronize = false;
			this.syncNumber = 0;
			this.waitTime = 0f;
		}

		// Token: 0x040018CA RID: 6346
		public const float DEFAULT_SPEED = 4f;

		// Token: 0x040018CB RID: 6347
		public Vector3 localPosition;

		// Token: 0x040018CC RID: 6348
		public float speed;

		// Token: 0x040018CD RID: 6349
		public bool slowDownForNextTurn;

		// Token: 0x040018CE RID: 6350
		public ScriptedPathNode.Stance stance;

		// Token: 0x040018CF RID: 6351
		public bool synchronize;

		// Token: 0x040018D0 RID: 6352
		public byte syncNumber;

		// Token: 0x040018D1 RID: 6353
		public float waitTime;

		// Token: 0x0200037A RID: 890
		public enum Stance
		{
			// Token: 0x040018D3 RID: 6355
			Standing,
			// Token: 0x040018D4 RID: 6356
			Crouched,
			// Token: 0x040018D5 RID: 6357
			Prone
		}
	}
}
