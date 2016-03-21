using UnityEngine;
using System.Collections;

public class Rigidbody2DForcer : Rigidbody2DAffecter {
	public void AddForce(Vector2 force, ForceMode2D forceMode) {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.AddForce(force, forceMode);
	}

	public void SetVelocity(Vector2 velocity) {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.velocity = velocity;
	}

	public void AddVelocity(Vector2 velocity) {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.velocity += velocity;
	}

	public void AddTorque(float torque) {
		foreach (Rigidbody2D rigid in rigidbodies) rigid.AddTorque(torque);
	}

	public Vector2 GetAverageDirection() {
		Vector2 direction = Vector2.zero;
		foreach (Rigidbody2D rigid in rigidbodies) direction += rigid.velocity;
		direction.Normalize();
		return direction;
	}
}
