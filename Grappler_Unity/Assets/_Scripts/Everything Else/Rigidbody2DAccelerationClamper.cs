using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rigidbody2DAccelerationClamper : MonoBehaviour {
	[SerializeField] private Cooldown cooldown;
	[SerializeField] private Rigidbody2D[] rigidbodies;
	[SerializeField] private float maxAcceleration = 2;

	private Dictionary<Rigidbody2D, Vector2> rigidbodyPreviousVelocities;

	private void Awake() {
		rigidbodyPreviousVelocities = new Dictionary<Rigidbody2D, Vector2>();
		foreach (Rigidbody2D rigid in rigidbodies) rigidbodyPreviousVelocities.Add(rigid, rigid.velocity);
	}

	private void UpdateRigidbodyPreviousSpeeds() {
		foreach (Rigidbody2D rigid in rigidbodies) rigidbodyPreviousVelocities[rigid] = rigid.velocity;
	}

	private void FixedUpdate () {
//		ClampRigidbodies();
//		UpdateRigidbodyPreviousSpeeds();
	}

	private void ClampRigidbodies() {
		float percent = cooldown.GetCooldownPercentLeft();
		if (percent <= 0) return;

		foreach (Rigidbody2D rigid in rigidbodies) {
			Vector2 newVelocity = rigid.velocity;
			Vector2 previousVelocity = GetPreviousVelocity(rigid);
			Vector2 velocityChange = newVelocity - previousVelocity;
			Vector2 velocityChangeDirection = velocityChange.normalized;
			float currentSpeedChange = velocityChange.magnitude;
			float newSpeedChange;
			if (currentSpeedChange < maxAcceleration) newSpeedChange = currentSpeedChange;
			else newSpeedChange = Mathf.Lerp(currentSpeedChange, maxAcceleration, percent);
			newVelocity = previousVelocity + velocityChangeDirection * newSpeedChange;
			rigid.velocity = newVelocity;
		}
	}

	private Vector2 GetPreviousVelocity(Rigidbody2D rigid) {
		return rigidbodyPreviousVelocities[rigid];
	}
}
