using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003EC RID: 1004
	public class CommandRoomClickable : MonoBehaviour
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x000A87BC File Offset: 0x000A69BC
		protected virtual void Awake()
		{
			if (this.hoverActivate != null)
			{
				this.hoverActivate.SetActive(false);
			}
			this.colliders = base.GetComponentsInChildren<Collider>();
			Collider[] array = this.colliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.layer = 29;
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000137B8 File Offset: 0x000119B8
		private void Start()
		{
			ClickableManager.RegisterClickable(this);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000A8814 File Offset: 0x000A6A14
		public void ActivateColliders()
		{
			Collider[] array = this.colliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000A8840 File Offset: 0x000A6A40
		public void DeactivateColliders()
		{
			Collider[] array = this.colliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000137C0 File Offset: 0x000119C0
		public virtual void OnStartHover()
		{
			if (this.hoverActivate != null)
			{
				this.hoverActivate.SetActive(true);
			}
			this.userIsHovering = true;
			this.hoverStayEventConsumed = false;
			this.hoverStayAction.Start();
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000137F5 File Offset: 0x000119F5
		public virtual void OnEndHover()
		{
			if (this.hoverActivate != null)
			{
				this.hoverActivate.SetActive(false);
			}
			this.userIsHovering = false;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnHoverStay()
		{
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00013818 File Offset: 0x00011A18
		protected void Update()
		{
			if (!this.hoverStayEventConsumed && this.userIsHovering && this.hoverStayAction.TrueDone())
			{
				this.OnHoverStay();
				this.hoverStayEventConsumed = true;
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnClick()
		{
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnRightClick()
		{
		}

		// Token: 0x04001AF1 RID: 6897
		public const int LAYER = 29;

		// Token: 0x04001AF2 RID: 6898
		public GameObject hoverActivate;

		// Token: 0x04001AF3 RID: 6899
		public int clickableGroup;

		// Token: 0x04001AF4 RID: 6900
		private Collider[] colliders;

		// Token: 0x04001AF5 RID: 6901
		private TimedAction hoverStayAction = new TimedAction(0.6f, false);

		// Token: 0x04001AF6 RID: 6902
		[NonSerialized]
		public bool userIsHovering;

		// Token: 0x04001AF7 RID: 6903
		private bool hoverStayEventConsumed;
	}
}
