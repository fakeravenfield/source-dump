using System;
using UnityEngine;

namespace Ravenfield.Modes.Scripted
{
	// Token: 0x020003D3 RID: 979
	public class TriggeredComponent : MonoBehaviour
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x000A50EC File Offset: 0x000A32EC
		protected virtual void Awake()
		{
			this.triggerHash = new int[this.triggers.Length];
			for (int i = 0; i < this.triggers.Length; i++)
			{
				this.triggerHash[i] = this.triggers[i].ToLowerInvariant().GetHashCode();
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000A513C File Offset: 0x000A333C
		public bool OnGlobalTrigger(int triggerHash)
		{
			if (this.onlyTriggerOnce && this.hasBeenTriggered)
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < this.triggerHash.Length; i++)
			{
				if (triggerHash == this.triggerHash[i])
				{
					this.OnTrigger(i);
					this.hasBeenTriggered = true;
					result = true;
					if (this.onlyTriggerOnce)
					{
						return true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00012C6C File Offset: 0x00010E6C
		protected virtual void OnTrigger(int triggerIndex)
		{
			Debug.Log(((this != null) ? this.ToString() : null) + " OnTrigger(" + this.triggers[triggerIndex] + ")");
		}

		// Token: 0x04001A2C RID: 6700
		public string[] triggers;

		// Token: 0x04001A2D RID: 6701
		public bool onlyTriggerOnce;

		// Token: 0x04001A2E RID: 6702
		private int[] triggerHash;

		// Token: 0x04001A2F RID: 6703
		private bool hasBeenTriggered;
	}
}
