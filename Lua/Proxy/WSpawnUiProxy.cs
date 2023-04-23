using System;
using Lua.Wrapper;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A27 RID: 2599
	[Proxy(typeof(WSpawnUi))]
	public class WSpawnUiProxy : IProxy
	{
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x0003D1C6 File Offset: 0x0003B3C6
		public static bool hasBeenClosed
		{
			get
			{
				return WSpawnUi.GetHasBeenClosed();
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060052DC RID: 21212 RVA: 0x0003D1CD File Offset: 0x0003B3CD
		public static bool hasBeenOpen
		{
			get
			{
				return WSpawnUi.GetHasBeenOpen();
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public static bool isOpen
		{
			get
			{
				return WSpawnUi.GetIsOpen();
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060052DE RID: 21214 RVA: 0x0003D1DB File Offset: 0x0003B3DB
		// (set) Token: 0x060052DF RID: 21215 RVA: 0x0003D1E2 File Offset: 0x0003B3E2
		public static bool playerCanSelectSpawnPoint
		{
			get
			{
				return WSpawnUi.GetPlayerCanSelectSpawnPoint();
			}
			set
			{
				WSpawnUi.SetPlayerCanSelectSpawnPoint(value);
			}
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0002F641 File Offset: 0x0002D841
		[MoonSharpHidden]
		public object GetValue()
		{
			throw new InvalidOperationException("Proxied type is static.");
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0003D1EA File Offset: 0x0003B3EA
		public static void Close()
		{
			WSpawnUi.Close();
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x001395E0 File Offset: 0x001377E0
		public static SpawnPointProxy GetSelectedSpawnPoint(SpawnPointProxy spawnPoint)
		{
			SpawnPoint spawnPoint2 = null;
			if (spawnPoint != null)
			{
				spawnPoint2 = spawnPoint._value;
			}
			return SpawnPointProxy.New(WSpawnUi.GetSelectedSpawnPoint(spawnPoint2));
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0003D1F1 File Offset: 0x0003B3F1
		public static void Open()
		{
			WSpawnUi.Open();
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		public static void OpenBattlePlan()
		{
			WSpawnUi.OpenBattlePlan();
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x00139604 File Offset: 0x00137804
		public static void SetLoadoutOverride(GameObjectProxy loadout)
		{
			GameObject loadoutOverride = null;
			if (loadout != null)
			{
				loadoutOverride = loadout._value;
			}
			WSpawnUi.SetLoadoutOverride(loadoutOverride);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0003D1FF File Offset: 0x0003B3FF
		public static void SetLoadoutVisible(bool visible)
		{
			WSpawnUi.SetLoadoutVisible(visible);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00139624 File Offset: 0x00137824
		public static void SetMinimapOverride(GameObjectProxy minimap)
		{
			GameObject minimapOverride = null;
			if (minimap != null)
			{
				minimapOverride = minimap._value;
			}
			WSpawnUi.SetMinimapOverride(minimapOverride);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0003D207 File Offset: 0x0003B407
		public static void SetMinimapVisible(bool visible)
		{
			WSpawnUi.SetMinimapVisible(visible);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00139644 File Offset: 0x00137844
		public static void SetSelectedSpawnPoint(SpawnPointProxy spawnPoint)
		{
			SpawnPoint selectedSpawnPoint = null;
			if (spawnPoint != null)
			{
				selectedSpawnPoint = spawnPoint._value;
			}
			WSpawnUi.SetSelectedSpawnPoint(selectedSpawnPoint);
		}
	}
}
