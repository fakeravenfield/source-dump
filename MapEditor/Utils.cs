using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000730 RID: 1840
	public static class Utils
	{
		// Token: 0x06002E23 RID: 11811 RVA: 0x00107BE8 File Offset: 0x00105DE8
		public static Bounds GetBounds(GameObject go)
		{
			Bounds bounds = new Bounds(go.transform.position, Vector3.zero);
			Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
			if (componentsInChildren.Any<Renderer>())
			{
				bounds = componentsInChildren.First<Renderer>().bounds;
				foreach (Renderer renderer in componentsInChildren)
				{
					bounds.Encapsulate(renderer.bounds);
				}
			}
			return bounds;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x00107C4C File Offset: 0x00105E4C
		public static float GetBottomOffset(GameObject go)
		{
			Bounds bounds = Utils.GetBounds(go);
			return go.transform.InverseTransformPoint(bounds.min).y;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x00107C78 File Offset: 0x00105E78
		public static Transform[] GetChildren(GameObject go)
		{
			Transform[] array = new Transform[go.transform.childCount];
			for (int i = 0; i < go.transform.childCount; i++)
			{
				array[i] = go.transform.GetChild(i);
			}
			return array;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x0001FD37 File Offset: 0x0001DF37
		public static IEnumerable<Transform> GetAllChildren(GameObject go)
		{
			int num;
			for (int i = 0; i < go.transform.childCount; i = num + 1)
			{
				Transform child = go.transform.GetChild(i);
				yield return child;
				foreach (Transform transform in Utils.GetAllChildren(child.gameObject))
				{
					yield return transform;
				}
				IEnumerator<Transform> enumerator = null;
				child = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00107CBC File Offset: 0x00105EBC
		public static void DestroyChildren(GameObject go)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(go.transform.GetChild(i).gameObject);
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00107CF8 File Offset: 0x00105EF8
		public static void SetChildrenActive(GameObject go, bool active)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				go.transform.GetChild(i).gameObject.SetActive(active);
			}
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x00107D34 File Offset: 0x00105F34
		public static void MoveToLayer(GameObject go, int layer)
		{
			foreach (Transform transform in Utils.GetAllChildren(go))
			{
				transform.gameObject.layer = layer;
			}
			go.gameObject.layer = layer;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x00107D90 File Offset: 0x00105F90
		public static void SetStatic(GameObject go, bool isStatic)
		{
			foreach (Transform transform in Utils.GetAllChildren(go))
			{
				transform.gameObject.isStatic = isStatic;
			}
			go.isStatic = isStatic;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x00107DE8 File Offset: 0x00105FE8
		public static void CopyLocalTransform(Transform source, Transform dest)
		{
			dest.transform.localPosition = source.transform.localPosition;
			dest.transform.localRotation = source.transform.localRotation;
			dest.transform.localScale = source.transform.localScale;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00107E38 File Offset: 0x00106038
		public static GameObject Instantiate(GameObject source, Transform parent, Type[] types, bool destroyEmpty = false)
		{
			GameObject gameObject = new GameObject();
			gameObject.SetActive(false);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(source, gameObject.transform);
			Component[] componentsInChildren = gameObject2.GetComponentsInChildren<Component>();
			Component[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Stairways stairways = array[i] as Stairways;
				if (stairways && !stairways.IsBuilt())
				{
					stairways.Build();
				}
			}
			using (IEnumerator<Component> enumerator = (from c in componentsInChildren
			orderby Utils.RemoveOrder(c) descending
			select c).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Component component = enumerator.Current;
					if (!(from t in types
					where t.IsInstanceOfType(component)
					select t).Any<Type>())
					{
						UnityEngine.Object.DestroyImmediate(component);
					}
				}
			}
			if (destroyEmpty)
			{
				Utils.DestroyEmptyChildren(gameObject2.transform);
			}
			gameObject2.transform.parent = parent;
			Utils.CopyLocalTransform(source.transform, gameObject2.transform);
			UnityEngine.Object.DestroyImmediate(gameObject);
			return gameObject2;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x00107F64 File Offset: 0x00106164
		private static bool DestroyEmptyChildren(Transform parent)
		{
			bool flag = true;
			foreach (Transform transform in Utils.GetChildren(parent.gameObject))
			{
				if (Utils.DestroyEmptyChildren(transform))
				{
					UnityEngine.Object.DestroyImmediate(transform.gameObject);
				}
				else
				{
					flag = false;
				}
			}
			if (flag || parent.childCount == 0)
			{
				flag = !(from c in parent.GetComponents<Component>()
				where !(c is Transform)
				select c).Any<Component>();
			}
			return flag;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x0001FD47 File Offset: 0x0001DF47
		private static int RemoveOrder(Component c)
		{
			if (c is Stairways)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x00107FE8 File Offset: 0x001061E8
		public static GameObject CloneGameObject(GameObject source, Transform parent, params Type[] types)
		{
			GameObject gameObject = new GameObject();
			gameObject.SetActive(false);
			GameObject gameObject2 = new GameObject(source.name);
			gameObject2.transform.parent = gameObject.transform;
			Utils.CopyComponents(source, gameObject2, types);
			for (int i = 0; i < source.transform.childCount; i++)
			{
				Utils.CloneGameObject(source.transform.GetChild(i).gameObject, gameObject2.transform, types);
			}
			gameObject2.transform.parent = parent;
			Utils.CopyLocalTransform(source.transform, gameObject2.transform);
			UnityEngine.Object.DestroyImmediate(gameObject);
			return gameObject2;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x00108080 File Offset: 0x00106280
		private static void CopyComponents(GameObject source, GameObject dest, params Type[] types)
		{
			Component[] components = source.GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
			{
				Component component = components[i];
				if ((from t in types
				where t.IsInstanceOfType(component)
				select t).Any<Type>() && !(component is Transform))
				{
					Component component2 = dest.AddComponent(component.GetType());
					if (!Utils.CopyProperties(component, component2))
					{
						UnityEngine.Object.DestroyImmediate(component2);
					}
				}
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x00108100 File Offset: 0x00106300
		private static bool CopyProperties(Component source, Component dest)
		{
			Type type = source.GetType();
			if (type != dest.GetType())
			{
				return false;
			}
			while (type != typeof(Component))
			{
				if (!Utils.CopyProperties(source, dest, type))
				{
					return false;
				}
				type = type.BaseType;
			}
			return true;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0010814C File Offset: 0x0010634C
		private static bool CopyProperties(Component source, Component dest, Type type)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			foreach (PropertyInfo propertyInfo in type.GetProperties(bindingAttr))
			{
				if (!Utils.IsPropertyForbidden(type, propertyInfo.Name))
				{
					try
					{
						if (propertyInfo.CanWrite && propertyInfo.CanRead)
						{
							propertyInfo.SetValue(dest, propertyInfo.GetValue(source, null), null);
						}
					}
					catch
					{
					}
				}
			}
			foreach (FieldInfo fieldInfo in type.GetFields(bindingAttr))
			{
				fieldInfo.SetValue(dest, fieldInfo.GetValue(source));
			}
			return true;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x001081E8 File Offset: 0x001063E8
		private static bool IsPropertyForbidden(Type type, string name)
		{
			return (from f in Utils.FORBIDDEN_PROPERTIES
			where f.type == type && f.name == name
			select f).Any<Utils.ForbiddenProperty>();
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00108224 File Offset: 0x00106424
		public static string ColorToHex(Color color)
		{
			int num = Mathf.RoundToInt(color.r * 255f);
			int num2 = Mathf.RoundToInt(color.g * 255f);
			int num3 = Mathf.RoundToInt(color.b * 255f);
			int num4 = Mathf.RoundToInt(color.a * 255f);
			return string.Format("#{0:X02}{1:X02}{2:X02}{3:X02}", new object[]
			{
				num,
				num2,
				num3,
				num4
			});
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x001082B0 File Offset: 0x001064B0
		public static Color HexToColor(string hex)
		{
			int num = 255;
			int num2 = 255;
			int num3 = 255;
			int num4 = 255;
			hex = hex.Trim(new char[]
			{
				'#',
				' ',
				'\t',
				'\n',
				'\r'
			});
			try
			{
				if (hex.Length == 3)
				{
					num = int.Parse(hex[0].ToString() + hex[0].ToString(), NumberStyles.HexNumber);
					num2 = int.Parse(hex[1].ToString() + hex[1].ToString(), NumberStyles.HexNumber);
					num3 = int.Parse(hex[2].ToString() + hex[2].ToString(), NumberStyles.HexNumber);
				}
				else if (hex.Length == 6 || hex.Length == 8)
				{
					num = int.Parse(hex[0].ToString() + hex[1].ToString(), NumberStyles.HexNumber);
					num2 = int.Parse(hex[2].ToString() + hex[3].ToString(), NumberStyles.HexNumber);
					num3 = int.Parse(hex[4].ToString() + hex[5].ToString(), NumberStyles.HexNumber);
					if (hex.Length == 8)
					{
						num4 = int.Parse(hex[6].ToString() + hex[7].ToString(), NumberStyles.HexNumber);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				Debug.LogErrorFormat("Unable to parse HEX value as color: {0}", new object[]
				{
					hex
				});
			}
			float r = Mathf.Clamp01((float)num / 255f);
			float g = Mathf.Clamp01((float)num2 / 255f);
			float b = Mathf.Clamp01((float)num3 / 255f);
			float a = Mathf.Clamp01((float)num4 / 255f);
			return new Color(r, g, b, a);
		}

		// Token: 0x04002A47 RID: 10823
		private static readonly Utils.ForbiddenProperty[] FORBIDDEN_PROPERTIES = new Utils.ForbiddenProperty[]
		{
			new Utils.ForbiddenProperty(typeof(MeshFilter), "mesh"),
			new Utils.ForbiddenProperty(typeof(Renderer), "material"),
			new Utils.ForbiddenProperty(typeof(Renderer), "materials"),
			new Utils.ForbiddenProperty(typeof(Renderer), "sharedMaterial"),
			new Utils.ForbiddenProperty(typeof(ParticleSystem), "randomSeed")
		};

		// Token: 0x02000731 RID: 1841
		private struct ForbiddenProperty
		{
			// Token: 0x06002E37 RID: 11831 RVA: 0x0001FD54 File Offset: 0x0001DF54
			public ForbiddenProperty(Type type, string name)
			{
				this.type = type;
				this.name = name;
			}

			// Token: 0x04002A48 RID: 10824
			public Type type;

			// Token: 0x04002A49 RID: 10825
			public string name;
		}
	}
}
