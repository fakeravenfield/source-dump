using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000723 RID: 1827
	public class WorldUI : WindowBase
	{
		// Token: 0x06002DDA RID: 11738 RVA: 0x0001F961 File Offset: 0x0001DB61
		protected override void OnInitialize()
		{
			base.OnInitialize();
			base.RegisterAllOnValueChangeCallbacks(new DelOnValueChangedCallback(this.OnApply));
			this.waterLevel.SetRange(-100f, 1000f, 100f);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x001075C0 File Offset: 0x001057C0
		protected override void OnShow()
		{
			base.OnShow();
			WaterLevel waterLevel = MapEditorAssistant.instance.editor.editorTerrain.biomeContainer.GetWaterLevel();
			this.waterLevel.SetValue(waterLevel.transform.position.y);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0001F995 File Offset: 0x0001DB95
		protected override void OnHide()
		{
			base.OnHide();
			this.OnApply();
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0001F9A3 File Offset: 0x0001DBA3
		public void OnApply()
		{
			MapEditorAssistant.instance.editor.editorTerrain.biomeContainer.GetWaterLevel().transform.position = new Vector3(0f, this.waterLevel.GetValue(), 0f);
		}

		// Token: 0x04002A29 RID: 10793
		public SliderWithInput waterLevel;
	}
}
