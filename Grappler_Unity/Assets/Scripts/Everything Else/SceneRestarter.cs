using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour {
	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.R)) Restart();
	}
}
