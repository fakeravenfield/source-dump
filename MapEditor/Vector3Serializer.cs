using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x0200065D RID: 1629
	[TypeSerializer(typeof(Vector3))]
	public class Vector3Serializer : ITypeSerializer
	{
		// Token: 0x06002961 RID: 10593 RVA: 0x0001C663 File Offset: 0x0001A863
		public object Deserialize(string value, GoPropertyProvider.DeserializeContext ctx)
		{
			return JsonUtility.FromJson<Vector3>(value);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0001C670 File Offset: 0x0001A870
		public string Serialize(object value)
		{
			return JsonUtility.ToJson((Vector3)value);
		}
	}
}
