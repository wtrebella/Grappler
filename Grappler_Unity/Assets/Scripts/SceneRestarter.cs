using UnityEngine;
using System.Collections;

public class SceneRestarter : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(Application.loadedLevelName);
	}
}
