using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006BB RID: 1723
	public static class UtilsUI
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x00101B90 File Offset: 0x000FFD90
		public static Vector2 GetSize(RectTransform rt)
		{
			Vector3[] array = new Vector3[4];
			rt.GetWorldCorners(array);
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float num3 = float.MaxValue;
			float num4 = float.MinValue;
			foreach (Vector3 vector in array)
			{
				num = Mathf.Min(num, vector.x);
				num2 = Mathf.Max(num2, vector.x);
				num3 = Mathf.Min(num3, vector.y);
				num4 = Mathf.Max(num4, vector.y);
			}
			return new Vector2(num2 - num, num4 - num3);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00101C2C File Offset: 0x000FFE2C
		public static Rect GetBoundingRect(IEnumerable<RectTransform> transforms)
		{
			if (transforms == null || !transforms.Any<RectTransform>())
			{
				throw new ArgumentException("No transforms given", "rtArray");
			}
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float num3 = float.MaxValue;
			float num4 = float.MinValue;
			foreach (RectTransform rectTransform in transforms)
			{
				Vector3[] array = new Vector3[4];
				rectTransform.GetWorldCorners(array);
				foreach (Vector3 vector in array)
				{
					num = Mathf.Min(num, vector.x);
					num2 = Mathf.Max(num2, vector.x);
					num3 = Mathf.Min(num3, vector.y);
					num4 = Mathf.Max(num4, vector.y);
				}
			}
			float width = num2 - num;
			float height = num4 - num3;
			return new Rect(num, num3, width, height);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0001DE60 File Offset: 0x0001C060
		public static RectTransform[] GetChildren(RectTransform rt)
		{
			return rt.GetComponentsInChildren<RectTransform>().Except(new RectTransform[]
			{
				rt
			}).ToArray<RectTransform>();
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00101D24 File Offset: 0x000FFF24
		public static void EnableRayCastTargets(GameObject go, bool enable)
		{
			Image[] componentsInChildren = go.GetComponentsInChildren<Image>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].raycastTarget = enable;
			}
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00101D50 File Offset: 0x000FFF50
		public static void SuspendLayout(GameObject go)
		{
			ContentSizeFitter[] componentsInChildren = go.GetComponentsInChildren<ContentSizeFitter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
			UtilsUI.EnableLayoutGroups(go, false);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00101D84 File Offset: 0x000FFF84
		public static void ResumeLayout(GameObject go)
		{
			ContentSizeFitter[] componentsInChildren = go.GetComponentsInChildren<ContentSizeFitter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
			UtilsUI.EnableLayoutGroups(go, true);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00101DB8 File Offset: 0x000FFFB8
		public static void EnableLayoutGroups(GameObject go, bool enable)
		{
			List<UtilsUI.LayoutGroupInfo> list = new List<UtilsUI.LayoutGroupInfo>();
			LayoutGroup component = go.GetComponent<LayoutGroup>();
			if (component)
			{
				list.Add(new UtilsUI.LayoutGroupInfo
				{
					depth = 0,
					group = component
				});
			}
			UtilsUI.FindLayoutGroups(go, 1, ref list);
			UtilsUI.LayoutGroupInfo[] array;
			if (enable)
			{
				array = (from x in list
				orderby x.depth
				select x).ToArray<UtilsUI.LayoutGroupInfo>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].group.enabled = true;
				}
				return;
			}
			array = (from x in list
			orderby x.depth descending
			select x).ToArray<UtilsUI.LayoutGroupInfo>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].group.enabled = false;
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00101EA8 File Offset: 0x001000A8
		private static void FindLayoutGroups(GameObject go, int depth, ref List<UtilsUI.LayoutGroupInfo> groups)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				GameObject gameObject = go.transform.GetChild(i).gameObject;
				LayoutGroup component = gameObject.GetComponent<LayoutGroup>();
				if (component)
				{
					groups.Add(new UtilsUI.LayoutGroupInfo
					{
						depth = depth,
						group = component
					});
				}
				UtilsUI.FindLayoutGroups(gameObject, depth + 1, ref groups);
			}
		}

		// Token: 0x020006BC RID: 1724
		private struct LayoutGroupInfo
		{
			// Token: 0x04002843 RID: 10307
			public int depth;

			// Token: 0x04002844 RID: 10308
			public LayoutGroup group;
		}
	}
}
