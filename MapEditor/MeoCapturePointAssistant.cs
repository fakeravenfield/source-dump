using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000635 RID: 1589
	public class MeoCapturePointAssistant : MeoAssistant
	{
		// Token: 0x060028AE RID: 10414 RVA: 0x0001C087 File Offset: 0x0001A287
		private void Awake()
		{
			this.flagRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0001C095 File Offset: 0x0001A295
		public Renderer GetFlagRenderer()
		{
			return this.flagRenderer;
		}

		// Token: 0x040026A0 RID: 9888
		public CircleRenderer ceilingCircle;

		// Token: 0x040026A1 RID: 9889
		public CircleRenderer captureCircle;

		// Token: 0x040026A2 RID: 9890
		public CircleRenderer floorCircle;

		// Token: 0x040026A3 RID: 9891
		public CircleRenderer protectionCircle;

		// Token: 0x040026A4 RID: 9892
		private Renderer flagRenderer;
	}
}
