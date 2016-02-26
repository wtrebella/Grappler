using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	public static void LoadSetupSceneThenGameScene() {
		SceneManager.LoadScene("RootScene", LoadSceneMode.Single);
		SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
	}
}
