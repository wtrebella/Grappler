using UnityEngine;
using System.Collections;

public class CircleRaycasterTest : MonoBehaviour {
	[SerializeField] private KeyCode key = KeyCode.T;
	[SerializeField] private ArcRaycaster raycaster;

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKeyDown(key)) {
			Collider2D foundCollider;
			if (raycaster.FindCollider(out foundCollider)) Debug.Log(Time.time + ", " + foundCollider.name);
		}
	}
}
