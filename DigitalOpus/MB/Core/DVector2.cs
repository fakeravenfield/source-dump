using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045B RID: 1115
	public struct DVector2
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x000151DB File Offset: 0x000133DB
		public static DVector2 Subtract(DVector2 a, DVector2 b)
		{
			return new DVector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000151FC File Offset: 0x000133FC
		public DVector2(double xx, double yy)
		{
			this.x = xx;
			this.y = yy;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0001520C File Offset: 0x0001340C
		public DVector2(DVector2 r)
		{
			this.x = r.x;
			this.y = r.y;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00015226 File Offset: 0x00013426
		public Vector2 GetVector2()
		{
			return new Vector2((float)this.x, (float)this.y);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x000B889C File Offset: 0x000B6A9C
		public bool IsContainedIn(DRect r)
		{
			return this.x >= r.x && this.y >= r.y && this.x <= r.x + r.width && this.y <= r.y + r.height;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000B88F4 File Offset: 0x000B6AF4
		public bool IsContainedInWithMargin(DRect r)
		{
			return this.x >= r.x - DVector2.epsilon && this.y >= r.y - DVector2.epsilon && this.x <= r.x + r.width + DVector2.epsilon && this.y <= r.y + r.height + DVector2.epsilon;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0001523B File Offset: 0x0001343B
		public override string ToString()
		{
			return string.Format("({0},{1})", this.x, this.y);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0001525D File Offset: 0x0001345D
		public string ToString(string formatS)
		{
			return string.Format("({0},{1})", this.x.ToString(formatS), this.y.ToString(formatS));
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000B8964 File Offset: 0x000B6B64
		public static double Distance(DVector2 a, DVector2 b)
		{
			double num = b.x - a.x;
			double num2 = b.y - a.y;
			return Math.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x04001D28 RID: 7464
		private static double epsilon = 1E-05;

		// Token: 0x04001D29 RID: 7465
		public double x;

		// Token: 0x04001D2A RID: 7466
		public double y;
	}
}
