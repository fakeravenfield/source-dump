using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MapEditor
{
	// Token: 0x0200062B RID: 1579
	public class MePrimitiveObject : MonoBehaviour, IPropertyChangeNotify
	{
		// Token: 0x06002886 RID: 10374 RVA: 0x0001BF00 File Offset: 0x0001A100
		private void Awake()
		{
			this.OnPropertyChanged();
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000FAC94 File Offset: 0x000F8E94
		public void OnPropertyChanged()
		{
			MeshRenderer component = base.GetComponent<MeshRenderer>();
			if (this.material == null)
			{
				component.sharedMaterial = this.baseMaterial;
				return;
			}
			this.material.onTextureChanged.RemoveListener(new UnityAction(this.OnPropertyChanged));
			this.material.onTextureChanged.AddListener(new UnityAction(this.OnPropertyChanged));
			component.sharedMaterial = this.material.GetMaterial(this.baseMaterial);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000FAD10 File Offset: 0x000F8F10
		public static bool IsMaterialInUse(MapEditorMaterial material)
		{
			return UnityEngine.Object.FindObjectsOfType<MePrimitiveObject>().Any((MePrimitiveObject t) => t.material == material);
		}

		// Token: 0x04002684 RID: 9860
		public Material baseMaterial;

		// Token: 0x04002685 RID: 9861
		public MapEditorMaterial material;
	}
}
