using System;

namespace MapEditor
{
	// Token: 0x02000655 RID: 1621
	[TypeSerializer(typeof(AudioAsset))]
	public class AudioAssetSerializer : ITypeSerializer
	{
		// Token: 0x06002949 RID: 10569 RVA: 0x0001C605 File Offset: 0x0001A805
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			return AssetTable.GetAudioClip(new AssetId(value));
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0001C617 File Offset: 0x0001A817
		public string Serialize(object value)
		{
			return ((AudioAsset)value).id.guid;
		}
	}
}
