using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D7 RID: 727
public class SquadOrderPoint : MonoBehaviour
{
	// Token: 0x06001343 RID: 4931 RVA: 0x0000F55F File Offset: 0x0000D75F
	public bool HasOnIssueDelegate()
	{
		return this.OnIssue != null;
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x0000F56A File Offset: 0x0000D76A
	private void Awake()
	{
		this.image = base.GetComponentInChildren<RawImage>();
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x0000F578 File Offset: 0x0000D778
	private void Update()
	{
		if (!this.registered && SquadPointUi.instance != null)
		{
			SquadPointUi.Register(this);
			this.registered = true;
		}
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x0000F59C File Offset: 0x0000D79C
	private void OnDestroy()
	{
		SquadPointUi.Unregister(this);
		if (this.image != null)
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
		}
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x0000F5C2 File Offset: 0x0000D7C2
	public override string ToString()
	{
		if (this.type == SquadOrderPoint.ObjectiveType.Custom)
		{
			return this.targetText;
		}
		return this.type.ToString() + " " + this.targetText;
	}

	// Token: 0x040014BC RID: 5308
	[NonSerialized]
	public SquadOrderPoint.ObjectiveType type;

	// Token: 0x040014BD RID: 5309
	[NonSerialized]
	public RawImage image;

	// Token: 0x040014BE RID: 5310
	private bool registered;

	// Token: 0x040014BF RID: 5311
	[NonSerialized]
	public string targetText = "";

	// Token: 0x040014C0 RID: 5312
	[NonSerialized]
	public float maxIssueDistance = 100f;

	// Token: 0x040014C1 RID: 5313
	public SquadOrderPoint.OnIssueDel OnIssue;

	// Token: 0x020002D8 RID: 728
	public enum ObjectiveType
	{
		// Token: 0x040014C3 RID: 5315
		Attack,
		// Token: 0x040014C4 RID: 5316
		Defend,
		// Token: 0x040014C5 RID: 5317
		Enter,
		// Token: 0x040014C6 RID: 5318
		Exit,
		// Token: 0x040014C7 RID: 5319
		Commandeer,
		// Token: 0x040014C8 RID: 5320
		Custom
	}

	// Token: 0x020002D9 RID: 729
	// (Invoke) Token: 0x0600134A RID: 4938
	public delegate void OnIssueDel();
}
