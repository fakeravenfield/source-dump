using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x02000A2C RID: 2604
	[Proxy(typeof(WeaponManager.WeaponEntry))]
	public class WeaponEntryProxy : IProxy
	{
		// Token: 0x0600530D RID: 21261 RVA: 0x0003D30B File Offset: 0x0003B50B
		[MoonSharpHidden]
		public WeaponEntryProxy(WeaponManager.WeaponEntry value)
		{
			this._value = value;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x0003D31A File Offset: 0x0003B51A
		public WeaponManager.WeaponEntry.Distance distance
		{
			get
			{
				return WWeaponEntry.GetDistance(this._value);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600530F RID: 21263 RVA: 0x0003D327 File Offset: 0x0003B527
		public bool isUsableByAi
		{
			get
			{
				return WWeaponEntry.GetIsUsableByAi(this._value);
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x0003D334 File Offset: 0x0003B534
		public string name
		{
			get
			{
				return WWeaponEntry.GetName(this._value);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06005311 RID: 21265 RVA: 0x0003D341 File Offset: 0x0003B541
		public GameObjectProxy prefab
		{
			get
			{
				return GameObjectProxy.New(WWeaponEntry.GetPrefab(this._value));
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x0003D353 File Offset: 0x0003B553
		public WeaponProxy prefabWeapon
		{
			get
			{
				return WeaponProxy.New(WWeaponEntry.GetPrefabWeapon(this._value));
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06005313 RID: 21267 RVA: 0x0003D365 File Offset: 0x0003B565
		public WeaponManager.WeaponSlot slot
		{
			get
			{
				return WWeaponEntry.GetSlot(this._value);
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x0003D372 File Offset: 0x0003B572
		public string[] tags
		{
			get
			{
				return WWeaponEntry.GetTags(this._value);
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x0003D37F File Offset: 0x0003B57F
		public WeaponManager.WeaponEntry.LoadoutType type
		{
			get
			{
				return WWeaponEntry.GetType(this._value);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x0003D38C File Offset: 0x0003B58C
		public SpriteProxy uiSprite
		{
			get
			{
				return SpriteProxy.New(WWeaponEntry.GetUiSprite(this._value));
			}
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0003D39E File Offset: 0x0003B59E
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x00139790 File Offset: 0x00137990
		[MoonSharpHidden]
		public static WeaponEntryProxy New(WeaponManager.WeaponEntry value)
		{
			if (value == null)
			{
				return null;
			}
			WeaponEntryProxy weaponEntryProxy = (WeaponEntryProxy)ObjectCache.Get(typeof(WeaponEntryProxy), value);
			if (weaponEntryProxy == null)
			{
				weaponEntryProxy = new WeaponEntryProxy(value);
				ObjectCache.Add(typeof(WeaponEntryProxy), value, weaponEntryProxy);
			}
			return weaponEntryProxy;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0003D3A6 File Offset: 0x0003B5A6
		public GameObjectProxy InstantiateImposter(Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			return GameObjectProxy.New(WWeaponEntry.InstantiateImposter(this._value, position._value, rotation._value));
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x040032A8 RID: 12968
		[MoonSharpHidden]
		public WeaponManager.WeaponEntry _value;
	}
}
