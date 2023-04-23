using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class TestObjective : MonoBehaviour
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0000D9DD File Offset: 0x0000BBDD
	private void Start()
	{
		base.Invoke("AddObjective", 0.2f);
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0008BB6C File Offset: 0x00089D6C
	private void AddObjective()
	{
		ScoreboardUi.activeScoreboardType = ScoreboardUi.ActiveScoreboardUI.Objective;
		this.shoot = ObjectiveUi.CreateObjective("Shoot", base.transform);
		this.noShoot = ObjectiveUi.CreateObjective("Don't shoot >:D", ActorManager.instance.actors[1].ragdoll.HumanBoneTransformAnimated(HumanBodyBones.Chest));
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0000D9EF File Offset: 0x0000BBEF
	private void OnDisable()
	{
		this.shoot.SetCompleted();
		this.noShoot.SetFailed();
	}

	// Token: 0x0400127A RID: 4730
	private ObjectiveEntry shoot;

	// Token: 0x0400127B RID: 4731
	private ObjectiveEntry noShoot;
}
