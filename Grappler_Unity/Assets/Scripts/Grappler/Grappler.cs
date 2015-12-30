using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleRope))]
[RequireComponent(typeof(GrapplerSwipeForcer))]
[RequireComponent(typeof(GrapplerEnemyInteraction))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	private GrapplerEnemyInteraction enemyInteraction;
	private GrappleRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private GrapplerSwipeForcer swipeForcer;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		enemyInteraction = GetComponent<GrapplerEnemyInteraction>();
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrappleRope>();
		swipeForcer = GetComponent<GrapplerSwipeForcer>();
		currentState = GrapplerStates.Falling;
	}

	private void Start() {
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		SwipeDetector.instance.SignalTap += HandleTap;
		enemyInteraction.SignalHitEnemy += HandleHitEnemy;
	}

	private void HandleHitEnemy(MountainEnemy enemy) {
		currentState = GrapplerStates.Dead;
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (CurrentStateIs(GrapplerStates.Falling)) ConnectGrappleIfAble(swipeDirection);
		else if (CurrentStateIs(GrapplerStates.Grappling)) {
			ReleaseGrapple();
			swipeForcer.ApplySwipeForce(swipeDirection, swipeMagnitude);
			currentState = GrapplerStates.Falling;
		}
	}

	private void HandleTap() {
		if (CurrentStateIs(GrapplerStates.Grappling)) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private void ConnectGrappleIfAble(Vector2 direction) {
		if (!grappleRope.IsRetracted()) return;

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

	private bool FindAnchorable(out Anchorable anchorable, Vector2 direction) {
		return anchorableFinder.FindAnchorable(out anchorable, direction);
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}
}
