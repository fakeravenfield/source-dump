using System;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x02000684 RID: 1668
	public abstract class TransformTool : AbstractTool, IShowPropertiesSidebarUI
	{
		// Token: 0x06002A70 RID: 10864 RVA: 0x0001D2CB File Offset: 0x0001B4CB
		protected override void OnInitialize()
		{
			base.OnInitialize();
			MeTools.instance.onOrientationChanged.AddListener(new UnityAction(this.OnOrientationChanged));
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0001D2EF File Offset: 0x0001B4EF
		protected override void OnActivate()
		{
			this.state = TransformTool.State.WaitForSelection;
			this.reorient = true;
			base.OnActivate();
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000FF608 File Offset: 0x000FD808
		protected override void OnDeactivate()
		{
			TransformGizmo gizmo = this.GetGizmo();
			if (gizmo)
			{
				gizmo.Deactivate();
			}
			this.EndTransform();
			base.OnDeactivate();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000FF638 File Offset: 0x000FD838
		protected override void Update()
		{
			base.Update();
			TransformGizmo gizmo = this.GetGizmo();
			Selection selection = base.GetSelection();
			gizmo.SetInteractable(!selection.IsActionDisabled(this.GetAction()));
			if (this.state == TransformTool.State.WaitForSelection)
			{
				if (selection.Empty())
				{
					if (gizmo.IsActive())
					{
						gizmo.Deactivate();
					}
				}
				else
				{
					if (!gizmo.IsActive())
					{
						gizmo.Activate(null);
					}
					this.reselect = false;
					this.state = TransformTool.State.WaitForPart;
				}
			}
			if (this.state == TransformTool.State.WaitForPart)
			{
				if (this.reselect)
				{
					this.state = TransformTool.State.WaitForSelection;
				}
				else
				{
					if (selection.Any())
					{
						gizmo.transform.position = selection.GetPivot();
					}
					if (this.reorient)
					{
						this.reorient = false;
						gizmo.transform.rotation = this.OrientGizmo(selection);
					}
					if (gizmo.HasSelectedPart())
					{
						this.StartTransform(selection);
						this.undoAction = this.CreateUndoableAction(selection);
						this.transformed = false;
						this.state = TransformTool.State.Transform;
					}
				}
			}
			if (this.state == TransformTool.State.Transform)
			{
				if (gizmo.HasSelectedPart() && !this.reselect)
				{
					if (this.ModifyTransform(selection))
					{
						this.transformed = true;
						return;
					}
				}
				else
				{
					this.EndTransform();
					this.state = TransformTool.State.WaitForSelection;
				}
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0001D305 File Offset: 0x0001B505
		protected virtual void OnOrientationChanged()
		{
			this.reorient = true;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0001D30E File Offset: 0x0001B50E
		protected override void OnSelectionChanged()
		{
			this.reselect = true;
			this.reorient = true;
			base.OnSelectionChanged();
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000FF764 File Offset: 0x000FD964
		protected virtual Quaternion OrientGizmo(Selection selection)
		{
			Quaternion result = Quaternion.identity;
			if (MeTools.instance.GetOrientation() == MeTools.Orientation.WithSelection)
			{
				result = this.RotationOfSelection(selection);
			}
			return result;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000FF790 File Offset: 0x000FD990
		protected Quaternion RotationOfSelection(Selection selection)
		{
			Quaternion result = Quaternion.identity;
			if (selection.GetLength() == 1)
			{
				result = selection.GetObjects()[0].transform.rotation;
			}
			return result;
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000FF7C4 File Offset: 0x000FD9C4
		protected Vector3 SnapToGrid(Vector3 p)
		{
			if (MeInput.instance.SnapToGridModifier())
			{
				float gridSize = MapEditor.instance.GetGridSize();
				float x = Mathf.Round(p.x / gridSize) * gridSize;
				float y = Mathf.Round(p.y / gridSize) * gridSize;
				float z = Mathf.Round(p.z / gridSize) * gridSize;
				p = new Vector3(x, y, z);
			}
			return p;
		}

		// Token: 0x06002A79 RID: 10873
		public abstract MapEditor.Action GetAction();

		// Token: 0x06002A7A RID: 10874
		public abstract TransformGizmo GetGizmo();

		// Token: 0x06002A7B RID: 10875
		protected abstract UndoableAction CreateUndoableAction(Selection selection);

		// Token: 0x06002A7C RID: 10876
		protected abstract void StartTransform(Selection selection);

		// Token: 0x06002A7D RID: 10877
		protected abstract bool ModifyTransform(Selection selection);

		// Token: 0x06002A7E RID: 10878 RVA: 0x0001D324 File Offset: 0x0001B524
		protected void EndTransform()
		{
			if (this.undoAction != null)
			{
				if (this.transformed)
				{
					MapEditor.instance.AddUndoableAction(this.undoAction);
				}
				this.undoAction = null;
			}
		}

		// Token: 0x0400278F RID: 10127
		private UndoableAction undoAction;

		// Token: 0x04002790 RID: 10128
		private bool transformed;

		// Token: 0x04002791 RID: 10129
		private bool reorient;

		// Token: 0x04002792 RID: 10130
		private bool reselect;

		// Token: 0x04002793 RID: 10131
		private TransformTool.State state;

		// Token: 0x02000685 RID: 1669
		private enum State
		{
			// Token: 0x04002795 RID: 10133
			WaitForSelection,
			// Token: 0x04002796 RID: 10134
			WaitForPart,
			// Token: 0x04002797 RID: 10135
			Transform
		}
	}
}
