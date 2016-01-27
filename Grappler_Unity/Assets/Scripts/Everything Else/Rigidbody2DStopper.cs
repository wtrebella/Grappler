using UnityEngine;
using System.Collections;
using System;

public class Rigidbody2DStopper : MonoBehaviour {
	public Action SignalSlowed;
	public Action SignalStopped;

	[SerializeField] private float slowSpeed = 10.0f;
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
		float speed = rigid.velocity.magnitude;
		float previousSpeed = speed;
		while (speed > stopSpeed && !exitCoroutine) {
			Vector2 currentVelocity = rigid.velocity;
			Vector2 velocity = currentVelocity * stopRate;
			rigid.velocity = velocity;
			if (speed < previousSpeed && speed < slowSpeed) {
				if (SignalSlowed != null) SignalSlowed();
			}
			yield return new WaitForFixedUpdate();
		}

		if (SignalStopped != null) SignalStopped();
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