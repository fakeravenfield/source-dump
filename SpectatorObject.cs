using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class SpectatorObject : MonoBehaviour
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x0000A25C File Offset: 0x0000845C
	private void Start()
	{
		if (GameManager.PlayerTeam() != -1 ^ this.destroyInSpectatorMode)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000D6B RID: 3435
	public bool destroyInSpectatorMode;
}
