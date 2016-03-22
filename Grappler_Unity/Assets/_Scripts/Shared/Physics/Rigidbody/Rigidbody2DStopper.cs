using UnityEngine;
using System.Collections;
using System;

public class Rigidbody2DStopper : Rigidbody2DAffecter {
	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	public Action SignalSlowed;
	public Action SignalStopped;

	[SerializeField] private float stopThreshold = 5.0f;
	[SerializeField] private float stopRate = 0.7f;

	public void StartStoppingProcess() {
		StopRigidbody();
	}

	public void Cancel() {
		StopStoppingCoroutine();
	}

	private IEnumerator StopRigidbodyCoroutine() {
		Rigidbody2D rigid = rigidbodies[0];

		float speed = rigid.velocity.magnitude;

		while (ShouldContinueRunningStopCoroutine(speed)) {
			if (player.groundDetector.IsCloseToGround()) {
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
		return player.groundDetector.IsCloseToGround() && speed < stopThreshold;
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