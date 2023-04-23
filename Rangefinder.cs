using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E0 RID: 224
public class Rangefinder : MonoBehaviour
{
	// Token: 0x060006BA RID: 1722 RVA: 0x0005FF5C File Offset: 0x0005E15C
	public void Sample()
	{
		try
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out raycastHit, this.maxDistance, -12618245))
			{
				this.rangeText.text = ((int)raycastHit.distance).ToString();
			}
			else
			{
				this.rangeText.text = this.noReadingText;
			}
			this.sampleAction.Start();
		}
		catch (Exception e)
		{
			ModManager.HandleModException(e);
		}
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00006518 File Offset: 0x00004718
	private void Update()
	{
		if (this.sampleAction.TrueDone())
		{
			this.Sample();
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0005FFEC File Offset: 0x0005E1EC
	private void Awake()
	{
		float lifetime = 1f / this.samplesPerSecond;
		this.sampleAction = new TimedAction(lifetime, false);
		this.Sample();
	}

	// Token: 0x0400069C RID: 1692
	private const int MASK = -12618245;

	// Token: 0x0400069D RID: 1693
	public Text rangeText;

	// Token: 0x0400069E RID: 1694
	public string noReadingText = "-";

	// Token: 0x0400069F RID: 1695
	public float maxDistance = 1000f;

	// Token: 0x040006A0 RID: 1696
	public float samplesPerSecond = 2f;

	// Token: 0x040006A1 RID: 1697
	private TimedAction sampleAction = new TimedAction(1f, false);
}
