using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000605 RID: 1541
	public abstract class SelectableGizmoPart : MonoBehaviour
	{
		// Token: 0x06002776 RID: 10102 RVA: 0x000F8390 File Offset: 0x000F6590
		protected virtual void Awake()
		{
			this.gizmo = base.GetComponentInChildren<AbstractGizmo>();
			if (!this.gizmo)
			{
				this.gizmo = base.GetComponentInParent<AbstractGizmo>();
			}
			this.editor = MapEditor.instance;
			this.input = this.editor.GetInput();
			base.enabled = false;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Start()
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnEnable()
		{
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Update()
		{
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0001B447 File Offset: 0x00019647
		public AbstractGizmo GetGizmo()
		{
			return this.gizmo;
		}

		// Token: 0x0600277C RID: 10108
		public abstract void OnInteractableChanged();

		// Token: 0x0600277D RID: 10109
		public abstract void Select();

		// Token: 0x0600277E RID: 10110
		public abstract void Deselect();

		// Token: 0x0600277F RID: 10111 RVA: 0x0001B24D File Offset: 0x0001944D
		public virtual bool IsSelected()
		{
			return base.enabled;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000F83E8 File Offset: 0x000F65E8
		public static SelectableGizmoPart RayCast(Ray ray)
		{
			SelectableGizmoPart selectableGizmoPart = null;
			int gizmoPartLayerMask = Layers.GetGizmoPartLayerMask();
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 1000f, gizmoPartLayerMask))
			{
				selectableGizmoPart = raycastHit.transform.GetComponentInChildren<SelectableGizmoPart>();
				if (!selectableGizmoPart)
				{
					selectableGizmoPart = raycastHit.transform.GetComponentInParent<SelectableGizmoPart>();
				}
			}
			return selectableGizmoPart;
		}

		// Token: 0x04002587 RID: 9607
		private const float RAYCAST_MAX_DISTANCE = 1000f;

		// Token: 0x04002588 RID: 9608
		protected AbstractGizmo gizmo;

		// Token: 0x04002589 RID: 9609
		protected MapEditor editor;

		// Token: 0x0400258A RID: 9610
		protected MeInput input;
	}
}
