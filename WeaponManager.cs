using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class WeaponManager : MonoBehaviour
{
	// Token: 0x06000862 RID: 2146 RVA: 0x00067ED4 File Offset: 0x000660D4
	private void Awake()
	{
		WeaponManager.instance = this;
		foreach (WeaponManager.WeaponEntry weaponEntry in this.weapons)
		{
			weaponEntry.sourceMod = ModInformation.OfficialContent;
			weaponEntry.UpdateUiSpriteFromPrefab();
		}
		WeaponManager.ClearContentModWeapons();
		this.CalculateWeaponNameHashes();
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00067F40 File Offset: 0x00066140
	public void CalculateWeaponNameHashes()
	{
		foreach (WeaponManager.WeaponEntry weaponEntry in this.allWeapons)
		{
			if (weaponEntry.nameHash == -1)
			{
				weaponEntry.nameHash = weaponEntry.name.GetHashCode();
			}
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00067FA8 File Offset: 0x000661A8
	public void SortWeaponEntries()
	{
		foreach (WeaponManager.WeaponEntry weaponEntry in this.allWeapons)
		{
			if (weaponEntry.sourceMod == null)
			{
				Debug.LogErrorFormat("Could not determine source mod of weapon entry {0}", new object[]
				{
					weaponEntry.name
				});
				weaponEntry.sourceMod = ModInformation.Unknown;
			}
		}
		this.allWeapons.Sort();
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x000076B0 File Offset: 0x000058B0
	public static void AddWeaponEntryIfNotAlreadyAdded(WeaponManager.WeaponEntry weaponEntry)
	{
		if (!WeaponManager.instance.allWeapons.Contains(weaponEntry))
		{
			weaponEntry.UpdateUiSpriteFromPrefab();
			weaponEntry.sourceMod = ModInformation.OfficialContent;
			WeaponManager.instance.allWeapons.Add(weaponEntry);
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x000076E5 File Offset: 0x000058E5
	public static void UnlockSecretWeapon()
	{
		if (!PlayerPrefs.HasKey("railgun unlock"))
		{
			PlayerPrefs.SetInt("railgun unlock", 1);
			PlayerPrefs.Save();
		}
		WeaponManager.AddWeaponEntryIfNotAlreadyAdded(GameManager.IsConnectedToSteam() ? WeaponManager.instance.secretWeapon : WeaponManager.instance.secretMemeWeapon);
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00007725 File Offset: 0x00005925
	public static void UnlockSecretHalloweenWeapon()
	{
		if (!PlayerPrefs.HasKey("sword unlock"))
		{
			PlayerPrefs.SetInt("sword unlock", 1);
			PlayerPrefs.Save();
		}
		WeaponManager.AddWeaponEntryIfNotAlreadyAdded(WeaponManager.instance.secretHalloweenWeapon);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00007752 File Offset: 0x00005952
	public static void ClearContentModWeapons()
	{
		WeaponManager.instance.allWeapons = new List<WeaponManager.WeaponEntry>(WeaponManager.instance.weapons);
		if (PlayerPrefs.HasKey("railgun unlock"))
		{
			WeaponManager.UnlockSecretWeapon();
		}
		if (PlayerPrefs.HasKey("sword unlock"))
		{
			WeaponManager.UnlockSecretHalloweenWeapon();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0006802C File Offset: 0x0006622C
	public static bool CanBeUsedByAi(WeaponManager.WeaponEntry entry, int team)
	{
		if (entry.slot == WeaponManager.WeaponSlot.Primary)
		{
			return WeaponManager.instance.aiPrimary[team].Contains(entry);
		}
		if (entry.slot == WeaponManager.WeaponSlot.Secondary)
		{
			return WeaponManager.instance.aiSecondary[team].Contains(entry);
		}
		return WeaponManager.instance.aiAllGear[team].Contains(entry);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00068084 File Offset: 0x00066284
	public void SetupAiWeaponEntries(List<WeaponManager.WeaponEntry> entries)
	{
		this.aiPrimary = new List<WeaponManager.WeaponEntry>[2];
		this.aiSecondary = new List<WeaponManager.WeaponEntry>[2];
		this.aiAllGear = new List<WeaponManager.WeaponEntry>[2];
		this.aiSmallGear = new List<WeaponManager.WeaponEntry>[2];
		this.aiLargeGear = new List<WeaponManager.WeaponEntry>[2];
		for (int i = 0; i <= 1; i++)
		{
			this.aiPrimary[i] = new List<WeaponManager.WeaponEntry>();
			this.aiSecondary[i] = new List<WeaponManager.WeaponEntry>();
			this.aiAllGear[i] = new List<WeaponManager.WeaponEntry>();
			this.aiSmallGear[i] = new List<WeaponManager.WeaponEntry>();
			this.aiLargeGear[i] = new List<WeaponManager.WeaponEntry>();
			foreach (WeaponManager.WeaponEntry weaponEntry in entries)
			{
				if (GameManager.instance.gameInfo.team[i].IsWeaponEntryAvailable(weaponEntry) && weaponEntry.usableByAi)
				{
					if (weaponEntry.slot == WeaponManager.WeaponSlot.Primary)
					{
						this.aiPrimary[i].Add(weaponEntry);
					}
					else if (weaponEntry.slot == WeaponManager.WeaponSlot.Secondary)
					{
						this.aiSecondary[i].Add(weaponEntry);
					}
					else
					{
						this.aiAllGear[i].Add(weaponEntry);
						if (weaponEntry.slot == WeaponManager.WeaponSlot.Gear)
						{
							this.aiSmallGear[i].Add(weaponEntry);
						}
						else
						{
							this.aiLargeGear[i].Add(weaponEntry);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x000681E8 File Offset: 0x000663E8
	public static List<WeaponManager.WeaponEntry> GetWeaponEntriesOfSlot(WeaponManager.WeaponSlot slot)
	{
		List<WeaponManager.WeaponEntry> list = new List<WeaponManager.WeaponEntry>();
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (weaponEntry.slot == slot)
			{
				list.Add(weaponEntry);
			}
		}
		return list;
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00068250 File Offset: 0x00066450
	private static WeaponManager.WeaponEntry GetAiEntry(List<WeaponManager.WeaponEntry> entries, AiActorController.LoadoutPickStrategy strategy)
	{
		if (entries.Count == 0)
		{
			return null;
		}
		int num = UnityEngine.Random.Range(0, entries.Count);
		WeaponManager.WeaponEntry weaponEntry = null;
		for (int i = 0; i < entries.Count; i++)
		{
			int index = (num + i) % entries.Count;
			if (strategy.type == WeaponManager.WeaponEntry.LoadoutType.Normal || entries[index].type == strategy.type)
			{
				if (strategy.distance == WeaponManager.WeaponEntry.Distance.Any || entries[index].distance == WeaponManager.WeaponEntry.Distance.Any || strategy.distance == entries[index].distance)
				{
					return entries[index];
				}
				weaponEntry = entries[index];
			}
		}
		if (weaponEntry != null)
		{
			return weaponEntry;
		}
		return entries[num];
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0000778F File Offset: 0x0000598F
	public static WeaponManager.WeaponEntry GetAiWeaponPrimary(AiActorController.LoadoutPickStrategy strategy, int team)
	{
		return WeaponManager.GetAiEntry(WeaponManager.instance.aiPrimary[team], strategy);
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000077A3 File Offset: 0x000059A3
	public static WeaponManager.WeaponEntry GetAiWeaponSecondary(AiActorController.LoadoutPickStrategy strategy, int team)
	{
		return WeaponManager.GetAiEntry(WeaponManager.instance.aiSecondary[team], strategy);
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x000077B7 File Offset: 0x000059B7
	public static WeaponManager.WeaponEntry GetAiWeaponAllGear(AiActorController.LoadoutPickStrategy strategy, int team)
	{
		return WeaponManager.GetAiEntry(WeaponManager.instance.aiAllGear[team], strategy);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000077CB File Offset: 0x000059CB
	public static WeaponManager.WeaponEntry GetAiWeaponSmallGear(AiActorController.LoadoutPickStrategy strategy, int team)
	{
		return WeaponManager.GetAiEntry(WeaponManager.instance.aiSmallGear[team], strategy);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000077DF File Offset: 0x000059DF
	public static WeaponManager.WeaponEntry GetAiWeaponLargeGear(AiActorController.LoadoutPickStrategy strategy, int team)
	{
		return WeaponManager.GetAiEntry(WeaponManager.instance.aiLargeGear[team], strategy);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x000682F8 File Offset: 0x000664F8
	public static WeaponManager.WeaponEntry GetWeaponEntryByName(string name, ModInformation prioritizedMod = null)
	{
		WeaponManager.WeaponEntry result = null;
		foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
		{
			if (string.Equals(name, weaponEntry.name, StringComparison.InvariantCultureIgnoreCase))
			{
				if (weaponEntry.sourceMod == prioritizedMod)
				{
					return weaponEntry;
				}
				result = weaponEntry;
			}
		}
		return result;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0006836C File Offset: 0x0006656C
	public static Dictionary<string, List<WeaponManager.WeaponEntry>> GetWeaponTagDictionary(WeaponManager.WeaponSlot slot, List<string> officialTags, out List<string> sortedTags, bool allSlots)
	{
		Dictionary<string, List<WeaponManager.WeaponEntry>> weaponsTagged = new Dictionary<string, List<WeaponManager.WeaponEntry>>();
		sortedTags = new List<string>();
		List<WeaponManager.WeaponEntry> list;
		if (allSlots)
		{
			list = new List<WeaponManager.WeaponEntry>(WeaponManager.instance.allWeapons);
		}
		else
		{
			list = WeaponManager.GetWeaponEntriesOfSlot(slot);
			if (slot == WeaponManager.WeaponSlot.Gear)
			{
				list.AddRange(WeaponManager.GetWeaponEntriesOfSlot(WeaponManager.WeaponSlot.LargeGear));
			}
		}
		foreach (WeaponManager.WeaponEntry weaponEntry in list)
		{
			if (weaponEntry.tags == null || weaponEntry.tags.Length == 0)
			{
				WeaponManager.AssignAutomaticTag(weaponEntry);
			}
			foreach (string text in weaponEntry.tags)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string key = text.ToUpperInvariant();
					if (!weaponsTagged.ContainsKey(key))
					{
						weaponsTagged.Add(key, new List<WeaponManager.WeaponEntry>());
					}
					weaponsTagged[key].Add(weaponEntry);
				}
			}
		}
		foreach (string item in weaponsTagged.Keys)
		{
			if (!officialTags.Contains(item))
			{
				sortedTags.Add(item);
			}
		}
		sortedTags.Sort((string x, string y) => weaponsTagged[y].Count.CompareTo(weaponsTagged[x].Count));
		sortedTags.InsertRange(0, officialTags);
		foreach (string key2 in sortedTags)
		{
			if (!weaponsTagged.ContainsKey(key2))
			{
				weaponsTagged.Add(key2, new List<WeaponManager.WeaponEntry>());
			}
		}
		return weaponsTagged;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00068550 File Offset: 0x00066750
	private static void AssignAutomaticTag(WeaponManager.WeaponEntry entry)
	{
		string text = "UNTAGGED";
		Weapon component = entry.prefab.GetComponent<Weapon>();
		if (entry.slot == WeaponManager.WeaponSlot.Primary)
		{
			if (component.configuration.projectilesPerShot > 1)
			{
				text = "CLOSE QUARTERS";
			}
			else if (component.configuration.auto)
			{
				text = "ASSAULT";
			}
			else
			{
				text = "MARKSMAN";
			}
		}
		else if (entry.slot == WeaponManager.WeaponSlot.Secondary)
		{
			if (component.configuration.auto)
			{
				text = "PDW";
			}
			else
			{
				text = "HANDGUNS";
			}
		}
		else if (entry.slot == WeaponManager.WeaponSlot.Gear || entry.slot == WeaponManager.WeaponSlot.LargeGear)
		{
			if (component.configuration.projectilePrefab == null)
			{
				text = "EQUIPMENT";
			}
			else if (component.configuration.projectilePrefab.GetComponent<GrenadeProjectile>() != null)
			{
				text = "GRENADES";
			}
			else if (component.configuration.projectilePrefab.GetComponent<ExplodingProjectile>() != null)
			{
				text = "ANTI ARMOR";
			}
			else
			{
				text = "EQUIPMENT";
			}
		}
		if (entry.slot == WeaponManager.WeaponSlot.Gear || entry.slot == WeaponManager.WeaponSlot.LargeGear || component.configuration.loud)
		{
			entry.tags = new string[]
			{
				text
			};
			return;
		}
		entry.tags = new string[]
		{
			text,
			"STEALTH"
		};
	}

	// Token: 0x040008E7 RID: 2279
	public static WeaponManager instance;

	// Token: 0x040008E8 RID: 2280
	private const string SECRET_WEAPON_KEY = "railgun unlock";

	// Token: 0x040008E9 RID: 2281
	private const string SECRET_HALLOWEEN_WEAPON_KEY = "sword unlock";

	// Token: 0x040008EA RID: 2282
	public Mesh defaultArms;

	// Token: 0x040008EB RID: 2283
	public Mesh boxArms;

	// Token: 0x040008EC RID: 2284
	public List<WeaponManager.WeaponEntry> weapons;

	// Token: 0x040008ED RID: 2285
	public WeaponManager.WeaponEntry secretWeapon;

	// Token: 0x040008EE RID: 2286
	public WeaponManager.WeaponEntry secretMemeWeapon;

	// Token: 0x040008EF RID: 2287
	public WeaponManager.WeaponEntry secretHalloweenWeapon;

	// Token: 0x040008F0 RID: 2288
	[NonSerialized]
	public List<WeaponManager.WeaponEntry> allWeapons;

	// Token: 0x040008F1 RID: 2289
	private List<WeaponManager.WeaponEntry>[] aiPrimary;

	// Token: 0x040008F2 RID: 2290
	private List<WeaponManager.WeaponEntry>[] aiSecondary;

	// Token: 0x040008F3 RID: 2291
	private List<WeaponManager.WeaponEntry>[] aiAllGear;

	// Token: 0x040008F4 RID: 2292
	private List<WeaponManager.WeaponEntry>[] aiSmallGear;

	// Token: 0x040008F5 RID: 2293
	private List<WeaponManager.WeaponEntry>[] aiLargeGear;

	// Token: 0x0200011B RID: 283
	public enum WeaponSlot
	{
		// Token: 0x040008F7 RID: 2295
		Primary,
		// Token: 0x040008F8 RID: 2296
		Secondary,
		// Token: 0x040008F9 RID: 2297
		Gear,
		// Token: 0x040008FA RID: 2298
		LargeGear
	}

	// Token: 0x0200011C RID: 284
	[Serializable]
	public class WeaponEntry : IComparable<WeaponManager.WeaponEntry>
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x00068694 File Offset: 0x00066894
		public int CompareTo(WeaponManager.WeaponEntry other)
		{
			int num = this.sourceMod.CompareTo(other.sourceMod);
			if (num != 0)
			{
				return num;
			}
			if (this.sortPriority != other.sortPriority)
			{
				return this.sortPriority.CompareTo(other.sortPriority);
			}
			return this.name.CompareTo(other.name);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000686EC File Offset: 0x000668EC
		public void UpdateUiSpriteFromPrefab()
		{
			try
			{
				this.uiSprite = this.prefab.GetComponent<Weapon>().uiSprite;
			}
			catch
			{
			}
		}

		// Token: 0x040008FB RID: 2299
		public const float DISTANCE_SHORT = 120f;

		// Token: 0x040008FC RID: 2300
		public const float DISTANCE_MID = 500f;

		// Token: 0x040008FD RID: 2301
		public string name = "Weapon";

		// Token: 0x040008FE RID: 2302
		public GameObject prefab;

		// Token: 0x040008FF RID: 2303
		public WeaponManager.WeaponSlot slot;

		// Token: 0x04000900 RID: 2304
		public bool hidden;

		// Token: 0x04000901 RID: 2305
		public bool usableByAi = true;

		// Token: 0x04000902 RID: 2306
		[HideInInspector]
		public bool usableByAiAllowOverride = true;

		// Token: 0x04000903 RID: 2307
		public bool isAvailableByDefault = true;

		// Token: 0x04000904 RID: 2308
		[NonSerialized]
		public int nameHash = -1;

		// Token: 0x04000905 RID: 2309
		public Sprite uiSprite;

		// Token: 0x04000906 RID: 2310
		[NonSerialized]
		public ModInformation sourceMod;

		// Token: 0x04000907 RID: 2311
		public WeaponManager.WeaponEntry.LoadoutType type;

		// Token: 0x04000908 RID: 2312
		public WeaponManager.WeaponEntry.Distance distance = WeaponManager.WeaponEntry.Distance.Any;

		// Token: 0x04000909 RID: 2313
		public int sortPriority;

		// Token: 0x0400090A RID: 2314
		public string[] tags;

		// Token: 0x0200011D RID: 285
		public enum LoadoutType
		{
			// Token: 0x0400090C RID: 2316
			Normal,
			// Token: 0x0400090D RID: 2317
			Stealth,
			// Token: 0x0400090E RID: 2318
			AntiArmor,
			// Token: 0x0400090F RID: 2319
			Repair,
			// Token: 0x04000910 RID: 2320
			ResupplyAmmo,
			// Token: 0x04000911 RID: 2321
			ResupplyHealth,
			// Token: 0x04000912 RID: 2322
			SmokeScreen
		}

		// Token: 0x0200011E RID: 286
		public enum Distance
		{
			// Token: 0x04000914 RID: 2324
			Short,
			// Token: 0x04000915 RID: 2325
			Mid,
			// Token: 0x04000916 RID: 2326
			Far,
			// Token: 0x04000917 RID: 2327
			Any,
			// Token: 0x04000918 RID: 2328
			Auto
		}
	}

	// Token: 0x0200011F RID: 287
	[Serializable]
	public class LoadoutSet
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00007829 File Offset: 0x00005A29
		public LoadoutSet()
		{
			this.primary = null;
			this.secondary = null;
			this.gear1 = null;
			this.gear2 = null;
			this.gear3 = null;
		}

		// Token: 0x04000919 RID: 2329
		public WeaponManager.WeaponEntry primary;

		// Token: 0x0400091A RID: 2330
		public WeaponManager.WeaponEntry secondary;

		// Token: 0x0400091B RID: 2331
		public WeaponManager.WeaponEntry gear1;

		// Token: 0x0400091C RID: 2332
		public WeaponManager.WeaponEntry gear2;

		// Token: 0x0400091D RID: 2333
		public WeaponManager.WeaponEntry gear3;
	}
}
