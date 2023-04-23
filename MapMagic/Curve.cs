using System;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000557 RID: 1367
	public class Curve
	{
		// Token: 0x06002290 RID: 8848 RVA: 0x000DD2D8 File Offset: 0x000DB4D8
		public Curve(AnimationCurve animCurve)
		{
			this.points = new Curve.Point[animCurve.keys.Length];
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i] = new Curve.Point(animCurve.keys[i]);
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000DD330 File Offset: 0x000DB530
		public float Evaluate(float time)
		{
			if (time <= this.points[0].time)
			{
				return this.points[0].val;
			}
			if (time >= this.points[this.points.Length - 1].time)
			{
				return this.points[this.points.Length - 1].val;
			}
			for (int i = 0; i < this.points.Length - 1; i++)
			{
				if (time > this.points[i].time && time <= this.points[i + 1].time)
				{
					Curve.Point point = this.points[i];
					Curve.Point point2 = this.points[i + 1];
					float num = point2.time - point.time;
					float num2 = (time - point.time) / num;
					float num3 = num2 * num2;
					float num4 = num3 * num2;
					float num5 = 2f * num4 - 3f * num3 + 1f;
					float num6 = num4 - 2f * num3 + num2;
					float num7 = num4 - num3;
					float num8 = -2f * num4 + 3f * num3;
					return num5 * point.val + num6 * point.outTangent * num + num7 * point2.inTangent * num + num8 * point2.val;
				}
			}
			return 0f;
		}

		// Token: 0x0400225C RID: 8796
		public Curve.Point[] points;

		// Token: 0x02000558 RID: 1368
		public struct Point
		{
			// Token: 0x06002292 RID: 8850 RVA: 0x00018365 File Offset: 0x00016565
			public Point(Keyframe key)
			{
				this.time = key.time;
				this.val = key.value;
				this.inTangent = key.inTangent;
				this.outTangent = key.outTangent;
			}

			// Token: 0x0400225D RID: 8797
			public float time;

			// Token: 0x0400225E RID: 8798
			public float val;

			// Token: 0x0400225F RID: 8799
			public float inTangent;

			// Token: 0x04002260 RID: 8800
			public float outTangent;
		}
	}
}
