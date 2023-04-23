using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class Eyes : MonoBehaviour
{
	// Token: 0x06000682 RID: 1666 RVA: 0x000061DF File Offset: 0x000043DF
	private void Awake()
	{
		this.renderer = base.GetComponent<SkinnedMeshRenderer>();
		this.AutoUpdateEye();
		this.eyeCardMaterial = this.eyeCardRenderer.material;
		if (this.lookOrigin == null)
		{
			this.lookOrigin = base.transform;
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0005F864 File Offset: 0x0005DA64
	private void Update()
	{
		try
		{
			for (int i = 0; i < 2; i++)
			{
				this.weight[i] = Mathf.MoveTowards(this.weight[i], (this.targetBlendShapeIndex == i) ? 100f : 0f, 2000f * Time.deltaTime);
				this.renderer.SetBlendShapeWeight(i, this.weight[i]);
			}
		}
		catch (Exception)
		{
		}
		if (this.lookTarget != null)
		{
			this.SetLookIKPoint(this.lookTarget.position);
		}
		this.targetOffset = new Vector2(Mathf.Clamp(this.targetOffset.x, -this.maxOffset.x, this.maxOffset.x), Mathf.Clamp(this.targetOffset.y, -this.maxOffset.y, this.maxOffset.y));
		this.offset = Vector2.MoveTowards(this.offset, this.targetOffset, Time.deltaTime * 5f);
		this.eyeCardMaterial.SetVector("_Offset", this.offset);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0005F990 File Offset: 0x0005DB90
	public void SetLookIKPoint(Vector3 lookPoint)
	{
		Vector3 vector = lookPoint - this.lookOrigin.position;
		float d = Mathf.Max(vector.z, 0.1f);
		Vector3 vector2 = vector / d;
		this.targetOffset = new Vector2(vector2.x, -vector2.y) * this.lookAtMultiplier * 0.25f;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0000621E File Offset: 0x0000441E
	public void AutoUpdateEye()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.AutoUpdateCoroutine());
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00006233 File Offset: 0x00004433
	private void LookRandomDirection()
	{
		this.targetOffset = new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.15f, 0.15f));
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0000625E File Offset: 0x0000445E
	private IEnumerator AutoUpdateCoroutine()
	{
		for (;;)
		{
			this.OpenEye();
			yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 4f));
			this.CloseEye();
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0000626D File Offset: 0x0000446D
	public void Blink()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.BlinkCoroutine());
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00006282 File Offset: 0x00004482
	private IEnumerator BlinkCoroutine()
	{
		this.CloseEye();
		yield return new WaitForSeconds(0.2f);
		this.OpenEye();
		yield break;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00006291 File Offset: 0x00004491
	private void OpenEye()
	{
		this.targetBlendShapeIndex = -1;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0000629A File Offset: 0x0000449A
	private void SquintEye()
	{
		this.targetBlendShapeIndex = 0;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x000062A3 File Offset: 0x000044A3
	private void CloseEye()
	{
		this.targetBlendShapeIndex = 1;
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x000062AC File Offset: 0x000044AC
	public void ForceOpenEye()
	{
		base.StopAllCoroutines();
		this.OpenEye();
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x000062BA File Offset: 0x000044BA
	public void ForceSquintEye()
	{
		base.StopAllCoroutines();
		this.SquintEye();
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000062C8 File Offset: 0x000044C8
	public void ForceCloseEye()
	{
		base.StopAllCoroutines();
		this.CloseEye();
	}

	// Token: 0x04000667 RID: 1639
	private const float AUTO_MIN_OPEN_TIME = 1f;

	// Token: 0x04000668 RID: 1640
	private const float AUTO_MAX_OPEN_TIME = 4f;

	// Token: 0x04000669 RID: 1641
	private const float BLINK_TIME = 0.2f;

	// Token: 0x0400066A RID: 1642
	private const float INTERPOLATION_RATE = 2000f;

	// Token: 0x0400066B RID: 1643
	private SkinnedMeshRenderer renderer;

	// Token: 0x0400066C RID: 1644
	public Renderer eyeCardRenderer;

	// Token: 0x0400066D RID: 1645
	public Transform lookOrigin;

	// Token: 0x0400066E RID: 1646
	public Transform lookTarget;

	// Token: 0x0400066F RID: 1647
	private Material eyeCardMaterial;

	// Token: 0x04000670 RID: 1648
	private int targetBlendShapeIndex;

	// Token: 0x04000671 RID: 1649
	private float[] weight = new float[2];

	// Token: 0x04000672 RID: 1650
	public float lookAtMultiplier = 1f;

	// Token: 0x04000673 RID: 1651
	public Vector2 maxOffset = new Vector2(0.3f, 0.17f);

	// Token: 0x04000674 RID: 1652
	private Vector2 offset;

	// Token: 0x04000675 RID: 1653
	private Vector2 targetOffset;
}
