using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Campaign
{
	// Token: 0x020003F6 RID: 1014
	public class StartBattleUi : MonoBehaviour
	{
		// Token: 0x06001973 RID: 6515 RVA: 0x000A9214 File Offset: 0x000A7414
		public static void ShowBattleUi(LevelClickable attackedLevel, int attackers, bool canCancel, bool canAutoResolve)
		{
			StartBattleUi.SetRendererLayer(attackedLevel.gameObject, 30);
			if (attackedLevel.owner == ConquestCampaign.instance.playerTeam)
			{
				StartBattleUi.instance.battleTitle.text = "DEFENDING " + attackedLevel.displayName;
			}
			else
			{
				StartBattleUi.instance.battleTitle.text = "ATTACKING " + attackedLevel.displayName;
			}
			StartBattleUi.instance.buttonContainer.SetActive(true);
			StartBattleUi.instance.cancelButton.interactable = canCancel;
			StartBattleUi.instance.autoResolveButton.interactable = canAutoResolve;
			foreach (SoldierMiniature soldierMiniature in attackedLevel.soldiers)
			{
				StartBattleUi.SetRendererLayer(soldierMiniature.gameObject, 30);
			}
			StartBattleUi.instance.nAttackers = attackers;
			StartBattleUi.instance.nDefenders = attackedLevel.soldiers.Count;
			for (int i = 0; i < StartBattleUi.instance.attackers.Length; i++)
			{
				StartBattleUi.instance.attackers[i].SetTeam(1 - attackedLevel.owner);
				StartBattleUi.instance.attackers[i].gameObject.SetActive(i < attackers);
				StartBattleUi.instance.attackers[i].ResetTransform();
				if (i < attackers)
				{
					StartBattleUi.instance.attackers[i].PlayDropAnimation(0.4f + (float)i * 0.05f);
				}
				StartBattleUi.instance.attackerActive[i] = (i < StartBattleUi.instance.nAttackers);
			}
			for (int j = 0; j < StartBattleUi.instance.defenders.Length; j++)
			{
				StartBattleUi.instance.defenders[j].SetTeam(attackedLevel.owner);
				StartBattleUi.instance.defenders[j].gameObject.SetActive(false);
				StartBattleUi.instance.defenders[j].transform.localScale = Vector3.one;
				StartBattleUi.instance.defenders[j].ResetTransform();
				StartBattleUi.instance.defenderActive[j] = (j < StartBattleUi.instance.nDefenders);
			}
			StartBattleUi.instance.targetLevel = attackedLevel;
			StartBattleUi.instance.camera.enabled = true;
			StartBattleUi.instance.CopyCamera(CommandRoomMainCamera.instance.camera);
			StartBattleUi.instance.moveLevelAction.Start();
			StartBattleUi.instance.battleCanvas.SetActive(true);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000A9494 File Offset: 0x000A7694
		public static void HideBattleUi()
		{
			StartBattleUi.instance.battleCanvas.SetActive(false);
			StartBattleUi.instance.camera.enabled = false;
			if (StartBattleUi.instance.targetLevel != null)
			{
				StartBattleUi.SetRendererLayer(StartBattleUi.instance.targetLevel.gameObject, 29);
				foreach (SoldierMiniature soldierMiniature in StartBattleUi.instance.targetLevel.soldiers)
				{
					StartBattleUi.SetRendererLayer(soldierMiniature.gameObject, 0);
				}
				StartBattleUi.instance.targetLevel = null;
			}
			SoldierMiniature[] array = StartBattleUi.instance.attackers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(false);
			}
			array = StartBattleUi.instance.defenders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(false);
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000A9594 File Offset: 0x000A7794
		private static void SetRendererLayer(GameObject gameObject, int layer)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00013C0F File Offset: 0x00011E0F
		public static bool IsOpen()
		{
			return StartBattleUi.instance.battleCanvas.active;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000A95C4 File Offset: 0x000A77C4
		private void Awake()
		{
			StartBattleUi.instance = this;
			this.camera = base.GetComponent<Camera>();
			this.targetFov = this.camera.fieldOfView;
			this.camera.enabled = false;
			this.battleCanvas.SetActive(false);
			this.attackerActive = new bool[3];
			this.defenderActive = new bool[3];
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000A9624 File Offset: 0x000A7824
		private void Start()
		{
			this.conquest = (CampaignBase.instance as ConquestCampaign);
			this.attackers = new SoldierMiniature[this.attackerRoots.Length];
			this.defenders = new SoldierMiniature[this.defenderRoots.Length];
			ConquestCampaign conquestCampaign = (ConquestCampaign)CampaignBase.instance;
			for (int i = 0; i < this.attackerRoots.Length; i++)
			{
				this.attackers[i] = UnityEngine.Object.Instantiate<GameObject>(conquestCampaign.soldierMiniaturePrefab, this.attackerRoots[i]).GetComponent<SoldierMiniature>();
				this.attackers[i].SetRootTransform(this.attackerRoots[i]);
				StartBattleUi.SetRendererLayer(this.attackers[i].gameObject, 31);
			}
			for (int j = 0; j < this.defenderRoots.Length; j++)
			{
				this.defenders[j] = UnityEngine.Object.Instantiate<GameObject>(conquestCampaign.soldierMiniaturePrefab, this.defenderRoots[j]).GetComponent<SoldierMiniature>();
				this.defenders[j].SetRootTransform(this.defenderRoots[j]);
				StartBattleUi.SetRendererLayer(this.defenders[j].gameObject, 31);
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000A972C File Offset: 0x000A792C
		private void LateUpdate()
		{
			if (this.targetLevel != null && this.camera.enabled)
			{
				Quaternion quaternion = SMath.DeltaRotation(this.focusPoint.localRotation, Quaternion.identity);
				this.focusPoint.position - base.transform.position;
				Vector3 vector = this.targetLevel.transform.position - quaternion * this.focusPoint.localPosition;
				if (this.moveLevelAction.TrueDone())
				{
					base.transform.rotation = quaternion;
					base.transform.position = vector;
					this.camera.fieldOfView = this.targetFov;
					return;
				}
				float t = this.moveLevelAction.Ratio();
				base.transform.rotation = Quaternion.Slerp(this.originRotation, quaternion, t);
				base.transform.position = Vector3.Lerp(this.originPosition, vector, t);
				this.camera.fieldOfView = Mathf.Lerp(this.originFov, this.targetFov, t);
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000A9844 File Offset: 0x000A7A44
		private void CopyCamera(Camera camera)
		{
			this.originPosition = camera.transform.position;
			this.originRotation = camera.transform.rotation;
			this.originFov = camera.fieldOfView;
			base.transform.position = this.originPosition;
			base.transform.rotation = this.originRotation;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00013C20 File Offset: 0x00011E20
		public void AutoBattle()
		{
			this.buttonContainer.SetActive(false);
			base.StartCoroutine(this.AutoBattleCoroutine());
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00013C3B File Offset: 0x00011E3B
		private IEnumerator AutoBattleCoroutine()
		{
			BattleResult result = this.conquest.AutoBattle();
			for (int j = 0; j < this.targetLevel.soldiers.Count; j++)
			{
				Transform transform = this.targetLevel.soldiers[j].transform;
				this.defenders[j].gameObject.SetActive(true);
				this.defenders[j].transform.position = transform.position;
				this.defenders[j].transform.rotation = transform.rotation;
				this.defenders[j].TransitionToRootTransform(this.defenderRoots[j], false);
			}
			foreach (SoldierMiniature soldierMiniature in StartBattleUi.instance.targetLevel.soldiers)
			{
				StartBattleUi.SetRendererLayer(soldierMiniature.gameObject, 0);
			}
			yield return new WaitForSeconds(1f);
			int owner = this.targetLevel.owner;
			int num = 1 - this.targetLevel.owner;
			Debug.LogFormat("Auto battle result, Attacker: {0}, Defender: {1}, Winner {2}", new object[]
			{
				num,
				owner,
				result.winningTeam
			});
			Debug.LogFormat("Attackers: {0} attacked, {1} remaining.", new object[]
			{
				this.nAttackers,
				result.remainingBattalions[num]
			});
			Debug.LogFormat("Defenders: {0} defended, {1} remaining.", new object[]
			{
				this.nDefenders,
				result.remainingBattalions[owner]
			});
			int num2 = this.nAttackers - result.remainingBattalions[num];
			int num3 = this.nDefenders - result.remainingBattalions[owner];
			bool flag = result.winningTeam == num;
			for (int k = 0; k < num2; k++)
			{
				float delay = (!flag && k == num2 - 1) ? 1.7f : UnityEngine.Random.Range(0.4f, 1.5f);
				base.StartCoroutine(this.PlayDeathAnimation(this.attackers[k], delay));
			}
			for (int l = 0; l < num3; l++)
			{
				float delay2 = (flag && l == num3 - 1) ? 1.7f : UnityEngine.Random.Range(0.4f, 1.5f);
				base.StartCoroutine(this.PlayDeathAnimation(this.defenders[l], delay2));
			}
			base.StartCoroutine(this.AutoBattleEffects());
			int num4;
			for (int i = 0; i < 5; i = num4 + 1)
			{
				for (int m = 0; m < this.attackers.Length; m++)
				{
					if (this.attackerActive[m])
					{
						this.attackers[m].TransitionToLocalOffset(new Vector3(UnityEngine.Random.Range(-this.autoBattlePositionWobble, this.autoBattlePositionWobble), 0f, UnityEngine.Random.Range(-this.autoBattlePositionWobble, this.autoBattlePositionWobble)), Quaternion.Euler(0f, UnityEngine.Random.Range(-this.autoBattleAngleWobble, this.autoBattleAngleWobble), 0f));
					}
				}
				for (int n = 0; n < this.defenders.Length; n++)
				{
					if (this.defenderActive[n])
					{
						this.defenders[n].TransitionToLocalOffset(new Vector3(UnityEngine.Random.Range(-this.autoBattlePositionWobble, this.autoBattlePositionWobble), 0f, UnityEngine.Random.Range(-this.autoBattlePositionWobble, this.autoBattlePositionWobble)), Quaternion.Euler(0f, UnityEngine.Random.Range(-this.autoBattleAngleWobble, this.autoBattleAngleWobble), 0f));
					}
				}
				yield return new WaitForSeconds(0.4f);
				num4 = i;
			}
			yield return new WaitForSeconds(0.5f);
			this.conquest.ResolveBattleResult(result);
			StartBattleUi.HideBattleUi();
			yield break;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00013C4A File Offset: 0x00011E4A
		private IEnumerator AutoBattleEffects()
		{
			int num2;
			for (int i = 0; i < 15; i = num2 + 1)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
				bool flag = UnityEngine.Random.Range(0, 2) != 0;
				int num = UnityEngine.Random.Range(0, 3);
				if (!flag)
				{
					if (this.attackerActive[num])
					{
						this.CreateAutoBattleParticle(this.attackers[num].transform);
					}
				}
				else if (this.defenderActive[num])
				{
					this.CreateAutoBattleParticle(this.defenders[num].transform);
				}
				num2 = i;
			}
			yield break;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000A98A4 File Offset: 0x000A7AA4
		private void CreateAutoBattleParticle(Transform root)
		{
			this.autoBattleParticles.transform.rotation = root.transform.rotation;
			this.autoBattleParticles.transform.position = root.transform.position + root.transform.rotation * new Vector3(0f, 0.05f, 0.05f);
			this.autoBattleParticles.Emit(1);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00013C59 File Offset: 0x00011E59
		private IEnumerator PlayDeathAnimation(SoldierMiniature soldier, float delay)
		{
			yield return new WaitForSeconds(delay);
			soldier.PlayDeathAnimation();
			int num;
			for (int i = 0; i < this.attackers.Length; i = num + 1)
			{
				if (soldier == this.attackers[i])
				{
					this.attackerActive[i] = false;
					yield return null;
				}
				num = i;
			}
			for (int i = 0; i < this.defenders.Length; i = num + 1)
			{
				if (soldier == this.defenders[i])
				{
					this.defenderActive[i] = false;
					yield return null;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00013C76 File Offset: 0x00011E76
		public void StartBattle()
		{
			this.conquest.StartBattle(this.targetLevel);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00013C89 File Offset: 0x00011E89
		public void CancelBattle()
		{
			StartBattleUi.HideBattleUi();
		}

		// Token: 0x04001B2E RID: 6958
		public const int FOCUS_LAYER = 30;

		// Token: 0x04001B2F RID: 6959
		public const int BATTLE_LAYER = 31;

		// Token: 0x04001B30 RID: 6960
		public static StartBattleUi instance;

		// Token: 0x04001B31 RID: 6961
		public Transform focusPoint;

		// Token: 0x04001B32 RID: 6962
		public Transform[] attackerRoots;

		// Token: 0x04001B33 RID: 6963
		public Transform[] defenderRoots;

		// Token: 0x04001B34 RID: 6964
		private int nAttackers;

		// Token: 0x04001B35 RID: 6965
		private int nDefenders;

		// Token: 0x04001B36 RID: 6966
		private bool[] attackerActive;

		// Token: 0x04001B37 RID: 6967
		private bool[] defenderActive;

		// Token: 0x04001B38 RID: 6968
		public GameObject battleCanvas;

		// Token: 0x04001B39 RID: 6969
		public GameObject buttonContainer;

		// Token: 0x04001B3A RID: 6970
		public Button cancelButton;

		// Token: 0x04001B3B RID: 6971
		public Button autoResolveButton;

		// Token: 0x04001B3C RID: 6972
		public Text battleTitle;

		// Token: 0x04001B3D RID: 6973
		public float autoBattlePositionWobble = 0.01f;

		// Token: 0x04001B3E RID: 6974
		public float autoBattleAngleWobble = 20f;

		// Token: 0x04001B3F RID: 6975
		public ParticleSystem autoBattleParticles;

		// Token: 0x04001B40 RID: 6976
		private Camera camera;

		// Token: 0x04001B41 RID: 6977
		private LevelClickable targetLevel;

		// Token: 0x04001B42 RID: 6978
		private SoldierMiniature[] attackers;

		// Token: 0x04001B43 RID: 6979
		private SoldierMiniature[] defenders;

		// Token: 0x04001B44 RID: 6980
		private TimedAction moveLevelAction = new TimedAction(0.3f, false);

		// Token: 0x04001B45 RID: 6981
		private Vector3 originPosition;

		// Token: 0x04001B46 RID: 6982
		private Quaternion originRotation;

		// Token: 0x04001B47 RID: 6983
		private float originFov;

		// Token: 0x04001B48 RID: 6984
		private float targetFov;

		// Token: 0x04001B49 RID: 6985
		private ConquestCampaign conquest;
	}
}
