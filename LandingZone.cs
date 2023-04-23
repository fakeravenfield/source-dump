using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class LandingZone : MonoBehaviour
{
	// Token: 0x060004A5 RID: 1189 RVA: 0x00056224 File Offset: 0x00054424
	public Vector3 GetLandingPoint()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f, 0f));
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00004EE3 File Offset: 0x000030E3
	private void Awake()
	{
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00004EF1 File Offset: 0x000030F1
	private void Start()
	{
		if (this.target != null && this.target.gameObject.activeInHierarchy)
		{
			this.target.AddLandingZone(this);
		}
	}

	// Token: 0x04000493 RID: 1171
	public SpawnPoint target;
}
