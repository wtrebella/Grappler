using UnityEngine;
using System.Collections;
using System;

public class Rigidbody2DStopper : MonoBehaviour {
	public Action SignalSlowed;
	public Action SignalStopped;

	[SerializeField] private GroundDetector groundDetector;
	[SerializeField] private float stopSpeed = 5.0f;
	[SerializeField] private float stopRate = 1.0f;
	[SerializeField] private Rigidbody2D[] rigidbodies;

	private bool exitCoroutine = false;

	public void Stop() {
		StopRigidbodies();
	}

	public void Cancel() {
		exitCoroutine = true;
	}

	private void StopRigidbodies() {
		foreach (Rigidbody2D rigid in rigidbodies) StopRigidbody(rigid);
	}

	private IEnumerator StopRigidbodyCoroutine(Rigidbody2D rigid) {
		exitCoroutine = false;
		float speed = rigid.velocity.magnitude;

		while (ShouldContinueRunningStopCoroutine(rigid, speed)) {
			Vector2 currentVelocity = rigid.velocity;
			Vector2 velocity = currentVelocity * stopRate;
			speed = velocity.magnitude;
			rigid.velocity = velocity;

			yield return new WaitForFixedUpdate();
		}

		if (SignalStopped != null) SignalStopped();
	}

	private bool RigidbodyHasBeenStopped(Rigidbody2D rigid, float speed) {
		return groundDetector.IsCloseToGround() && speed < stopSpeed;
	}

	private bool ShouldContinueRunningStopCoroutine(Rigidbody2D rigid, float speed) {
		return !RigidbodyHasBeenStopped(rigid, speed) && !exitCoroutine;
	}

	private void StopRigidbody(Rigidbody2D rigid) {
		StartCoroutine(StopRigidbodyCoroutine(rigid));
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}