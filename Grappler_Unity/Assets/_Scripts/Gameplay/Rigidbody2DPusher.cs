using UnityEngine;
using System.Collections;

public class Rigidbody2DPusher : Rigidbody2DAffecter {
	[SerializeField] private float force;
	[SerializeField] private Vector2 direction;

	private Rigidbody2D rigid;

	private void Awake() {
		direction.Normalize();
		rigid = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		
	}
	
	private void FixedUpdate() {
		rigid.AddForce(direction * force);
	}
}
