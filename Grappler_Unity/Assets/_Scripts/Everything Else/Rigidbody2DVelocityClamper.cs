using UnityEngine;
using System.Collections;

public enum ClampType {
	XY,
	XYUp,
	X,
	Y
}

public class Rigidbody2DVelocityClamper : MonoBehaviour {
	[SerializeField] private Cooldown cooldown;
	[SerializeField] private Rigidbody2D[] rigidbodies;
	[SerializeField] private float maxSpeed = 10;
	[SerializeField] private ClampType clampType;

	private float sqrMaxSpeed;

	private void Awake() {
		sqrMaxSpeed = Mathf.Pow(maxSpeed, 2);
	}

	private void FixedUpdate () {
		ClampRigidbodies();
	}

	private void ClampRigidbodies() {
		float percent = cooldown.GetCooldownPercentLeft();

		foreach (Rigidbody2D rigid in rigidbodies) {
			if (rigid.velocity.sqrMagnitude > sqrMaxSpeed) {
				Vector2 clampedVelocity = rigid.velocity;
				clampedVelocity = rigid.velocity.normalized * maxSpeed;
				if (clampType == ClampType.X) clampedVelocity.y = rigid.velocity.y;
				else if (clampType == ClampType.Y) clampedVelocity.x = rigid.velocity.x;
				else if (clampType == ClampType.XYUp) {
					if (rigid.velocity.y < 0) clampedVelocity.y = rigid.velocity.y;
				}
				rigid.velocity = Vector2.Lerp(rigid.velocity, clampedVelocity, percent);
			}
		}
	}
}
