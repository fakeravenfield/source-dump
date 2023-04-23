using System;
using Lua;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class GameModeTester : MonoBehaviour
{
	// Token: 0x060000A8 RID: 168 RVA: 0x00002AF3 File Offset: 0x00000CF3
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			this.InstantiateManagers();
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0003F910 File Offset: 0x0003DB10
	private void InstantiateManagers()
	{
		GameObject gameObject = new GameObject(base.GetType().Name);
		gameObject.SetActive(false);
		RsGameMode rsGameMode = UnityEngine.Object.FindObjectOfType<RsGameMode>();
		rsGameMode.transform.SetParent(gameObject.transform, true);
		rsGameMode.gameObject.AddComponent<ScriptedGameMode>();
		GameManager component = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("_Managers") as GameObject, gameObject.transform).GetComponent<GameManager>();
		component.gameModeParameters = new GameModeParameters
		{
			gameModePrefab = rsGameMode.gameObject,
			balance = 0.5f,
			actorCount = 10
		};
		component.editorTestGameModePrefab = rsGameMode.gameObject;
		component.hasNonDefaultGameModeParameters = true;
		component.transform.SetParent(null, true);
	}
}
