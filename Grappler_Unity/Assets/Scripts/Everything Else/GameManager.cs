using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public void RestartGame() {
		Application.LoadLevel(Application.loadedLevel);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}
}
