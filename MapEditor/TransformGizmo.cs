using System;
using UnityEngine;

namespace MapEditor
{
	// Token: 0x02000608 RID: 1544
	public abstract class TransformGizmo : AbstractGizmo, IShowPropertiesSidebarUI
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x0001B4A3 File Offset: 0x000196A3
		public float InputSensitivity()
		{
			return Mathf.Abs(this.inputSensitivity.transform.localScale.x);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000F8A40 File Offset: 0x000F6C40
		protected override void Start()
		{
			GameObject gameObject = new GameObject("Input Sensitivity");
			gameObject.transform.SetParent(base.transform, false);
			this.inputSensitivity = gameObject.AddComponent<ScaleWithDistance>();
			this.inputSensitivity.distanceScaleFactor = 0.001f;
			this.inputSensitivity.orthographicFactor = 0.034f;
			base.Start();
		}

		// Token: 0x04002599 RID: 9625
		private ScaleWithDistance inputSensitivity;
	}
}
