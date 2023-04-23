using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class TestSMath : MonoBehaviour
{
	// Token: 0x06001447 RID: 5191 RVA: 0x00095CD8 File Offset: 0x00093ED8
	private void Update()
	{
		Debug.DrawLine(base.transform.position, this.end.position, Color.white);
		Debug.DrawLine(this.probe.position, SMath.LineSegmentVsPointClosest(base.transform.position, this.end.position, this.probe.position), Color.red);
	}

	// Token: 0x040015A4 RID: 5540
	public Transform end;

	// Token: 0x040015A5 RID: 5541
	public Transform probe;
}
