using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x020009A3 RID: 2467
	[Wrapper(typeof(WeaponManager.WeaponEntry))]
	[Name("WeaponEntry")]
	public static class WWeaponEntry
	{
		// Token: 0x06003F33 RID: 16179 RVA: 0x0002AB36 File Offset: 0x00028D36
		[Getter]
		public static WeaponManager.WeaponEntry.Distance GetDistance(WeaponManager.WeaponEntry self)
		{
			return self.distance;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x0002AB3E File Offset: 0x00028D3E
		[Getter]
		public static GameObject GetPrefab(WeaponManager.WeaponEntry self)
		{
			return self.prefab;
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0012F634 File Offset: 0x0012D834
		[Getter]
		public static Weapon GetPrefabWeapon(WeaponManager.WeaponEntry self)
		{
			try
			{
				return self.prefab.GetComponent<Weapon>();
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0002AB46 File Offset: 0x00028D46
		[Getter]
		public static string GetName(WeaponManager.WeaponEntry self)
		{
			return self.name;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0002AB4E File Offset: 0x00028D4E
		[Getter]
		public static WeaponManager.WeaponSlot GetSlot(WeaponManager.WeaponEntry self)
		{
			return self.slot;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0002AB56 File Offset: 0x00028D56
		[Getter]
		[Doc("The list of weapon tags, such as ``Assault``, ``Marksman`` etc.")]
		public static string[] GetTags(WeaponManager.WeaponEntry self)
		{
			return self.tags;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0002AB5E File Offset: 0x00028D5E
		[Getter]
		public static WeaponManager.WeaponEntry.LoadoutType GetType(WeaponManager.WeaponEntry self)
		{
			return self.type;
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0002AB66 File Offset: 0x00028D66
		[Getter]
		public static bool GetIsUsableByAi(WeaponManager.WeaponEntry self)
		{
			return self.usableByAi;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0012F668 File Offset: 0x0012D868
		[Doc("Instantiates an imposter object of the weapon prefab.[..] The imposter object contains the third person transform of the weapon prefab, and has its ``Weapon`` component culled.")]
		public static GameObject InstantiateImposter(WeaponManager.WeaponEntry self, Vector3 position, Quaternion rotation)
		{
			Transform transform;
			UnityEngine.Object obj = self.prefab.GetComponent<Weapon>().CreateTpImposter(out transform);
			transform.parent = null;
			transform.position = position;
			transform.rotation = rotation;
			UnityEngine.Object.Destroy(obj);
			return transform.gameObject;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0002AB6E File Offset: 0x00028D6E
		[Getter]
		public static Sprite GetUiSprite(WeaponManager.WeaponEntry self)
		{
			return self.uiSprite;
		}
	}
}
