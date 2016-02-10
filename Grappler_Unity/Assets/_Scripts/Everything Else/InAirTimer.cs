using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Timer))]
public class InAirTimer : MonoBehaviour {
	[SerializeField] private LayerMask layerMask;

	private Timer timer;

	private void Awake() {
		timer = GetComponent<Timer>();
		timer.StartTimer();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		CheckForResetTimerLayer(collision.collider);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		CheckForResetTimerLayer(collider);
	}

	private void CheckForResetTimerLayer(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, layerMask)) timer.ResetTimer();
	}
}
