using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Timer))]
public class AirTimeTimer : MonoBehaviour {
	public UnityEventWithFloat OnAirTimeIncreased;
	public UnityEventWithFloat OnResetTimerDueToCollision;
	public UnityEventWithFloat OnResetTimerDueToGrapple;
	public UnityEventWithFloat OnAirTimeCompletedOverThreshold;

	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float speedThreshold = 10;

	private Timer timer;
	private float previousTime = 0;
	private float currentSpeed = 0;

	public void OnGrapple() {
		ResetTimerDueToGrapple();
	}

	public void OnSpeedChanged(float speed) {
		currentSpeed = speed;
	}

	private void Awake() {
		timer = GetComponent<Timer>();
		timer.StartTimer();
	}

	private void Update() {
		if (currentSpeed < speedThreshold) return;
		float timerSeconds = timer.value;
		if (timerSeconds > previousTime) {
			previousTime = timerSeconds;

			if (OnAirTimeIncreased != null) OnAirTimeIncreased.Invoke(timerSeconds);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		CheckForResetTimerLayer(collision.collider);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		CheckForResetTimerLayer(collider);
	}

	private void CheckForResetTimerLayer(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, layerMask)) ResetTimerDueToCollision();
	}

	private void ResetTimerDueToGrapple() {
		if (OnResetTimerDueToGrapple != null) OnResetTimerDueToGrapple.Invoke(timer.value);
		if (currentSpeed > speedThreshold) {
			if (OnAirTimeCompletedOverThreshold != null) OnAirTimeCompletedOverThreshold.Invoke(timer.value);
		}
		ResetTimer();
	}

	private void ResetTimerDueToCollision() {
		if (OnResetTimerDueToCollision != null) OnResetTimerDueToCollision.Invoke(timer.value);
		ResetTimer();
	}

	private void ResetTimer() {
		currentSpeed = 0;
		previousTime = 0;
		timer.ResetTimer();
	}
}
