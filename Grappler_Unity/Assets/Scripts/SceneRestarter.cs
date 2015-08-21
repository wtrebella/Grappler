using UnityEngine;
using System.Collections;

public class SceneRestarter : MonoBehaviour {
	public void Restart() {
		Application.LoadLevel(Application.loadedLevelName);
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.R)) Restart();
	}
}
