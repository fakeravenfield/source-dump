using System;
using System.Collections;
using UnityEngine;

namespace MapMagicDemo
{
	// Token: 0x020004A9 RID: 1193
	public class LodDisable : MonoBehaviour
	{
		// Token: 0x06001DF3 RID: 7667 RVA: 0x000162C4 File Offset: 0x000144C4
		private void Update()
		{
			if (!this.processing)
			{
				base.StartCoroutine(this.ProcessLods());
			}
			if (this.test)
			{
				this.test = false;
				this.Test();
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000162F0 File Offset: 0x000144F0
		private IEnumerator ProcessLods()
		{
			this.processing = true;
			int counter = 0;
			foreach (object obj in this.parent)
			{
				Transform transform = (Transform)obj;
				if (!(transform == null) && !(transform.name == "ObjectPool Unused"))
				{
					Vector3 camPos = Camera.main.transform.position;
					foreach (object obj2 in transform)
					{
						Transform transform2 = (Transform)obj2;
						float sqrMagnitude = (camPos - transform2.position).sqrMagnitude;
						LODGroup component = transform2.GetComponent<LODGroup>();
						if (!(component == null))
						{
							if (sqrMagnitude > this.maxDist * this.maxDist && component.enabled)
							{
								component.enabled = false;
							}
							if (sqrMagnitude < this.maxDist * this.maxDist && !component.enabled)
							{
								component.enabled = true;
							}
							int num = counter;
							counter = num + 1;
							if (counter >= this.objPerFrame)
							{
								counter = 0;
								yield return null;
							}
						}
					}
					IEnumerator enumerator2 = null;
					camPos = default(Vector3);
				}
			}
			IEnumerator enumerator = null;
			this.processing = false;
			yield break;
			yield break;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		private void Test()
		{
			int num = 0;
			int num2 = 0;
			foreach (object obj in this.parent)
			{
				foreach (object obj2 in ((Transform)obj))
				{
					Transform transform = (Transform)obj2;
					if (transform.gameObject.activeSelf)
					{
						LODGroup component = transform.GetComponent<LODGroup>();
						if (!(component == null))
						{
							num++;
							if (component.enabled)
							{
								num2++;
							}
						}
					}
				}
			}
			Debug.Log(string.Concat(new string[]
			{
				"Num Lodgroups:",
				num.ToString(),
				" Active:",
				num2.ToString(),
				" Disabled:",
				(num - num2).ToString()
			}));
		}

		// Token: 0x04001E68 RID: 7784
		public Transform parent;

		// Token: 0x04001E69 RID: 7785
		public bool processing;

		// Token: 0x04001E6A RID: 7786
		public float maxDist = 500f;

		// Token: 0x04001E6B RID: 7787
		public int objPerFrame = 1000;

		// Token: 0x04001E6C RID: 7788
		public bool test;
	}
}
