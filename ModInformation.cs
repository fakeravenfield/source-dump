using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class ModInformation : IComparable<ModInformation>
{
	// Token: 0x06000E37 RID: 3639 RVA: 0x0007EE4C File Offset: 0x0007D04C
	public ModInformation(string path)
	{
		this.path = path;
		this.workshopItemId = (PublishedFileId_t)0UL;
		string[] array = this.path.Replace('\\', '/').Split(new char[]
		{
			'/'
		});
		this.directoryName = array[array.Length - 1];
		this.title = this.directoryName;
		this.enabled = this.LoadEnabledPreference();
		this.iconTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);
		this.isStaged = true;
		this.LoadContent();
		this.GenerateDescriptionFromContent();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0007EF10 File Offset: 0x0007D110
	public ModInformation(PublishedFileId_t id)
	{
		this.path = "";
		this.workshopItemId = id;
		this.description = "Fetching info...";
		this.title = "Workshop Item #" + id.m_PublishedFileId.ToString();
		this.enabled = this.LoadEnabledPreference();
		this.iconTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);
		this.isStaged = false;
		this.TryLoadContent();
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0007EFB8 File Offset: 0x0007D1B8
	public bool TryLoadContent()
	{
		if (string.IsNullOrEmpty(this.path))
		{
			if (!this.IsWorkshopItem() || !GameManager.IsConnectedToSteam() || !GameManager.instance.steamworks.IsSubscribedItemInstalled(this.workshopItemId))
			{
				return false;
			}
			this.path = GameManager.instance.steamworks.GetSubscribedItemPath(this.workshopItemId);
			string[] array = this.path.Replace('\\', '/').Split(new char[]
			{
				'/'
			});
			this.directoryName = array[array.Length - 1];
		}
		this.LoadContent();
		if (this.HasLoadedContent() && this.content.HasIconImage())
		{
			ModManager.LoadTextureFile(this.content.iconFile, this);
		}
		return this.HasLoadedContent();
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0007F078 File Offset: 0x0007D278
	private void LoadContent()
	{
		try
		{
			this.content = new FolderModContent(this.path);
			if (this.content.allContent.Count > 0)
			{
				this.fileCreatedTime = this.content.allContent[0].LastWriteTime;
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			this.content = null;
			this.contentError = true;
		}
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0007F0EC File Offset: 0x0007D2EC
	private void GenerateDescriptionFromContent()
	{
		List<FileInfo> maps = this.content.GetMaps();
		string str = "This mod contains ";
		if (this.content.HasGameContent())
		{
			str += "custom game content";
			if (maps.Count > 0)
			{
				str += " and ";
			}
		}
		if (maps.Count > 0)
		{
			if (maps.Count == 1)
			{
				str += "1 map: ";
			}
			else
			{
				str = str + maps.Count.ToString() + " maps: ";
			}
			for (int i = 0; i < maps.Count; i++)
			{
				str += maps[i].Name.Substring(0, maps[i].Name.Length - 4);
				if (i < maps.Count - 1)
				{
					str += ", ";
				}
			}
		}
		this.description = str;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0000B6BC File Offset: 0x000098BC
	public bool HasLoadedContent()
	{
		return this.content != null;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0000B6C7 File Offset: 0x000098C7
	public bool HasWeaponContentMods()
	{
		return this.weaponContentMods.Count > 0;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0000B6D7 File Offset: 0x000098D7
	public bool HasVehicleContentMods()
	{
		return this.vehicleContentMods.Count > 0;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0000B6E7 File Offset: 0x000098E7
	public bool HasSkinContentMods()
	{
		return this.skinContentMods.Count > 0;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0000B6F7 File Offset: 0x000098F7
	public bool IsDownloadingContent()
	{
		return !this.HasLoadedContent() && !this.contentError;
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0000B70C File Offset: 0x0000990C
	public bool IsWorkshopItem()
	{
		return this.workshopItemId.m_PublishedFileId > 0UL;
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0007F1D4 File Offset: 0x0007D3D4
	public void SetupPanel(ModPanel panel)
	{
		this.panel = panel;
		if (this.IsWorkshopItem())
		{
			this.panel.SetupWorkshopMod(this.workshopItemId.m_PublishedFileId);
			if (this.IsDownloadingContent())
			{
				this.panel.ShowLoading();
			}
		}
		else
		{
			this.panel.SetupStagedMod();
		}
		this.panel.SetText(this.title, this.description);
		if (this.enabled)
		{
			this.panel.ShowEnabled();
		}
		else
		{
			this.panel.ShowDisabled();
		}
		if (this.contentError)
		{
			if (this.IsWorkshopItem())
			{
				this.panel.ShowError("Could not load mod folder. Please exit the game and validate your local files.");
			}
			else
			{
				this.panel.ShowError("Could not load mod folder");
			}
		}
		if (this.content.HasIconImage())
		{
			this.panel.SetIconTexture(this.iconTexture);
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0000B71D File Offset: 0x0000991D
	public void ItemDownloadComplete()
	{
		if (this.panel != null)
		{
			this.panel.EndLoading();
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0000B738 File Offset: 0x00009938
	public void UpdateInfo(string title, string description)
	{
		this.hasFetchedWorkshopInfo = true;
		this.title = title;
		this.description = description;
		if (this.panel != null)
		{
			this.panel.SetText(this.title, this.description);
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0000B774 File Offset: 0x00009974
	public void ToggleEnabled()
	{
		this.SetEnabled(!this.enabled);
		if (this.HasLoadedContent())
		{
			ModManager.instance.ContentChanged();
		}
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0007F2B0 File Offset: 0x0007D4B0
	private void SetEnabled(bool enabled)
	{
		this.enabled = enabled;
		PlayerPrefs.SetInt(this.PlayerPreferenceKey(), this.enabled ? 1 : 0);
		PlayerPrefs.Save();
		if (this.panel != null)
		{
			if (this.enabled)
			{
				this.panel.ShowEnabled();
				return;
			}
			this.panel.ShowDisabled();
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0000B797 File Offset: 0x00009997
	private bool LoadEnabledPreference()
	{
		return !PlayerPrefs.HasKey(this.PlayerPreferenceKey()) || PlayerPrefs.GetInt(this.PlayerPreferenceKey()) != 0;
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x0000B7B6 File Offset: 0x000099B6
	public void ReapplyPanelState()
	{
		if (this.panel != null)
		{
			if (this.enabled)
			{
				this.panel.ShowEnabled();
				return;
			}
			this.panel.ShowDisabled();
		}
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0000B7E5 File Offset: 0x000099E5
	public bool IsActive()
	{
		return this.enabled && this.HasLoadedContent();
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0000B7F7 File Offset: 0x000099F7
	private string PlayerPreferenceKey()
	{
		if (this.IsWorkshopItem())
		{
			return "EnableWMod_" + this.workshopItemId.m_PublishedFileId.ToString();
		}
		return "EnableSMod_" + this.title;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0007F310 File Offset: 0x0007D510
	public override string ToString()
	{
		if (this.isOfficialContent)
		{
			return "Official Content";
		}
		if (!this.IsWorkshopItem())
		{
			return "Staged mod: " + this.title;
		}
		if (this.hasFetchedWorkshopInfo)
		{
			return "Workshop mod #" + this.workshopItemId.m_PublishedFileId.ToString() + ": " + this.title;
		}
		return "Workshop mod #" + this.workshopItemId.m_PublishedFileId.ToString();
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x0007F38C File Offset: 0x0007D58C
	public int CompareTo(ModInformation other)
	{
		if (this.isOfficialContent && !other.isOfficialContent)
		{
			return -1;
		}
		if (!this.isOfficialContent && other.isOfficialContent)
		{
			return 1;
		}
		if (this.isStaged && !other.isStaged)
		{
			return -1;
		}
		if (!this.isStaged && other.isStaged)
		{
			return 1;
		}
		return -this.fileCreatedTime.CompareTo(other.fileCreatedTime);
	}

	// Token: 0x04000F26 RID: 3878
	public static ModInformation OfficialContent = new ModInformation("")
	{
		isOfficialContent = true,
		directoryName = "",
		title = "Official",
		fileCreatedTime = DateTime.Now
	};

	// Token: 0x04000F27 RID: 3879
	public static ModInformation Unknown = new ModInformation("")
	{
		directoryName = "Unknown",
		title = "Unknown",
		fileCreatedTime = DateTime.Now
	};

	// Token: 0x04000F28 RID: 3880
	public string path;

	// Token: 0x04000F29 RID: 3881
	public string directoryName;

	// Token: 0x04000F2A RID: 3882
	public string title;

	// Token: 0x04000F2B RID: 3883
	public string description;

	// Token: 0x04000F2C RID: 3884
	public PublishedFileId_t workshopItemId;

	// Token: 0x04000F2D RID: 3885
	public FolderModContent content;

	// Token: 0x04000F2E RID: 3886
	public bool contentError;

	// Token: 0x04000F2F RID: 3887
	public bool enabled = true;

	// Token: 0x04000F30 RID: 3888
	public Texture2D iconTexture;

	// Token: 0x04000F31 RID: 3889
	private ModPanel panel;

	// Token: 0x04000F32 RID: 3890
	public DateTime fileCreatedTime;

	// Token: 0x04000F33 RID: 3891
	public List<VehicleContentMod> vehicleContentMods = new List<VehicleContentMod>();

	// Token: 0x04000F34 RID: 3892
	public List<WeaponContentMod> weaponContentMods = new List<WeaponContentMod>();

	// Token: 0x04000F35 RID: 3893
	public List<ActorSkinContentMod> skinContentMods = new List<ActorSkinContentMod>();

	// Token: 0x04000F36 RID: 3894
	public List<MutatorContentMod> mutatorContentMods = new List<MutatorContentMod>();

	// Token: 0x04000F37 RID: 3895
	public bool hideInModList;

	// Token: 0x04000F38 RID: 3896
	public int requiredGameVersion;

	// Token: 0x04000F39 RID: 3897
	private bool hasFetchedWorkshopInfo;

	// Token: 0x04000F3A RID: 3898
	[NonSerialized]
	public bool isOfficialContent;

	// Token: 0x04000F3B RID: 3899
	[NonSerialized]
	public bool isStaged;
}
