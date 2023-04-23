using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000677 RID: 1655
	public class PlaceTool : AbstractTool, IShowPropertiesSidebarUI
	{
		// Token: 0x06002A24 RID: 10788 RVA: 0x000FEBAC File Offset: 0x000FCDAC
		protected override void OnInitialize()
		{
			this.placeholder = new GameObject("Placeholder")
			{
				transform = 
				{
					parent = base.transform
				}
			}.AddComponent<PlaceTool.Placeholder>();
			base.OnInitialize();
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0001CF4B File Offset: 0x0001B14B
		protected override void OnDeactivate()
		{
			this.SetObjectsToPlace(null);
			base.OnDeactivate();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000FEBE8 File Offset: 0x000FCDE8
		protected override void Update()
		{
			base.Update();
			if (!this.placeholder.CanPlace())
			{
				return;
			}
			Ray ray = MeInput.instance.CursorToSceneRay();
			bool flag;
			bool flag2;
			Vector3? vector = MapEditorTerrain.instance.RayCast(ray, out flag, out flag2);
			if (vector != null && (flag || flag2))
			{
				bool flag3 = MeInput.instance.DragObjectInScene();
				if (flag3 && !this.wasDragging)
				{
					this.spawnPosition = new Vector3?(vector.Value);
					this.startAngle = null;
					this.SetPosition(vector.Value);
				}
				else if (flag3 && this.wasDragging)
				{
					if (this.startAngle == null)
					{
						Camera camera = MapEditor.instance.GetCamera();
						Vector3 a = camera.WorldToScreenPoint(vector.Value);
						Vector3 b = camera.WorldToScreenPoint(this.spawnPosition.Value);
						if ((a - b).magnitude > 50f)
						{
							Vector3 vector2 = this.spawnPosition.Value - vector.Value;
							float num = Mathf.Atan2(vector2.z, -vector2.x) * 57.29578f;
							this.startAngle = new float?(num - this.placeholder.transform.eulerAngles.y);
						}
					}
					else
					{
						Vector3 vector3 = this.spawnPosition.Value - vector.Value;
						float y = Mathf.Atan2(vector3.z, -vector3.x) * 57.29578f - this.startAngle.Value;
						this.placeholder.transform.rotation = Quaternion.Euler(0f, y, 0f);
					}
				}
				else if (!flag3 && this.wasDragging)
				{
					MapEditorObject[] array = this.placeholder.PlaceObjects();
					MapEditor.instance.AddUndoableAction(new PlaceAction(array));
					Selection selection = new Selection((from eo in array
					select eo.GetSelectableObject()).ToArray<SelectableObject>());
					if (!MeInput.instance.MultiPlaceModifier())
					{
						MapEditor.instance.SetSelection(selection);
						MeTools.instance.SwitchToNoopTool();
					}
					else
					{
						MapEditor.instance.SetSelection(Selection.empty);
					}
				}
				else if (!flag3 && !this.wasDragging)
				{
					this.SetPosition(vector.Value);
				}
				this.wasDragging = flag3;
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0001CF5A File Offset: 0x0001B15A
		private void SetPosition(Vector3 position)
		{
			this.placeholder.transform.position = position;
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x0000257D File Offset: 0x0000077D
		public override bool IsSelectionChangeAllowed()
		{
			return false;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x0001CF6D File Offset: 0x0001B16D
		public void SetObjectsToPlace(params MapEditorObject[] editorObjects)
		{
			base.Initialize();
			this.placeholder.SetPreviewObjects(editorObjects);
		}

		// Token: 0x04002767 RID: 10087
		private const int ACTIVATION_RADIUS = 50;

		// Token: 0x04002768 RID: 10088
		private PlaceTool.Placeholder placeholder;

		// Token: 0x04002769 RID: 10089
		private bool wasDragging;

		// Token: 0x0400276A RID: 10090
		private Vector3? spawnPosition;

		// Token: 0x0400276B RID: 10091
		private float? startAngle;

		// Token: 0x02000678 RID: 1656
		public class Placeholder : MonoBehaviour
		{
			// Token: 0x06002A2B RID: 10795 RVA: 0x0001CF81 File Offset: 0x0001B181
			public bool CanPlace()
			{
				return this.previewObjects != null;
			}

			// Token: 0x06002A2C RID: 10796 RVA: 0x000FEE68 File Offset: 0x000FD068
			public void SetPreviewObjects(params MapEditorObject[] editorObjects)
			{
				if (this.previewObjects != null)
				{
					MapEditorObject[] array = this.previewObjects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Destroy();
					}
				}
				this.previewObjects = editorObjects;
				base.transform.rotation = Quaternion.identity;
				if (this.previewObjects != null)
				{
					Vector3 vector = Vector3.zero;
					foreach (MapEditorObject mapEditorObject in this.previewObjects)
					{
						vector += mapEditorObject.transform.position;
					}
					vector /= (float)this.previewObjects.Length;
					foreach (MapEditorObject mapEditorObject2 in this.previewObjects)
					{
						mapEditorObject2.transform.SetParent(null, true);
						mapEditorObject2.transform.localPosition -= vector;
						mapEditorObject2.transform.SetParent(base.transform, false);
						mapEditorObject2.RenderAsPreview(true);
					}
				}
			}

			// Token: 0x06002A2D RID: 10797 RVA: 0x000FEF54 File Offset: 0x000FD154
			public MapEditorObject[] PlaceObjects()
			{
				List<MapEditorObject> list = new List<MapEditorObject>();
				if (this.previewObjects != null)
				{
					MapEditorObject[] array = this.previewObjects;
					for (int i = 0; i < array.Length; i++)
					{
						MapEditorObject mapEditorObject = array[i].Clone();
						mapEditorObject.transform.SetParent(null, true);
						mapEditorObject.RenderAsPreview(false);
						list.Add(mapEditorObject);
					}
				}
				return list.ToArray();
			}

			// Token: 0x0400276C RID: 10092
			private MapEditorObject[] previewObjects;
		}
	}
}
