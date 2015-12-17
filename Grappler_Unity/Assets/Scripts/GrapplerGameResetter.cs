using UnityEngine;
using System.Collections;

public class GrapplerGameResetter : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<Anchorable>() == null) Application.LoadLevel(Application.loadedLevel);
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(Application.loadedLevel);
	}
}
