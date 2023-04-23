using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public static class ConvexHull
{
	// Token: 0x060013F2 RID: 5106 RVA: 0x0000FE3E File Offset: 0x0000E03E
	public static List<int> ComputeXZ(List<Vector3> points)
	{
		return ConvexHull.Compute((from p in points
		select new Vector2(p.x, p.z)).ToList<Vector2>());
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x0000FE6F File Offset: 0x0000E06F
	public static List<int> ComputeXY(List<Vector3> points)
	{
		return ConvexHull.Compute((from p in points
		select new Vector2(p.x, p.y)).ToList<Vector2>());
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x00095048 File Offset: 0x00093248
	public static List<int> Compute(List<Vector2> points)
	{
		List<int> list = new List<int>();
		int num = ConvexHull.LowestXCoord(points);
		int num2 = num;
		int num3 = 0;
		while (num3++ <= 100)
		{
			list.Add(num2);
			int num4 = 0;
			for (int i = 1; i < points.Count; i++)
			{
				if (num4 == num2 || ConvexHull.ccw(points[num2], points[num4], points[i]) < 0f)
				{
					num4 = i;
				}
			}
			num2 = num4;
			if (num == num4)
			{
				return list;
			}
		}
		return null;
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x000950C4 File Offset: 0x000932C4
	private static int LowestXCoord(List<Vector2> array)
	{
		int result = -1;
		float num = float.MaxValue;
		for (int i = 0; i < array.Count; i++)
		{
			if (array[i].x < num)
			{
				result = i;
				num = array[i].x;
			}
		}
		return result;
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
	public static float ccw(Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return Mathf.Sign((p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y));
	}
}
