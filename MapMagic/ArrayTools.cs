using System;
using System.Collections.Generic;

namespace MapMagic
{
	// Token: 0x02000554 RID: 1364
	public static class ArrayTools
	{
		// Token: 0x06002263 RID: 8803 RVA: 0x000DBB14 File Offset: 0x000D9D14
		public static int Find(this Array array, object obj)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array.GetValue(i) == obj)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000DBB40 File Offset: 0x000D9D40
		public static int Find<T>(T[] array, T obj) where T : class
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == obj)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000181F6 File Offset: 0x000163F6
		public static void RemoveAt<T>(ref T[] array, int num)
		{
			array = ArrayTools.RemoveAt<T>(array, num);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000DBB74 File Offset: 0x000D9D74
		public static T[] RemoveAt<T>(T[] array, int num)
		{
			T[] array2 = new T[array.Length - 1];
			for (int i = 0; i < array2.Length; i++)
			{
				if (i < num)
				{
					array2[i] = array[i];
				}
				else
				{
					array2[i] = array[i + 1];
				}
			}
			return array2;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00018202 File Offset: 0x00016402
		public static void Remove<T>(ref T[] array, T obj) where T : class
		{
			array = ArrayTools.Remove<T>(array, obj);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000DBBC0 File Offset: 0x000D9DC0
		public static T[] Remove<T>(T[] array, T obj) where T : class
		{
			int num = ArrayTools.Find<T>(array, obj);
			return ArrayTools.RemoveAt<T>(array, num);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0001820E File Offset: 0x0001640E
		public static void Add<T>(ref T[] array, int after, T element = default(T))
		{
			array = ArrayTools.Add<T>(array, after, element);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000DBBDC File Offset: 0x000D9DDC
		public static T[] Add<T>(T[] array, int after, T element = default(T))
		{
			if (array == null || array.Length == 0)
			{
				return new T[]
				{
					element
				};
			}
			if (after > array.Length - 1)
			{
				after = array.Length - 1;
			}
			T[] array2 = new T[array.Length + 1];
			for (int i = 0; i < array2.Length; i++)
			{
				if (i <= after)
				{
					array2[i] = array[i];
				}
				else if (i == after + 1)
				{
					array2[i] = element;
				}
				else
				{
					array2[i] = array[i - 1];
				}
			}
			return array2;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0001821B File Offset: 0x0001641B
		public static T[] Add<T>(T[] array, T element = default(T))
		{
			return ArrayTools.Add<T>(array, array.Length - 1, element);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00018229 File Offset: 0x00016429
		public static void Add<T>(ref T[] array, T element = default(T))
		{
			array = ArrayTools.Add<T>(array, array.Length - 1, element);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0001823B File Offset: 0x0001643B
		public static void Resize<T>(ref T[] array, int newSize, T element = default(T))
		{
			array = ArrayTools.Resize<T>(array, newSize, element);
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000DBC5C File Offset: 0x000D9E5C
		public static T[] Resize<T>(T[] array, int newSize, T element = default(T))
		{
			if (array.Length == newSize)
			{
				return array;
			}
			if (newSize > array.Length)
			{
				array = ArrayTools.Add<T>(array, element);
				array = ArrayTools.Resize<T>(array, newSize, default(T));
				return array;
			}
			array = ArrayTools.RemoveAt<T>(array, array.Length - 1);
			array = ArrayTools.Resize<T>(array, newSize, default(T));
			return array;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00018248 File Offset: 0x00016448
		public static void Append<T>(ref T[] array, T[] additional)
		{
			array = ArrayTools.Append<T>(array, additional);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000DBCB4 File Offset: 0x000D9EB4
		public static T[] Append<T>(T[] array, T[] additional)
		{
			T[] array2 = new T[array.Length + additional.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i];
			}
			for (int j = 0; j < additional.Length; j++)
			{
				array2[j + array.Length] = additional[j];
			}
			return array2;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000DBD0C File Offset: 0x000D9F0C
		public static void Switch<T>(T[] array, int num1, int num2)
		{
			if (num1 < 0 || num1 >= array.Length || num2 < 0 || num2 >= array.Length)
			{
				return;
			}
			T t = array[num1];
			array[num1] = array[num2];
			array[num2] = t;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000DBD4C File Offset: 0x000D9F4C
		public static void Switch<T>(T[] array, T obj1, T obj2) where T : class
		{
			int num = ArrayTools.Find<T>(array, obj1);
			int num2 = ArrayTools.Find<T>(array, obj2);
			ArrayTools.Switch<T>(array, num, num2);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00018254 File Offset: 0x00016454
		public static void QSort(float[] array)
		{
			ArrayTools.QSort(array, 0, array.Length - 1);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000DBD74 File Offset: 0x000D9F74
		public static void QSort(float[] array, int l, int r)
		{
			float num = array[l + (r - l) / 2];
			int i = l;
			int num2 = r;
			while (i <= num2)
			{
				while (array[i] < num)
				{
					i++;
				}
				while (array[num2] > num)
				{
					num2--;
				}
				if (i <= num2)
				{
					float num3 = array[i];
					array[i] = array[num2];
					array[num2] = num3;
					i++;
					num2--;
				}
			}
			if (i < r)
			{
				ArrayTools.QSort(array, i, r);
			}
			if (l < num2)
			{
				ArrayTools.QSort(array, l, num2);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00018262 File Offset: 0x00016462
		public static void QSort<T>(T[] array, float[] reference)
		{
			ArrayTools.QSort<T>(array, reference, 0, reference.Length - 1);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000DBDE0 File Offset: 0x000D9FE0
		public static void QSort<T>(T[] array, float[] reference, int l, int r)
		{
			float num = reference[l + (r - l) / 2];
			int i = l;
			int num2 = r;
			while (i <= num2)
			{
				while (reference[i] < num)
				{
					i++;
				}
				while (reference[num2] > num)
				{
					num2--;
				}
				if (i <= num2)
				{
					float num3 = reference[i];
					reference[i] = reference[num2];
					reference[num2] = num3;
					T t = array[i];
					array[i] = array[num2];
					array[num2] = t;
					i++;
					num2--;
				}
			}
			if (i < r)
			{
				ArrayTools.QSort<T>(array, reference, i, r);
			}
			if (l < num2)
			{
				ArrayTools.QSort<T>(array, reference, l, num2);
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00018271 File Offset: 0x00016471
		public static void QSort<T>(List<T> list, float[] reference)
		{
			ArrayTools.QSort<T>(list, reference, 0, reference.Length - 1);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000DBE6C File Offset: 0x000DA06C
		public static void QSort<T>(List<T> list, float[] reference, int l, int r)
		{
			float num = reference[l + (r - l) / 2];
			int i = l;
			int num2 = r;
			while (i <= num2)
			{
				while (reference[i] < num)
				{
					i++;
				}
				while (reference[num2] > num)
				{
					num2--;
				}
				if (i <= num2)
				{
					float num3 = reference[i];
					reference[i] = reference[num2];
					reference[num2] = num3;
					T value = list[i];
					list[i] = list[num2];
					list[num2] = value;
					i++;
					num2--;
				}
			}
			if (i < r)
			{
				ArrayTools.QSort<T>(list, reference, i, r);
			}
			if (l < num2)
			{
				ArrayTools.QSort<T>(list, reference, l, num2);
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000DBEF8 File Offset: 0x000DA0F8
		public static int[] Order(int[] array, int[] order = null, int max = 0, int steps = 1000000, int[] stepsArray = null)
		{
			if (max == 0)
			{
				max = array.Length;
			}
			if (stepsArray == null)
			{
				stepsArray = new int[steps + 1];
			}
			else
			{
				steps = stepsArray.Length - 1;
			}
			int[] array2 = new int[steps + 1];
			for (int i = 0; i < max; i++)
			{
				array2[array[i]]++;
			}
			int num = 0;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] += num;
				num = array2[j];
			}
			for (int k = array2.Length - 1; k > 0; k--)
			{
				array2[k] = array2[k - 1];
			}
			array2[0] = 0;
			if (order == null)
			{
				order = new int[max];
			}
			for (int l = 0; l < max; l++)
			{
				int num2 = array[l];
				int num3 = array2[num2];
				order[num3] = l;
				array2[num2]++;
			}
			return order;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000DBFC8 File Offset: 0x000DA1C8
		public static T[] Convert<T, Y>(Y[] src)
		{
			T[] array = new T[src.Length];
			for (int i = 0; i < src.Length; i++)
			{
				array[i] = (T)((object)src[i]);
			}
			return array;
		}
	}
}
