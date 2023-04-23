using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x02000705 RID: 1797
	public class SaveMapUI : WindowBase
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x00105144 File Offset: 0x00103344
		protected override void Awake()
		{
			base.Awake();
			this.editor = MapEditor.instance;
			this.buttonSave.onClick.AddListener(new UnityAction(this.ButtonSaveClicked));
			this.buttonCancel.onClick.AddListener(new UnityAction(this.ButtonCancelClicked));
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0010519C File Offset: 0x0010339C
		protected override void OnShow()
		{
			base.OnShow();
			this.dialogResult = SaveMapUI.DialogResult.Canceled;
			this.savedFilePath = null;
			if (MapEditor.HasDescriptorFilePath())
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(MapEditor.descriptorFilePath);
				this.mapName.SetText(fileNameWithoutExtension);
			}
			this.BuildMapList();
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0001EFB2 File Offset: 0x0001D1B2
		protected override void OnHide()
		{
			base.OnHide();
			if (this.callback != null)
			{
				this.callback(this.dialogResult, this.savedFilePath);
				this.callback = null;
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0001EFE0 File Offset: 0x0001D1E0
		public void SetCallback(SaveMapUI.Callback callback)
		{
			this.callback = callback;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0001EFE9 File Offset: 0x0001D1E9
		public void QuickSave()
		{
			if (MapEditor.HasDescriptorFilePath())
			{
				base.StartCoroutine(this.SaveDescriptorRoutine(true));
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0001F000 File Offset: 0x0001D200
		private void ButtonCancelClicked()
		{
			this.dialogResult = SaveMapUI.DialogResult.Canceled;
			this.savedFilePath = null;
			base.Hide();
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0001F016 File Offset: 0x0001D216
		private void ButtonSaveClicked()
		{
			base.StartCoroutine(this.SaveDescriptorRoutine(false));
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0001F026 File Offset: 0x0001D226
		private IEnumerator SaveDescriptorRoutine(bool quickSave = false)
		{
			bool savePathfinding = this.togglePathfinding.isOn && !quickSave;
			if (savePathfinding && !this.AreAllNeighboursAssigned())
			{
				yield return MessageUI.ShowMessageRoutine("Neighbours are unassigned. Pathfinding will not work as expected.\r\n\r\nUse the NEIGHBOURS dialog to correct the problem.", "Warning");
			}
			string text = this.mapName.GetText();
			if (text != null)
			{
				text = text.Trim(new char[]
				{
					' ',
					'\t',
					'.'
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				MessageUI.ShowMessage("Please enter a file name. We need to know what to call this map!", "Enter a file name", null);
				yield break;
			}
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			if (text.Intersect(invalidFileNameChars).Any<char>())
			{
				MessageUI.ShowMessage("The entered file name contains invalid characters. Use only alpha numeric characters such as ABC and 123.", "Invalid file name", null);
				yield break;
			}
			string filePath = MapDescriptor.GetFilePathToSave(text);
			bool flag = false;
			try
			{
				flag = File.Exists(filePath);
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage(string.Format("An error occurred while trying to figure out if a map descriptor with the same name already exists. Please try another name.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
				Debug.LogException(ex);
				yield break;
			}
			if (flag && !quickSave)
			{
				string message = "Overwrite existing file?";
				yield return MessageUI.ShowQuestionRoutine(message, "Overwrite");
				if (MessageUI.GetInstance().GetDialogResult() == MessageUI.DialogResult.Yes)
				{
					yield return this.SaveDescriptor(filePath, savePathfinding);
				}
			}
			else
			{
				yield return this.SaveDescriptor(filePath, savePathfinding);
			}
			yield break;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0001F03C File Offset: 0x0001D23C
		private IEnumerator SaveDescriptor(string filePath, bool savePathfinding)
		{
			if (savePathfinding)
			{
				MapEditor.instance.HideNavMesh();
				yield return MapEditor.instance.GenerateNavMeshAsync();
			}
			try
			{
				MapDescriptorSettings settings = new MapDescriptorSettings
				{
					generateNavMesh = false,
					savePathfinding = savePathfinding,
					isAutosave = false
				};
				MapDescriptor.SaveScene(filePath, settings);
				this.savedFilePath = filePath;
				this.dialogResult = SaveMapUI.DialogResult.Saved;
				MapEditor.descriptorFilePath = filePath;
			}
			catch (Exception ex)
			{
				MessageUI.ShowMessage(string.Format("An error occurred while saving the map descriptor to disk. Please try another name and with pathfinding disabled.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message), "Error", null);
				Debug.LogException(ex);
				yield break;
			}
			base.Hide();
			SystemMessagesUI.ShowMessage("Saved");
			yield break;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x001051E4 File Offset: 0x001033E4
		private void BuildMapList()
		{
			this.mapList.Clear();
			try
			{
				using (IEnumerator<string> enumerator = (from d in MapDescriptor.FindMapDescriptorsFast(false)
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
				string message = string.Format("An error occurred while looking for map files. The map list will be empty. You can still enter a name and save the map.\r\n\r\nError:\r\n{0}: {1}", ex.GetType().Name, ex.Message);
				Debug.LogException(ex);
				MessageUI.ShowMessageRoutine(message, "Error");
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0001F059 File Offset: 0x0001D259
		private void ItemClicked(string mapName)
		{
			this.mapName.SetText(mapName);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x001052C8 File Offset: 0x001034C8
		private bool AreAllNeighboursAssigned()
		{
			List<MeoCapturePoint> list = this.editor.FindObjectsToSave<MeoCapturePoint>().ToList<MeoCapturePoint>();
			foreach (MeoCapturePoint.Neighbour neighbour in list.SelectMany((MeoCapturePoint cp) => cp.GetNeighbours()).ToArray<MeoCapturePoint.Neighbour>())
			{
				list.Remove(neighbour.capturePointA);
				list.Remove(neighbour.capturePointB);
			}
			return !list.Any<MeoCapturePoint>();
		}

		// Token: 0x0400297F RID: 10623
		public ListView mapList;

		// Token: 0x04002980 RID: 10624
		public InputWithText mapName;

		// Token: 0x04002981 RID: 10625
		public Button buttonSave;

		// Token: 0x04002982 RID: 10626
		public Button buttonCancel;

		// Token: 0x04002983 RID: 10627
		public Toggle togglePathfinding;

		// Token: 0x04002984 RID: 10628
		private MapEditor editor;

		// Token: 0x04002985 RID: 10629
		private SaveMapUI.DialogResult dialogResult;

		// Token: 0x04002986 RID: 10630
		private string savedFilePath;

		// Token: 0x04002987 RID: 10631
		private SaveMapUI.Callback callback;

		// Token: 0x02000706 RID: 1798
		public enum DialogResult
		{
			// Token: 0x04002989 RID: 10633
			Saved,
			// Token: 0x0400298A RID: 10634
			Canceled
		}

		// Token: 0x02000707 RID: 1799
		// (Invoke) Token: 0x06002D16 RID: 11542
		public delegate void Callback(SaveMapUI.DialogResult result, string filePath);
	}
}
