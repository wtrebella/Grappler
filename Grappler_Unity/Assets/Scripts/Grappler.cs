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

	private void Start() {
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (CurrentStateIs(GrapplerStates.Falling)) ConnectGrappleIfAble(swipeDirection);
		else if (CurrentStateIs(GrapplerStates.Grappling)) {
			ReleaseGrapple();
			ApplySwipeForce(swipeDirection, swipeMagnitude);
			currentState = GrapplerStates.Falling;
		}
	}

	private void Falling_UpdateState() {

	}
	
	private void Grappling_UpdateState() {

	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private void ConnectGrappleIfAble(Vector2 direction) {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable, direction)) {
			if (grappleRope.IsRetracted()) {
				ConnectGrapple(anchorable);
				currentState = GrapplerStates.Grappling;
			}
		}
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleRope.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ApplySwipeForce(Vector2 swipeDirection, float swipeMagnitude) {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.AddForce(swipeDirection * swipeMagnitude, ForceMode2D.Impulse);
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
	}

	private bool FindAnchorable(out Anchorable anchorable, Vector2 direction) {
		return anchorableFinder.FindAnchorable(out anchorable, direction);
	}

	private void EnableRotation() {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		r.constraints = RigidbodyConstraints2D.None;
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}
}
