using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class SpawnPointConnection : MonoBehaviour
{
	// Token: 0x0600112C RID: 4396 RVA: 0x0000D93C File Offset: 0x0000BB3C
	private void Start()
	{
		base.Invoke("Test", 0.1f);
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0008B974 File Offset: 0x00089B74
	public void Test()
	{
		Debug.Log(string.Concat(new string[]
		{
			"Connection test ",
			this.a.name,
			" to ",
			this.b.name,
			")"
		}));
		Debug.Log("Land? " + SpawnPointNeighborManager.HasLandConnection(this.a, this.b).ToString());
		Debug.Log("Water? " + SpawnPointNeighborManager.HasWaterConnection(this.a, this.b).ToString());
	}

	// Token: 0x04001270 RID: 4720
	public SpawnPoint a;

	// Token: 0x04001271 RID: 4721
	public SpawnPoint b;
}
