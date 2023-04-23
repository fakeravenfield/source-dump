using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006CB RID: 1739
	public class HierarchySidebarUI : SidebarBase
	{
		// Token: 0x06002BE6 RID: 11238 RVA: 0x0010275C File Offset: 0x0010095C
		protected override void DoInitialize()
		{
			base.DoInitialize();
			this.input = this.editor.GetInput();
			this.editor.onObjectCreated.AddListener(delegate()
			{
				this.updateNext = true;
			});
			this.editor.onObjectDestroyed.AddListener(delegate()
			{
				this.updateNext = true;
			});
			this.buttonCreateGroup.onClick.AddListener(new UnityAction(this.CreateGroupOrMerge));
			this.buttonRemoveGroup.onClick.AddListener(new UnityAction(this.RemoveGroup));
			this.buttonRenameGroup.onClick.AddListener(new UnityAction(this.RenameGroup));
			this.UpdateHierarchy();
			if (this.startCollapsed)
			{
				this.Collapse();
			}
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0001E281 File Offset: 0x0001C481
		protected override void Update()
		{
			base.Update();
			if (this.updateNext || Time.time - this.lastUpdate > 3f)
			{
				this.lastUpdate = Time.time;
				this.updateNext = false;
				this.UpdateHierarchy();
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public override void Show()
		{
			base.Show();
			this.UpdateHierarchy();
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00102820 File Offset: 0x00100A20
		private void UpdateHierarchy()
		{
			this.lastUpdate = Time.time;
			if (!base.IsExpanded())
			{
				return;
			}
			HashSet<string> expandedPaths = this.FindExpandedPaths();
			this.treeView.Clear();
			MeoPrefab[] array = this.editor.FindObjectsToSave<MeoPrefab>();
			MapEditorObject[] objects = this.editor.FindObjectsToSave<MapEditorObject>().Except(array).ToArray<MapEditorObject>();
			string name = "Prefabs";
			MapEditorObject[] objects2 = array;
			this.AddCategory(name, objects2, expandedPaths);
			this.AddCategory("Game Objects", objects, expandedPaths);
			foreach (HierarchySidebarUI.MapEditorGroup mapEditorGroup in this.groups)
			{
				this.AddCategory(mapEditorGroup.name, mapEditorGroup.objects.ToArray(), expandedPaths);
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x001028F0 File Offset: 0x00100AF0
		private void AddCategory(string name, MapEditorObject[] objects, HashSet<string> expandedPaths)
		{
			TreeView treeView = this.treeView.Add(name);
			treeView.RenderAsCategory();
			treeView.RegisterOnClick(delegate
			{
				this.SelectObjects(objects);
			});
			this.AddObjects(objects, treeView, expandedPaths);
			string path = this.GetPath(treeView);
			if (!expandedPaths.Contains(path))
			{
				treeView.Collapse();
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0010295C File Offset: 0x00100B5C
		private void AddObjects(MapEditorObject[] objects, TreeView view, HashSet<string> expandedPaths)
		{
			if (objects.Any<MapEditorObject>())
			{
				using (Dictionary<string, MapEditorObject[]>.Enumerator enumerator = (from obj in objects
				group obj by obj.GetCategoryName() into kv
				orderby kv.Key
				select kv).ToDictionary((IGrouping<string, MapEditorObject> kv) => kv.Key, (IGrouping<string, MapEditorObject> kv) => kv.ToArray<MapEditorObject>()).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, MapEditorObject[]> kv = enumerator.Current;
						string key = kv.Key;
						TreeView treeView = view.Add(key);
						if (kv.Value.Length > 1)
						{
							treeView.SetText(string.Format("{0}s ({1})", key, kv.Value.Length));
						}
						treeView.RegisterOnClick(delegate
						{
							this.SelectObjects(kv.Value);
						});
					}
				}
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x00102AA4 File Offset: 0x00100CA4
		private void SelectObjects(params MapEditorObject[] objects)
		{
			SelectableObject[] array = (from obj in objects
			select obj.GetSelectableObject()).ToArray<SelectableObject>();
			if (this.input.AddToSelectionModifier())
			{
				Selection selection = this.editor.GetSelection().AddRange(array);
				this.editor.SetSelection(selection);
				return;
			}
			Selection selection2 = new Selection(array);
			this.editor.SetSelection(selection2);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x00102B20 File Offset: 0x00100D20
		private HashSet<string> FindExpandedPaths()
		{
			return new HashSet<string>(from v in this.treeView.GetAllItems()
			where v.IsExpanded()
			select this.GetPath(v));
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x00102B74 File Offset: 0x00100D74
		private string GetPath(TreeView view)
		{
			string text = "";
			while (view != null && !string.IsNullOrEmpty(view.GetText()))
			{
				text = view.GetText() + "/" + text;
				view = view.GetParent();
			}
			return text;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x00102BBC File Offset: 0x00100DBC
		private string NextGroupName()
		{
			string str = "Group ";
			int num = this.nextGroup;
			this.nextGroup = num + 1;
			return str + num.ToString();
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x00102BEC File Offset: 0x00100DEC
		private void CreateGroupOrMerge()
		{
			HierarchySidebarUI.SelectionInfo selectionInfo = this.GetSelectionInfo();
			if (selectionInfo.groups.Any<HierarchySidebarUI.MapEditorGroup>())
			{
				List<MapEditorObject> list = new List<MapEditorObject>(selectionInfo.objects);
				foreach (HierarchySidebarUI.MapEditorGroup mapEditorGroup in selectionInfo.groups)
				{
					list.AddRange(mapEditorGroup.objects);
					this.groups.Remove(mapEditorGroup);
				}
				HierarchySidebarUI.MapEditorGroup item = new HierarchySidebarUI.MapEditorGroup(selectionInfo.groups.First<HierarchySidebarUI.MapEditorGroup>().name, list.Distinct<MapEditorObject>());
				this.groups.Add(item);
			}
			else
			{
				HierarchySidebarUI.MapEditorGroup item2 = new HierarchySidebarUI.MapEditorGroup(this.NextGroupName(), selectionInfo.objects);
				this.groups.Add(item2);
			}
			this.UpdateHierarchy();
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00102CA4 File Offset: 0x00100EA4
		private void RemoveGroup()
		{
			foreach (HierarchySidebarUI.MapEditorGroup item in this.GetSelectionInfo().groups)
			{
				this.groups.Remove(item);
			}
			this.UpdateHierarchy();
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00102CE4 File Offset: 0x00100EE4
		private void RenameGroup()
		{
			HierarchySidebarUI.SelectionInfo selectionInfo = this.GetSelectionInfo();
			if (selectionInfo.groups.Length == 1)
			{
				HierarchySidebarUI.MapEditorGroup group = selectionInfo.groups.First<HierarchySidebarUI.MapEditorGroup>();
				string name = group.name;
				MessageUI.ShowPrompt("Enter a new group name", "Rename group", group.name, delegate(MessageUI msg)
				{
					if (msg.GetDialogResult() == MessageUI.DialogResult.Ok)
					{
						group.name = msg.GetInputText();
						this.UpdateHierarchy();
					}
				});
				return;
			}
			if (selectionInfo.groups.Length > 1)
			{
				MessageUI.ShowMessage("It is not possible to rename multiple groups at once.", "Rename group", null);
			}
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x00102D70 File Offset: 0x00100F70
		private HierarchySidebarUI.SelectionInfo GetSelectionInfo()
		{
			MapEditorObject[] selectedObjects = (from obj in this.editor.GetSelection().GetObjects()
			select obj.GetComponent<MapEditorObject>() into obj
			where obj
			select obj).ToArray<MapEditorObject>();
			HierarchySidebarUI.MapEditorGroup[] selectedGroups = (from g in this.groups
			where g.objects.Intersect(selectedObjects).Any<MapEditorObject>()
			select g).ToArray<HierarchySidebarUI.MapEditorGroup>();
			return new HierarchySidebarUI.SelectionInfo(selectedObjects, selectedGroups);
		}

		// Token: 0x04002887 RID: 10375
		public TreeView treeView;

		// Token: 0x04002888 RID: 10376
		public Button buttonCreateGroup;

		// Token: 0x04002889 RID: 10377
		public Button buttonRemoveGroup;

		// Token: 0x0400288A RID: 10378
		public Button buttonRenameGroup;

		// Token: 0x0400288B RID: 10379
		public bool startCollapsed;

		// Token: 0x0400288C RID: 10380
		private MeInput input;

		// Token: 0x0400288D RID: 10381
		private const float UPDATE_INTERVAL = 3f;

		// Token: 0x0400288E RID: 10382
		private float lastUpdate;

		// Token: 0x0400288F RID: 10383
		private bool updateNext;

		// Token: 0x04002890 RID: 10384
		private int nextGroup = 1;

		// Token: 0x04002891 RID: 10385
		private List<HierarchySidebarUI.MapEditorGroup> groups = new List<HierarchySidebarUI.MapEditorGroup>();

		// Token: 0x020006CC RID: 1740
		private class MapEditorGroup
		{
			// Token: 0x06002BF8 RID: 11256 RVA: 0x0001E2F6 File Offset: 0x0001C4F6
			public MapEditorGroup(string name, IEnumerable<MapEditorObject> objects)
			{
				this.name = name;
				this.objects = new List<MapEditorObject>(objects);
			}

			// Token: 0x04002892 RID: 10386
			public string name;

			// Token: 0x04002893 RID: 10387
			public List<MapEditorObject> objects;
		}

		// Token: 0x020006CD RID: 1741
		private struct SelectionInfo
		{
			// Token: 0x06002BF9 RID: 11257 RVA: 0x0001E311 File Offset: 0x0001C511
			public SelectionInfo(MapEditorObject[] selectedObjects, HierarchySidebarUI.MapEditorGroup[] selectedGroups)
			{
				this.objects = selectedObjects;
				this.groups = selectedGroups;
			}

			// Token: 0x04002894 RID: 10388
			public readonly MapEditorObject[] objects;

			// Token: 0x04002895 RID: 10389
			public readonly HierarchySidebarUI.MapEditorGroup[] groups;
		}
	}
}
