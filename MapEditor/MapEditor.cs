using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x0200060D RID: 1549
	public class MapEditor : MonoBehaviour
	{
		// Token: 0x060027AE RID: 10158 RVA: 0x0001B638 File Offset: 0x00019838
		public static bool IsAvailable()
		{
			return MapEditor.instance && MapEditor.instance.gameObject;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0001B657 File Offset: 0x00019857
		public static bool HasDescriptorFilePath()
		{
			return !string.IsNullOrEmpty(MapEditor.descriptorFilePath);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000F8C18 File Offset: 0x000F6E18
		private void Awake()
		{
			MapEditor.instance = this;
			this.onSelectionChanged = new UnityEvent();
			this.onObjectCreated = new UnityEvent();
			this.onObjectDestroyed = new UnityEvent();
			this.SetGridSize(MapEditorPrefs.gridSize);
			this.autosaveInterval = MapEditorPrefs.autosaveInterval;
			this.lastAutosave = Time.time;
			this.input = this.GetOrCreateComponent<MeInput>();
			this.editorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			this.actionHistory = new ActionHistory();
			this.selection = Selection.empty;
			this.clipboard = new GameObject("Clipboard");
			this.clipboard.SetActive(false);
			this.objectsInClipboard = new List<MapEditorObject>();
			this.sceneryCamera = UnityEngine.Object.FindObjectOfType<MeoSceneryCamera>();
			if (this.sceneryCamera == null)
			{
				this.sceneryCamera = MeoSceneryCamera.Create(null);
			}
			this.cameras = new CameraBase[]
			{
				this.flyingCamera
			};
			this.SelectCamera(0);
			this.MuteAudio(MapEditorPrefs.muteAudio);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0001B666 File Offset: 0x00019866
		private void Start()
		{
			this.DayTime();
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0001B66E File Offset: 0x0001986E
		private void OnDestroy()
		{
			MapEditorPrefs.autosaveInterval = this.autosaveInterval;
			MapEditorPrefs.gridSize = this.GetGridSize();
			MapEditorPrefs.muteAudio = this.IsAudioMuted();
			this.MuteAudio(false);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000F8D10 File Offset: 0x000F6F10
		private void Update()
		{
			if (this.input.DeactivateTool())
			{
				MeTools.instance.SwitchToNoopTool();
			}
			this.HandleClickInScene();
			this.HandleCopyPaste();
			this.HandleDelete();
			this.HandleUndoRedo();
			this.HandleAutoSave();
			if (this.input.QuickSave() && !this.editorUI.saveMapWindow.IsVisible())
			{
				this.editorUI.ShowOnlySaveDialog();
				this.editorUI.saveMapWindow.QuickSave();
			}
			if (this.objectWasCreated)
			{
				this.objectWasCreated = false;
				this.onObjectCreated.Invoke();
			}
			if (this.objectWasDestroyed)
			{
				this.objectWasDestroyed = false;
				this.onObjectDestroyed.Invoke();
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000F8DC0 File Offset: 0x000F6FC0
		private void HandleClickInScene()
		{
			if (this.input.ClickInScene())
			{
				this.dragStarted = true;
			}
			bool flag = this.dragStarted && this.input.DragObjectInScene();
			bool flag2 = !this.wasDragging && flag;
			bool flag3 = this.wasDragging && flag;
			bool flag4 = this.wasDragging && !flag;
			this.wasDragging = flag;
			bool flag5 = true;
			if (MeTools.instance.GetCurrent() && !MeTools.instance.GetCurrent().IsSelectionChangeAllowed())
			{
				flag5 = false;
			}
			SelectionRectangle selectionRectangle = this.editorUI.selectionRectangle;
			if (!flag5)
			{
				this.dragStarted = false;
				selectionRectangle.Hide();
				return;
			}
			if (!flag2)
			{
				if (flag3)
				{
					selectionRectangle.SetEndPosition(Input.mousePosition);
					if (selectionRectangle.IsSizeThresholdReached())
					{
						selectionRectangle.Show();
						return;
					}
				}
				else if (flag4)
				{
					if (selectionRectangle.IsStartSet())
					{
						if (selectionRectangle.IsVisible())
						{
							SelectableObject[] objects = selectionRectangle.GetSelection();
							if (this.input.AddToSelectionModifier())
							{
								this.AddToSelection(objects);
								return;
							}
							this.SetSelection(new Selection(objects));
							return;
						}
						else
						{
							SelectableObject selectableObject = SelectableObject.RayCast(this.input.CursorToSceneRay());
							if (this.input.AddToSelectionModifier())
							{
								this.ToggleSelection(new SelectableObject[]
								{
									selectableObject
								});
								return;
							}
							this.SelectOnly(selectableObject);
							return;
						}
					}
				}
				else
				{
					this.dragStarted = false;
					selectionRectangle.Hide();
				}
				return;
			}
			SelectableGizmoPart selectableGizmoPart = SelectableGizmoPart.RayCast(this.input.CursorToSceneRay());
			if (selectableGizmoPart)
			{
				selectableGizmoPart.GetGizmo().Activate(selectableGizmoPart);
				return;
			}
			selectionRectangle.SetStartPosition(Input.mousePosition);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000F8F60 File Offset: 0x000F7160
		private void HandleCopyPaste()
		{
			if (this.input.DuplicateSelection())
			{
				Selection selection = this.GetSelection();
				if (selection.Any())
				{
					List<MapEditorObject> list = new List<MapEditorObject>();
					foreach (SelectableObject selectableObject in selection.GetObjects())
					{
						MapEditorObject component = selectableObject.GetComponent<MapEditorObject>();
						if (component && !selectableObject.IsActionDisabled(MapEditor.Action.Clone))
						{
							MapEditorObject item = component.Clone();
							list.Add(item);
						}
					}
					if (list.Any<MapEditorObject>())
					{
						MeTools.instance.SwitchToPlaceTool(list.ToArray());
						return;
					}
				}
			}
			else if (this.input.CopySelection() || this.input.CutSelection())
			{
				foreach (MapEditorObject mapEditorObject in this.objectsInClipboard)
				{
					mapEditorObject.Destroy();
				}
				this.objectsInClipboard.Clear();
				bool flag = this.input.CutSelection();
				Selection selection2 = this.GetSelection();
				if (selection2.Any())
				{
					foreach (SelectableObject selectableObject2 in selection2.GetObjects())
					{
						MapEditorObject component2 = selectableObject2.GetComponent<MapEditorObject>();
						if (component2 && !selectableObject2.IsActionDisabled(MapEditor.Action.Clone))
						{
							MapEditorObject mapEditorObject2 = component2.Clone();
							mapEditorObject2.transform.SetParent(this.clipboard.transform, true);
							this.objectsInClipboard.Add(mapEditorObject2);
							if (flag)
							{
								component2.Delete();
							}
						}
					}
					if (flag)
					{
						DeleteAction action = new DeleteAction(selection2);
						this.AddUndoableAction(action);
						this.SetSelection(Selection.empty);
						return;
					}
				}
			}
			else if (this.input.Paste())
			{
				List<MapEditorObject> list2 = new List<MapEditorObject>();
				foreach (MapEditorObject mapEditorObject3 in this.objectsInClipboard)
				{
					if (mapEditorObject3)
					{
						MapEditorObject mapEditorObject4 = mapEditorObject3.Clone();
						mapEditorObject4.transform.SetParent(null, true);
						list2.Add(mapEditorObject4);
					}
				}
				if (list2.Any<MapEditorObject>())
				{
					MeTools.instance.SwitchToPlaceTool(list2.ToArray());
				}
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000F91B0 File Offset: 0x000F73B0
		private void HandleDelete()
		{
			if (this.input.DeleteSelectedObjects())
			{
				Selection selection = this.GetSelection();
				if (selection.Any())
				{
					SelectableObject[] objects = selection.GetObjects();
					DeleteAction action = new DeleteAction(selection);
					this.AddUndoableAction(action);
					foreach (SelectableObject selectableObject in objects)
					{
						if (!selectableObject.IsActionDisabled(MapEditor.Action.Delete))
						{
							selectableObject.Delete();
						}
					}
				}
				this.SetSelection(Selection.empty);
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000F9220 File Offset: 0x000F7420
		private void HandleUndoRedo()
		{
			if (this.input.UndoAction() && this.actionHistory.CanUndo())
			{
				this.actionHistory.Undo();
				this.SetSelection(this.selection);
			}
			if (this.input.RedoAction() && this.actionHistory.CanRedo())
			{
				this.actionHistory.Redo();
				this.SetSelection(this.selection);
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000F9290 File Offset: 0x000F7490
		private void HandleAutoSave()
		{
			if (this.autosaveInterval <= 0f)
			{
				return;
			}
			if (Time.time - this.lastAutosave < this.autosaveInterval)
			{
				return;
			}
			this.lastAutosave = Time.time;
			try
			{
				MapDescriptorSettings settings = new MapDescriptorSettings
				{
					generateNavMesh = false,
					savePathfinding = false,
					isAutosave = true
				};
				string text = null;
				DateTime t = DateTime.MaxValue;
				for (int i = 0; i < 10; i++)
				{
					string filePathToSave = MapDescriptor.GetFilePathToSave("Autosave #" + i.ToString());
					if (!File.Exists(filePathToSave))
					{
						text = filePathToSave;
						break;
					}
					DateTime lastWriteTime = File.GetLastWriteTime(filePathToSave);
					if (lastWriteTime < t)
					{
						t = lastWriteTime;
						text = filePathToSave;
					}
				}
				MapDescriptor.SaveScene(text, settings);
				string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
				SystemMessagesUI.ShowMessage(string.Format("Autosaved as '{0}'", fileNameWithoutExtension));
			}
			catch (Exception exception)
			{
				Debug.LogError("Unable to autosave map descriptor. See next exception for details.");
				Debug.LogException(exception);
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0001B698 File Offset: 0x00019898
		private void AddToSelection(params SelectableObject[] objects)
		{
			if (this.selection.Any())
			{
				objects = this.selection.GetObjects().Concat(objects).ToArray<SelectableObject>();
			}
			this.SetSelection(new Selection(objects));
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000F9388 File Offset: 0x000F7588
		private void ToggleSelection(params SelectableObject[] objects)
		{
			if (this.selection.Any())
			{
				IEnumerable<SelectableObject> first = this.selection.GetObjects().Union(objects);
				IEnumerable<SelectableObject> second = this.selection.GetObjects().Intersect(objects);
				objects = first.Except(second).ToArray<SelectableObject>();
			}
			this.SetSelection(new Selection(objects));
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0001B6CB File Offset: 0x000198CB
		private void SelectOnly(SelectableObject obj)
		{
			if (obj)
			{
				this.SetSelection(new Selection(new SelectableObject[]
				{
					obj
				}));
				return;
			}
			this.SetSelection(Selection.empty);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000F93E0 File Offset: 0x000F75E0
		public void SetSelection(Selection selection)
		{
			SelectableObject[] objects = this.selection.RemoveDestroyed().GetObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].DisableOutline();
			}
			this.selection = selection.RemoveDestroyed();
			if (this.selection.Any())
			{
				objects = this.selection.GetObjects();
				for (int i = 0; i < objects.Length; i++)
				{
					objects[i].EnableOutline();
				}
			}
			this.onSelectionChanged.Invoke();
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000F9460 File Offset: 0x000F7660
		private void SelectCamera(int index)
		{
			if (index < 0 || index >= this.cameras.Length)
			{
				Debug.LogError("Camera index out of range");
				return;
			}
			if (this.selectedCameraIndex != index)
			{
				this.selectedCameraIndex = index;
				CameraBase[] array = this.cameras;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].gameObject.SetActive(false);
				}
				this.cameras[index].gameObject.SetActive(true);
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0001B6F6 File Offset: 0x000198F6
		public Camera GetCamera()
		{
			return this.cameras[this.selectedCameraIndex].GetComponent<Camera>();
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0001B70A File Offset: 0x0001990A
		public MeoSceneryCamera GetSceneryCamera()
		{
			return this.sceneryCamera;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0001B712 File Offset: 0x00019912
		public MeInput GetInput()
		{
			return this.input;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0001B71A File Offset: 0x0001991A
		public MapEditorUI GetEditorUI()
		{
			return this.editorUI;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0001B722 File Offset: 0x00019922
		public MapEditorTerrain GetEditorTerrain()
		{
			return this.editorTerrain;
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0001B72A File Offset: 0x0001992A
		public Selection GetSelection()
		{
			return this.selection;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0001B732 File Offset: 0x00019932
		public T[] FindObjectsToSave<T>() where T : MapEditorObject
		{
			return (from eo in UnityEngine.Object.FindObjectsOfType<T>()
			where eo.ShouldSave()
			select eo).ToArray<T>();
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x0001B762 File Offset: 0x00019962
		public void AddUndoableAction(UndoableAction action)
		{
			this.actionHistory.Add(action);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0001B770 File Offset: 0x00019970
		public ActionHistory GetActionHistory()
		{
			return this.actionHistory;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0001B778 File Offset: 0x00019978
		public void NotifyObjectCreated()
		{
			this.objectWasCreated = true;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0001B781 File Offset: 0x00019981
		public void NotifyObjectDestroyed()
		{
			this.objectWasDestroyed = true;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0001B78A File Offset: 0x0001998A
		public bool IsNavMeshVisible()
		{
			return PathfindingManager.instance.IsRenderingNavMesh();
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0001B796 File Offset: 0x00019996
		public void HideNavMesh()
		{
			PathfindingManager.instance.HideNavmeshes();
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0001B7A2 File Offset: 0x000199A2
		public void ShowNavMesh()
		{
			PathfindingManager.instance.DisplayNavmeshes();
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000F94D0 File Offset: 0x000F76D0
		public void GenerateNavMesh()
		{
			IEnumerator enumerator = this.GenerateNavMeshAsync();
			while (enumerator.MoveNext())
			{
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0001B7AE File Offset: 0x000199AE
		public IEnumerator GenerateNavMeshAsync()
		{
			this.editorUI.progressWindow.SetTitle("GENERATING NAV-MESH");
			this.editorUI.progressWindow.Show();
			MapEditorAssistant assistant = MapEditorAssistant.instance;
			WaterLevel.instance.gameObject.layer = LayerMask.NameToLayer("Water");
			Transform transform = assistant.pathfindingBoxLand.transform;
			Transform transform2 = assistant.pathfindingBoxWater.transform;
			float waterLevel = this.editorTerrain.GetWaterLevel();
			float num = this.editorTerrain.HighestMountain() + 10f;
			float num2 = (float)this.editorTerrain.GetTerrainSize() + 20f;
			transform.localScale = new Vector3(num2, num + 20f, num2);
			transform.position = new Vector3(0f, num / 2f + waterLevel - 10f, 0f);
			transform2.localScale = new Vector3(num2 * 2f, 5f, num2 * 2f);
			transform2.position = new Vector3(0f, waterLevel, 0f);
			PathfindingRelevantPoint[] array = UnityEngine.Object.FindObjectsOfType<PathfindingRelevantPoint>();
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i].gameObject);
			}
			MeoCapturePoint.DistributeSpawnPoints();
			PathfindingBox[] array2 = (from p in this.FindObjectsToSave<MapEditorObject>()
			select p.GetComponentInChildren<PathfindingBox>() into p
			where p != null && p != assistant.pathfindingBoxLand && p != assistant.pathfindingBoxWater
			select p).ToArray<PathfindingBox>();
			assistant.pathfindingBoxLand.blockers = array2;
			assistant.pathfindingBoxWater.blockers = (array2.Clone() as PathfindingBox[]);
			foreach (Progress progress in PathfindingManager.instance.ScanCustomLevelAsync())
			{
				this.editorUI.progressWindow.SetStatus(progress.description);
				this.editorUI.progressWindow.SetProgress(progress.progress);
				yield return null;
			}
			IEnumerator<Progress> enumerator = null;
			this.editorUI.progressWindow.Hide();
			yield break;
			yield break;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0001B7BD File Offset: 0x000199BD
		public float GetGridSize()
		{
			return this.gridSize;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0001B7C5 File Offset: 0x000199C5
		public void SetGridSize(float size)
		{
			this.gridSize = Mathf.Clamp(size, 0.01f, 10f);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0001B7DD File Offset: 0x000199DD
		public void DayTime()
		{
			MapEditorAssistant mapEditorAssistant = MapEditorAssistant.instance;
			mapEditorAssistant.timeOfDay.ApplyMapEditorDay();
			this.isNight = false;
			RenderSettings.fog = (mapEditorAssistant.timeOfDay.atmosphere.fogDensity > 0f);
			PostProcessingManager.FindAndApplyLevelColorGrading(false);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0001B817 File Offset: 0x00019A17
		public void NightTime()
		{
			MapEditorAssistant mapEditorAssistant = MapEditorAssistant.instance;
			mapEditorAssistant.timeOfDay.ApplyNight();
			this.isNight = true;
			RenderSettings.fog = (mapEditorAssistant.timeOfDay.atmosphere.fogDensity > 0f);
			PostProcessingManager.FindAndApplyLevelColorGrading(true);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0001B851 File Offset: 0x00019A51
		public void ReapplyTimeOfDay()
		{
			if (this.isNight)
			{
				this.NightTime();
				return;
			}
			this.DayTime();
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0001B868 File Offset: 0x00019A68
		public void MuteAudio(bool mute)
		{
			AudioListener.pause = mute;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0001B870 File Offset: 0x00019A70
		public bool IsAudioMuted()
		{
			return AudioListener.pause;
		}

		// Token: 0x040025A5 RID: 9637
		public static string descriptorFilePath;

		// Token: 0x040025A6 RID: 9638
		public static bool isTestingMap;

		// Token: 0x040025A7 RID: 9639
		public static MapEditor instance;

		// Token: 0x040025A8 RID: 9640
		public CameraBase flyingCamera;

		// Token: 0x040025A9 RID: 9641
		public MaterialList materialList;

		// Token: 0x040025AA RID: 9642
		public MapEditorTerrain editorTerrain;

		// Token: 0x040025AB RID: 9643
		[NonSerialized]
		public UnityEvent onSelectionChanged;

		// Token: 0x040025AC RID: 9644
		[NonSerialized]
		public UnityEvent onObjectCreated;

		// Token: 0x040025AD RID: 9645
		[NonSerialized]
		public UnityEvent onObjectDestroyed;

		// Token: 0x040025AE RID: 9646
		[NonSerialized]
		public float autosaveInterval;

		// Token: 0x040025AF RID: 9647
		[NonSerialized]
		public string mapDisplayName = "Unnamed Level";

		// Token: 0x040025B0 RID: 9648
		[NonSerialized]
		public bool isNight;

		// Token: 0x040025B1 RID: 9649
		private float gridSize;

		// Token: 0x040025B2 RID: 9650
		public const float MIN_GRID_SIZE = 0.01f;

		// Token: 0x040025B3 RID: 9651
		public const float MAX_GRID_SIZE = 10f;

		// Token: 0x040025B4 RID: 9652
		private MeInput input;

		// Token: 0x040025B5 RID: 9653
		private MapEditorUI editorUI;

		// Token: 0x040025B6 RID: 9654
		private ActionHistory actionHistory;

		// Token: 0x040025B7 RID: 9655
		private Selection selection;

		// Token: 0x040025B8 RID: 9656
		private bool dragStarted;

		// Token: 0x040025B9 RID: 9657
		private bool wasDragging;

		// Token: 0x040025BA RID: 9658
		private GameObject clipboard;

		// Token: 0x040025BB RID: 9659
		private List<MapEditorObject> objectsInClipboard;

		// Token: 0x040025BC RID: 9660
		private MeoSceneryCamera sceneryCamera;

		// Token: 0x040025BD RID: 9661
		private CameraBase[] cameras;

		// Token: 0x040025BE RID: 9662
		private int selectedCameraIndex;

		// Token: 0x040025BF RID: 9663
		private bool objectWasCreated;

		// Token: 0x040025C0 RID: 9664
		private bool objectWasDestroyed;

		// Token: 0x040025C1 RID: 9665
		private float lastAutosave;

		// Token: 0x0200060E RID: 1550
		[Flags]
		public enum Action
		{
			// Token: 0x040025C3 RID: 9667
			Translate = 1,
			// Token: 0x040025C4 RID: 9668
			Rotate = 2,
			// Token: 0x040025C5 RID: 9669
			Scale = 4,
			// Token: 0x040025C6 RID: 9670
			Delete = 8,
			// Token: 0x040025C7 RID: 9671
			Clone = 16,
			// Token: 0x040025C8 RID: 9672
			Place = 32
		}
	}
}
