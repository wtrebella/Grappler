using UnityEngine;
using System.Collections;

public class Rigidbody2DForcer : MonoBehaviour {
	[SerializeField] private Rigidbody2D[] rigidbodies;

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

	private void Awake() {
		
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
