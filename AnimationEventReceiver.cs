using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class AnimationEventReceiver : MonoBehaviour
{
	// Token: 0x0600089D RID: 2205 RVA: 0x00068980 File Offset: 0x00066B80
	private void Awake()
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			this.triggers[i].Initialize();
		}
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x000689C0 File Offset: 0x00066BC0
	public void ResetTriggers()
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			this.triggers[i].Reset();
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x000689F4 File Offset: 0x00066BF4
	public void Trigger(string name)
	{
		bool flag = this.animator.GetCurrentAnimatorStateInfo(0).speed < 0f;
		int hashCode = name.GetHashCode();
		int i = 0;
		while (i < this.triggers.Length)
		{
			if (hashCode == this.triggers[i].hash)
			{
				if (flag)
				{
					this.triggers[i].Reset();
					return;
				}
				this.triggers[i].Trigger();
				return;
			}
			else
			{
				i++;
			}
		}
		Debug.LogError("Animation event " + name + " was not consumed");
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00068A88 File Offset: 0x00066C88
	private static void ForEach<T>(T[] array, AnimationEventReceiver.DelItemCallback<T> callback)
	{
		if (array == null)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			callback(array[i]);
		}
	}

	// Token: 0x04000937 RID: 2359
	private Animator animator;

	// Token: 0x04000938 RID: 2360
	public AnimationEventReceiver.TriggerGroup[] triggers;

	// Token: 0x0200012A RID: 298
	// (Invoke) Token: 0x060008A3 RID: 2211
	private delegate void DelItemCallback<T>(T item);

	// Token: 0x0200012B RID: 299
	[Serializable]
	public struct TriggerGroup
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public void Initialize()
		{
			this.hash = this.name.GetHashCode();
			this.Reset();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00068AB4 File Offset: 0x00066CB4
		public void Trigger()
		{
			AnimationEventReceiver.ForEach<GameObject>(this.activate, delegate(GameObject a)
			{
				a.SetActive(true);
			});
			AnimationEventReceiver.ForEach<GameObject>(this.deactivate, delegate(GameObject a)
			{
				a.SetActive(false);
			});
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00068B18 File Offset: 0x00066D18
		public void Reset()
		{
			AnimationEventReceiver.ForEach<GameObject>(this.activate, delegate(GameObject a)
			{
				a.SetActive(false);
			});
			AnimationEventReceiver.ForEach<GameObject>(this.deactivate, delegate(GameObject a)
			{
				a.SetActive(true);
			});
		}

		// Token: 0x04000939 RID: 2361
		public string name;

		// Token: 0x0400093A RID: 2362
		[NonSerialized]
		public int hash;

		// Token: 0x0400093B RID: 2363
		public GameObject[] activate;

		// Token: 0x0400093C RID: 2364
		public GameObject[] deactivate;
	}
}
