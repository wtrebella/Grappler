using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}
}
