using System;
using System.Linq;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x02000761 RID: 1889
	[Serializable]
	public struct PrefabObjectDataV1
	{
		// Token: 0x06002EC1 RID: 11969 RVA: 0x001099CC File Offset: 0x00107BCC
		public PrefabObjectDataV1(MeoPrefab prefab)
		{
			this.name = prefab.name;
			this.transform = new TransformDataV1(prefab.transform);
			this.prefab = new AssetIdDataV1(prefab.GetAssetId());
			this.properties = null;
			GoPropertyProvider component = prefab.GetComponent<GoPropertyProvider>();
			if (component)
			{
				this.properties = (from p in component.GetBindings()
				select new KeyValueDataV1(p.memberName, GoPropertyProvider.Serialize(p))).ToArray<KeyValueDataV1>();
			}
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00109A54 File Offset: 0x00107C54
		public void CopyTo(MeoPrefab prefab, GoPropertyProvider.DeserializeContext ctx)
		{
			prefab.name = this.name;
			this.transform.CopyTo(prefab.transform);
			PropertyProvider component = prefab.GetComponent<PropertyProvider>();
			this.CopyPropertiesTo(component, ctx);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00109A90 File Offset: 0x00107C90
		public void CopyPropertiesTo(PropertyProvider provider, GoPropertyProvider.DeserializeContext ctx)
		{
			if (provider == null || this.properties == null || this.properties.Length == 0)
			{
				return;
			}
			var array = (from p in provider.GetBindings()
			join kv in this.properties on p.memberName equals kv.key
			select new
			{
				property = p,
				serializedValue = kv.value
			}).ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				var <>f__AnonymousType = array[i];
				GoPropertyProvider.Deserialize(<>f__AnonymousType.property, <>f__AnonymousType.serializedValue, ctx);
			}
		}

		// Token: 0x04002AE9 RID: 10985
		public string name;

		// Token: 0x04002AEA RID: 10986
		public AssetIdDataV1 prefab;

		// Token: 0x04002AEB RID: 10987
		public TransformDataV1 transform;

		// Token: 0x04002AEC RID: 10988
		public KeyValueDataV1[] properties;
	}
}
