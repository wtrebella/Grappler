using UnityEngine;
using System.Collections;

public class Eagle : StateMachine {
	[SerializeField] private float flySpeed = 1;

	private enum EagleStates {Setup, Flying, Falling}
	private Rigidbody2D rigid;

	public void StartFlying() {
		currentState = EagleStates.Flying;
	}

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		currentState = EagleStates.Setup;
		rigid.isKinematic = true;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (((EagleStates)currentState) == EagleStates.Falling) return;

		currentState = EagleStates.Falling;
	}

	private void Flying_EnterState() {
		rigid.isKinematic = false;
	}
	
	private void Flying_FixedUpdateState() {
		ApplyFlyForce();
	}

	private void ApplyFlyForce() {
		rigid.AddForce(new Vector2(-flySpeed, -Physics2D.gravity.y * rigid.mass));
	}
}
