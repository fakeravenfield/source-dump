using System;
using System.Collections.Generic;
using System.IO;
using MapEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001E7 RID: 487
public class InstantActionMaps : MonoBehaviour
{
	// Token: 0x06000D08 RID: 3336 RVA: 0x0000A99C File Offset: 0x00008B9C
	private void Awake()
	{
		InstantActionMaps.instance = this;
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0007B240 File Offset: 0x00079440
	public static void SelectedCustomMapEntry(InstantActionMaps.MapEntry entry)
	{
		if (!InstantActionMaps.instance.hasCustomMapOption)
		{
			InstantActionMaps.instance.entries.Add(entry);
			InstantActionMaps.instance.mapDropdown.options.Add(new Dropdown.OptionData(entry.name));
			InstantActionMaps.instance.hasCustomMapOption = true;
		}
		else
		{
			InstantActionMaps.instance.entries[InstantActionMaps.instance.customMapOptionIndex] = entry;
			InstantActionMaps.instance.mapDropdown.options[InstantActionMaps.instance.customMapOptionIndex].text = entry.name;
		}
		InstantActionMaps.instance.mapDropdown.value = 0;
		InstantActionMaps.instance.mapDropdown.value = InstantActionMaps.instance.customMapOptionIndex;
		InstantActionMaps.instance.MapChanged(InstantActionMaps.instance.customMapOptionIndex);
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0007B318 File Offset: 0x00079518
	private void OnSkinDropdownChanged(int index)
	{
		if (this.ignoreSkinDropdownCallbacks)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			int num = this.skinDropdowns[i].value - 1;
			if (num < 0)
			{
				GameManager.instance.gameInfo.team[i].skin = null;
			}
			else
			{
				GameManager.instance.gameInfo.team[i].skin = ModManager.instance.actorSkins[num];
			}
		}
		GamePreview.UpdatePreview();
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0007B394 File Offset: 0x00079594
	private void Start()
	{
		this.mapDropdown.onValueChanged.AddListener(new UnityAction<int>(this.MapChanged));
		this.botNumberField.onValueChanged.AddListener(new UnityAction<string>(this.BotNumberChanged));
		this.balanceSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateBalanceKnob));
		Dropdown[] array = this.skinDropdowns;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onValueChanged.AddListener(new UnityAction<int>(this.OnSkinDropdownChanged));
		}
		this.SetupMapsList();
		this.SetupGameModeList();
		this.SetupGameConfigurationPaths();
		this.SetupSkinList();
		if (GameManager.instance.hasNonDefaultGameModeParameters)
		{
			this.MimicGameModeParameters();
		}
		MainMenu.instance.OpenPageIndex(9);
		MainMenu.instance.OpenPageIndex(3);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0007B464 File Offset: 0x00079664
	private void SetupSkinList()
	{
		List<string> list = new List<string>();
		list.Add("Default");
		foreach (ActorSkin actorSkin in ModManager.instance.actorSkins)
		{
			list.Add(actorSkin.name);
		}
		for (int i = 0; i < 2; i++)
		{
			this.skinDropdowns[i].ClearOptions();
			this.skinDropdowns[i].AddOptions(list);
		}
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0007B4F8 File Offset: 0x000796F8
	private void MimicGameModeParameters()
	{
		GameManager.GameParameters().noTurrets = true;
		GameModeType gameModeType = GameManager.GameParameters().gameModePrefab.GetComponent<GameModeBase>().gameModeType;
		for (int i = 0; i < this.gameModes.Length; i++)
		{
			if (this.gameModes[i] == gameModeType)
			{
				this.gameModeDropdown.value = i;
				break;
			}
		}
		this.botNumberField.text = GameManager.GameParameters().actorCount.ToString();
		this.respawnTimeField.text = GameManager.GameParameters().respawnTime.ToString();
		this.nightToggle.isOn = GameManager.GameParameters().nightMode;
		this.reverseToggle.isOn = GameManager.GameParameters().reverseMode;
		this.configFlagsToggle.isOn = GameManager.GameParameters().configFlags;
		this.playerHasAllWeaponsToggle.isOn = GameManager.GameParameters().playerHasAllWeapons;
		this.bloodExplosionsToggle.isOn = GameManager.GameParameters().bloodExplosions;
		int playerTeam = GameManager.GameParameters().playerTeam;
		if (playerTeam == -1)
		{
			this.teamDropdown.value = 2;
		}
		else
		{
			this.teamDropdown.value = playerTeam;
		}
		this.balanceSlider.value = GameManager.GameParameters().balance;
		this.gameLengthDropdown.value = GameManager.GameParameters().gameLength;
		this.mapDropdown.value = GameManager.GameParameters().loadedLevelEntry;
		this.UpdateSkinDropdowns();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0007B65C File Offset: 0x0007985C
	private void UpdateSkinDropdowns()
	{
		this.ignoreSkinDropdownCallbacks = true;
		for (int i = 0; i < 2; i++)
		{
			try
			{
				this.skinDropdowns[i].value = ModManager.instance.actorSkins.IndexOf(GameManager.instance.gameInfo.team[i].skin) + 1;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
		this.ignoreSkinDropdownCallbacks = false;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0000A9A4 File Offset: 0x00008BA4
	private void Update()
	{
		if (this.NeedsToLoadModContent())
		{
			this.SetupMapsList();
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000A9B4 File Offset: 0x00008BB4
	private void OnEnable()
	{
		GamePreview.UpdatePreview();
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0000A9BB File Offset: 0x00008BBB
	private bool NeedsToLoadModContent()
	{
		return this.loadedModContentVersion != ModManager.instance.contentVersion;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0007B6D4 File Offset: 0x000798D4
	private void SetupGameModeList()
	{
		List<string> list = new List<string>();
		GameModeType[] array = this.gameModes;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameModePrefab = GameManager.GetGameModePrefab(array[i]);
			list.Add(gameModePrefab.GetComponent<GameModeBase>().gameModeName);
		}
		this.gameModeDropdown.AddOptions(list);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0007B724 File Offset: 0x00079924
	private void SetupMapsList()
	{
		this.customMapsBrowser.SetupCustomMapEntries();
		this.loadedModContentVersion = ModManager.instance.contentVersion;
		this.mapDropdown.ClearOptions();
		List<string> list = new List<string>();
		this.entries = new List<InstantActionMaps.MapEntry>(this.officialEntries);
		int num = 0;
		this.customMapOptionIndex = this.officialEntries.Count;
		this.hasCustomMapOption = false;
		if (GameManager.instance.lastMapEntry != null)
		{
			if (GameManager.instance.lastMapEntry.isCustomMap)
			{
				this.hasCustomMapOption = true;
				this.entries.Add(GameManager.instance.lastMapEntry);
				num = this.customMapOptionIndex;
			}
			else
			{
				this.entries.IndexOf(GameManager.instance.lastMapEntry);
			}
		}
		foreach (InstantActionMaps.MapEntry mapEntry in this.entries)
		{
			list.Add(mapEntry.name);
		}
		this.mapDropdown.AddOptions(list);
		this.mapDropdown.value = num;
		this.MapChanged(num);
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0007B84C File Offset: 0x00079A4C
	private void AddBundleMapEntry(FileInfo file)
	{
		InstantActionMaps.MapEntry mapEntry = new InstantActionMaps.MapEntry();
		mapEntry.name = file.Name.Substring(0, file.Name.Length - 4);
		mapEntry.sceneName = file.FullName;
		mapEntry.isCustomMap = true;
		mapEntry.hasLoadedMetaData = false;
		mapEntry.suggestedBots = 40;
		mapEntry.image = this.defaultBundleSprite;
		this.entries.Add(mapEntry);
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0007B8B8 File Offset: 0x00079AB8
	private void UpdateBalanceKnob(float balance)
	{
		if (balance != 0.5f && Mathf.Abs(balance - 0.5f) < 0.05f)
		{
			balance = 0.5f;
			this.balanceSlider.value = balance;
		}
		this.balanceSliderImage.color = ((balance < 0.5f) ? Color.Lerp(Color.blue, Color.white, balance * 2f) : Color.Lerp(Color.white, Color.red, balance * 2f - 1f));
		int eagle;
		int raven;
		InstantActionMaps.BalanceToBattalionCount(balance, out eagle, out raven);
		GamePreview.instance.UpdateTeamVisibility(eagle, raven);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0000A9D2 File Offset: 0x00008BD2
	public static void BalanceToBattalionCount(float balance, out int blueCount, out int redCount)
	{
		blueCount = Mathf.CeilToInt((1f - balance) / 0.5f * 3f);
		redCount = Mathf.CeilToInt(balance / 0.5f * 3f);
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0007B950 File Offset: 0x00079B50
	private void MapChanged(int i)
	{
		this.image.sprite = this.entries[i].image;
		if (!this.botNumberManuallyChanged)
		{
			this.botNumberAutoUpdate = true;
			this.botNumberField.text = this.entries[i].suggestedBots.ToString();
			this.botNumberAutoUpdate = false;
		}
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0000AA02 File Offset: 0x00008C02
	private void BotNumberChanged(string s)
	{
		if (!this.botNumberAutoUpdate)
		{
			this.botNumberManuallyChanged = true;
		}
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0007B9B0 File Offset: 0x00079BB0
	public void StartGame()
	{
		GameModeParameters gameModeParameters = new GameModeParameters();
		gameModeParameters.gameModePrefab = GameManager.GetGameModePrefab(this.gameModes[this.gameModeDropdown.value]);
		Debug.Log(gameModeParameters.gameModePrefab);
		gameModeParameters.nightMode = this.nightToggle.isOn;
		gameModeParameters.playerHasAllWeapons = this.playerHasAllWeaponsToggle.isOn;
		gameModeParameters.bloodExplosions = this.bloodExplosionsToggle.isOn;
		int num = this.teamDropdown.value;
		if (num >= 2 || num < 0)
		{
			num = -1;
		}
		gameModeParameters.playerTeam = num;
		gameModeParameters.reverseMode = this.reverseToggle.isOn;
		gameModeParameters.configFlags = this.configFlagsToggle.isOn;
		int suggestedBots;
		if (!int.TryParse(this.botNumberField.text, out suggestedBots))
		{
			suggestedBots = this.entries[this.mapDropdown.value].suggestedBots;
		}
		gameModeParameters.actorCount = suggestedBots;
		gameModeParameters.balance = this.balanceSlider.value;
		InstantActionMaps.BalanceToBattalionCount(gameModeParameters.balance, out gameModeParameters.conquestBattalions[0], out gameModeParameters.conquestBattalions[1]);
		int value;
		if (!int.TryParse(this.respawnTimeField.text, out value))
		{
			value = 5;
		}
		gameModeParameters.respawnTime = Mathf.Clamp(value, 1, 120);
		gameModeParameters.gameLength = this.gameLengthDropdown.value;
		gameModeParameters.loadedLevelEntry = this.mapDropdown.value;
		if (this.gameModes[this.gameModeDropdown.value] == GameModeType.Haunted)
		{
			gameModeParameters.nightMode = true;
		}
		GameManager.StartLevel(this.entries[this.mapDropdown.value], gameModeParameters);
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0007BB48 File Offset: 0x00079D48
	public void SetupGameConfigurationPaths()
	{
		this.gameConfigurationPaths = new List<InstantActionMaps.ConfigurationFileInfo>();
		string folderPath = SaveConfigurationPage.GetFolderPath();
		List<string> list = new List<string>();
		list.Add("LOAD CONFIGURATION");
		list.Add("Default Weapons/Vehicles");
		List<InstantActionMaps.ConfigurationFileInfo> list2 = new List<InstantActionMaps.ConfigurationFileInfo>();
		this.gameConfigDropdown.ClearOptions();
		if (Directory.Exists(folderPath))
		{
			try
			{
				foreach (string path in Directory.GetFiles(folderPath))
				{
					list2.Add(new InstantActionMaps.ConfigurationFileInfo(path, null));
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Could not get files from the configuration save directory, exception follows:");
				Debug.LogException(exception);
			}
		}
		foreach (ModInformation modInformation in ModManager.instance.mods)
		{
			try
			{
				foreach (FileInfo fileInfo in modInformation.content.GetGameConfiguration())
				{
					list2.Add(new InstantActionMaps.ConfigurationFileInfo(fileInfo.FullName, modInformation));
				}
			}
			catch (Exception exception2)
			{
				Debug.LogError("Could not load configuration files from mod " + modInformation.title + ", exception follows:");
				Debug.LogException(exception2);
			}
		}
		foreach (InstantActionMaps.ConfigurationFileInfo configurationFileInfo in list2)
		{
			try
			{
				if (configurationFileInfo.path.EndsWith(".rgc"))
				{
					this.gameConfigurationPaths.Add(configurationFileInfo);
					string[] array = configurationFileInfo.path.Replace('\\', '/').Split(new char[]
					{
						'/'
					});
					string text = array[array.Length - 1];
					text = text.Remove(text.Length - 4);
					list.Add(text);
				}
			}
			catch (Exception exception3)
			{
				Debug.LogError("Could not load configuration file " + configurationFileInfo.ToString() + ", exception follows:");
				Debug.LogException(exception3);
			}
		}
		this.gameConfigDropdown.AddOptions(list);
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0007BD98 File Offset: 0x00079F98
	public void SelectGameConfiguration(string name)
	{
		this.gameConfigDropdown.value = 0;
		for (int i = 0; i < this.gameConfigDropdown.options.Count; i++)
		{
			if (this.gameConfigDropdown.options[i].text == name)
			{
				this.gameConfigDropdown.value = i;
				return;
			}
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0007BDF8 File Offset: 0x00079FF8
	public void LoadGameConfiguration(int dropdownIndex)
	{
		if (dropdownIndex == 0)
		{
			return;
		}
		if (dropdownIndex == 1)
		{
			GameManager.instance.gameInfo.LoadOfficial();
			GamePreview.UpdatePreview();
			return;
		}
		int index = dropdownIndex - 2;
		SerializedGameInfo serializedGameInfo = SerializedGameInfo.DeserializeFile(this.gameConfigurationPaths[index].path, GameManager.instance.gameInfo);
		this.UpdateSkinDropdowns();
		this.playerHasAllWeaponsToggle.isOn = serializedGameInfo.playerHasAllWeapons;
		GamePreview.UpdatePreview();
	}

	// Token: 0x04000E05 RID: 3589
	public static InstantActionMaps instance;

	// Token: 0x04000E06 RID: 3590
	private List<InstantActionMaps.MapEntry> entries;

	// Token: 0x04000E07 RID: 3591
	[NonSerialized]
	public List<InstantActionMaps.ConfigurationFileInfo> gameConfigurationPaths;

	// Token: 0x04000E08 RID: 3592
	public CustomMapsBrowser customMapsBrowser;

	// Token: 0x04000E09 RID: 3593
	public List<InstantActionMaps.MapEntry> officialEntries;

	// Token: 0x04000E0A RID: 3594
	public GameModeType[] gameModes;

	// Token: 0x04000E0B RID: 3595
	public Dropdown mapDropdown;

	// Token: 0x04000E0C RID: 3596
	public Dropdown gameModeDropdown;

	// Token: 0x04000E0D RID: 3597
	public Image image;

	// Token: 0x04000E0E RID: 3598
	public InputField botNumberField;

	// Token: 0x04000E0F RID: 3599
	public InputField respawnTimeField;

	// Token: 0x04000E10 RID: 3600
	private bool botNumberAutoUpdate;

	// Token: 0x04000E11 RID: 3601
	private bool botNumberManuallyChanged;

	// Token: 0x04000E12 RID: 3602
	public Toggle nightToggle;

	// Token: 0x04000E13 RID: 3603
	public Toggle reverseToggle;

	// Token: 0x04000E14 RID: 3604
	public Toggle configFlagsToggle;

	// Token: 0x04000E15 RID: 3605
	public Toggle playerHasAllWeaponsToggle;

	// Token: 0x04000E16 RID: 3606
	public Toggle bloodExplosionsToggle;

	// Token: 0x04000E17 RID: 3607
	public Dropdown teamDropdown;

	// Token: 0x04000E18 RID: 3608
	public Slider balanceSlider;

	// Token: 0x04000E19 RID: 3609
	public Image balanceSliderImage;

	// Token: 0x04000E1A RID: 3610
	public Dropdown gameLengthDropdown;

	// Token: 0x04000E1B RID: 3611
	public Dropdown gameConfigDropdown;

	// Token: 0x04000E1C RID: 3612
	public Dropdown[] skinDropdowns;

	// Token: 0x04000E1D RID: 3613
	public Sprite defaultBundleSprite;

	// Token: 0x04000E1E RID: 3614
	private uint loadedModContentVersion;

	// Token: 0x04000E1F RID: 3615
	private int customMapOptionIndex;

	// Token: 0x04000E20 RID: 3616
	private bool hasCustomMapOption;

	// Token: 0x04000E21 RID: 3617
	private bool ignoreSkinDropdownCallbacks;

	// Token: 0x020001E8 RID: 488
	public struct ConfigurationFileInfo
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x0007BE64 File Offset: 0x0007A064
		public ConfigurationFileInfo(string path, ModInformation sourceMod)
		{
			this.path = path;
			this.sourceMod = sourceMod;
			this.targetWorkshopItem = 0U;
			if (this.sourceMod != null)
			{
				string text = this.path + ".workshopid";
				uint num;
				if (File.Exists(text) && uint.TryParse(File.ReadAllText(text), out num))
				{
					this.targetWorkshopItem = num;
				}
			}
		}

		// Token: 0x04000E22 RID: 3618
		private const string WORKSHOP_ID_EXTENSION = ".workshopid";

		// Token: 0x04000E23 RID: 3619
		public string path;

		// Token: 0x04000E24 RID: 3620
		public ModInformation sourceMod;

		// Token: 0x04000E25 RID: 3621
		public uint targetWorkshopItem;
	}

	// Token: 0x020001E9 RID: 489
	[Serializable]
	public class MapEntry
	{
		// Token: 0x06000D1F RID: 3359 RVA: 0x0000AA13 File Offset: 0x00008C13
		public bool IsRFLBundle()
		{
			return this.sceneName.Substring(this.sceneName.Length - 4) == ".rfl";
		}

		// Token: 0x04000E26 RID: 3622
		public string name = "Map";

		// Token: 0x04000E27 RID: 3623
		public string sceneName = "map";

		// Token: 0x04000E28 RID: 3624
		[NonSerialized]
		public bool isCustomMap;

		// Token: 0x04000E29 RID: 3625
		public bool hasLoadedMetaData = true;

		// Token: 0x04000E2A RID: 3626
		public Sprite image;

		// Token: 0x04000E2B RID: 3627
		public int suggestedBots = 50;

		// Token: 0x04000E2C RID: 3628
		public bool nightVersion;

		// Token: 0x04000E2D RID: 3629
		[NonSerialized]
		public SceneConstructor.Mode mapEditorMode = SceneConstructor.Mode.Play;
	}
}
