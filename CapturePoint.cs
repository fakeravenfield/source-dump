using System;
using System.Collections.Generic;
using Lua;
using Lua.Wrapper;
using Ravenfield.ScriptedMission;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class CapturePoint : SpawnPoint
{
	// Token: 0x06000B3A RID: 2874 RVA: 0x000743C0 File Offset: 0x000725C0
	protected override void Awake()
	{
		base.Awake();
		if (this.contestedSpawnpointContainer != null)
		{
			int childCount = this.contestedSpawnpointContainer.childCount;
			this.contestedSpawnpointIsSafe = new bool[childCount];
			this.contestedSpawnpointFlatDirection = new Vector3[childCount];
			for (int i = 0; i < childCount; i++)
			{
				this.contestedSpawnpointFlatDirection[i] = (this.contestedSpawnpointContainer.GetChild(i).transform.position - base.transform.position).ToGround().normalized;
			}
			foreach (Renderer renderer in this.contestedSpawnpointContainer.GetComponentsInChildren<Renderer>())
			{
				renderer.enabled = false;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(renderer.transform.position + Vector3.up * 0.5f, Vector3.down), out raycastHit, 3f, 1))
				{
					renderer.transform.position = raycastHit.point + Vector3.up * 0.5f;
				}
			}
			this.ClearContestedSpawnpointSafeFlags();
		}
		this.lastOwnerUpdateTime = Time.time;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00009521 File Offset: 0x00007721
	protected override void Start()
	{
		base.Start();
		if (this.flagParent != null)
		{
			this.flagRenderer = this.flagParent.GetComponentInChildren<Renderer>();
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x000744F4 File Offset: 0x000726F4
	private void Update()
	{
		if (this.flagParent != null)
		{
			Vector3 localPosition = this.flagParent.localPosition;
			localPosition.y = this.control;
			this.flagParent.localPosition = Vector3.Lerp(this.flagParent.localPosition, localPosition, 3f * Time.deltaTime);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00074550 File Offset: 0x00072750
	public void UpdateOwner(List<int> attackingActors, int team0Count, int team1Count)
	{
		float num = Time.time - this.lastOwnerUpdateTime;
		this.lastOwnerUpdateTime = Time.time;
		this.UpdateHighPriorityTargets();
		if (this.contestedSpawnpointContainer != null)
		{
			this.ClearContestedSpawnpointSafeFlags();
		}
		this.isContested = false;
		if (!this.canCapture)
		{
			return;
		}
		this.isContested = (attackingActors.Count > 0);
		for (int i = 0; i < attackingActors.Count; i++)
		{
			Actor actor = ActorManager.instance.actors[attackingActors[i]];
			this.MakeHighPriorityTarget(actor);
			if (this.contestedSpawnpointContainer != null)
			{
				this.UpdateContestedSpawnpointSafeFlags(actor);
			}
		}
		int owner = this.owner;
		int num2;
		int num3;
		int num4;
		if (team0Count > team1Count)
		{
			num2 = 0;
			num3 = team0Count;
			num4 = team1Count;
		}
		else if (team1Count > team0Count)
		{
			num2 = 1;
			num3 = team1Count;
			num4 = team0Count;
		}
		else
		{
			num2 = -1;
			num3 = team0Count;
			num4 = team1Count;
		}
		int num5 = num3 - num4;
		if (num2 != -1)
		{
			if (num2 != this.pendingOwner)
			{
				if (num5 > 0)
				{
					this.control -= (0.025f + (float)num5 * 0.01f) * 3f * this.captureRate * num;
				}
				if (this.control <= 0f)
				{
					if (this.owner != -1)
					{
						this.SetOwner(-1, false);
					}
					this.control = 0.01f;
					this.SetPendingOwner(num2);
				}
			}
			else
			{
				this.control = Mathf.Clamp01(this.control + (0.025f + (float)num5 * 0.01f) * this.captureRate * num);
				if (this.control == 1f && this.owner != this.pendingOwner)
				{
					this.SetOwner(this.pendingOwner, false);
					foreach (Actor actor2 in ActorManager.ActorsOnTeam(this.pendingOwner))
					{
						if (actor2.GetCurrentCapturePoint() == this)
						{
							actor2.OnGotFlagCapture();
						}
					}
				}
			}
		}
		if (this.isContested)
		{
			this.unsafeAction.Start();
		}
		if (this.flagRenderer != null)
		{
			this.flagRenderer.enabled = (this.control > 0f);
		}
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00009548 File Offset: 0x00007748
	private void MakeHighPriorityTarget(Actor actor)
	{
		this.highPriorityTargets.Add(actor);
		actor.MarkHighPriorityTarget(10f);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00074784 File Offset: 0x00072984
	private void UpdateHighPriorityTargets()
	{
		this.highPriorityTargets.RemoveAll((Actor actor) => !actor.IsHighPriorityTarget());
		this.isUnderAttackByVehicles = false;
		using (List<Actor>.Enumerator enumerator = this.highPriorityTargets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsSeated())
				{
					this.isUnderAttackByVehicles = true;
				}
			}
		}
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00074810 File Offset: 0x00072A10
	private void ClearContestedSpawnpointSafeFlags()
	{
		for (int i = 0; i < this.contestedSpawnpointIsSafe.Length; i++)
		{
			this.contestedSpawnpointIsSafe[i] = true;
		}
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0007483C File Offset: 0x00072A3C
	private void UpdateContestedSpawnpointSafeFlags(Actor attacker)
	{
		Vector3 normalized = (attacker.Position() - base.transform.position).ToGround().normalized;
		for (int i = 0; i < this.contestedSpawnpointIsSafe.Length; i++)
		{
			if (this.contestedSpawnpointIsSafe[i])
			{
				float num = Vector3.Dot(normalized, this.contestedSpawnpointFlatDirection[i]);
				this.contestedSpawnpointIsSafe[i] = (num < 0.8f);
			}
		}
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00009561 File Offset: 0x00007761
	public override bool IsSafe()
	{
		return this.unsafeAction.TrueDone();
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0000956E File Offset: 0x0000776E
	public void SetPendingOwner(int team)
	{
		this.pendingOwner = team;
		if (this.flagRenderer != null)
		{
			this.flagRenderer.material.color = Color.Lerp(ColorScheme.TeamColor(team), Color.black, 0.2f);
		}
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000748B0 File Offset: 0x00072AB0
	public override void SetOwner(int team, bool initialOwner = false)
	{
		if (initialOwner)
		{
			if (team == -1)
			{
				this.control = 0f;
			}
			else
			{
				this.control = 1f;
			}
		}
		int owner = this.owner;
		base.SetOwner(team, initialOwner);
		this.SetPendingOwner(team);
		GameModeBase.instance.CapturePointChangedOwner(this, owner, initialOwner);
		if (team > -1)
		{
			RavenscriptManager.events.onCapturePointCaptured.Invoke(this, (WTeam)team);
		}
		else
		{
			RavenscriptManager.events.onCapturePointNeutralized.Invoke(this, (WTeam)owner);
		}
		try
		{
			MinimapUi.UpdateSpawnPointButtons();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000095AA File Offset: 0x000077AA
	public override float GotoRadius()
	{
		return Mathf.Min(this.captureRange * 0.9f, 70f);
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000095C2 File Offset: 0x000077C2
	public override Vector3 GetSpawnPosition()
	{
		if (this.isContested && this.contestedSpawnpointContainer != null)
		{
			return this.GetSafeSpawnPosition();
		}
		return base.GetSpawnPosition();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00074944 File Offset: 0x00072B44
	private Vector3 GetSafeSpawnPosition()
	{
		int childCount = this.contestedSpawnpointContainer.childCount;
		if (childCount == 0)
		{
			return base.GetSpawnPosition();
		}
		int num = UnityEngine.Random.Range(0, childCount);
		for (int i = 0; i < childCount; i++)
		{
			int num2 = (num + i) % childCount;
			if (this.contestedSpawnpointIsSafe[num2])
			{
				return this.contestedSpawnpointContainer.GetChild(num2).position;
			}
		}
		return this.contestedSpawnpointContainer.GetChild(num).position;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000749B0 File Offset: 0x00072BB0
	public Vector3 RandomContestedSpawnPointPosition()
	{
		int childCount = this.contestedSpawnpointContainer.childCount;
		if (childCount == 0)
		{
			return base.GenerateRandomSpawnPosition();
		}
		return this.contestedSpawnpointContainer.GetChild(UnityEngine.Random.Range(0, childCount)).position;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x000095E7 File Offset: 0x000077E7
	public override Vector3 RandomPatrolPosition()
	{
		if (this.contestedSpawnpointContainer != null && UnityEngine.Random.Range(0f, 1f) < 0.3f)
		{
			return this.RandomContestedSpawnPointPosition();
		}
		return base.RandomPatrolPosition();
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000749EC File Offset: 0x00072BEC
	public override bool IsInsideCaptureRange(Vector3 position)
	{
		Vector3 vector = position - base.transform.position;
		return vector.y > -this.captureFloor && vector.y < this.captureCeiling && vector.ToGround().magnitude < this.captureRange;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0000961A File Offset: 0x0000781A
	public override float GetCaptureRange()
	{
		return this.captureRange;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00074A40 File Offset: 0x00072C40
	public override bool IsInsideTransportDropRange(Vector3 position)
	{
		return (position - base.transform.position).ToGround().magnitude < this.captureRange;
	}

	// Token: 0x04000C66 RID: 3174
	public const int LAYER = 20;

	// Token: 0x04000C67 RID: 3175
	private const int NEUTRAL_TEAM = -1;

	// Token: 0x04000C68 RID: 3176
	private const float BASE_CAPTURE_RATE = 0.025f;

	// Token: 0x04000C69 RID: 3177
	private const float CAPTURE_RATE_PER_PERSON = 0.01f;

	// Token: 0x04000C6A RID: 3178
	private const float BASE_NEUTRALIZE_RATE_MULTIPLIER = 3f;

	// Token: 0x04000C6B RID: 3179
	private const float CONTESTED_SPAWNPOINT_SAFE_DOT = 0.8f;

	// Token: 0x04000C6C RID: 3180
	private const float MAX_PATROL_RADIUS = 70f;

	// Token: 0x04000C6D RID: 3181
	public Transform contestedSpawnpointContainer;

	// Token: 0x04000C6E RID: 3182
	private Vector3[] contestedSpawnpointFlatDirection;

	// Token: 0x04000C6F RID: 3183
	private bool[] contestedSpawnpointIsSafe;

	// Token: 0x04000C70 RID: 3184
	public float captureRange = 25f;

	// Token: 0x04000C71 RID: 3185
	public float captureFloor = 5f;

	// Token: 0x04000C72 RID: 3186
	public float captureCeiling = 20f;

	// Token: 0x04000C73 RID: 3187
	public float captureRate = 1f;

	// Token: 0x04000C74 RID: 3188
	[NonSerialized]
	public bool canCapture = true;

	// Token: 0x04000C75 RID: 3189
	public TriggerVolume captureVolume;

	// Token: 0x04000C76 RID: 3190
	public Transform flagParent;

	// Token: 0x04000C77 RID: 3191
	[NonSerialized]
	public float control = 1f;

	// Token: 0x04000C78 RID: 3192
	[NonSerialized]
	public int pendingOwner;

	// Token: 0x04000C79 RID: 3193
	[NonSerialized]
	public Renderer flagRenderer;

	// Token: 0x04000C7A RID: 3194
	[NonSerialized]
	public bool isContested;

	// Token: 0x04000C7B RID: 3195
	private TimedAction unsafeAction = new TimedAction(6f, false);

	// Token: 0x04000C7C RID: 3196
	[NonSerialized]
	public bool isUnderAttackByVehicles;

	// Token: 0x04000C7D RID: 3197
	[NonSerialized]
	public List<Actor> highPriorityTargets = new List<Actor>();

	// Token: 0x04000C7E RID: 3198
	private float lastOwnerUpdateTime;
}
