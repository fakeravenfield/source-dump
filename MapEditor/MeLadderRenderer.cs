using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000628 RID: 1576
	public class MeLadderRenderer : MonoBehaviour
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x0001BE5A File Offset: 0x0001A05A
		private void Start()
		{
			this.ladder = base.GetComponentInParent<Ladder>();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000FA844 File Offset: 0x000F8A44
		private void LateUpdate()
		{
			this.startCircle.transform.position = this.ladder.GetBottomExitPosition();
			this.endCircle.transform.position = this.ladder.GetTopExitPosition();
			int num = this.CircleHashCode();
			if (this.previousHash != num)
			{
				this.previousHash = num;
				Vector3[] array = MePathfindingLinkRenderer.PointsBetween(this.startCircle.transform.position, this.endCircle.transform.position);
				this.line.positionCount = array.Length;
				this.line.SetPositions(array);
			}
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000FA8E0 File Offset: 0x000F8AE0
		public int CircleHashCode()
		{
			return (1584462887 * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.startCircle.transform.position)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.endCircle.transform.position);
		}

		// Token: 0x04002672 RID: 9842
		public LineRenderer line;

		// Token: 0x04002673 RID: 9843
		public CircleRenderer startCircle;

		// Token: 0x04002674 RID: 9844
		public CircleRenderer endCircle;

		// Token: 0x04002675 RID: 9845
		private int previousHash;

		// Token: 0x04002676 RID: 9846
		private Ladder ladder;
	}
}
