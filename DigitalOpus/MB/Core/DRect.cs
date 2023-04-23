using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045C RID: 1116
	public struct DRect
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x00015291 File Offset: 0x00013491
		public DRect(Rect r)
		{
			this.x = (double)r.x;
			this.y = (double)r.y;
			this.width = (double)r.width;
			this.height = (double)r.height;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000152CB File Offset: 0x000134CB
		public DRect(Vector2 o, Vector2 s)
		{
			this.x = (double)o.x;
			this.y = (double)o.y;
			this.width = (double)s.x;
			this.height = (double)s.y;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00015301 File Offset: 0x00013501
		public DRect(DRect r)
		{
			this.x = r.x;
			this.y = r.y;
			this.width = r.width;
			this.height = r.height;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00015333 File Offset: 0x00013533
		public DRect(float xx, float yy, float w, float h)
		{
			this.x = (double)xx;
			this.y = (double)yy;
			this.width = (double)w;
			this.height = (double)h;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00015356 File Offset: 0x00013556
		public DRect(double xx, double yy, double w, double h)
		{
			this.x = xx;
			this.y = yy;
			this.width = w;
			this.height = h;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00015375 File Offset: 0x00013575
		public Rect GetRect()
		{
			return new Rect((float)this.x, (float)this.y, (float)this.width, (float)this.height);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00015398 File Offset: 0x00013598
		public DVector2 minD
		{
			get
			{
				return new DVector2(this.x, this.y);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000153AB File Offset: 0x000135AB
		public DVector2 maxD
		{
			get
			{
				return new DVector2(this.x + this.width, this.y + this.height);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000153CC File Offset: 0x000135CC
		public Vector2 min
		{
			get
			{
				return new Vector2((float)this.x, (float)this.y);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000153E1 File Offset: 0x000135E1
		public Vector2 max
		{
			get
			{
				return new Vector2((float)(this.x + this.width), (float)(this.y + this.height));
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00015404 File Offset: 0x00013604
		public Vector2 size
		{
			get
			{
				return new Vector2((float)this.width, (float)this.height);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00015419 File Offset: 0x00013619
		public DVector2 center
		{
			get
			{
				return new DVector2(this.x + this.width / 2.0, this.y + this.height / 2.0);
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000B8998 File Offset: 0x000B6B98
		public override bool Equals(object obj)
		{
			DRect drect = (DRect)obj;
			return drect.x == this.x && drect.y == this.y && drect.width == this.width && drect.height == this.height;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0001544E File Offset: 0x0001364E
		public static bool operator ==(DRect a, DRect b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00015463 File Offset: 0x00013663
		public static bool operator !=(DRect a, DRect b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000B89E8 File Offset: 0x000B6BE8
		public override string ToString()
		{
			return string.Format("(x={0},y={1},w={2},h={3})", new object[]
			{
				this.x.ToString("F5"),
				this.y.ToString("F5"),
				this.width.ToString("F5"),
				this.height.ToString("F5")
			});
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000B8A54 File Offset: 0x000B6C54
		public void Expand(float amt)
		{
			this.x -= (double)amt;
			this.y -= (double)amt;
			this.width += (double)(amt * 2f);
			this.height += (double)(amt * 2f);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000B8AAC File Offset: 0x000B6CAC
		public bool Encloses(DRect smallToTestIfFits)
		{
			double num = smallToTestIfFits.x;
			double num2 = smallToTestIfFits.y;
			double num3 = smallToTestIfFits.x + smallToTestIfFits.width;
			double num4 = smallToTestIfFits.y + smallToTestIfFits.height;
			double num5 = this.x;
			double num6 = this.y;
			double num7 = this.x + this.width;
			double num8 = this.y + this.height;
			return num5 <= num && num <= num7 && num5 <= num3 && num3 <= num7 && num6 <= num2 && num2 <= num8 && num6 <= num4 && num4 <= num8;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0001547B File Offset: 0x0001367B
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.width.GetHashCode() ^ this.height.GetHashCode();
		}

		// Token: 0x04001D2B RID: 7467
		public double x;

		// Token: 0x04001D2C RID: 7468
		public double y;

		// Token: 0x04001D2D RID: 7469
		public double width;

		// Token: 0x04001D2E RID: 7470
		public double height;
	}
}
