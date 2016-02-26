using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameLoader : AutoSingleton<GameLoader>
{
	static bool gameLoaded = false;

	void Awake() {
		if (GameManager.DoesExist() && !gameLoaded) {
			gameLoaded = true;
			DestroyImmediate(GameManager.instance.gameObject);
			GameManager.LoadSetupSceneThenGameScene();
		}
	}

	void Start() {
		if (!GameManager.DoesExist()) {
			gameLoaded = true;
			GameManager.LoadSetupSceneThenGameScene();
		}
	}
}