using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameLoader : AutoSingleton<GameLoader>
{
	static bool gameLoaded = false;

	void Awake() {
		if (GameStateManager.DoesExist() && !gameLoaded) {
			gameLoaded = true;
			DestroyImmediate(GameStateManager.instance.gameObject);
			GameStateManager.LoadSetupSceneThenGameScene();
		}
	}

	void Start() {
		if (!GameStateManager.DoesExist()) {
			gameLoaded = true;
			GameStateManager.LoadSetupSceneThenGameScene();
		}
	}
}