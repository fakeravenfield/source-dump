using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000636 RID: 1590
	public class MeoPrefab : MapEditorObject
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x0001C09D File Offset: 0x0001A29D
		public AssetId GetAssetId()
		{
			return this.assetId;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x0001C0A5 File Offset: 0x0001A2A5
		public override string GetCategoryName()
		{
			return base.name;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000FB138 File Offset: 0x000F9338
		public override MapEditorObject Clone()
		{
			MeoPrefab meoPrefab = MeoPrefab.Create(this.assetId, base.transform.parent);
			Utils.CopyLocalTransform(base.transform, meoPrefab.transform);
			PropertyProvider.CopyProperties(this, meoPrefab, false);
			IPropertyChangeNotify component = meoPrefab.GetComponent<IPropertyChangeNotify>();
			if (component != null)
			{
				component.OnPropertyChanged();
			}
			return meoPrefab;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000FB188 File Offset: 0x000F9388
		public static MeoPrefab Create(AssetId id, Transform parent = null)
		{
			GameObject prefab = MapEditorAssistant.instance.GetPrefab(id);
			MeoPrefabAssistant componentInChildren = prefab.GetComponentInChildren<MeoPrefabAssistant>();
			bool flag = componentInChildren && componentInChildren.instantiatePrefab;
			bool flag2 = componentInChildren && componentInChildren.serializedObject;
			MeoPrefab meoPrefab;
			if (flag)
			{
				GameObject inEditorRendering = componentInChildren.inEditorRendering;
				meoPrefab = MapEditorObject.Instantiate<MeoPrefab>(prefab, inEditorRendering, prefab.name, parent);
				if (flag2)
				{
					MeoPrefabAssistant componentInChildren2 = meoPrefab.GetComponentInChildren<MeoPrefabAssistant>();
					meoPrefab.gameObject.AddComponent<GoPropertyProvider>().SetTarget(componentInChildren2.serializedObject);
				}
			}
			else
			{
				meoPrefab = MapEditorObject.Create<MeoPrefab>(prefab, prefab.name, parent, true);
				if (flag2)
				{
					meoPrefab.gameObject.AddComponent<PrefabPropertyProvider>().SetTarget(componentInChildren.serializedObject);
				}
			}
			meoPrefab.assetId = id;
			return meoPrefab;
		}

		// Token: 0x040026A5 RID: 9893
		private AssetId assetId;
	}
}
