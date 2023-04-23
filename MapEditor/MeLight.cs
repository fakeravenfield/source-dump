using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000629 RID: 1577
	public class MeLight : MonoBehaviour, IPropertyChangeNotify
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x0001BE68 File Offset: 0x0001A068
		private void Awake()
		{
			this.OnPropertyChanged();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000FA934 File Offset: 0x000F8B34
		public void OnPropertyChanged()
		{
			if (this.light == null)
			{
				this.light = base.gameObject.AddComponent<Light>();
			}
			this.light.type = LightType.Point;
			this.light.color = this.color;
			this.light.range = this.range;
			this.light.intensity = this.intensity;
		}

		// Token: 0x04002677 RID: 9847
		public Color color = Color.white;

		// Token: 0x04002678 RID: 9848
		[Range(0f, 100f)]
		public float range = 10f;

		// Token: 0x04002679 RID: 9849
		[Range(0f, 100f)]
		public float intensity = 1f;

		// Token: 0x0400267A RID: 9850
		private Light light;
	}
}
