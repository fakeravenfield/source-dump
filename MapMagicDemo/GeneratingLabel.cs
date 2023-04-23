using System;
using MapMagic;
using UnityEngine;

namespace MapMagicDemo
{
	// Token: 0x020004A8 RID: 1192
	public class GeneratingLabel : MonoBehaviour
	{
		// Token: 0x06001DF1 RID: 7665 RVA: 0x000C493C File Offset: 0x000C2B3C
		private void OnGUI()
		{
			if (this.magic == null)
			{
				this.magic = UnityEngine.Object.FindObjectOfType<MapMagic>();
			}
			if (!this.magic.terrains.complete)
			{
				GUI.Box(new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height - 100), 200f, 27f), "");
				GUI.Box(new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height - 100), 200f, 27f), "Generating new terrain...");
			}
		}

		// Token: 0x04001E67 RID: 7783
		public MapMagic magic;
	}
}
