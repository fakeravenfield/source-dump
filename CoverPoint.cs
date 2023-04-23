using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class CoverPoint : MonoBehaviour
{
	// Token: 0x06000494 RID: 1172 RVA: 0x0005617C File Offset: 0x0005437C
	public bool CoversDirection(Vector3 direction)
	{
		return Vector3.Dot(direction.ToGround().normalized, base.transform.forward) >= 0.707f;
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00004E4A File Offset: 0x0000304A
	public bool CoversPoint(Vector3 point)
	{
		return this.CoversDirection(point - base.transform.position);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00004E4A File Offset: 0x0000304A
	public bool CoversPointAndCanSee(Vector3 point)
	{
		return this.CoversDirection(point - base.transform.position);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00004E63 File Offset: 0x00003063
	public bool CanSee(Vector3 point)
	{
		return !Physics.Linecast(CoverPlacer.CoverPointRayOrigin(this), point, 8388609);
	}

	// Token: 0x04000487 RID: 1159
	public const float COS_MAX_COVER_ANGLE = 0.707f;

	// Token: 0x04000488 RID: 1160
	public CoverPoint.Type type;

	// Token: 0x04000489 RID: 1161
	public byte coverage;

	// Token: 0x0400048A RID: 1162
	[NonSerialized]
	public bool taken;

	// Token: 0x02000093 RID: 147
	[Serializable]
	public enum Type
	{
		// Token: 0x0400048C RID: 1164
		LeanLeft,
		// Token: 0x0400048D RID: 1165
		LeanRight,
		// Token: 0x0400048E RID: 1166
		Crouch
	}
}
