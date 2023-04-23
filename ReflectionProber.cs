using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class ReflectionProber : MonoBehaviour
{
	// Token: 0x06000C07 RID: 3079 RVA: 0x00009E7B File Offset: 0x0000807B
	private void Awake()
	{
		ReflectionProber.instance = this;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00009E83 File Offset: 0x00008083
	public void SetupProbes()
	{
		base.StartCoroutine(this.SetupProbesCoroutine());
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00009E92 File Offset: 0x00008092
	private IEnumerator SetupProbesCoroutine()
	{
		this.normalProbe.RenderProbe();
		yield return new WaitForEndOfFrame();
		if (TimeOfDay.instance != null)
		{
			TimeOfDay.instance.ApplyNightvision();
			this.nightVisionProbe.RenderProbe();
			yield return new WaitForEndOfFrame();
			TimeOfDay.instance.ResetAtmosphere();
		}
		else
		{
			this.Reset();
		}
		yield break;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00009EA1 File Offset: 0x000080A1
	public void SwitchToNightVision()
	{
		this.normalProbe.size = this.disabledBounds;
		this.nightVisionProbe.size = this.enabledBounds;
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00009EC5 File Offset: 0x000080C5
	public void Reset()
	{
		this.normalProbe.size = this.enabledBounds;
		this.nightVisionProbe.size = this.disabledBounds;
	}

	// Token: 0x04000D1C RID: 3356
	public static ReflectionProber instance;

	// Token: 0x04000D1D RID: 3357
	public ReflectionProbe normalProbe;

	// Token: 0x04000D1E RID: 3358
	public ReflectionProbe nightVisionProbe;

	// Token: 0x04000D1F RID: 3359
	private Vector3 enabledBounds = new Vector3(9999999f, 9999999f, 9999999f);

	// Token: 0x04000D20 RID: 3360
	private Vector3 disabledBounds = new Vector3(0f, 0f, 0f);
}
