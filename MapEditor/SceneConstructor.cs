using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapEditor
{
	// Token: 0x020005E9 RID: 1513
	public class SceneConstructor : MonoBehaviour
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060026DB RID: 9947 RVA: 0x000F5998 File Offset: 0x000F3B98
		// (remove) Token: 0x060026DC RID: 9948 RVA: 0x000F59D0 File Offset: 0x000F3BD0
		private event EventHandler<SceneConstructionProgress> progressEvent;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060026DD RID: 9949 RVA: 0x000F5A08 File Offset: 0x000F3C08
		// (remove) Token: 0x060026DE RID: 9950 RVA: 0x000F5A40 File Offset: 0x000F3C40
		private event EventHandler completedEvent;

		// Token: 0x060026DF RID: 9951 RVA: 0x000F5A78 File Offset: 0x000F3C78
		private void Awake()
		{
			SceneConstructorSettings sceneConstructorSettings = UnityEngine.Object.FindObjectOfType<SceneConstructorSettings>();
			if (sceneConstructorSettings)
			{
				if (sceneConstructorSettings.sceneConstructor != null)
				{
					this.sceneConstructor = sceneConstructorSettings.sceneConstructor;
				}
				else
				{
					Debug.LogError("No ISceneConstructor set!");
				}
				if (!string.IsNullOrEmpty(sceneConstructorSettings.sceneToActivate))
				{
					this.sceneToActivate = SceneManager.GetSceneByName(sceneConstructorSettings.sceneToActivate);
				}
				UnityEngine.Object.Destroy(sceneConstructorSettings.gameObject);
			}
			else
			{
				Debug.LogError("No SceneConstructorSettings found!");
			}
			base.enabled = false;
			if (this.sceneConstructor != null)
			{
				Debug.Log("Scene construction starting");
				this.sceneConstructor.StartSceneConstruction();
			}
			base.StartCoroutine(this.SceneConstructionRoutine());
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0001AE16 File Offset: 0x00019016
		private IEnumerator SceneConstructionRoutine()
		{
			yield return null;
			if (this.sceneToActivate.IsValid())
			{
				SceneManager.SetActiveScene(this.sceneToActivate);
			}
			if (this.sceneConstructor != null)
			{
				foreach (SceneConstructionProgress e in this.sceneConstructor.ConstructSceneAsync())
				{
					if (this.progressEvent != null)
					{
						this.progressEvent(this, e);
					}
					yield return null;
				}
				IEnumerator<SceneConstructionProgress> enumerator = null;
			}
			LoadingScreen loadingScreen = UnityEngine.Object.FindObjectOfType<LoadingScreen>();
			if (loadingScreen)
			{
				Scene scene = loadingScreen.gameObject.scene;
				if (scene.isLoaded)
				{
					Debug.Log("Unloading loading screen (from SceneConstructor)");
					AsyncOperation unload = SceneManager.UnloadSceneAsync(scene);
					while (unload.isDone)
					{
						yield return null;
					}
					unload = null;
				}
			}
			if (this.sceneConstructor != null)
			{
				Debug.Log("Scene construction ending");
				this.sceneConstructor.EndSceneConstruction();
			}
			base.enabled = true;
			yield break;
			yield break;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000F5B1C File Offset: 0x000F3D1C
		private void Update()
		{
			if (!this || !base.gameObject || this.isConstructed)
			{
				return;
			}
			object obj = this.mutex;
			lock (obj)
			{
				Debug.Log("Scene is constructed");
				if (this.completedEvent != null)
				{
					this.completedEvent(this, EventArgs.Empty);
				}
				this.isConstructed = true;
			}
			base.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000F5BB8 File Offset: 0x000F3DB8
		public void OnProgress(Action<SceneConstructionProgress> callback)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (!this.isConstructed)
				{
					this.progressEvent += delegate(object s, SceneConstructionProgress e)
					{
						callback(e);
					};
				}
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000F5C1C File Offset: 0x000F3E1C
		public void OnSceneConstructed(Action callback)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (this.isConstructed)
				{
					callback();
				}
				else
				{
					this.completedEvent += delegate(object s, EventArgs e)
					{
						callback();
					};
				}
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000F5C8C File Offset: 0x000F3E8C
		public static InstantActionMaps.MapEntry InstantActionMapsEntry(string filePath, SceneConstructor.Mode mode)
		{
			bool flag = mode == SceneConstructor.Mode.Edit;
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
			return new InstantActionMaps.MapEntry
			{
				name = fileNameWithoutExtension + (flag ? " in MAP EDITOR" : ""),
				sceneName = filePath,
				isCustomMap = true,
				mapEditorMode = mode
			};
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000F5CDC File Offset: 0x000F3EDC
		public static void GotoLoadingScreen(string filePath, SceneConstructor.Mode mode, bool nightMode)
		{
			InstantActionMaps.MapEntry entry = SceneConstructor.InstantActionMapsEntry(filePath, mode);
			GameModeParameters gameModeParameters = new GameModeParameters
			{
				actorCount = 50,
				balance = 0.5f,
				bloodExplosions = false,
				configFlags = false,
				gameLength = 1,
				loadedLevelEntry = 10,
				nightMode = nightMode,
				noTurrets = false,
				noVehicles = false,
				playerHasAllWeapons = true,
				playerTeam = 0,
				respawnTime = 5,
				reverseMode = false
			};
			MapEditorAssistant instance = MapEditorAssistant.instance;
			if (instance)
			{
				gameModeParameters.gameModePrefab = instance.defaultGameMode.gameObject;
			}
			GameManager.StartLevel(entry, gameModeParameters);
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0001AE25 File Offset: 0x00019025
		public static IEnumerable<SceneConstructionProgress> LoadingScreenWork(InstantActionMaps.MapEntry entry)
		{
			string text = entry.sceneName;
			bool flag = entry.mapEditorMode == SceneConstructor.Mode.Edit;
			bool isTestingMap = entry.mapEditorMode == SceneConstructor.Mode.PlayTest;
			if (!Path.IsPathRooted(text))
			{
				text = MapDescriptor.GetFilePathToLoad(Path.GetFileNameWithoutExtension(text));
			}
			MapDescriptor.ParseResult parseResult = MapDescriptor.LoadFile(text);
			MapEditor.isTestingMap = isTestingMap;
			MapEditor.descriptorFilePath = text;
			ISceneConstructor constructor = flag ? parseResult.editorConstructor : parseResult.gameConstructor;
			AsyncOperation loadScene = SceneConstructor.LoadSceneAsync(flag, constructor, LoadSceneMode.Additive);
			while (!loadScene.isDone)
			{
				float num = Mathf.Clamp01(loadScene.progress);
				yield return new SceneConstructionProgress("Loading scene", 0.1f * num);
			}
			Debug.Log("Scene is loaded");
			bool constructed = false;
			SceneConstructionProgress progress = new SceneConstructionProgress("Loading scene", 0f);
			SceneConstructor sceneConstructor = UnityEngine.Object.FindObjectOfType<SceneConstructor>();
			sceneConstructor.OnProgress(delegate(SceneConstructionProgress p)
			{
				progress = p;
			});
			sceneConstructor.OnSceneConstructed(delegate
			{
				constructed = true;
			});
			while (!constructed)
			{
				yield return new SceneConstructionProgress(progress.status, 0.1f + 0.9f * progress.progress);
			}
			yield break;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000F5D7C File Offset: 0x000F3F7C
		private static AsyncOperation LoadSceneAsync(bool loadEditor, ISceneConstructor constructor, LoadSceneMode mode = LoadSceneMode.Additive)
		{
			string text = loadEditor ? "MapEditor" : "MapEditor-GameScene";
			Debug.Log("Loading scene: " + text);
			if (constructor != null)
			{
				SceneConstructorSettings sceneConstructorSettings = new GameObject("SceneConstructorSettings").AddComponent<SceneConstructorSettings>();
				sceneConstructorSettings.sceneConstructor = constructor;
				sceneConstructorSettings.sceneToActivate = text;
				sceneConstructorSettings.PersistToNextScene();
			}
			return SceneManager.LoadSceneAsync(text, mode);
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0001AE35 File Offset: 0x00019035
		public static AsyncOperation ReplaceSceneAsync(bool loadEditor, ISceneConstructor constructor)
		{
			return SceneConstructor.LoadSceneAsync(loadEditor, constructor, LoadSceneMode.Single);
		}

		// Token: 0x04002519 RID: 9497
		private const string EDITOR_SCENE_NAME = "MapEditor";

		// Token: 0x0400251A RID: 9498
		private const string GAME_SCENE_NAME = "MapEditor-GameScene";

		// Token: 0x0400251B RID: 9499
		private ISceneConstructor sceneConstructor;

		// Token: 0x0400251C RID: 9500
		private Scene sceneToActivate;

		// Token: 0x0400251D RID: 9501
		private object mutex = new object();

		// Token: 0x0400251E RID: 9502
		private bool isConstructed;

		// Token: 0x020005EA RID: 1514
		public enum Mode
		{
			// Token: 0x04002522 RID: 9506
			Edit,
			// Token: 0x04002523 RID: 9507
			Play,
			// Token: 0x04002524 RID: 9508
			PlayTest
		}
	}
}
