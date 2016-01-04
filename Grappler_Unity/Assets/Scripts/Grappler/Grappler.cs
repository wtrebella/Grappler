using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrapplerRope))]
[RequireComponent(typeof(GrapplerEnemyInteraction))]
[RequireComponent(typeof(GrapplerLavaInteraction))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	public Action SignalGrapplerDied;

	[SerializeField] private GrapplerArm leftArm;
	[SerializeField] private GrapplerArm rightArm;

	private GrapplerEnemyInteraction enemyInteraction;
	private GrapplerLavaInteraction lavaInteraction;
	private GrapplerRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		lavaInteraction = GetComponent<GrapplerLavaInteraction>();
		enemyInteraction = GetComponent<GrapplerEnemyInteraction>();
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrapplerRope>();
		currentState = GrapplerStates.Falling;
	}

	private void Start() {
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		SwipeDetector.instance.SignalTap += HandleTap;
		enemyInteraction.SignalHitEnemy += HandleHitEnemy;
		lavaInteraction.SignalEnteredLava += HandleEnteredLava;
	}

	private void HandleHitEnemy(MountainEnemy enemy) {
		ReleaseGrappleIfConnected();
	}

	private void HandleEnteredLava() {
		ReleaseGrappleIfConnected();
		currentState = GrapplerStates.Dead;
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
		GrabRopeWithArms();
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
		ReleaseRopeWithArms();
	}

	private void GrabRopeWithArms() {
		leftArm.Grab();
		rightArm.Grab();
	}

	private void ReleaseRopeWithArms() {
		leftArm.Release();
		rightArm.Release();
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
