using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009C7 RID: 2503
	[Proxy(typeof(GameObject))]
	public class GameObjectProxy : IProxy
	{
		// Token: 0x060044B9 RID: 17593 RVA: 0x0002FC02 File Offset: 0x0002DE02
		[MoonSharpHidden]
		public GameObjectProxy(GameObject value)
		{
			this._value = value;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x0002FC11 File Offset: 0x0002DE11
		public GameObjectProxy(string name)
		{
			this._value = WGameObject.Constructor(name);
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0002FC25 File Offset: 0x0002DE25
		public GameObjectProxy()
		{
			this._value = WGameObject.Constructor();
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0002FC38 File Offset: 0x0002DE38
		public GameObjectProxy(string name, object[] components)
		{
			this._value = WGameObject.Constructor(name, components);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x0002FC4D File Offset: 0x0002DE4D
		public bool activeInHierarchy
		{
			get
			{
				return WGameObject.GetActiveInHierarchy(this._value);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x0002FC5A File Offset: 0x0002DE5A
		public bool activeSelf
		{
			get
			{
				return WGameObject.GetActiveSelf(this._value);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x0002FC67 File Offset: 0x0002DE67
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WGameObject.GetGameObject(this._value));
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x0002FC79 File Offset: 0x0002DE79
		// (set) Token: 0x060044C1 RID: 17601 RVA: 0x0002FC86 File Offset: 0x0002DE86
		public int layer
		{
			get
			{
				return WGameObject.GetLayer(this._value);
			}
			set
			{
				WGameObject.SetLayer(this._value, value);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x0002FC94 File Offset: 0x0002DE94
		// (set) Token: 0x060044C3 RID: 17603 RVA: 0x0002FCA1 File Offset: 0x0002DEA1
		public string name
		{
			get
			{
				return WGameObject.GetName(this._value);
			}
			set
			{
				WGameObject.SetName(this._value, value);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x0002FCAF File Offset: 0x0002DEAF
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x0002FCBC File Offset: 0x0002DEBC
		public string tag
		{
			get
			{
				return WGameObject.GetTag(this._value);
			}
			set
			{
				WGameObject.SetTag(this._value, value);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x0002FCCA File Offset: 0x0002DECA
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WGameObject.GetTransform(this._value));
			}
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x0002FCDC File Offset: 0x0002DEDC
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x00130808 File Offset: 0x0012EA08
		[MoonSharpHidden]
		public static GameObjectProxy New(GameObject value)
		{
			if (value == null)
			{
				return null;
			}
			GameObjectProxy gameObjectProxy = (GameObjectProxy)ObjectCache.Get(typeof(GameObjectProxy), value);
			if (gameObjectProxy == null)
			{
				gameObjectProxy = new GameObjectProxy(value);
				ObjectCache.Add(typeof(GameObjectProxy), value, gameObjectProxy);
			}
			return gameObjectProxy;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x0002FCE4 File Offset: 0x0002DEE4
		[MoonSharpUserDataMetamethod("__call")]
		public static GameObjectProxy Call(DynValue _, string name)
		{
			return new GameObjectProxy(name);
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x0002FCEC File Offset: 0x0002DEEC
		[MoonSharpUserDataMetamethod("__call")]
		public static GameObjectProxy Call(DynValue _)
		{
			return new GameObjectProxy();
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0002FCF3 File Offset: 0x0002DEF3
		[MoonSharpUserDataMetamethod("__call")]
		public static GameObjectProxy Call(DynValue _, string name, object[] components)
		{
			return new GameObjectProxy(name, components);
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x0002FCFC File Offset: 0x0002DEFC
		public bool CompareTag(string tag)
		{
			return WGameObject.CompareTag(this._value, tag);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0002FD0A File Offset: 0x0002DF0A
		public static GameObjectProxy CreatePrimitive(PrimitiveType type)
		{
			return GameObjectProxy.New(WGameObject.CreatePrimitive(type));
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0002FD17 File Offset: 0x0002DF17
		public static void Destroy(DynValue value)
		{
			WGameObject.Destroy(value);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0002FD1F File Offset: 0x0002DF1F
		public static void Destroy(DynValue value, float time)
		{
			WGameObject.Destroy(value, time);
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x0002FD28 File Offset: 0x0002DF28
		public static GameObjectProxy Find(string name)
		{
			return GameObjectProxy.New(WGameObject.Find(name));
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0002FD35 File Offset: 0x0002DF35
		public static GameObject[] FindGameObjectsWithTag(string tag)
		{
			return WGameObject.FindGameObjectsWithTag(tag);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0002FD3D File Offset: 0x0002DF3D
		public static GameObjectProxy FindGameObjectWithTag(string tag)
		{
			return GameObjectProxy.New(WGameObject.FindGameObjectWithTag(tag));
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0002FD4A File Offset: 0x0002DF4A
		public static DynValue FindObjectOfType(Table prototype)
		{
			return WGameObject.FindObjectOfType(prototype);
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x0002FD52 File Offset: 0x0002DF52
		public static object FindObjectOfType(object userData)
		{
			return WGameObject.FindObjectOfType(userData);
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x0002FD5A File Offset: 0x0002DF5A
		public static DynValue[] FindObjectsOfType(Table prototype)
		{
			return WGameObject.FindObjectsOfType(prototype);
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x0002FD62 File Offset: 0x0002DF62
		public static object[] FindObjectsOfType(object userData)
		{
			return WGameObject.FindObjectsOfType(userData);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x0002FD6A File Offset: 0x0002DF6A
		public DynValue GetComponent(Table prototype)
		{
			return WGameObject.GetComponent(this._value, prototype);
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x0002FD78 File Offset: 0x0002DF78
		public object GetComponent(object type)
		{
			return WGameObject.GetComponent(this._value, type);
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0002FD86 File Offset: 0x0002DF86
		public DynValue GetComponentInChildren(Table prototype)
		{
			return WGameObject.GetComponentInChildren(this._value, prototype);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0002FD94 File Offset: 0x0002DF94
		public object GetComponentInChildren(object type)
		{
			return WGameObject.GetComponentInChildren(this._value, type);
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x0002FDA2 File Offset: 0x0002DFA2
		public DynValue GetComponentInParent(Table prototype)
		{
			return WGameObject.GetComponentInParent(this._value, prototype);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x0002FDB0 File Offset: 0x0002DFB0
		public object GetComponentInParent(object type)
		{
			return WGameObject.GetComponentInParent(this._value, type);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0002FDBE File Offset: 0x0002DFBE
		public DynValue[] GetComponents(Table prototype)
		{
			return WGameObject.GetComponents(this._value, prototype);
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0002FDCC File Offset: 0x0002DFCC
		public object[] GetComponents(object type)
		{
			return WGameObject.GetComponents(this._value, type);
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0002FDDA File Offset: 0x0002DFDA
		public DynValue[] GetComponentsInChildren(Table prototype)
		{
			return WGameObject.GetComponentsInChildren(this._value, prototype);
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		public object[] GetComponentsInChildren(object type)
		{
			return WGameObject.GetComponentsInChildren(this._value, type);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0002FDF6 File Offset: 0x0002DFF6
		public DynValue[] GetComponentsInParent(Table prototype)
		{
			return WGameObject.GetComponentsInParent(this._value, prototype);
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0002FE04 File Offset: 0x0002E004
		public object[] GetComponentsInParent(object type)
		{
			return WGameObject.GetComponentsInParent(this._value, type);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0002FE12 File Offset: 0x0002E012
		public static Type GetTypeFromUserData(object userData)
		{
			return WGameObject.GetTypeFromUserData(userData);
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x00130854 File Offset: 0x0012EA54
		public static GameObjectProxy Instantiate(GameObjectProxy prefab)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			return GameObjectProxy.New(WGameObject.Instantiate(prefab2));
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x00130878 File Offset: 0x0012EA78
		public static GameObjectProxy Instantiate(GameObjectProxy prefab, TransformProxy parent)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			return GameObjectProxy.New(WGameObject.Instantiate(prefab2, parent2));
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x001308AC File Offset: 0x0012EAAC
		public static GameObjectProxy Instantiate(GameObjectProxy prefab, Vector3Proxy position, QuaternionProxy rotation)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			return GameObjectProxy.New(WGameObject.Instantiate(prefab2, position._value, rotation._value));
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x001308F8 File Offset: 0x0012EAF8
		public static GameObjectProxy Instantiate(GameObjectProxy prefab, Vector3Proxy position, QuaternionProxy rotation, TransformProxy parent)
		{
			GameObject prefab2 = null;
			if (prefab != null)
			{
				prefab2 = prefab._value;
			}
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			return GameObjectProxy.New(WGameObject.Instantiate(prefab2, position._value, rotation._value, parent2));
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x0002FE1A File Offset: 0x0002E01A
		public void SetActive(bool value)
		{
			WGameObject.SetActive(this._value, value);
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x0002FE28 File Offset: 0x0002E028
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400315F RID: 12639
		[MoonSharpHidden]
		public GameObject _value;
	}
}
