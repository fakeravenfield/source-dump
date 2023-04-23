using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000564 RID: 1380
	public static class Extensions
	{
		// Token: 0x060022FF RID: 8959 RVA: 0x000E290C File Offset: 0x000E0B0C
		public static bool InRange(this Rect rect, Vector2 pos)
		{
			return (rect.center - pos).sqrMagnitude < rect.width / 2f * (rect.width / 2f);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00018846 File Offset: 0x00016A46
		public static Vector3 ToDir(this float angle)
		{
			return new Vector3(Mathf.Sin(angle * 0.017453292f), 0f, Mathf.Cos(angle * 0.017453292f));
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0001886A File Offset: 0x00016A6A
		public static float ToAngle(this Vector3 dir)
		{
			return Mathf.Atan2(dir.x, dir.z) * 57.29578f;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0000FF52 File Offset: 0x0000E152
		public static Vector3 V3(this Vector2 v2)
		{
			return new Vector3(v2.x, 0f, v2.y);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0000FF3F File Offset: 0x0000E13F
		public static Vector2 V2(this Vector3 v3)
		{
			return new Vector2(v3.x, v3.z);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00018883 File Offset: 0x00016A83
		public static Vector3 ToV3(this float f)
		{
			return new Vector3(f, f, f);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000E294C File Offset: 0x000E0B4C
		public static Quaternion EulerToQuat(this Vector3 v)
		{
			Quaternion identity = Quaternion.identity;
			identity.eulerAngles = v;
			return identity;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000E2968 File Offset: 0x000E0B68
		public static Quaternion EulerToQuat(this float f)
		{
			Quaternion identity = Quaternion.identity;
			identity.eulerAngles = new Vector3(0f, f, 0f);
			return identity;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00018846 File Offset: 0x00016A46
		public static Vector3 Direction(this float angle)
		{
			return new Vector3(Mathf.Sin(angle * 0.017453292f), 0f, Mathf.Cos(angle * 0.017453292f));
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x0001886A File Offset: 0x00016A6A
		public static float Angle(this Vector3 dir)
		{
			return Mathf.Atan2(dir.x, dir.z) * 57.29578f;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0001888D File Offset: 0x00016A8D
		public static Rect Clamp(this Rect r, float p)
		{
			return new Rect(r.x, r.y, r.width * p, r.height);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000188B2 File Offset: 0x00016AB2
		public static Rect ClampFromLeft(this Rect r, float p)
		{
			return new Rect(r.x + r.width * (1f - p), r.y, r.width * p, r.height);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000188E7 File Offset: 0x00016AE7
		public static Rect Clamp(this Rect r, int p)
		{
			return new Rect(r.x, r.y, (float)p, r.height);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00018905 File Offset: 0x00016B05
		public static Rect ClampFromLeft(this Rect r, int p)
		{
			return new Rect(r.x + (r.width - (float)p), r.y, (float)p, r.height);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0001892E File Offset: 0x00016B2E
		public static Rect ToRect(this Vector3 center, float range)
		{
			return new Rect(center.x - range, center.z - range, range * 2f, range * 2f);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000E2994 File Offset: 0x000E0B94
		public static bool Intersects(this Rect r1, Rect r2)
		{
			Vector2 min = r1.min;
			Vector2 max = r1.max;
			Vector2 min2 = r2.min;
			Vector2 max2 = r2.max;
			return max2.x >= min.x && min2.x <= max.x && max2.y >= min.y && min2.y <= max.y;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000E29FC File Offset: 0x000E0BFC
		public static bool Intersects(this Rect r1, Rect[] rects)
		{
			for (int i = 0; i < rects.Length; i++)
			{
				if (r1.Intersects(rects[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000E2A2C File Offset: 0x000E0C2C
		public static bool Contains(this Rect r1, Rect r2)
		{
			Vector2 min = r1.min;
			Vector2 max = r1.max;
			Vector2 min2 = r2.min;
			Vector2 max2 = r2.max;
			return min2.x > min.x && max2.x < max.x && min2.y > min.y && max2.y < max.y;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00018953 File Offset: 0x00016B53
		public static Rect Extend(this Rect r, float f)
		{
			return new Rect(r.x - f, r.y - f, r.width + f * 2f, r.height + f * 2f);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000E2A94 File Offset: 0x000E0C94
		public static Coord RoundToCoord(this Vector2 pos)
		{
			int num = (int)(pos.x + 0.5f);
			if (pos.x < 0f)
			{
				num--;
			}
			int num2 = (int)(pos.y + 0.5f);
			if (pos.y < 0f)
			{
				num2--;
			}
			return new Coord(num, num2);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0001898A File Offset: 0x00016B8A
		public static Coord FloorToCoord(this Vector3 pos, float cellSize)
		{
			return new Coord(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.z / cellSize));
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000189AB File Offset: 0x00016BAB
		public static Coord CeilToCoord(this Vector3 pos, float cellSize)
		{
			return new Coord(Mathf.CeilToInt(pos.x / cellSize), Mathf.CeilToInt(pos.z / cellSize));
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000189CC File Offset: 0x00016BCC
		public static Coord RoundToCoord(this Vector3 pos, float cellSize)
		{
			return new Coord(Mathf.RoundToInt(pos.x / cellSize), Mathf.RoundToInt(pos.z / cellSize));
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000E2AE8 File Offset: 0x000E0CE8
		public static CoordRect ToCoordRect(this Vector3 pos, float range, float cellSize)
		{
			Coord coord = new Coord(Mathf.FloorToInt((pos.x - range) / cellSize), Mathf.FloorToInt((pos.z - range) / cellSize));
			Coord c = new Coord(Mathf.FloorToInt((pos.x + range) / cellSize), Mathf.FloorToInt((pos.z + range) / cellSize)) + 1;
			return new CoordRect(coord, c - coord);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000E2B54 File Offset: 0x000E0D54
		public static CoordRect GetHeightRect(this Terrain terrain)
		{
			float num = terrain.terrainData.size.x / (float)terrain.terrainData.heightmapResolution;
			int num2 = (int)(terrain.transform.localPosition.x / num + 0.5f);
			if (terrain.transform.localPosition.x < 0f)
			{
				num2--;
			}
			int num3 = (int)(terrain.transform.localPosition.z / num + 0.5f);
			if (terrain.transform.localPosition.z < 0f)
			{
				num3--;
			}
			return new CoordRect(num2, num3, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000E2C08 File Offset: 0x000E0E08
		public static CoordRect GetSplatRect(this Terrain terrain)
		{
			float num = terrain.terrainData.size.x / (float)terrain.terrainData.alphamapResolution;
			int num2 = (int)(terrain.transform.localPosition.x / num + 0.5f);
			if (terrain.transform.localPosition.x < 0f)
			{
				num2--;
			}
			int num3 = (int)(terrain.transform.localPosition.z / num + 0.5f);
			if (terrain.transform.localPosition.z < 0f)
			{
				num3--;
			}
			return new CoordRect(num2, num3, terrain.terrainData.alphamapResolution, terrain.terrainData.alphamapResolution);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000E2CBC File Offset: 0x000E0EBC
		public static float[,] SafeGetHeights(this TerrainData data, int offsetX, int offsetZ, int sizeX, int sizeZ)
		{
			if (offsetX < 0)
			{
				sizeX += offsetX;
				offsetX = 0;
			}
			if (offsetZ < 0)
			{
				sizeZ += offsetZ;
				offsetZ = 0;
			}
			int heightmapResolution = data.heightmapResolution;
			if (sizeX + offsetX > heightmapResolution)
			{
				sizeX = heightmapResolution - offsetX;
			}
			if (sizeZ + offsetZ > heightmapResolution)
			{
				sizeZ = heightmapResolution - offsetZ;
			}
			return data.GetHeights(offsetX, offsetZ, sizeX, sizeZ);
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000E2D0C File Offset: 0x000E0F0C
		public static float[,,] SafeGetAlphamaps(this TerrainData data, int offsetX, int offsetZ, int sizeX, int sizeZ)
		{
			if (offsetX < 0)
			{
				sizeX += offsetX;
				offsetX = 0;
			}
			if (offsetZ < 0)
			{
				sizeZ += offsetZ;
				offsetZ = 0;
			}
			int alphamapResolution = data.alphamapResolution;
			if (sizeX + offsetX > alphamapResolution)
			{
				sizeX = alphamapResolution - offsetX;
			}
			if (sizeZ + offsetZ > alphamapResolution)
			{
				sizeZ = alphamapResolution - offsetZ;
			}
			return data.GetAlphamaps(offsetX, offsetZ, sizeX, sizeZ);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000E2D5C File Offset: 0x000E0F5C
		public static List<Type> GetAllChildTypes(this Type type)
		{
			List<Type> list = new List<Type>();
			Type[] types = Assembly.GetAssembly(type).GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].IsSubclassOf(type))
				{
					list.Add(types[i]);
				}
			}
			return list;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000E2DA0 File Offset: 0x000E0FA0
		public static Texture2D ColorTexture(int width, int height, Color color)
		{
			Texture2D texture2D = new Texture2D(width, height);
			Color[] pixels = texture2D.GetPixels(0, 0, width, height);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = color;
			}
			texture2D.SetPixels(0, 0, width, height, pixels);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000189ED File Offset: 0x00016BED
		public static bool Equal(Vector3 v1, Vector3 v2)
		{
			return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00018A28 File Offset: 0x00016C28
		public static bool Equal(Ray r1, Ray r2)
		{
			return Extensions.Equal(r1.origin, r2.origin) && Extensions.Equal(r1.direction, r2.direction);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000E2DE8 File Offset: 0x000E0FE8
		public static void RemoveChildren(this Transform tfm)
		{
			for (int i = tfm.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(tfm.GetChild(i).gameObject);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000E2E1C File Offset: 0x000E101C
		public static Transform FindChildRecursive(this Transform tfm, string name)
		{
			int childCount = tfm.childCount;
			for (int i = 0; i < childCount; i++)
			{
				if (tfm.GetChild(i).name == name)
				{
					return tfm.GetChild(i);
				}
			}
			for (int j = 0; j < childCount; j++)
			{
				Transform transform = tfm.GetChild(j).FindChildRecursive(name);
				if (transform != null)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0000296E File Offset: 0x00000B6E
		public static void ToggleDisplayWireframe(this Transform tfm, bool show)
		{
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000E2E80 File Offset: 0x000E1080
		public static int ToInt(this Coord coord)
		{
			int num = (coord.x < 0) ? (-coord.x) : coord.x;
			int num2 = (coord.z < 0) ? (-coord.z) : coord.z;
			return (((coord.z < 0) ? 1000000000 : 0) + num * 30000 + num2) * ((coord.x < 0) ? -1 : 1);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000E2EE8 File Offset: 0x000E10E8
		public static Coord ToCoord(this int hash)
		{
			int num = (hash < 0) ? (-hash) : hash;
			int num2 = num / 1000000000 * 1000000000;
			int num3 = (num - num2) / 30000;
			int num4 = num - num2 - num3 * 30000;
			return new Coord((hash < 0) ? (-num3) : num3, (num2 == 0) ? num4 : (-num4));
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00018A54 File Offset: 0x00016C54
		public static void CheckAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value, bool replace = true)
		{
			if (dict.ContainsKey(key))
			{
				if (replace)
				{
					dict[key] = value;
					return;
				}
			}
			else
			{
				dict.Add(key, value);
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00018A73 File Offset: 0x00016C73
		public static void CheckRemove<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
		{
			if (dict.ContainsKey(key))
			{
				dict.Remove(key);
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000E2F38 File Offset: 0x000E1138
		public static TValue CheckGet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
		{
			if (dict.ContainsKey(key))
			{
				return dict[key];
			}
			return default(TValue);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00018A86 File Offset: 0x00016C86
		public static void CheckAdd<T>(this HashSet<T> set, T obj)
		{
			if (!set.Contains(obj))
			{
				set.Add(obj);
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00018A99 File Offset: 0x00016C99
		public static void CheckRemove<T>(this HashSet<T> set, T obj)
		{
			if (set.Contains(obj))
			{
				set.Remove(obj);
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00018AAC File Offset: 0x00016CAC
		public static void SetState<T>(this HashSet<T> set, T obj, bool state)
		{
			if (state && !set.Contains(obj))
			{
				set.Add(obj);
			}
			if (!state && set.Contains(obj))
			{
				set.Remove(obj);
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000E2F60 File Offset: 0x000E1160
		public static void Normalize(this float[,,] array, int pinnedLayer)
		{
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			int length3 = array.GetLength(2);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					float num = 0f;
					for (int k = 0; k < length3; k++)
					{
						if (k != pinnedLayer)
						{
							num += array[i, j, k];
						}
					}
					float num2 = array[i, j, pinnedLayer];
					if (num2 > 1f)
					{
						num2 = 1f;
						array[i, j, pinnedLayer] = 1f;
					}
					if (num2 < 0f)
					{
						num2 = 0f;
						array[i, j, pinnedLayer] = 0f;
					}
					float num3 = 1f - num2;
					float num4 = (num > 0f) ? (num3 / num) : 0f;
					for (int l = 0; l < length3; l++)
					{
						if (l != pinnedLayer)
						{
							array[i, j, l] *= num4;
						}
					}
				}
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000E306C File Offset: 0x000E126C
		public static void DrawDebug(this Vector3 pos, float range = 1f, Color color = default(Color))
		{
			if (color.a < 0.001f)
			{
				color = Color.white;
			}
			Debug.DrawLine(pos + new Vector3(-1f, 0f, 1f) * range, pos + new Vector3(1f, 0f, 1f) * range, color);
			Debug.DrawLine(pos + new Vector3(1f, 0f, 1f) * range, pos + new Vector3(1f, 0f, -1f) * range, color);
			Debug.DrawLine(pos + new Vector3(1f, 0f, -1f) * range, pos + new Vector3(-1f, 0f, -1f) * range, color);
			Debug.DrawLine(pos + new Vector3(-1f, 0f, -1f) * range, pos + new Vector3(-1f, 0f, 1f) * range, color);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000E31A8 File Offset: 0x000E13A8
		public static void DrawDebug(this Rect rect, Color color = default(Color))
		{
			if (color.a < 0.001f)
			{
				color = Color.white;
			}
			Debug.DrawLine(new Vector3(rect.x, 0f, rect.y), new Vector3(rect.x + rect.width, 0f, rect.y), color);
			Debug.DrawLine(new Vector3(rect.x + rect.width, 0f, rect.y), new Vector3(rect.x + rect.width, 0f, rect.y + rect.height), color);
			Debug.DrawLine(new Vector3(rect.x + rect.width, 0f, rect.y + rect.height), new Vector3(rect.x, 0f, rect.y + rect.height), color);
			Debug.DrawLine(new Vector3(rect.x, 0f, rect.y + rect.height), new Vector3(rect.x, 0f, rect.y), color);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000E32E4 File Offset: 0x000E14E4
		public static void Resize(this Terrain terrain, int resolution, Vector3 size)
		{
			if ((terrain.terrainData.size - size).sqrMagnitude > 0.01f || terrain.terrainData.heightmapResolution != resolution)
			{
				if (resolution <= 64)
				{
					terrain.terrainData.heightmapResolution = resolution;
					terrain.terrainData.size = new Vector3(size.x, size.y, size.z);
					return;
				}
				terrain.terrainData.heightmapResolution = 65;
				terrain.Flush();
				int num = (resolution - 1) / 64;
				terrain.terrainData.size = new Vector3(size.x / (float)num, size.y, size.z / (float)num);
				terrain.terrainData.heightmapResolution = resolution;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00018AD6 File Offset: 0x00016CD6
		public static Transform AddChild(this Transform tfm, string name = "", Vector3 offset = default(Vector3))
		{
			return new GameObject
			{
				name = name,
				transform = 
				{
					parent = tfm,
					localPosition = offset
				}
			}.transform;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00018B01 File Offset: 0x00016D01
		public static IEnumerable<Vector3> CircleAround(this Vector3 center, float radius, int numPoints, bool endWhereStart = false)
		{
			float radianStep = 6.2831855f / (float)numPoints;
			int num;
			if (endWhereStart)
			{
				num = numPoints;
				numPoints = num + 1;
			}
			for (int i = 0; i < numPoints; i = num + 1)
			{
				float f = (float)i * radianStep;
				Vector3 a = new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
				yield return center + a * radius;
				num = i;
			}
			yield break;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000E33A4 File Offset: 0x000E15A4
		public static float EvaluateMultithreaded(this AnimationCurve curve, float time)
		{
			int num = curve.keys.Length;
			if (time <= curve.keys[0].time)
			{
				return curve.keys[0].value;
			}
			if (time >= curve.keys[num - 1].time)
			{
				return curve.keys[num - 1].value;
			}
			int num2 = 0;
			int num3 = 0;
			while (num3 < num - 1 && curve.keys[num2 + 1].time <= time)
			{
				num2++;
				num3++;
			}
			float num4 = curve.keys[num2 + 1].time - curve.keys[num2].time;
			float num5 = (time - curve.keys[num2].time) / num4;
			float num6 = num5 * num5;
			float num7 = num6 * num5;
			float num8 = 2f * num7 - 3f * num6 + 1f;
			float num9 = num7 - 2f * num6 + num5;
			float num10 = num7 - num6;
			float num11 = -2f * num7 + 3f * num6;
			return num8 * curve.keys[num2].value + num9 * curve.keys[num2].outTangent * num4 + num10 * curve.keys[num2 + 1].inTangent * num4 + num11 * curve.keys[num2 + 1].value;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000E3518 File Offset: 0x000E1718
		public static bool IdenticalTo(this AnimationCurve c1, AnimationCurve c2)
		{
			if (c1 == null || c2 == null)
			{
				return false;
			}
			if (c1.keys.Length != c2.keys.Length)
			{
				return false;
			}
			int num = c1.keys.Length;
			for (int i = 0; i < num; i++)
			{
				if (c1.keys[i].time != c2.keys[i].time || c1.keys[i].value != c2.keys[i].value || c1.keys[i].inTangent != c2.keys[i].inTangent || c1.keys[i].outTangent != c2.keys[i].outTangent)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000E35F0 File Offset: 0x000E17F0
		public static Keyframe[] Copy(this Keyframe[] src)
		{
			Keyframe[] array = new Keyframe[src.Length];
			for (int i = 0; i < src.Length; i++)
			{
				array[i].value = src[i].value;
				array[i].time = src[i].time;
				array[i].inTangent = src[i].inTangent;
				array[i].outTangent = src[i].outTangent;
			}
			return array;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00018B26 File Offset: 0x00016D26
		public static AnimationCurve Copy(this AnimationCurve src)
		{
			return new AnimationCurve
			{
				keys = src.keys.Copy()
			};
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000E3678 File Offset: 0x000E1878
		public static void GetPropertiesFrom<T1, T2>(this T1 dst, T2 src) where T1 : class where T2 : class
		{
			PropertyInfo[] properties = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			PropertyInfo[] properties2 = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
			for (int i = 0; i < properties.Length; i++)
			{
				for (int j = 0; j < properties2.Length; j++)
				{
					if (properties[i].Name == properties2[j].Name && properties2[j].CanWrite)
					{
						properties2[j].SetValue(dst, properties[i].GetValue(src, null), null);
					}
				}
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00018B3E File Offset: 0x00016D3E
		public static IEnumerable<FieldInfo> UsableFields(this Type type, bool nonPublic = false)
		{
			BindingFlags bindingAttr;
			if (nonPublic)
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public);
			}
			FieldInfo[] fields = type.GetFields(bindingAttr);
			int num;
			for (int i = 0; i < fields.Length; i = num + 1)
			{
				FieldInfo fieldInfo = fields[i];
				if (!fieldInfo.IsLiteral && !fieldInfo.FieldType.IsPointer && !fieldInfo.IsNotSerialized)
				{
					yield return fieldInfo;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00018B55 File Offset: 0x00016D55
		public static IEnumerable<PropertyInfo> UsableProperties(this Type type, bool nonPublic = false, bool skipItems = true)
		{
			BindingFlags bindingAttr;
			if (nonPublic)
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public);
			}
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			int num;
			for (int i = 0; i < properties.Length; i = num + 1)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.CanWrite && (!skipItems || !(propertyInfo.Name == "Item")))
				{
					yield return propertyInfo;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00018B73 File Offset: 0x00016D73
		public static IEnumerable<MemberInfo> UsableMembers(this Type type, bool nonPublic = false, bool skipItems = true)
		{
			BindingFlags flags;
			if (nonPublic)
			{
				flags = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			else
			{
				flags = (BindingFlags.Instance | BindingFlags.Public);
			}
			FieldInfo[] fields = type.GetFields(flags);
			int num;
			for (int i = 0; i < fields.Length; i = num + 1)
			{
				FieldInfo fieldInfo = fields[i];
				if (!fieldInfo.IsLiteral && !fieldInfo.FieldType.IsPointer && !fieldInfo.IsNotSerialized)
				{
					yield return fieldInfo;
				}
				num = i;
			}
			PropertyInfo[] properties = type.GetProperties(flags);
			for (int i = 0; i < properties.Length; i = num + 1)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.CanWrite && (!skipItems || !(propertyInfo.Name == "Item")))
				{
					yield return propertyInfo;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000E370C File Offset: 0x000E190C
		public static Component CopyComponent(Component src, GameObject go)
		{
			Type type = src.GetType();
			Component component = go.AddComponent(type);
			foreach (FieldInfo fieldInfo in type.UsableFields(true))
			{
				fieldInfo.SetValue(component, fieldInfo.GetValue(src));
			}
			foreach (PropertyInfo propertyInfo in type.UsableProperties(true, true))
			{
				if (!(propertyInfo.Name == "name"))
				{
					try
					{
						propertyInfo.SetValue(component, propertyInfo.GetValue(src, null), null);
					}
					catch
					{
					}
				}
			}
			return component;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000E37E8 File Offset: 0x000E19E8
		public static void ReflectionReset<T>(this T obj)
		{
			Type type = obj.GetType();
			T t = (T)((object)Activator.CreateInstance(type));
			foreach (FieldInfo fieldInfo in type.UsableFields(true))
			{
				fieldInfo.SetValue(obj, fieldInfo.GetValue(t));
			}
			foreach (PropertyInfo propertyInfo in type.UsableProperties(true, true))
			{
				propertyInfo.SetValue(obj, propertyInfo.GetValue(t, null), null);
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000E38BC File Offset: 0x000E1ABC
		public static T ReflectionCopy<T>(this T obj)
		{
			Type type = obj.GetType();
			T t = (T)((object)Activator.CreateInstance(type));
			foreach (FieldInfo fieldInfo in type.UsableFields(true))
			{
				fieldInfo.SetValue(t, fieldInfo.GetValue(obj));
			}
			foreach (PropertyInfo propertyInfo in type.UsableProperties(true, true))
			{
				propertyInfo.SetValue(t, propertyInfo.GetValue(obj, null), null);
			}
			return t;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000E3990 File Offset: 0x000E1B90
		public static object Parse(this string s, Type t)
		{
			if (s.Contains("="))
			{
				s = s.Remove(0, s.IndexOf('=') + 1);
			}
			object result;
			try
			{
				object obj;
				if (t == typeof(float))
				{
					obj = float.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(int))
				{
					obj = int.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(bool))
				{
					obj = bool.Parse(s);
				}
				else if (t == typeof(string))
				{
					obj = s;
				}
				else if (t == typeof(byte))
				{
					obj = byte.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(short))
				{
					obj = short.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(long))
				{
					obj = long.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(double))
				{
					obj = double.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(char))
				{
					obj = char.Parse(s);
				}
				else if (t == typeof(decimal))
				{
					obj = decimal.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(sbyte))
				{
					obj = sbyte.Parse(s, CultureInfo.InvariantCulture);
				}
				else if (t == typeof(uint))
				{
					obj = uint.Parse(s, CultureInfo.InvariantCulture);
				}
				else
				{
					if (!(t == typeof(ulong)))
					{
						return null;
					}
					obj = ulong.Parse(s, CultureInfo.InvariantCulture);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Tried to deserialize type {0}, value {1}", new object[]
				{
					t.ToString(),
					s
				});
				throw ex;
			}
			return result;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000E3BE8 File Offset: 0x000E1DE8
		public static float GetInterpolated(this float[,] array, float x, float z)
		{
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			int num = (int)x;
			int num2 = num + 1;
			if (num2 >= length)
			{
				num2 = length - 1;
			}
			int num3 = (int)z;
			int num4 = num3 + 1;
			if (num4 >= length2)
			{
				num4 = length2 - 1;
			}
			float num5 = x - (float)num;
			float num6 = z - (float)num3;
			float num7 = array[num, num3] * (1f - num5) + array[num2, num3] * num5;
			float num8 = array[num, num4] * (1f - num5) + array[num2, num4] * num5;
			return num7 * (1f - num6) + num8 * num6;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000E3C84 File Offset: 0x000E1E84
		public static float RectangularDistToRect(this Vector3 pos, Vector3 min, float size)
		{
			float num = min.x - pos.x;
			float num2 = pos.x - min.x - size;
			float num3;
			if (num >= 0f)
			{
				num3 = num;
			}
			else if (num2 >= 0f)
			{
				num3 = num2;
			}
			else
			{
				num3 = 0f;
			}
			float num4 = min.z - pos.z;
			float num5 = pos.z - min.z - size;
			float num6;
			if (num4 >= 0f)
			{
				num6 = num4;
			}
			else if (num5 >= 0f)
			{
				num6 = num5;
			}
			else
			{
				num6 = 0f;
			}
			if (num3 > num6)
			{
				return num3;
			}
			return num6;
		}
	}
}
