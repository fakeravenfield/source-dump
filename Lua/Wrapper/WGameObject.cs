using System;
using System.Collections.Generic;
using System.Linq;
using Lua.Proxy;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000951 RID: 2385
	[Wrapper(typeof(GameObject), includeTarget = false)]
	[Name("GameObject")]
	public static class WGameObject
	{
		// Token: 0x06003C56 RID: 15446 RVA: 0x0012E328 File Offset: 0x0012C528
		public static Type GetTypeFromUserData(object userData)
		{
			if (userData is Type)
			{
				ProxyAttribute proxyAttribute = ((Type)userData).GetCustomAttributes(false).FirstOrDefault((object a) => a is ProxyAttribute) as ProxyAttribute;
				if (proxyAttribute != null)
				{
					return proxyAttribute.proxiedType;
				}
			}
			return null;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x0012E380 File Offset: 0x0012C580
		private static UnityEngine.Object GetUnityObject(DynValue value)
		{
			UnityEngine.Object result = null;
			if (value.Type == DataType.UserData)
			{
				if (value.UserData.ProxyValueReferenceEvaluatesToNil())
				{
					return null;
				}
				object @object = value.UserData.Object;
				if (@object != null)
				{
					if (@object is IProxy)
					{
						result = ((@object as IProxy).GetValue() as UnityEngine.Object);
					}
					else if (@object is UnityEngine.Object)
					{
						result = (@object as UnityEngine.Object);
					}
				}
			}
			else if (value.Type == DataType.Table)
			{
				ScriptedBehaviour scriptedBehaviour = ScriptedBehaviour.FindInstance(value.Table);
				if (scriptedBehaviour != null)
				{
					result = scriptedBehaviour;
				}
			}
			return result;
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x00028E6E File Offset: 0x0002706E
		public static GameObject Instantiate(GameObject prefab)
		{
			return UnityEngine.Object.Instantiate<GameObject>(prefab);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x00028E76 File Offset: 0x00027076
		public static GameObject Instantiate(GameObject prefab, Transform parent)
		{
			return UnityEngine.Object.Instantiate<GameObject>(prefab, parent);
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x00028E7F File Offset: 0x0002707F
		public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
		{
			return UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00028E89 File Offset: 0x00027089
		public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
		{
			return UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation, parent);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x0012E404 File Offset: 0x0012C604
		[Doc("Destroys the specified Unity Object")]
		public static void Destroy(DynValue value)
		{
			UnityEngine.Object unityObject = WGameObject.GetUnityObject(value);
			if (!(unityObject != null))
			{
				string str = "can't destroy Lua value: ";
				DynValue dynValue = value.ToScalar();
				throw new ScriptRuntimeException(str + ((dynValue != null) ? dynValue.ToString() : null));
			}
			GameObject gameObject = unityObject as GameObject;
			if (gameObject != null && gameObject.CompareTag("No Destroy"))
			{
				try
				{
					ScriptConsole.instance.LogError("Object {0} is tagged '{1}' and cannot be destroyed.", new object[]
					{
						gameObject,
						gameObject.tag
					});
				}
				catch
				{
				}
				return;
			}
			UnityEngine.Object.Destroy(unityObject);
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x0012E4A0 File Offset: 0x0012C6A0
		[Doc("Destroys the Unity Object after a the specified time delay")]
		public static void Destroy(DynValue value, float time)
		{
			UnityEngine.Object unityObject = WGameObject.GetUnityObject(value);
			if (!(unityObject != null))
			{
				throw new ScriptRuntimeException("can't destroy Lua value: " + ((value != null) ? value.ToString() : null));
			}
			GameObject gameObject = unityObject as GameObject;
			if (gameObject != null && gameObject.CompareTag("No Destroy"))
			{
				try
				{
					ScriptConsole.instance.LogError("Object {0} is tagged '{1}' and cannot be destroyed.", new object[]
					{
						gameObject,
						gameObject.tag
					});
				}
				catch
				{
				}
				return;
			}
			UnityEngine.Object.Destroy(unityObject, time);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x0012E538 File Offset: 0x0012C738
		public static DynValue FindObjectOfType(Table prototype)
		{
			ScriptedBehaviour scriptedBehaviour = ScriptedBehaviour.FindOfType(prototype);
			if (scriptedBehaviour != null)
			{
				return scriptedBehaviour.GetSelf();
			}
			return DynValue.Nil;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x0012E564 File Offset: 0x0012C764
		public static object FindObjectOfType(object userData)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(userData);
			if (typeFromUserData != null)
			{
				return UnityEngine.Object.FindObjectOfType(typeFromUserData);
			}
			return null;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x00028E94 File Offset: 0x00027094
		public static DynValue[] FindObjectsOfType(Table prototype)
		{
			return (from s in ScriptedBehaviour.FindAllOfType(prototype)
			select s.GetSelf()).ToArray<DynValue>();
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x0012E58C File Offset: 0x0012C78C
		public static object[] FindObjectsOfType(object userData)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(userData);
			if (typeFromUserData != null)
			{
				return UnityEngine.Object.FindObjectsOfType(typeFromUserData);
			}
			return new object[0];
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x00028EC5 File Offset: 0x000270C5
		public static GameObject Find(string name)
		{
			return GameObject.Find(name);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00028ECD File Offset: 0x000270CD
		public static GameObject[] FindGameObjectsWithTag(string tag)
		{
			return GameObject.FindGameObjectsWithTag(tag);
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x00028ED5 File Offset: 0x000270D5
		public static GameObject FindGameObjectWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x00028EDD File Offset: 0x000270DD
		public static GameObject CreatePrimitive(PrimitiveType type)
		{
			return GameObject.CreatePrimitive(type);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x00028EE5 File Offset: 0x000270E5
		[Getter]
		public static bool GetActiveInHierarchy(GameObject self)
		{
			return self.activeInHierarchy;
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00028EED File Offset: 0x000270ED
		[Getter]
		public static bool GetActiveSelf(GameObject self)
		{
			return self.activeSelf;
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x00028EF5 File Offset: 0x000270F5
		[Getter]
		public static int GetLayer(GameObject self)
		{
			return self.layer;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x00028EFD File Offset: 0x000270FD
		[Setter]
		public static void SetLayer(GameObject self, int value)
		{
			self.layer = value;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x00028F06 File Offset: 0x00027106
		[Getter]
		public static string GetTag(GameObject self)
		{
			return self.tag;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x00028F0E File Offset: 0x0002710E
		[Setter]
		public static void SetTag(GameObject self, string value)
		{
			self.tag = value;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x00028F17 File Offset: 0x00027117
		[Getter]
		public static string GetName(GameObject self)
		{
			return self.name;
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00028F1F File Offset: 0x0002711F
		[Setter]
		public static void SetName(GameObject self, string value)
		{
			self.name = value;
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x00028F28 File Offset: 0x00027128
		[Getter]
		public static Transform GetTransform(GameObject self)
		{
			return self.transform;
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00008D0C File Offset: 0x00006F0C
		[Getter]
		public static GameObject GetGameObject(GameObject self)
		{
			return self;
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x00028F30 File Offset: 0x00027130
		public static bool CompareTag(GameObject self, string tag)
		{
			return self.CompareTag(tag);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x00028F39 File Offset: 0x00027139
		public static void SetActive(GameObject self, bool value)
		{
			self.SetActive(value);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x0012E5B8 File Offset: 0x0012C7B8
		public static DynValue GetComponent(GameObject self, Table prototype)
		{
			ScriptedBehaviour script = ScriptedBehaviour.GetScript(self, prototype);
			if (script != null)
			{
				return script.GetSelf();
			}
			return DynValue.Nil;
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x0012E5E4 File Offset: 0x0012C7E4
		public static DynValue GetComponentInChildren(GameObject self, Table prototype)
		{
			ScriptedBehaviour scriptInChildren = ScriptedBehaviour.GetScriptInChildren(self, prototype);
			if (scriptInChildren != null)
			{
				return scriptInChildren.GetSelf();
			}
			return DynValue.Nil;
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x0012E610 File Offset: 0x0012C810
		public static DynValue GetComponentInParent(GameObject self, Table prototype)
		{
			ScriptedBehaviour scriptInParent = ScriptedBehaviour.GetScriptInParent(self, prototype);
			if (scriptInParent != null)
			{
				return scriptInParent.GetSelf();
			}
			return DynValue.Nil;
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x00028F42 File Offset: 0x00027142
		public static DynValue[] GetComponents(GameObject self, Table prototype)
		{
			return (from s in ScriptedBehaviour.GetScripts(self, prototype)
			select s.GetSelf()).ToArray<DynValue>();
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x00028F74 File Offset: 0x00027174
		public static DynValue[] GetComponentsInChildren(GameObject self, Table prototype)
		{
			return (from s in ScriptedBehaviour.GetScriptsInChildren(self, prototype)
			select s.GetSelf()).ToArray<DynValue>();
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x00028FA6 File Offset: 0x000271A6
		public static DynValue[] GetComponentsInParent(GameObject self, Table prototype)
		{
			return (from s in ScriptedBehaviour.GetScriptsInParent(self, prototype)
			select s.GetSelf()).ToArray<DynValue>();
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0012E63C File Offset: 0x0012C83C
		public static object GetComponent(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponent(typeFromUserData);
			}
			return null;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0012E664 File Offset: 0x0012C864
		public static object GetComponentInChildren(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponentInChildren(typeFromUserData);
			}
			return null;
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x0012E68C File Offset: 0x0012C88C
		public static object GetComponentInParent(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponentInParent(typeFromUserData);
			}
			return null;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x0012E6B4 File Offset: 0x0012C8B4
		public static object[] GetComponents(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponents(typeFromUserData);
			}
			return new object[0];
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x0012E6E4 File Offset: 0x0012C8E4
		public static object[] GetComponentsInChildren(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponentsInChildren(typeFromUserData);
			}
			return new object[0];
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x0012E714 File Offset: 0x0012C914
		public static object[] GetComponentsInParent(GameObject self, object type)
		{
			Type typeFromUserData = WGameObject.GetTypeFromUserData(type);
			if (typeFromUserData != null)
			{
				return self.GetComponentsInParent(typeFromUserData);
			}
			return new object[0];
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00028FD8 File Offset: 0x000271D8
		[Constructor]
		public static GameObject Constructor(string name)
		{
			return new GameObject(name);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00019D84 File Offset: 0x00017F84
		[Constructor]
		public static GameObject Constructor()
		{
			return new GameObject();
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x0012E744 File Offset: 0x0012C944
		[Constructor]
		public static GameObject Constructor(string name, object[] components)
		{
			IEnumerable<Type> source = components.Select(delegate(object userData)
			{
				Type typeFromUserData = WGameObject.GetTypeFromUserData(userData);
				if (typeFromUserData == null)
				{
					throw new ScriptRuntimeException("not a valid type");
				}
				return typeFromUserData;
			});
			return new GameObject(name, source.ToArray<Type>());
		}
	}
}
