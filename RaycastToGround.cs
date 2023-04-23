using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class RaycastToGround : MonoBehaviour
{
	// Token: 0x060006AB RID: 1707 RVA: 0x00006422 File Offset: 0x00004622
	private void Start()
	{
		this.yLastFrame = base.transform.position.y;
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0000643A File Offset: 0x0000463A
	private void LateUpdate()
	{
		this.Raycast(this.isFirstRay ? this.firstRayOriginStartVerticalOffset : this.rayOriginStartVerticalOffset, this.isFirstRay ? this.firstRayLengthBelowOrigin : this.rayLengthBelowOrigin);
		this.isFirstRay = false;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0005FBF4 File Offset: 0x0005DDF4
	private void Raycast(float verticalOffset, float rayLengthBelow)
	{
		Vector3 position = base.transform.position;
		Vector3 origin = position;
		if (this.allowIncrementalRayOrigin)
		{
			origin.y = this.yLastFrame;
		}
		Ray ray = new Ray(origin, Vector3.down);
		ray.origin += new Vector3(0f, verticalOffset, 0f);
		float maxDistance = verticalOffset + rayLengthBelow;
		int layerMask = 2232321;
		float y = position.y;
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
		{
			y = raycastHit.point.y;
		}
		float yOffset = y - position.y;
		this.ApplyVerticalOffset(base.transform, yOffset);
		foreach (Transform transform in this.offsetChildren)
		{
			this.ApplyVerticalOffset(transform, yOffset);
		}
		this.yLastFrame = y;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0005FCD0 File Offset: 0x0005DED0
	private void ApplyVerticalOffset(Transform transform, float yOffset)
	{
		Vector3 position = transform.position;
		position.y += yOffset;
		transform.position = position;
	}

	// Token: 0x0400068D RID: 1677
	public float firstRayOriginStartVerticalOffset = 5f;

	// Token: 0x0400068E RID: 1678
	public float firstRayLengthBelowOrigin = 5f;

	// Token: 0x0400068F RID: 1679
	public float rayOriginStartVerticalOffset = 1f;

	// Token: 0x04000690 RID: 1680
	public float rayLengthBelowOrigin = 1f;

	// Token: 0x04000691 RID: 1681
	public bool allowIncrementalRayOrigin;

	// Token: 0x04000692 RID: 1682
	public Transform[] offsetChildren;

	// Token: 0x04000693 RID: 1683
	private float yLastFrame;

	// Token: 0x04000694 RID: 1684
	private bool isFirstRay = true;
}
