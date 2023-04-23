using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000073 RID: 115
[CreateAssetMenu]
public class ActorNameSet : ScriptableObject
{
	// Token: 0x0600033F RID: 831 RVA: 0x000041D2 File Offset: 0x000023D2
	public ActorNameSet Clone()
	{
		ActorNameSet actorNameSet = ScriptableObject.CreateInstance<ActorNameSet>();
		actorNameSet.names = new List<string>(this.names);
		return actorNameSet;
	}

	// Token: 0x040002BC RID: 700
	public List<string> names;
}
