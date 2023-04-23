using System;
using MapEditor.Internal.DescriptorV1;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000657 RID: 1623
	[TypeSerializer(typeof(Color))]
	public class ColorSerializer : ITypeSerializer
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x000FCA34 File Offset: 0x000FAC34
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			return JsonUtility.FromJson<ColorDataV1>(value).ToColor();
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0001C631 File Offset: 0x0001A831
		public string Serialize(object value)
		{
			return JsonUtility.ToJson(new ColorDataV1((Color)value));
		}
	}
}
