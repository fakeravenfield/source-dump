using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000959 RID: 2393
	[Name("Time")]
	public static class WTime
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06003C9E RID: 15518 RVA: 0x000290EF File Offset: 0x000272EF
		// (set) Token: 0x06003C9F RID: 15519 RVA: 0x000290F6 File Offset: 0x000272F6
		[Doc("Sets the time scale of the game.[..] Also tweaks the Time.fixedDeltaTime value and audio pitch to match the assigned time scale.")]
		public static float timeScale
		{
			get
			{
				return Time.timeScale;
			}
			set
			{
				Time.timeScale = value;
				Time.fixedDeltaTime = Time.timeScale / 60f;
				FpsActorController.instance.mixer.SetFloat("pitch", Time.timeScale);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x00029128 File Offset: 0x00027328
		public static float time
		{
			get
			{
				return Time.time;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x0002912F File Offset: 0x0002732F
		public static float unscaledTime
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x00029136 File Offset: 0x00027336
		public static float timeSinceLevelLoad
		{
			get
			{
				return Time.timeSinceLevelLoad;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x0002913D File Offset: 0x0002733D
		public static float unscaledDeltaTime
		{
			get
			{
				return Time.unscaledDeltaTime;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x00029144 File Offset: 0x00027344
		public static float deltaTime
		{
			get
			{
				return Time.deltaTime;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x0002914B File Offset: 0x0002734B
		public static float fixedDeltaTime
		{
			get
			{
				return Time.fixedDeltaTime;
			}
		}
	}
}
