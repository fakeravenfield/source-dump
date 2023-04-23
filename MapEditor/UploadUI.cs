using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MapEditor.Internal.SanityCheck;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x0200071E RID: 1822
	public class UploadUI : WindowBase
	{
		// Token: 0x06002DA5 RID: 11685 RVA: 0x001067F0 File Offset: 0x001049F0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.rules = ValidationRule.FindAll();
			this.buttonConnect.onClick.AddListener(new UnityAction(this.ButtonConnect_Clicked));
			this.buttonRefreshItems.onClick.AddListener(new UnityAction(this.ButtonRefreshItems_Clicked));
			this.buttonCreateItem.onClick.AddListener(new UnityAction(this.ButtonCreateItem_Clicked));
			this.buttonOpenItemPage.onClick.AddListener(new UnityAction(this.ButtonOpenItemPage_Clicked));
			this.dropdownItems.onSelectedIndexChanged.AddListener(new UnityAction<int>(this.DropdownItems_SelectionChanged));
			this.buttonLoadImage.onClick.AddListener(new UnityAction(this.ButtonLoadImage_Click));
			this.dropdownItems.SetOptions(Array.Empty<string>());
			this.buttonPublish.onClick.AddListener(new UnityAction(this.ButtonPublish_Click));
			this.buttonLicense.onClick.AddListener(new UnityAction(this.ButtonLicense_Click));
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x00106900 File Offset: 0x00104B00
		protected override void OnShow()
		{
			base.OnShow();
			if (this.IsLevelValid())
			{
				if (this.IsSteamworksInitialized())
				{
					if (this.steamworks.HasCurrentItem())
					{
						this.DisplayItem(this.steamworks.currentItem);
						this.ShowStep(UploadUI.Step.EnterDetails);
					}
					else
					{
						this.DisplayItem(null);
						this.ShowStep(UploadUI.Step.SelectItem);
						if (!this.HasLocalItems() && !this.isLoadingLocalItems)
						{
							this.FindLocalItems();
						}
					}
				}
				else
				{
					this.ShowStep(UploadUI.Step.Connect);
				}
			}
			else
			{
				this.ShowStep(UploadUI.Step.Validate);
			}
			base.StartCoroutine(this.MonitorUploadProgress());
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		protected override void OnHide()
		{
			base.OnHide();
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0001F6F2 File Offset: 0x0001D8F2
		private void Update()
		{
			if (this.steamworks != null)
			{
				this.steamworks.Update();
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x0001F707 File Offset: 0x0001D907
		private IEnumerator MonitorUploadProgress()
		{
			MapEditorUI editorUI = MapEditor.instance.GetEditorUI();
			while (base.IsVisible())
			{
				if (this.steamworks != null && this.steamworks.IsUploadingItem())
				{
					editorUI.progressWindow.SetTitle("UPLOADING");
					editorUI.progressWindow.SetStatus("Uploading item to Steam Workshop");
					editorUI.progressWindow.SetProgress(0f);
					editorUI.progressWindow.Show();
					while (this.steamworks.IsUploadingItem())
					{
						float uploadProgress = this.steamworks.GetUploadProgress();
						editorUI.progressWindow.SetProgress(uploadProgress);
						yield return null;
					}
					editorUI.progressWindow.Hide();
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x00106990 File Offset: 0x00104B90
		private void ShowStep(UploadUI.Step step)
		{
			this.panelValidate.gameObject.SetActive(step >= UploadUI.Step.Validate);
			this.panelConnect.gameObject.SetActive(step >= UploadUI.Step.Connect);
			this.panelSelectItem.gameObject.SetActive(step >= UploadUI.Step.SelectItem);
			this.panelEnterDetails.gameObject.SetActive(step >= UploadUI.Step.EnterDetails);
			this.panelPublish.gameObject.SetActive(step >= UploadUI.Step.EnterDetails);
			this.buttonOpenItemPage.gameObject.SetActive(step >= UploadUI.Step.EnterDetails);
			if (step < UploadUI.Step.EnterDetails)
			{
				this.buttonPublish.interactable = true;
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x00106A38 File Offset: 0x00104C38
		private void ShowSteamworksError(string when)
		{
			string text = when + "\r\n\r\n" + this.steamworks.errorMessage;
			if (this.steamworks.lastResult == EResult.k_EResultTimeout)
			{
				text += "\r\n\r\nSince a timeout occured it could be necessary to wait a few minutes before trying again.";
			}
			MessageUI.ShowMessage(text, "Error", null);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x00106A84 File Offset: 0x00104C84
		private void Steamworks_StateChanged()
		{
			if (this.IsSteamworksInitialized())
			{
				this.textConnectionStatus.text = "Connected to Steam as " + this.steamworks.Username();
				if (this.steamworks.HasCurrentItem())
				{
					this.DisplayItem(this.steamworks.currentItem);
					this.ShowStep(UploadUI.Step.EnterDetails);
					return;
				}
				this.DisplayItem(null);
				this.ShowStep(UploadUI.Step.SelectItem);
				if (!this.HasLocalItems() && !this.isLoadingLocalItems)
				{
					this.FindLocalItems();
					return;
				}
			}
			else
			{
				this.textConnectionStatus.text = "Unable to connect to Steam";
				this.DisplayItem(null);
				this.ShowStep(UploadUI.Step.Connect);
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0001F716 File Offset: 0x0001D916
		private void Steamworks_CreateItemDone(bool ok, PublishedFileId_t itemId)
		{
			if (!ok)
			{
				this.ShowSteamworksError("Unable to create a new Steam Workshop item.");
				return;
			}
			if (this.steamworks.HasCurrentItem() && !Directory.Exists(this.CurrentItemStagingPath()))
			{
				Directory.CreateDirectory(this.CurrentItemStagingPath());
			}
			this.FindLocalItems();
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x0001F753 File Offset: 0x0001D953
		private void Steamworks_SubmitItemDone(bool ok)
		{
			if (!ok)
			{
				this.ShowSteamworksError("Unable to publish the Steam Workshop item.");
				return;
			}
			MessageUI.ShowMessage("Your level was successfully published on the Steam Workshop!\r\n\r\nPlease visit the workshop page and verify the submited values. A browser window should open automatically.", "Success", null);
			this.steamworks.OpenCommunityFilePage(this.steamworks.currentItem.itemId);
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x00106B24 File Offset: 0x00104D24
		private void Steamworks_LocalItemDetails(bool ok, SteamworksWrapper.UGCQueryResult[] details)
		{
			this.isLoadingLocalItems = false;
			if (!ok)
			{
				this.dropdownItems.SetOptions(new string[0]);
				this.ShowSteamworksError("Unable to get details about local Steam Workshop items.");
				return;
			}
			this.localDetails = details;
			this.localItems = (from d in details
			select d.details.m_nPublishedFileId.m_PublishedFileId).ToArray<ulong>();
			if (this.HasLocalItems())
			{
				string[] options = details.Select((SteamworksWrapper.UGCQueryResult r, int i) => this.GetItemTitle(this.localItems[i], r.details)).ToArray<string>();
				this.dropdownItems.SetOptions(options);
				this.SelectCurrentItemInDropdown();
				this.DropdownItems_SelectionChanged(this.dropdownItems.GetSelectedIndex());
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00106BD4 File Offset: 0x00104DD4
		private bool IsLevelValid()
		{
			bool flag = true;
			this.listViewProblems.gameObject.SetActive(true);
			this.listViewProblems.Clear();
			ValidationRule[] array = this.rules;
			for (int i = 0; i < array.Length; i++)
			{
				ValidationResult validationResult;
				if (!array[i].Validate(out validationResult))
				{
					flag = false;
					this.listViewProblems.Add(validationResult.message, new UnityAction(validationResult.openDetails.Invoke));
				}
			}
			if (flag)
			{
				this.textProblemCounter.text = "No problems found!";
				this.listViewProblems.gameObject.SetActive(false);
			}
			else
			{
				this.textProblemCounter.text = "Please fix the following problems:";
			}
			return flag;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0001F78F File Offset: 0x0001D98F
		private bool IsSteamworksInitialized()
		{
			return this.steamworks != null && this.steamworks.isInitialized;
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00106C7C File Offset: 0x00104E7C
		private bool InitializeSteamworks()
		{
			if (this.IsSteamworksInitialized())
			{
				return true;
			}
			this.steamworks = new SteamworksWrapper();
			this.steamworks.OnStateChanged = new SteamworksWrapper.DelOnStateChanged(this.Steamworks_StateChanged);
			this.steamworks.OnCreateItemDone = new SteamworksWrapper.DelOnCreateItemDone(this.Steamworks_CreateItemDone);
			this.steamworks.OnSubmitItemDone = new SteamworksWrapper.DelOnSubmitItemDone(this.Steamworks_SubmitItemDone);
			return this.steamworks.Initialize();
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00106CF0 File Offset: 0x00104EF0
		private void ButtonConnect_Clicked()
		{
			Debug.Log("Steamworks started? " + this.InitializeSteamworks().ToString());
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x0001F7A6 File Offset: 0x0001D9A6
		public string GetItemTitle(ulong itemId, SteamUGCDetails_t details)
		{
			if (!string.IsNullOrEmpty(details.m_rgchTitle))
			{
				return string.Format("{0} ({1})", details.m_rgchTitle, itemId);
			}
			return itemId.ToString();
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
		public string StagingFolderPath()
		{
			return Path.Combine(MapDescriptor.DATA_PATH, "Workshop Staging");
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00106D1C File Offset: 0x00104F1C
		public string CurrentItemStagingPath()
		{
			if (!this.steamworks.isInitialized)
			{
				throw new Exception("Steamworks is not initialized.");
			}
			if (!this.steamworks.HasCurrentItem())
			{
				throw new Exception("Steamworks has no current item.");
			}
			string path = this.steamworks.currentItem.itemId.m_PublishedFileId.ToString();
			string path2 = Path.Combine("Workshop Staging", path);
			return Path.Combine(MapDescriptor.DATA_PATH, path2);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0001F7E6 File Offset: 0x0001D9E6
		private bool HasLocalItems()
		{
			return this.localItems != null && this.localItems.Length != 0;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00106D8C File Offset: 0x00104F8C
		private void FindLocalItems()
		{
			List<ulong> list = new List<ulong>();
			if (!Directory.Exists(this.StagingFolderPath()))
			{
				Directory.CreateDirectory(this.StagingFolderPath());
			}
			string[] directories = Directory.GetDirectories(this.StagingFolderPath());
			for (int i = 0; i < directories.Length; i++)
			{
				string[] array = directories[i].Replace('\\', '/').Split(new char[]
				{
					'/'
				});
				ulong item;
				if (ulong.TryParse(array[array.Length - 1], out item))
				{
					list.Add(item);
				}
			}
			list.Sort((ulong x, ulong y) => y.CompareTo(x));
			this.isLoadingLocalItems = true;
			this.localItems = null;
			this.localDetails = null;
			this.dropdownItems.SetOptions(new string[]
			{
				"Loading..."
			});
			this.steamworks.SetCurrentItem(null);
			if (list.Any<ulong>())
			{
				PublishedFileId_t[] fileIds = (from id in list
				select new PublishedFileId_t(id)).ToArray<PublishedFileId_t>();
				this.steamworks.QuickQueryItemInfo(fileIds, new SteamworksWrapper.DelOnUGCQueryDone(this.Steamworks_LocalItemDetails));
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00106EB0 File Offset: 0x001050B0
		private void SelectCurrentItemInDropdown()
		{
			int selectedIndex = 0;
			if (this.steamworks.HasCurrentItem())
			{
				ulong publishedFileId = this.steamworks.currentItem.itemId.m_PublishedFileId;
				for (int i = 0; i < this.localItems.Length; i++)
				{
					if (this.localItems[i] == publishedFileId)
					{
						selectedIndex = i;
						break;
					}
				}
			}
			this.dropdownItems.SetSelectedIndex(selectedIndex);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		private void ButtonRefreshItems_Clicked()
		{
			this.FindLocalItems();
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0001F804 File Offset: 0x0001DA04
		private void ButtonCreateItem_Clicked()
		{
			if (!this.IsSteamworksInitialized())
			{
				throw new Exception("Steamworks is not initialized.");
			}
			this.steamworks.CreateWorkshopItem();
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0001F824 File Offset: 0x0001DA24
		private void ButtonOpenItemPage_Clicked()
		{
			if (!this.IsSteamworksInitialized())
			{
				throw new Exception("Steamworks is not initialized.");
			}
			if (this.steamworks.currentItem != null)
			{
				this.steamworks.OpenCommunityFilePage(this.steamworks.currentItem.itemId);
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x00106F10 File Offset: 0x00105110
		private void DropdownItems_SelectionChanged(int selectedIndex)
		{
			if (!this.IsSteamworksInitialized())
			{
				throw new Exception("Steamworks is not initialized.");
			}
			if (!this.HasLocalItems())
			{
				throw new Exception("Has no local items.");
			}
			if (selectedIndex < this.localItems.Length)
			{
				ulong currentItemId = this.localItems[selectedIndex];
				this.steamworks.SetCurrentItemId(currentItemId);
			}
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x00106F64 File Offset: 0x00105164
		private SteamUGCDetails_t? GetItemDetails(SteamworksWrapper.WorkshopItem item)
		{
			SteamUGCDetails_t? result = null;
			if (this.localDetails != null)
			{
				foreach (SteamworksWrapper.UGCQueryResult ugcqueryResult in this.localDetails)
				{
					if (ugcqueryResult.details.m_nPublishedFileId == item.itemId)
					{
						result = new SteamUGCDetails_t?(ugcqueryResult.details);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x00106FC8 File Offset: 0x001051C8
		private void DisplayItem(SteamworksWrapper.WorkshopItem item)
		{
			if (this.displayedItem == item)
			{
				return;
			}
			this.displayedItem = item;
			this.inputName.SetText("");
			this.inputDescription.SetText("");
			this.togglePublic.isOn = true;
			this.inputChangelog.SetText("");
			this.imagePreview.texture = null;
			SteamUGCDetails_t? itemDetails = this.GetItemDetails(item);
			if (itemDetails != null)
			{
				this.inputName.SetText(itemDetails.Value.m_rgchTitle);
				this.inputDescription.SetText(itemDetails.Value.m_rgchDescription);
				this.togglePublic.isOn = (itemDetails.Value.m_eVisibility == ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
			}
			if (item != null)
			{
				this.LoadDescriptorIconFile();
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x00107098 File Offset: 0x00105298
		private void LoadDescriptorIconFile()
		{
			try
			{
				string text = PhotoTool.DescriptorIconFilePath();
				if (string.IsNullOrEmpty(text))
				{
					throw new Exception("File path is empty. Please capture a photo and try again.");
				}
				if (Path.GetExtension(text).ToLower() != ".png")
				{
					throw new Exception("Only PNG files are supported. Please make sure the file name ends with \".PNG\".");
				}
				byte[] array = File.ReadAllBytes(text);
				if (array.Length > 1000000)
				{
					throw new Exception("File is too large. File size must be less than 1 MB.");
				}
				Texture2D texture2D = new Texture2D(4, 4, TextureFormat.RGB24, false);
				if (!texture2D.LoadImage(array, false))
				{
					throw new Exception("File is not a valid PNG image. Please try with another file.");
				}
				texture2D.name = text;
				if (texture2D.width < 32 || texture2D.height < 32)
				{
					throw new Exception("Image resolution too low. Please use an image larger than 32x32 pixels.");
				}
				this.imagePreview.texture = texture2D;
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage("Unable to load image file.\r\n\r\n" + ex.Message, "Error", null);
			}
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0001F861 File Offset: 0x0001DA61
		private void ButtonLoadImage_Click()
		{
			this.LoadDescriptorIconFile();
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00107180 File Offset: 0x00105380
		private void ValidateName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				IEnumerable<char> second = Path.GetInvalidPathChars().Concat("/\\:.");
				if (name.Intersect(second).Any<char>())
				{
					throw new Exception("Name contains invalid characters. Try using letters and numbers.");
				}
				name = name.Trim();
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Name is empty.");
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x0001F869 File Offset: 0x0001DA69
		private IEnumerator PublishAsync()
		{
			if (!this.steamworks.isInitialized)
			{
				throw new Exception("Steamworks is not initialized.");
			}
			if (!this.steamworks.HasCurrentItem())
			{
				throw new Exception("Steamworks has no current item.");
			}
			yield return MapEditor.instance.GenerateNavMeshAsync();
			string text = this.CurrentItemStagingPath();
			string text2 = Path.Combine(text, "icon.png");
			string text3 = Path.GetFileName(MapEditor.descriptorFilePath);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = "level.rfld";
			}
			string text4 = Path.Combine(text, text3);
			Debug.Log("Staging descriptor path: " + text4);
			FolderModContent folderModContent = new FolderModContent(text);
			folderModContent.Scan();
			if (folderModContent.HasAnyContent())
			{
				foreach (FileInfo fileInfo in folderModContent.allContent)
				{
					fileInfo.Delete();
				}
			}
			Texture2D texture2D = this.imagePreview.texture as Texture2D;
			if (texture2D)
			{
				byte[] bytes = texture2D.EncodeToPNG();
				File.WriteAllBytes(text2, bytes);
			}
			MapDescriptorSettings settings = new MapDescriptorSettings
			{
				generateNavMesh = false,
				savePathfinding = true,
				isAutosave = false
			};
			MapDescriptor.SaveScene(text4, settings);
			folderModContent.Scan();
			if (texture2D && !folderModContent.HasIconImage())
			{
				throw new Exception("Preview image not found in staging folder.");
			}
			if (!folderModContent.HasLevelDescriptor())
			{
				throw new Exception("Level descriptor not found in staging folder.");
			}
			if (folderModContent.HasDisallowedFiles() || folderModContent.allContent.Count != 1)
			{
				throw new Exception("Disallowed content found in staging folder. Please make sure the staging folder is empty and then try again.");
			}
			folderModContent.MarkAsUpdated(Path.GetFileName(text4));
			this.steamworks.currentItem.title = this.inputName.GetText();
			this.steamworks.currentItem.description = this.inputDescription.GetText();
			this.steamworks.currentItem.visibility = (this.togglePublic.isOn ? SteamworksWrapper.WorkshopItem.Visibility.Public : SteamworksWrapper.WorkshopItem.Visibility.Private);
			if (texture2D)
			{
				this.steamworks.currentItem.previewImagePath = text2;
			}
			this.steamworks.currentItem.tags = new List<string>();
			this.steamworks.currentItem.tags.Add("Level Descriptor");
			this.steamworks.currentItem.tags.Add("Maps");
			this.steamworks.SubmitCurrentItem(text, this.inputChangelog.GetText());
			yield break;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0001F878 File Offset: 0x0001DA78
		private void ButtonPublish_Click()
		{
			this.publishCoroutine = new MonitoredCoroutine(new Action<Exception>(this.PublishCoroutine_Done));
			base.StartCoroutine(this.publishCoroutine.Monitor(this.PublishAsync()));
			this.buttonPublish.interactable = false;
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0001F8B5 File Offset: 0x0001DAB5
		private void PublishCoroutine_Done(Exception error)
		{
			if (error != null)
			{
				MessageUI.ShowMessage("An error occured while preparing to publish your level. Please fix the following issue and then try again.\r\n\r\n" + error.Message, "Error", null);
			}
			this.buttonPublish.interactable = true;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0001F8E1 File Offset: 0x0001DAE1
		private void ButtonLicense_Click()
		{
			Application.OpenURL("http://steamcommunity.com/sharedfiles/workshoplegalagreement");
		}

		// Token: 0x040029F8 RID: 10744
		private const string STAGING_FOLDER = "Workshop Staging";

		// Token: 0x040029F9 RID: 10745
		private const int MAX_ICON_FILE_SIZE_BYTES = 1000000;

		// Token: 0x040029FA RID: 10746
		private const string LEVEL_DESCRIPTOR_FILE_NAME = "level.rfld";

		// Token: 0x040029FB RID: 10747
		private const string LEVEL_DESCRIPTOR_TAG = "Level Descriptor";

		// Token: 0x040029FC RID: 10748
		private const string MAPS_TAG = "Maps";

		// Token: 0x040029FD RID: 10749
		public RectTransform panelValidate;

		// Token: 0x040029FE RID: 10750
		public Text textProblemCounter;

		// Token: 0x040029FF RID: 10751
		public ListView listViewProblems;

		// Token: 0x04002A00 RID: 10752
		public RectTransform panelConnect;

		// Token: 0x04002A01 RID: 10753
		public Button buttonConnect;

		// Token: 0x04002A02 RID: 10754
		public Text textConnectionStatus;

		// Token: 0x04002A03 RID: 10755
		public RectTransform panelSelectItem;

		// Token: 0x04002A04 RID: 10756
		public DropdownWithText dropdownItems;

		// Token: 0x04002A05 RID: 10757
		public Button buttonRefreshItems;

		// Token: 0x04002A06 RID: 10758
		public Button buttonCreateItem;

		// Token: 0x04002A07 RID: 10759
		public Button buttonOpenItemPage;

		// Token: 0x04002A08 RID: 10760
		public RectTransform panelEnterDetails;

		// Token: 0x04002A09 RID: 10761
		public InputWithText inputName;

		// Token: 0x04002A0A RID: 10762
		public InputWithText inputDescription;

		// Token: 0x04002A0B RID: 10763
		public InputWithText inputChangelog;

		// Token: 0x04002A0C RID: 10764
		public RawImage imagePreview;

		// Token: 0x04002A0D RID: 10765
		public Button buttonLoadImage;

		// Token: 0x04002A0E RID: 10766
		public RectTransform panelPublish;

		// Token: 0x04002A0F RID: 10767
		public Toggle togglePublic;

		// Token: 0x04002A10 RID: 10768
		public Button buttonPublish;

		// Token: 0x04002A11 RID: 10769
		public Button buttonLicense;

		// Token: 0x04002A12 RID: 10770
		private ValidationRule[] rules;

		// Token: 0x04002A13 RID: 10771
		private SteamworksWrapper steamworks;

		// Token: 0x04002A14 RID: 10772
		private SteamworksWrapper.WorkshopItem displayedItem;

		// Token: 0x04002A15 RID: 10773
		private bool isLoadingLocalItems;

		// Token: 0x04002A16 RID: 10774
		private ulong[] localItems;

		// Token: 0x04002A17 RID: 10775
		private SteamworksWrapper.UGCQueryResult[] localDetails;

		// Token: 0x04002A18 RID: 10776
		private MonitoredCoroutine publishCoroutine;

		// Token: 0x0200071F RID: 1823
		private enum Step
		{
			// Token: 0x04002A1A RID: 10778
			Validate,
			// Token: 0x04002A1B RID: 10779
			Connect,
			// Token: 0x04002A1C RID: 10780
			SelectItem,
			// Token: 0x04002A1D RID: 10781
			EnterDetails
		}
	}
}
