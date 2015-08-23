using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	[SerializeField] private float speed = 1;

	private void FixedUpdate() {
		Vector3 position = transform.position;
		position.x += speed * Time.deltaTime;
		transform.position = position;
	}
}
