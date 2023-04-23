using System;
using System.Linq;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x020005FD RID: 1533
	public abstract class AbstractGizmo : MonoBehaviour
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000F7E64 File Offset: 0x000F6064
		protected virtual void Awake()
		{
			Utils.MoveToLayer(base.gameObject, Layers.GetGizmoPartLayer());
			this.editor = MapEditor.instance;
			this.input = this.editor.GetInput();
			this.isInteractable = true;
			this.parts = base.GetComponentsInChildren<SelectableGizmoPart>();
			this.Deactivate();
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Start()
		{
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0001B231 File Offset: 0x00019431
		protected virtual void OnEnable()
		{
			Utils.SetChildrenActive(base.gameObject, true);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0001B23F File Offset: 0x0001943F
		protected virtual void OnDisable()
		{
			Utils.SetChildrenActive(base.gameObject, false);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void Update()
		{
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0001B24D File Offset: 0x0001944D
		public bool IsActive()
		{
			return base.enabled;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0001B255 File Offset: 0x00019455
		public virtual void Activate(SelectableGizmoPart part = null)
		{
			base.enabled = true;
			if (part)
			{
				part.Select();
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0001B26C File Offset: 0x0001946C
		public void Deactivate()
		{
			base.enabled = false;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0001B275 File Offset: 0x00019475
		public bool HasSelectedPart()
		{
			return this.parts.Any((SelectableGizmoPart p) => p.IsSelected());
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0001B2A1 File Offset: 0x000194A1
		public bool IsInteractable()
		{
			return this.isInteractable;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000F7EB8 File Offset: 0x000F60B8
		public void SetInteractable(bool interactable)
		{
			if (this.isInteractable != interactable)
			{
				this.isInteractable = interactable;
				SelectableGizmoPart[] array = this.parts;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnInteractableChanged();
				}
			}
		}

		// Token: 0x04002571 RID: 9585
		protected MapEditor editor;

		// Token: 0x04002572 RID: 9586
		protected MeInput input;

		// Token: 0x04002573 RID: 9587
		protected bool isInteractable;

		// Token: 0x04002574 RID: 9588
		private SelectableGizmoPart[] parts;
	}
}
