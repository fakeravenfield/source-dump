using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[Serializable]
public class SerializedGameInfo
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x00005AB8 File Offset: 0x00003CB8
	public static SerializedGameInfo DeserializeFile(string path, GameInfoContainer target)
	{
		SerializedGameInfo serializedGameInfo = JsonUtility.FromJson<SerializedGameInfo>(File.ReadAllText(path));
		serializedGameInfo.Deserialize(target);
		return serializedGameInfo;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0005DB98 File Offset: 0x0005BD98
	private void Deserialize(GameInfoContainer target)
	{
		int num = Mathf.Min(target.team.Length, this.teamInfo.Length);
		for (int i = 0; i < num; i++)
		{
			target.team[i] = this.teamInfo[i].Deserialize();
		}
		if (this.mutatorInfo != null)
		{
			foreach (MutatorEntry mutatorEntry in ModManager.instance.loadedMutators)
			{
				mutatorEntry.isEnabled = false;
			}
			SerializedMutatorInfo[] array = this.mutatorInfo;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].Deserialize();
			}
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0005DC50 File Offset: 0x0005BE50
	public SerializedGameInfo(GameInfoContainer container)
	{
		this.teamInfo = new SerializedTeamInfo[container.team.Length];
		for (int i = 0; i < this.teamInfo.Length; i++)
		{
			this.teamInfo[i] = new SerializedTeamInfo(container.team[i]);
		}
		this.mutatorInfo = (from m in ModManager.GetAllEnabledMutators()
		select new SerializedMutatorInfo(m)).ToArray<SerializedMutatorInfo>();
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0005DCDC File Offset: 0x0005BEDC
	public void WriteToFile(string path)
	{
		string contents = JsonUtility.ToJson(this, true);
		File.WriteAllText(path, contents, Encoding.UTF8);
	}

	// Token: 0x040005D3 RID: 1491
	private const bool PRETTY_PRINT = true;

	// Token: 0x040005D4 RID: 1492
	public SerializedTeamInfo[] teamInfo;

	// Token: 0x040005D5 RID: 1493
	public SerializedMutatorInfo[] mutatorInfo;

	// Token: 0x040005D6 RID: 1494
	public bool playerHasAllWeapons = true;
}
