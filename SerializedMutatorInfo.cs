using System;
using System.Collections.Generic;
using System.Linq;
using Ravenfield.Mutator.Configuration;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[Serializable]
public class SerializedMutatorInfo
{
	// Token: 0x060005EF RID: 1519 RVA: 0x00005AE0 File Offset: 0x00003CE0
	public SerializedMutatorInfo()
	{
		this.sourceDirectory = "";
		this.sourceWorkshopId = 0UL;
		this.name = "";
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0005DD00 File Offset: 0x0005BF00
	public SerializedMutatorInfo(MutatorEntry mutator)
	{
		this.name = mutator.name;
		this.sourceDirectory = mutator.sourceMod.directoryName;
		this.sourceWorkshopId = mutator.sourceMod.workshopItemId.m_PublishedFileId;
		List<SerializedMutatorInfo.SerializedConfigurationField> list = new List<SerializedMutatorInfo.SerializedConfigurationField>();
		foreach (MutatorConfigurationSortableField mutatorConfigurationSortableField in mutator.configuration.GetAllFields())
		{
			if (!(mutatorConfigurationSortableField is MutatorConfigurationLabel))
			{
				list.Add(new SerializedMutatorInfo.SerializedConfigurationField(mutatorConfigurationSortableField));
			}
		}
		this.fields = list.ToArray();
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0005DDAC File Offset: 0x0005BFAC
	public void Deserialize()
	{
		MutatorEntry matchingMutator = this.GetMatchingMutator();
		if (matchingMutator == null)
		{
			Debug.Log("Could not deserialize Mutator Info with name " + this.name);
			return;
		}
		matchingMutator.isEnabled = true;
		SerializedMutatorInfo.SerializedConfigurationField[] array = this.fields;
		for (int i = 0; i < array.Length; i++)
		{
			SerializedMutatorInfo.SerializedConfigurationField serializedField = array[i];
			MutatorConfigurationSortableField mutatorConfigurationSortableField = matchingMutator.configuration.GetAllFields().FirstOrDefault((MutatorConfigurationSortableField f) => string.Equals(f.id, serializedField.id));
			if (mutatorConfigurationSortableField != null)
			{
				mutatorConfigurationSortableField.DeserializeValue(serializedField.serializedValue);
			}
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0005DE38 File Offset: 0x0005C038
	private MutatorEntry GetMatchingMutator()
	{
		foreach (MutatorEntry mutatorEntry in ModManager.instance.loadedMutators)
		{
			if (string.Equals(mutatorEntry.name, this.name))
			{
				if (mutatorEntry.sourceMod.isOfficialContent)
				{
					if (this.sourceWorkshopId == 0UL)
					{
						return mutatorEntry;
					}
					return null;
				}
				else if (mutatorEntry.sourceMod.workshopItemId.m_PublishedFileId == this.sourceWorkshopId || mutatorEntry.sourceMod.directoryName == this.sourceDirectory)
				{
					return mutatorEntry;
				}
			}
		}
		return null;
	}

	// Token: 0x040005D9 RID: 1497
	public string sourceDirectory;

	// Token: 0x040005DA RID: 1498
	public ulong sourceWorkshopId;

	// Token: 0x040005DB RID: 1499
	public string name;

	// Token: 0x040005DC RID: 1500
	public SerializedMutatorInfo.SerializedConfigurationField[] fields;

	// Token: 0x020000BA RID: 186
	[Serializable]
	public class SerializedConfigurationField
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00005B06 File Offset: 0x00003D06
		public SerializedConfigurationField(MutatorConfigurationSortableField field)
		{
			this.id = field.id;
			this.serializedValue = field.SerializeValue();
		}

		// Token: 0x040005DD RID: 1501
		public string id;

		// Token: 0x040005DE RID: 1502
		public string serializedValue;
	}
}
