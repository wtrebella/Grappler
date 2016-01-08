using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrapplerRope))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	public Action SignalGrapplerDied;

	private GrapplerRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrapplerRope>();
		currentState = GrapplerStates.Falling;
	}

	private void Start() {
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		SwipeDetector.instance.SignalTap += HandleTap;
	}

	private void HandleHitEnemy(MountainEnemy enemy) {
		ReleaseGrappleIfConnected();
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (CurrentStateIs(GrapplerStates.Falling)) ConnectGrappleIfAble(swipeDirection);
		else ReleaseGrappleIfConnected();
	}

	private void HandleTap() {
		ReleaseGrappleIfConnected();
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
		if (SignalGrapplerDied != null) SignalGrapplerDied();
	}

	private void ConnectGrappleIfAble(Vector2 direction) {
		if (!grappleRope.IsRetracted()) return;
		Debug.Log("connect");
		Anchorable anchorable;
		if (FindAnchorable(out anchorable, direction)) {
			ConnectGrapple(anchorable);
			currentState = GrapplerStates.Grappling;
		}
		else {
			grappleRope.Misfire(direction);
		}
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleRope.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
	}

	private void ReleaseGrappleIfConnected() {
		if (CurrentStateIs(GrapplerStates.Grappling)) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private bool FindAnchorable(out Anchorable anchorable, Vector2 direction) {
		return anchorableFinder.FindAnchorable(out anchorable, direction);
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}
}
