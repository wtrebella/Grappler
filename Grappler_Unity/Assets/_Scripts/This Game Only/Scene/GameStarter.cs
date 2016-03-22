using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {
	public void StartGame() {
		SceneManager.LoadScene("GameScene");
	}
}
