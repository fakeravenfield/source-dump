using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x020001C7 RID: 455
public class SpawnPointConnectionGraph
{
	// Token: 0x06000C44 RID: 3140 RVA: 0x0000A0DD File Offset: 0x000082DD
	public SpawnPointConnectionGraph()
	{
		this.spawns = new List<SpawnPoint>();
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0000A0F0 File Offset: 0x000082F0
	public SpawnPointConnectionGraph(SpawnPoint root, uint areaId)
	{
		this.spawns = new List<SpawnPoint>();
		this.areaId = areaId;
		this.Add(root);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0000A111 File Offset: 0x00008311
	public void Add(SpawnPoint spawnPoint)
	{
		this.spawns.Add(spawnPoint);
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0000A11F File Offset: 0x0000831F
	public bool Contains(SpawnPoint spawnPoint)
	{
		return this.spawns.Contains(spawnPoint);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0000A12D File Offset: 0x0000832D
	public SpawnPoint GetRoot()
	{
		return this.spawns[0];
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00078878 File Offset: 0x00076A78
	public override string ToString()
	{
		return "SpawnPointConnectionGraph: " + string.Join(", ", (from s in this.spawns
		select s.shortName).ToArray<string>());
	}

	// Token: 0x04000D5A RID: 3418
	public List<SpawnPoint> spawns;

	// Token: 0x04000D5B RID: 3419
	public uint areaId;
}
