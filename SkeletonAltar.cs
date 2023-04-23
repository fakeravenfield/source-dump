using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class SkeletonAltar : MonoBehaviour
{
	// Token: 0x060009BF RID: 2495 RVA: 0x0006C4EC File Offset: 0x0006A6EC
	private void Update()
	{
		foreach (Actor actor in ActorManager.AliveActorsOnTeam(0))
		{
			Vector3 vector = actor.Position() - this.trigger.position;
			if (Mathf.Abs(vector.y) < 2f && vector.ToGround().magnitude < 1.5f)
			{
				this.Trigger();
			}
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0006C57C File Offset: 0x0006A77C
	private void Trigger()
	{
		base.enabled = false;
		this.trigger.gameObject.SetActive(false);
		SkeletonAltar.used++;
		SpookOpsMode spookOpsMode = (SpookOpsMode)GameModeBase.instance;
		int num = UnityEngine.Random.Range(0, 3);
		if (spookOpsMode.favors > 0)
		{
			num = UnityEngine.Random.Range(0, 2);
			spookOpsMode.favors--;
		}
		if (num == 0)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.healthPrefab, this.spawnTransform.position, this.spawnTransform.rotation);
			return;
		}
		if (num == 1)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.ammoPrefab, this.spawnTransform.position, this.spawnTransform.rotation);
			return;
		}
		((SpookOpsMode)GameModeBase.instance).SpawnAmbush(base.transform.position);
	}

	// Token: 0x04000AAE RID: 2734
	public static int used;

	// Token: 0x04000AAF RID: 2735
	public Transform trigger;

	// Token: 0x04000AB0 RID: 2736
	public Transform spawnTransform;

	// Token: 0x04000AB1 RID: 2737
	public GameObject ammoPrefab;

	// Token: 0x04000AB2 RID: 2738
	public GameObject healthPrefab;
}
