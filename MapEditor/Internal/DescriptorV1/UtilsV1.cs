using System;
using UnityEngine;

namespace MapEditor.Internal.DescriptorV1
{
	// Token: 0x0200076F RID: 1903
	public static class UtilsV1
	{
		// Token: 0x06002EF2 RID: 12018 RVA: 0x0010A4B8 File Offset: 0x001086B8
		public static void ConvertV1Transform(PrefabObjectDataV1 data, GameObject go, PrefabAsset asset, Transform lateAwake)
		{
			Transform transform = asset.gameObject.transform;
			go.transform.SetPositionAndRotation(transform.position, transform.rotation);
			go.transform.localScale = transform.localScale;
			GameObject gameObject = new GameObject();
			gameObject.transform.parent = lateAwake.transform;
			go.transform.parent = gameObject.transform;
			data.transform.CopyTo(gameObject.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.SetParent(lateAwake.transform, true);
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}
}
