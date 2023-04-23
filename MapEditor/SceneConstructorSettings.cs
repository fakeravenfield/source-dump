using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapEditor
{
	// Token: 0x020005F0 RID: 1520
	public class SceneConstructorSettings : MonoBehaviour
	{
		// Token: 0x06002700 RID: 9984 RVA: 0x0001AEDC File Offset: 0x000190DC
		public void PersistToNextScene()
		{
			if (!this.persist)
			{
				this.persist = true;
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				SceneManager.sceneLoaded += this.SceneLoaded;
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000F61B0 File Offset: 0x000F43B0
		private void SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (this && base.gameObject && !this.persist)
			{
				SceneManager.sceneLoaded -= this.SceneLoaded;
				UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
			this.persist = false;
		}

		// Token: 0x04002535 RID: 9525
		[NonSerialized]
		public ISceneConstructor sceneConstructor;

		// Token: 0x04002536 RID: 9526
		[NonSerialized]
		public string sceneToActivate;

		// Token: 0x04002537 RID: 9527
		private bool persist;
	}
}
