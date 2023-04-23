using System;
using Ravenfield.Mutator.Configuration;
using UnityEngine;

// Token: 0x02000223 RID: 547
[Serializable]
public class MutatorEntry
{
	// Token: 0x06000EBA RID: 3770 RVA: 0x0000BC30 File Offset: 0x00009E30
	public override string ToString()
	{
		return "Mutator: " + this.name;
	}

	// Token: 0x04000F8B RID: 3979
	public string name = "New Mutator";

	// Token: 0x04000F8C RID: 3980
	public string description = "";

	// Token: 0x04000F8D RID: 3981
	public Texture2D menuImage;

	// Token: 0x04000F8E RID: 3982
	public GameObject mutatorPrefab;

	// Token: 0x04000F8F RID: 3983
	public MutatorConfiguration configuration;

	// Token: 0x04000F90 RID: 3984
	[NonSerialized]
	public ModInformation sourceMod;

	// Token: 0x04000F91 RID: 3985
	[NonSerialized]
	public bool isEnabled;
}
