using UnityEngine;
using System.Collections;

public class CoinMagnetism : MonoBehaviour {
	[SerializeField] private float magnetismStrength = 50;

	private Transform magnetizedTarget;
	private bool isMagnetized = false;
	private Vector2 smoothVelocity;

	private void OnTriggerEnter2D(Collider2D collider) {
		if (isMagnetized) return;
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) {
			Player player = collider.gameObject.GetComponentInParent<Player>();
			if (player.isFlipping) Magnetize(collider.gameObject.transform);
		}
	}

	private void FixedUpdate() {
		if (isMagnetized) {
			Vector2 currentPosition = (Vector2)transform.parent.position;
			Vector2 targetPosition = (Vector2)magnetizedTarget.position;
			Vector2 vector = targetPosition - currentPosition;
			float distance = vector.magnitude;
			Vector2 direction = vector.normalized;
			Vector2 moveDelta = direction * magnetismStrength * Time.fixedDeltaTime;
			moveDelta = Vector2.ClampMagnitude(moveDelta, distance);
			Vector2 newPosition = currentPosition + moveDelta;
			transform.parent.position = newPosition;
		}
	}

	public void Demagnetize() {
		isMagnetized = false;
		magnetizedTarget = null;
	}

	private void Magnetize(Transform target) {
		magnetizedTarget = target;
		isMagnetized = true;
	}
}
