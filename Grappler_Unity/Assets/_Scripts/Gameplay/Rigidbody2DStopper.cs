using UnityEngine;
using System.Collections;
using System;

public class Rigidbody2DStopper : MonoBehaviour {
	public Action SignalSlowed;
	public Action SignalStopped;

	[SerializeField] private GroundDetector groundDetector;
	[SerializeField] private float stopSpeed = 5.0f;
	[SerializeField] private float stopRate = 1.0f;
	[SerializeField] private Rigidbody2D rigid;

	public void StartStoppingProcess() {
		StopRigidbody();
	}

	public void Cancel() {
		StopStoppingCoroutine();
	}

	private IEnumerator StopRigidbodyCoroutine() {
		float speed = rigid.velocity.magnitude;

		while (ShouldContinueRunningStopCoroutine(speed)) {
			if (groundDetector.IsCloseToGround()) {
				Vector2 currentVelocity = rigid.velocity;
				Vector2 velocity = currentVelocity * stopRate;
				speed = velocity.magnitude;
				rigid.velocity = velocity;
			}

			yield return new WaitForFixedUpdate();
		}

		if (SignalStopped != null) SignalStopped();
	}

	private bool RigidbodyHasBeenStopped(float speed) {
		return groundDetector.IsCloseToGround() && speed < stopSpeed;
	}

	private bool ShouldContinueRunningStopCoroutine(float speed) {
		return !RigidbodyHasBeenStopped(speed);
	}

	private void StopRigidbody() {
		StartCoroutine("StopRigidbodyCoroutine");
	}

	private void StopStoppingCoroutine() {
		StopCoroutine("StopRigidbodyCoroutine");
	}
}