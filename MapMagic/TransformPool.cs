using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x020005B6 RID: 1462
	[Serializable]
	public class TransformPool
	{
		// Token: 0x060025ED RID: 9709 RVA: 0x000F2C60 File Offset: 0x000F0E60
		public void SetTransforms(TransformPool.InstanceDraft[] drafts)
		{
			IEnumerator enumerator = this.SetTransformsCoroutine(drafts);
			while (enumerator.MoveNext())
			{
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0001A379 File Offset: 0x00018579
		public IEnumerator SetTransformsCoroutine(TransformPool.InstanceDraft[] drafts)
		{
			if (!this.allowReposition)
			{
				for (int i = this.transforms.Count - 1; i >= 0; i--)
				{
					if (this.transforms[i] != null)
					{
						UnityEngine.Object.DestroyImmediate(this.transforms[i].gameObject);
					}
				}
				this.transforms.Clear();
			}
			for (int j = this.transforms.Count - 1; j >= 0; j--)
			{
				if (this.transforms[j] == null)
				{
					this.transforms.RemoveAt(j);
				}
			}
			if (this.transforms.Count > drafts.Length)
			{
				for (int k = this.transforms.Count - 1; k >= drafts.Length; k--)
				{
					if (this.transforms[k] != null)
					{
						UnityEngine.Object.DestroyImmediate(this.transforms[k].gameObject);
					}
					this.transforms.RemoveAt(k);
				}
			}
			for (int l = this.transforms.Count - 1; l >= 0; l--)
			{
				this.transforms[l].localPosition = drafts[l].pos;
				this.transforms[l].localRotation = drafts[l].rotation;
				this.transforms[l].localScale = drafts[l].scale;
			}
			int oldObjsCount = this.transforms.Count;
			int newObjsCount = drafts.Length - this.transforms.Count;
			int objsPerFrame = 400;
			int frames = (int)(1f * (float)newObjsCount / (float)objsPerFrame);
			int num2;
			for (int f = 0; f <= frames; f = num2 + 1)
			{
				int num = Mathf.Min(objsPerFrame, newObjsCount - f * objsPerFrame);
				for (int m = 0; m < num; m++)
				{
					Transform transform = UnityEngine.Object.Instantiate<Transform>(this.prefab, drafts[m].pos, drafts[m].rotation);
					transform.parent = this.parent;
					TransformPool.InstanceDraft instanceDraft = drafts[oldObjsCount + f * objsPerFrame + m];
					transform.localPosition = instanceDraft.pos;
					transform.localRotation = instanceDraft.rotation;
					if ((instanceDraft.scale - Vector3.one).sqrMagnitude > 0.0001f)
					{
						transform.localScale = instanceDraft.scale;
					}
					this.transforms.Add(transform);
				}
				yield return null;
				num2 = f;
			}
			for (int n = this.parent.childCount - 1; n >= 0; n--)
			{
				Transform child = this.parent.GetChild(n);
				if (child.name == "Unity54 Pool Hunk" && child.childCount == 0)
				{
					UnityEngine.Object.DestroyImmediate(child.gameObject);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000F2C80 File Offset: 0x000F0E80
		public void Clear()
		{
			for (int i = this.transforms.Count - 1; i >= 0; i--)
			{
				if (this.transforms[i] != null && this.transforms[i].gameObject != null)
				{
					UnityEngine.Object.DestroyImmediate(this.transforms[i].gameObject);
				}
			}
		}

		// Token: 0x0400247D RID: 9341
		public Transform parent;

		// Token: 0x0400247E RID: 9342
		public Transform prefab;

		// Token: 0x0400247F RID: 9343
		public List<Transform> transforms = new List<Transform>();

		// Token: 0x04002480 RID: 9344
		public bool allowReposition = true;

		// Token: 0x020005B7 RID: 1463
		public struct InstanceDraft
		{
			// Token: 0x04002481 RID: 9345
			public Vector3 pos;

			// Token: 0x04002482 RID: 9346
			public Vector3 scale;

			// Token: 0x04002483 RID: 9347
			public Quaternion rotation;
		}
	}
}
