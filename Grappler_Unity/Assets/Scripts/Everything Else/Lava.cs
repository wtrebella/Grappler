using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	[SerializeField] private Grappling grappler;

	private float speed_belowScreen = 10.0f;
	private float speed_onScreen = 1.5f;

	private void Awake() {

	}

	private void FixedUpdate() {
		UpdateMovement();
	}

	private void UpdateMovement() {
		Vector3 position = transform.position;
		position += new Vector3(0, GetSpeed() * Time.fixedDeltaTime, 0);
		transform.position = position;
	}

	private float GetSpeed() {
		if (IsOnScreen()) return speed_onScreen;
		else return speed_belowScreen;
	}

	private bool IsOnScreen() {
		return transform.position.y >= GameScreen.instance.minY;
	}

	private float GetObjectToThisDistance() {
		return (transform.position - grappler.transform.position).magnitude;
	}

	private Vector3 GetObjectToThisDirection() {
		return (transform.position - grappler.transform.position).normalized;
	}
}
