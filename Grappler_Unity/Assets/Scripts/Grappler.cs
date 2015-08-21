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
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrappleRope>();
		currentState = GrapplerStates.Falling;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		currentState = GrapplerStates.Dead;
	}

	private void Falling_UpdateState() {

	}
	
	private void Grappling_UpdateState() {
		if (Input.GetMouseButtonDown(0)) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private void ConnectGrappleIfAble(float angle) {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable, angle)) ConnectGrapple(anchorable);
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleRope.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
	}

	private bool FindAnchorable(out Anchorable anchorable, float angle) {
		return anchorableFinder.FindAnchorable(out anchorable, angle);
	}

	private void HandleSwipe(Vector2 swipeDirection) {
		if (!grappleRope.IsRetracting()) {
			float angle = WhitTools.DirectionToAngle(swipeDirection);
			ConnectGrappleIfAble(angle);
		}
	}
}
