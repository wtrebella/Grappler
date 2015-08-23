using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleRope))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	private GrappleRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrappleRope>();
		currentState = GrapplerStates.Falling;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (CurrentStateIs(GrapplerStates.Dead)) return;

		EnableRotation();
		Tumble();
		currentState = GrapplerStates.Dead;
	}

	private void Falling_UpdateState() {
		if (GetGrappleKey()) ConnectGrappleIfAble();
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleKey()) ReleaseGrapple();
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private bool GetGrappleKey() {
		return Input.GetKey(KeyCode.Space);
	}

	private void ConnectGrappleIfAble() {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable)) ConnectGrapple(anchorable);
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleRope.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
		currentState = GrapplerStates.Falling;
	}

	private bool FindAnchorable(out Anchorable anchorable) {
		return anchorableFinder.FindAnchorable(out anchorable);
	}

	private void EnableRotation() {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		r.constraints = RigidbodyConstraints2D.None;
	}

	private void Tumble() {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		Vector2 force = new Vector2(10, 0);
		Vector2 position = new Vector2(0, 1);
		r.AddForceAtPosition(force, position, ForceMode2D.Impulse);
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}
}
