using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006D6 RID: 1750
	public class LoadMapUI : WindowBase
	{
		// Token: 0x06002C0F RID: 11279 RVA: 0x00102E14 File Offset: 0x00101014
		protected override void Awake()
		{
			base.Awake();
			this.editor = MapEditor.instance;
			this.showAutosaveFiles.onValueChanged.AddListener(delegate(bool _)
			{
				this.BuildMapList();
			});
			this.buttonLoad.onClick.AddListener(new UnityAction(this.ButtonLoadClicked));
			this.buttonCancel.onClick.AddListener(new UnityAction(base.Hide));
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00102E88 File Offset: 0x00101088
		public void SetMode(SceneConstructor.Mode mode)
		{
			this.mode = mode;
			string text = (mode == SceneConstructor.Mode.Edit) ? "LOAD" : "PLAY";
			this.buttonLoad.SetText(text);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0001E401 File Offset: 0x0001C601
		protected override void OnShow()
		{
			base.OnShow();
			this.showAutosaveFiles.isOn = MapEditorPrefs.showAutosaveFiles;
			this.BuildMapList();
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00102EB8 File Offset: 0x001010B8
		private void ButtonLoadClicked()
		{
			MapEditorPrefs.showAutosaveFiles = this.showAutosaveFiles.isOn;
			try
			{
				string text = this.mapName.GetText();
				string filePathToLoad = MapDescriptor.GetFilePathToLoad(text);
				if (File.Exists(filePathToLoad))
				{
					SceneConstructor.GotoLoadingScreen(filePathToLoad, this.mode, false);
				}
				else
				{
					MessageUI.ShowMessage("No map descriptor was found with that name. Please try another name, or select a map from the list.\r\n\r\nEntered name: '" + text + "'", "Not found", null);
				}
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage(string.Format("An error occurred while trying to load the map descriptor.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
				Debug.LogException(ex);
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00102F5C File Offset: 0x0010115C
		private void BuildMapList()
		{
			this.mapList.Clear();
			try
			{
				using (IEnumerator<string> enumerator = (from d in MapDescriptor.FindMapDescriptorsFast(this.showAutosaveFiles.isOn)
				select Path.GetFileNameWithoutExtension(d)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string mapName = enumerator.Current;
						this.mapList.Add(mapName, delegate
						{
							this.ItemClicked(mapName);
						});
					}
				}
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage(string.Format("An error occurred while looking for map files. The map list will be empty. You can still enter a name and load the map.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
				Debug.LogException(ex);
			}
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0001E41F File Offset: 0x0001C61F
		private void ItemClicked(string mapName)
		{
			this.mapName.SetText(mapName);
		}

		// Token: 0x040028A7 RID: 10407
		public ListView mapList;

		// Token: 0x040028A8 RID: 10408
		public InputWithText mapName;

		// Token: 0x040028A9 RID: 10409
		public Toggle showAutosaveFiles;

		// Token: 0x040028AA RID: 10410
		public Button buttonLoad;

		// Token: 0x040028AB RID: 10411
		public Button buttonCancel;

		// Token: 0x040028AC RID: 10412
		private SceneConstructor.Mode mode;

		// Token: 0x040028AD RID: 10413
		private MapEditor editor;
	}
}
