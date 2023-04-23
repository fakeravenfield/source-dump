using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000993 RID: 2451
	[Name("SpawnUi")]
	[Doc("Use this class to control the loadout and deployment UI.")]
	public static class WSpawnUi
	{
		// Token: 0x06003E4C RID: 15948 RVA: 0x0002A1AF File Offset: 0x000283AF
		[Doc("Opens the UI.")]
		public static void Open()
		{
			LoadoutUi.Show(false);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0002A1B7 File Offset: 0x000283B7
		[Doc("Opens the UI in battle plan mode.")]
		public static void OpenBattlePlan()
		{
			LoadoutUi.Show(true);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0002A1BF File Offset: 0x000283BF
		[Doc("Closes the UI.")]
		public static void Close()
		{
			LoadoutUi.Hide(false);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0002A1C7 File Offset: 0x000283C7
		[Doc("Show or hide the loadout section.")]
		public static void SetLoadoutVisible(bool visible)
		{
			LoadoutUi.instance.SetLoadoutVisible(visible);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x0002A1D4 File Offset: 0x000283D4
		[Doc("Show or hide the minimap/deploy section.")]
		public static void SetMinimapVisible(bool visible)
		{
			LoadoutUi.instance.SetMinimapVisible(visible);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0002A1E1 File Offset: 0x000283E1
		[Doc("Override the loadout section of the UI.")]
		public static void SetLoadoutOverride(GameObject loadout)
		{
			LoadoutUi.instance.SetLoadoutOverride(loadout);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0002A1EE File Offset: 0x000283EE
		[Doc("Override the minimap section of the UI.")]
		public static void SetMinimapOverride(GameObject minimap)
		{
			LoadoutUi.instance.SetMinimapOverride(minimap);
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x0002A1FB File Offset: 0x000283FB
		[Doc("Sets the player selected spawn point.[..] If set to null, the player will not automatically spawn.")]
		public static void SetSelectedSpawnPoint(SpawnPoint spawnPoint)
		{
			MinimapUi.SelectSpawnPoint(spawnPoint);
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0002A203 File Offset: 0x00028403
		[Doc("Returns the player selected spawn point.")]
		public static SpawnPoint GetSelectedSpawnPoint(SpawnPoint spawnPoint)
		{
			return MinimapUi.instance.selectedSpawnPoint;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x0002A20F File Offset: 0x0002840F
		[Getter]
		[Doc("True if the player can select their spawn point from the loadout minimap.")]
		public static bool GetPlayerCanSelectSpawnPoint()
		{
			return MinimapUi.instance.playerCanSelectSpawnPoint;
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0002A21B File Offset: 0x0002841B
		[Setter]
		[Doc("True if the player can select their spawn point from the loadout minimap.")]
		public static void SetPlayerCanSelectSpawnPoint(bool enabled)
		{
			MinimapUi.SetPlayerCanSelectSpawnPoint(enabled);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0002A223 File Offset: 0x00028423
		[Getter]
		public static bool GetIsOpen()
		{
			return LoadoutUi.IsOpen();
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x0002A22A File Offset: 0x0002842A
		[Getter]
		[Doc("True if the UI has been open at least once this game.")]
		public static bool GetHasBeenOpen()
		{
			return LoadoutUi.HasBeenOpen();
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x0002A231 File Offset: 0x00028431
		[Getter]
		[Doc("True if the UI has been closed at least once this game.")]
		public static bool GetHasBeenClosed()
		{
			return LoadoutUi.HasBeenClosed();
		}
	}
}
