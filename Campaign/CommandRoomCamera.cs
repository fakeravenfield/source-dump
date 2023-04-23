using System;
using UnityEngine;

namespace Campaign
{
	// Token: 0x020003E6 RID: 998
	public class CommandRoomCamera : MonoBehaviour
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x000A7F60 File Offset: 0x000A6160
		private void Awake()
		{
			Camera component = base.GetComponent<Camera>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.originLocalPosition = base.transform.localPosition;
			this.originLocalRotation = base.transform.localRotation;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000135E5 File Offset: 0x000117E5
		private void Update()
		{
			if (!this.isActive)
			{
				base.transform.localPosition = this.originLocalPosition;
				base.transform.localRotation = this.originLocalRotation;
				return;
			}
			this.UpdateTransform();
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0000296E File Offset: 0x00000B6E
		protected virtual void UpdateTransform()
		{
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0000296E File Offset: 0x00000B6E
		public virtual void OnActivated()
		{
		}

		// Token: 0x04001ACF RID: 6863
		public float fieldOfView = 40f;

		// Token: 0x04001AD0 RID: 6864
		[NonSerialized]
		public bool isActive;

		// Token: 0x04001AD1 RID: 6865
		public CommandRoomCamera backTarget;

		// Token: 0x04001AD2 RID: 6866
		protected Vector3 originLocalPosition;

		// Token: 0x04001AD3 RID: 6867
		protected Quaternion originLocalRotation;

		// Token: 0x04001AD4 RID: 6868
		public int[] clickableGroups;

		// Token: 0x04001AD5 RID: 6869
		public bool showWorldMapUi;
	}
}
