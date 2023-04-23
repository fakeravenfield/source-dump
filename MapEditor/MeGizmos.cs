using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000600 RID: 1536
	public class MeGizmos : MonoBehaviour
	{
		// Token: 0x06002762 RID: 10082 RVA: 0x0001B315 File Offset: 0x00019515
		private void Awake()
		{
			MeGizmos.instance = this;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0001B31D File Offset: 0x0001951D
		public static TranslateGizmo CreateTranslateGizmo(Transform parent = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MeGizmos.instance.translateGizmoPrefab, parent);
			gameObject.transform.position = Vector3.zero;
			return gameObject.GetComponent<TranslateGizmo>();
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0001B344 File Offset: 0x00019544
		public static RotateGizmo CreateRotateGizmo(Transform parent = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MeGizmos.instance.rotateGizmoPrefab, parent);
			gameObject.transform.position = Vector3.zero;
			return gameObject.GetComponent<RotateGizmo>();
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0001B36B File Offset: 0x0001956B
		public static ScaleGizmo CreateScaleGizmo(Transform parent = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MeGizmos.instance.scaleGizmoPrefab, parent);
			gameObject.transform.position = Vector3.zero;
			return gameObject.GetComponent<ScaleGizmo>();
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0001B392 File Offset: 0x00019592
		public static TerrainBrushGizmo CreateTerrainBrushGizmo(Transform parent = null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MeGizmos.instance.terrainBrushGizmoPrefab, parent);
			gameObject.transform.position = Vector3.zero;
			return gameObject.GetComponent<TerrainBrushGizmo>();
		}

		// Token: 0x0400257D RID: 9597
		public GameObject translateGizmoPrefab;

		// Token: 0x0400257E RID: 9598
		public GameObject rotateGizmoPrefab;

		// Token: 0x0400257F RID: 9599
		public GameObject scaleGizmoPrefab;

		// Token: 0x04002580 RID: 9600
		public GameObject terrainBrushGizmoPrefab;

		// Token: 0x04002581 RID: 9601
		public static MeGizmos instance;
	}
}
