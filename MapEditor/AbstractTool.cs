using System;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x02000672 RID: 1650
	public abstract class AbstractTool : MonoBehaviour
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x0001CCD9 File Offset: 0x0001AED9
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x0001CCE1 File Offset: 0x0001AEE1
		private protected bool isInitialized { protected get; private set; }

		// Token: 0x060029F9 RID: 10745 RVA: 0x0001CCEA File Offset: 0x0001AEEA
		protected void Awake()
		{
			this.Deactivate();
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0001CCF2 File Offset: 0x0001AEF2
		protected void Start()
		{
			this.Initialize();
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0001CCFA File Offset: 0x0001AEFA
		protected void Initialize()
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.OnInitialize();
			}
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0001CD11 File Offset: 0x0001AF11
		protected virtual void OnInitialize()
		{
			MapEditor.instance.onSelectionChanged.AddListener(new UnityAction(this.OnSelectionChanged));
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0001CD2F File Offset: 0x0001AF2F
		protected void OnEnable()
		{
			this.Initialize();
			this.OnSelectionChanged();
			this.OnActivate();
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0001CD43 File Offset: 0x0001AF43
		protected void OnDisable()
		{
			this.OnDeactivate();
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnActivate()
		{
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void OnDeactivate()
		{
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000FE7C8 File Offset: 0x000FC9C8
		protected virtual void Update()
		{
			bool flag;
			this.selection = this.selection.RemoveDestroyed(out flag);
			if (flag)
			{
				this.OnSelectionChanged();
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0001CD4B File Offset: 0x0001AF4B
		protected Selection GetSelection()
		{
			return this.selection;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0001CD53 File Offset: 0x0001AF53
		protected virtual void OnSelectionChanged()
		{
			this.selection = ((!base.enabled) ? Selection.empty : MapEditor.instance.GetSelection());
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x0000476F File Offset: 0x0000296F
		public virtual bool IsSelectionChangeAllowed()
		{
			return true;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x0001CD74 File Offset: 0x0001AF74
		public void Activate()
		{
			base.enabled = true;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0001B26C File Offset: 0x0001946C
		public void Deactivate()
		{
			base.enabled = false;
		}

		// Token: 0x04002749 RID: 10057
		private Selection selection;
	}
}
