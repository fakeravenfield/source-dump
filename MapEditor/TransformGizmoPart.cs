using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000609 RID: 1545
	public abstract class TransformGizmoPart : SelectableGizmoPart
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0001B4BF File Offset: 0x000196BF
		protected new TransformGizmo gizmo
		{
			get
			{
				return this.gizmo as TransformGizmo;
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0001B4CC File Offset: 0x000196CC
		protected override void Awake()
		{
			this.renderers = base.GetComponentsInChildren<MeshRenderer>();
			if (this.renderers.Length != 0)
			{
				this.initialMaterial = this.renderers[0].sharedMaterial;
			}
			base.Awake();
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0001B4FC File Offset: 0x000196FC
		protected override void Update()
		{
			if (!this.input.DragObjectInScene())
			{
				this.Deselect();
				return;
			}
			this.UpdateTransform();
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0001B518 File Offset: 0x00019718
		public override void Deselect()
		{
			base.enabled = false;
			this.ResetMaterial();
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0001B527 File Offset: 0x00019727
		public override void Select()
		{
			if (this.gizmo.IsInteractable())
			{
				base.enabled = true;
				this.SetMaterial(this.selectionMaterial);
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0001B549 File Offset: 0x00019749
		public override void OnInteractableChanged()
		{
			this.Deselect();
			if (!this.gizmo.IsInteractable())
			{
				this.SetMaterial(this.disabledMaterial);
			}
		}

		// Token: 0x06002798 RID: 10136
		protected abstract void UpdateTransform();

		// Token: 0x06002799 RID: 10137 RVA: 0x0001B56A File Offset: 0x0001976A
		protected Vector3 Forward()
		{
			return this.gizmo.transform.TransformDirection(this.forward);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x0001B582 File Offset: 0x00019782
		protected void ResetMaterial()
		{
			this.SetMaterial(this.initialMaterial);
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000F8A9C File Offset: 0x000F6C9C
		protected void SetMaterial(Material material)
		{
			MeshRenderer[] array = this.renderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sharedMaterial = material;
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000F8AC8 File Offset: 0x000F6CC8
		protected Vector3 SnapToGrid(Vector3 p)
		{
			if (this.input.SnapToGridModifier())
			{
				float gridSize = this.editor.GetGridSize();
				float x = Mathf.Round(p.x / gridSize) * gridSize;
				float y = Mathf.Round(p.y / gridSize) * gridSize;
				float z = Mathf.Round(p.z / gridSize) * gridSize;
				p = new Vector3(x, y, z);
			}
			return p;
		}

		// Token: 0x0400259A RID: 9626
		public Vector3 forward;

		// Token: 0x0400259B RID: 9627
		public Material selectionMaterial;

		// Token: 0x0400259C RID: 9628
		public Material disabledMaterial;

		// Token: 0x0400259D RID: 9629
		private MeshRenderer[] renderers;

		// Token: 0x0400259E RID: 9630
		private Material initialMaterial;
	}
}
