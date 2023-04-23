using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001F1 RID: 497
public class MainMenu : MonoBehaviour
{
	// Token: 0x06000D56 RID: 3414 RVA: 0x0000ACA6 File Offset: 0x00008EA6
	private void Start()
	{
		this.OpenPageIndex(0);
		this.featuredModsPanel.Load();
		base.Invoke("PlayMusic", 1f);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0000ACCA File Offset: 0x00008ECA
	private void PlayMusic()
	{
		this.music.enabled = true;
		this.music.Play();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0007C6A0 File Offset: 0x0007A8A0
	private void Awake()
	{
		MainMenu.instance = this;
		if (GameManager.instance == null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.managerObject);
		}
		Time.timeScale = 1f;
		this.pages = new GameObject[]
		{
			this.splash,
			this.mainMenu,
			this.play,
			this.instantAction,
			this.campaign,
			null,
			this.mods,
			this.customMaps,
			this.weaponSwitch,
			this.vehicleSwitch,
			this.roadmap,
			this.saveGameConfig,
			this.featuredMods,
			this.mutators,
			this.mutatorConfig,
			this.vehiclePicker
		};
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0007C774 File Offset: 0x0007A974
	public void OpenPageIndex(int index)
	{
		if (index != 0 && !ModManager.instance.contentHasFinishedLoading)
		{
			return;
		}
		if (index == 5)
		{
			Options.Show();
		}
		if (this.page == 5)
		{
			Options.Hide();
		}
		this.page = index;
		for (int i = 0; i < this.pages.Length; i++)
		{
			if (this.pages[i] != null)
			{
				this.pages[i].SetActive(false);
			}
		}
		if (this.pages[index] != null)
		{
			this.pages[index].SetActive(true);
		}
		this.background.SetActive(index != 0);
		this.titleImage.SetActive(index <= 2);
		this.backButton.SetActive(index > 0);
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0007C830 File Offset: 0x0007AA30
	public void GoBack()
	{
		if (!ModManager.instance.contentHasFinishedLoading)
		{
			return;
		}
		switch (this.page)
		{
		case 1:
			this.OpenPageIndex(0);
			return;
		case 2:
			this.OpenPageIndex(1);
			return;
		case 3:
			this.OpenPageIndex(2);
			return;
		case 4:
			this.OpenPageIndex(2);
			return;
		case 7:
			this.OpenPageIndex(3);
			return;
		case 8:
			WeaponSwitch.instance.Apply();
			this.OpenPageIndex(3);
			return;
		case 9:
			this.OpenPageIndex(3);
			return;
		case 11:
			this.OpenPageIndex(3);
			return;
		case 13:
			this.OpenPageIndex(3);
			return;
		case 14:
			this.OpenPageIndex(13);
			return;
		case 15:
			this.OpenPageIndex(9);
			return;
		}
		this.OpenPageIndex(1);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0000ACE3 File Offset: 0x00008EE3
	public void StartMapEditor()
	{
		SceneManager.LoadScene("MapEditor/Scenes/MapEditor-generator");
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0007C904 File Offset: 0x0007AB04
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && this.page != 5 && this.page != 0)
		{
			this.GoBack();
			return;
		}
		if (Input.anyKeyDown && this.page == 0 && ModManager.instance.contentHasFinishedLoading)
		{
			this.OpenPageIndex(1);
		}
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0000ACEF File Offset: 0x00008EEF
	public void Shutdown()
	{
		Application.Quit();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0000ACF6 File Offset: 0x00008EF6
	public void PlayMovie()
	{
		SceneManager.LoadScene("Coastline UFO Cinematic");
	}

	// Token: 0x04000E5C RID: 3676
	public const int PAGE_SPLASH = 0;

	// Token: 0x04000E5D RID: 3677
	public const int PAGE_MENU = 1;

	// Token: 0x04000E5E RID: 3678
	public const int PAGE_PLAY = 2;

	// Token: 0x04000E5F RID: 3679
	public const int PAGE_INSTANT_ACTION = 3;

	// Token: 0x04000E60 RID: 3680
	public const int PAGE_CAMPAIGN = 4;

	// Token: 0x04000E61 RID: 3681
	public const int PAGE_OPTIONS = 5;

	// Token: 0x04000E62 RID: 3682
	public const int PAGE_MODS = 6;

	// Token: 0x04000E63 RID: 3683
	public const int PAGE_CUSTOM_MAPS = 7;

	// Token: 0x04000E64 RID: 3684
	public const int PAGE_WEAPON_SWITCH = 8;

	// Token: 0x04000E65 RID: 3685
	public const int PAGE_VEHICLE_SWITCH = 9;

	// Token: 0x04000E66 RID: 3686
	public const int PAGE_ROADMAP = 10;

	// Token: 0x04000E67 RID: 3687
	public const int PAGE_SAVE_GAME_CONFIG = 11;

	// Token: 0x04000E68 RID: 3688
	public const int PAGE_FEATURED_MODS = 12;

	// Token: 0x04000E69 RID: 3689
	public const int PAGE_MUTATORS = 13;

	// Token: 0x04000E6A RID: 3690
	public const int PAGE_MUTATOR_CONFIG = 14;

	// Token: 0x04000E6B RID: 3691
	public const int PAGE_VEHICLE_PICKER = 15;

	// Token: 0x04000E6C RID: 3692
	public static MainMenu instance;

	// Token: 0x04000E6D RID: 3693
	public GameObject managerObject;

	// Token: 0x04000E6E RID: 3694
	public GameObject background;

	// Token: 0x04000E6F RID: 3695
	public GameObject splash;

	// Token: 0x04000E70 RID: 3696
	public GameObject mainMenu;

	// Token: 0x04000E71 RID: 3697
	public GameObject play;

	// Token: 0x04000E72 RID: 3698
	public GameObject instantAction;

	// Token: 0x04000E73 RID: 3699
	public GameObject campaign;

	// Token: 0x04000E74 RID: 3700
	public GameObject mods;

	// Token: 0x04000E75 RID: 3701
	public GameObject customMaps;

	// Token: 0x04000E76 RID: 3702
	public GameObject weaponSwitch;

	// Token: 0x04000E77 RID: 3703
	public GameObject vehicleSwitch;

	// Token: 0x04000E78 RID: 3704
	public GameObject vehiclePicker;

	// Token: 0x04000E79 RID: 3705
	public GameObject roadmap;

	// Token: 0x04000E7A RID: 3706
	public GameObject saveGameConfig;

	// Token: 0x04000E7B RID: 3707
	public GameObject featuredMods;

	// Token: 0x04000E7C RID: 3708
	public GameObject mutators;

	// Token: 0x04000E7D RID: 3709
	public GameObject mutatorConfig;

	// Token: 0x04000E7E RID: 3710
	public GameObject titleImage;

	// Token: 0x04000E7F RID: 3711
	public GameObject backButton;

	// Token: 0x04000E80 RID: 3712
	public FeaturedModsPanel featuredModsPanel;

	// Token: 0x04000E81 RID: 3713
	public MenuMusic music;

	// Token: 0x04000E82 RID: 3714
	private GameObject[] pages;

	// Token: 0x04000E83 RID: 3715
	private int page;
}
