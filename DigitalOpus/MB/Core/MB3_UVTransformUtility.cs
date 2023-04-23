using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200045D RID: 1117
	public class MB3_UVTransformUtility
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x000B8B40 File Offset: 0x000B6D40
		public static void Test()
		{
			DRect drect = new DRect(0.5, 0.5, 2.0, 2.0);
			DRect drect2 = new DRect(0.25, 0.25, 3.0, 3.0);
			DRect drect3 = MB3_UVTransformUtility.InverseTransform(ref drect);
			DRect drect4 = MB3_UVTransformUtility.InverseTransform(ref drect2);
			DRect drect5 = MB3_UVTransformUtility.CombineTransforms(ref drect, ref drect4);
			Debug.Log(drect3);
			Debug.Log(drect5);
			Debug.Log("one mat trans " + MB3_UVTransformUtility.TransformPoint(ref drect, new Vector2(1f, 1f)).ToString());
			Debug.Log("one inv mat trans " + MB3_UVTransformUtility.TransformPoint(ref drect3, new Vector2(1f, 1f)).ToString("f4"));
			Debug.Log("zero " + MB3_UVTransformUtility.TransformPoint(ref drect5, new Vector2(0f, 0f)).ToString("f4"));
			Debug.Log("one " + MB3_UVTransformUtility.TransformPoint(ref drect5, new Vector2(1f, 1f)).ToString("f4"));
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000154AC File Offset: 0x000136AC
		public static float TransformX(DRect r, double x)
		{
			return (float)(r.width * x + r.x);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000B8CA4 File Offset: 0x000B6EA4
		public static DRect CombineTransforms(ref DRect r1, ref DRect r2)
		{
			return new DRect(r1.x * r2.width + r2.x, r1.y * r2.height + r2.y, r1.width * r2.width, r1.height * r2.height);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000B8CF8 File Offset: 0x000B6EF8
		public static Rect CombineTransforms(ref Rect r1, ref Rect r2)
		{
			return new Rect(r1.x * r2.width + r2.x, r1.y * r2.height + r2.y, r1.width * r2.width, r1.height * r2.height);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000B8D4C File Offset: 0x000B6F4C
		public static DRect InverseTransform(ref DRect t)
		{
			return new DRect
			{
				x = -t.x / t.width,
				y = -t.y / t.height,
				width = 1.0 / t.width,
				height = 1.0 / t.height
			};
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000B8DBC File Offset: 0x000B6FBC
		public static DRect GetShiftTransformToFitBinA(ref DRect A, ref DRect B)
		{
			DVector2 center = A.center;
			DVector2 center2 = B.center;
			DVector2 dvector = DVector2.Subtract(center, center2);
			double xx = (double)Convert.ToInt32(dvector.x);
			double yy = (double)Convert.ToInt32(dvector.y);
			return new DRect(xx, yy, 1.0, 1.0);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000B8E10 File Offset: 0x000B7010
		public static DRect GetEncapsulatingRectShifted(ref DRect uvRect1, ref DRect willBeIn)
		{
			DVector2 center = uvRect1.center;
			DVector2 center2 = willBeIn.center;
			DVector2 dvector = DVector2.Subtract(center, center2);
			double num = (double)Convert.ToInt32(dvector.x);
			double num2 = (double)Convert.ToInt32(dvector.y);
			DRect drect = new DRect(willBeIn);
			drect.x += num;
			drect.y += num2;
			double x = uvRect1.x;
			double y = uvRect1.y;
			double num3 = uvRect1.x + uvRect1.width;
			double num4 = uvRect1.y + uvRect1.height;
			double x2 = drect.x;
			double y2 = drect.y;
			double num5 = drect.x + drect.width;
			double num6 = drect.y + drect.height;
			double num8;
			double num7 = num8 = x;
			double num10;
			double num9 = num10 = y;
			if (x2 < num8)
			{
				num8 = x2;
			}
			if (x < num8)
			{
				num8 = x;
			}
			if (y2 < num10)
			{
				num10 = y2;
			}
			if (y < num10)
			{
				num10 = y;
			}
			if (num5 > num7)
			{
				num7 = num5;
			}
			if (num3 > num7)
			{
				num7 = num3;
			}
			if (num6 > num9)
			{
				num9 = num6;
			}
			if (num4 > num9)
			{
				num9 = num4;
			}
			return new DRect(num8, num10, num7 - num8, num9 - num10);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000B8F3C File Offset: 0x000B713C
		public static DRect GetEncapsulatingRect(ref DRect uvRect1, ref DRect uvRect2)
		{
			double x = uvRect1.x;
			double y = uvRect1.y;
			double num = uvRect1.x + uvRect1.width;
			double num2 = uvRect1.y + uvRect1.height;
			double x2 = uvRect2.x;
			double y2 = uvRect2.y;
			double num3 = uvRect2.x + uvRect2.width;
			double num4 = uvRect2.y + uvRect2.height;
			double num6;
			double num5 = num6 = x;
			double num8;
			double num7 = num8 = y;
			if (x2 < num6)
			{
				num6 = x2;
			}
			if (x < num6)
			{
				num6 = x;
			}
			if (y2 < num8)
			{
				num8 = y2;
			}
			if (y < num8)
			{
				num8 = y;
			}
			if (num3 > num5)
			{
				num5 = num3;
			}
			if (num > num5)
			{
				num5 = num;
			}
			if (num4 > num7)
			{
				num7 = num4;
			}
			if (num2 > num7)
			{
				num7 = num2;
			}
			return new DRect(num6, num8, num5 - num6, num7 - num8);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000B9008 File Offset: 0x000B7208
		public static bool RectContainsShifted(ref DRect bucket, ref DRect tryFit)
		{
			DVector2 center = bucket.center;
			DVector2 center2 = tryFit.center;
			DVector2 dvector = DVector2.Subtract(center, center2);
			double num = (double)Convert.ToInt32(dvector.x);
			double num2 = (double)Convert.ToInt32(dvector.y);
			DRect smallToTestIfFits = new DRect(tryFit);
			smallToTestIfFits.x += num;
			smallToTestIfFits.y += num2;
			return bucket.Encloses(smallToTestIfFits);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000B9070 File Offset: 0x000B7270
		public static bool RectContainsShifted(ref Rect bucket, ref Rect tryFit)
		{
			Vector2 center = bucket.center;
			Vector2 center2 = tryFit.center;
			Vector2 vector = center - center2;
			float num = (float)Convert.ToInt32(vector.x);
			float num2 = (float)Convert.ToInt32(vector.y);
			Rect rect = new Rect(tryFit);
			rect.x += num;
			rect.y += num2;
			return MB3_UVTransformUtility.RectContains(ref bucket, ref rect);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000B90DC File Offset: 0x000B72DC
		public static bool LineSegmentContainsShifted(float bucketOffset, float bucketLength, float tryFitOffset, float tryFitLength)
		{
			float num = bucketOffset + bucketLength / 2f;
			float num2 = tryFitOffset + tryFitLength / 2f;
			float num3 = (float)Convert.ToInt32(num - num2);
			tryFitOffset += num3;
			float num4 = tryFitOffset;
			float num5 = tryFitOffset + tryFitLength;
			float num6 = bucketOffset - 0.01f;
			float num7 = bucketOffset + bucketLength + 0.01f;
			return num6 <= num4 && num4 <= num7 && num6 <= num5 && num5 <= num7;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000B9140 File Offset: 0x000B7340
		public static bool RectContains(ref DRect bigRect, ref DRect smallToTestIfFits)
		{
			double x = smallToTestIfFits.x;
			double y = smallToTestIfFits.y;
			double num = smallToTestIfFits.x + smallToTestIfFits.width;
			double num2 = smallToTestIfFits.y + smallToTestIfFits.height;
			double num3 = bigRect.x - 0.009999999776482582;
			double num4 = bigRect.y - 0.009999999776482582;
			double num5 = bigRect.x + bigRect.width + 0.009999999776482582;
			double num6 = bigRect.y + bigRect.height + 0.009999999776482582;
			return num3 <= x && x <= num5 && num3 <= num && num <= num5 && num4 <= y && y <= num6 && num4 <= num2 && num2 <= num6;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000B91FC File Offset: 0x000B73FC
		public static bool RectContains(ref Rect bigRect, ref Rect smallToTestIfFits)
		{
			float x = smallToTestIfFits.x;
			float y = smallToTestIfFits.y;
			float num = smallToTestIfFits.x + smallToTestIfFits.width;
			float num2 = smallToTestIfFits.y + smallToTestIfFits.height;
			float num3 = bigRect.x - 0.01f;
			float num4 = bigRect.y - 0.01f;
			float num5 = bigRect.x + bigRect.width + 0.01f;
			float num6 = bigRect.y + bigRect.height + 0.01f;
			return num3 <= x && x <= num5 && num3 <= num && num <= num5 && num4 <= y && y <= num6 && num4 <= num2 && num2 <= num6;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000154BE File Offset: 0x000136BE
		public static Vector2 TransformPoint(ref DRect r, Vector2 p)
		{
			return new Vector2((float)(r.width * (double)p.x + r.x), (float)(r.height * (double)p.y + r.y));
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000154F1 File Offset: 0x000136F1
		public static DVector2 TransformPoint(ref DRect r, DVector2 p)
		{
			return new DVector2(r.width * p.x + r.x, r.height * p.y + r.y);
		}
	}
}
