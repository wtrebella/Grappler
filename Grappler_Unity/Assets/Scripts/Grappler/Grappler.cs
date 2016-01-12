using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrapplerRope))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	public Action SignalEnteredConnectedState;
	public Action SignalEnteredDisconnectedState;

	[SerializeField] private Climber climber;
	[SerializeField] private float wallPushStrength = 5;

	private GrapplerRope grapplerRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Disconnected, Connected};

	public void ReleaseGrappleIfConnected() {
		if (CurrentStateIs(GrapplerStates.Connected)) {
			ReleaseGrapple();
			currentState = GrapplerStates.Disconnected;
		}
	}

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grapplerRope = GetComponent<GrapplerRope>();
		currentState = GrapplerStates.Disconnected;
	}

	private void Start() {
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		SwipeDetector.instance.SignalTap += HandleTap;
	}

	private void HandleHitEnemy(MountainEnemy enemy) {
		ReleaseGrappleIfConnected();
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (climber.IsFalling()) ConnectGrappleInDirectionIfAble(swipeDirection);
		else if (climber.IsClimbing()) ConnectGrappleInCircleIfAble();
		else if (climber.IsGrappling()) ReleaseGrappleIfConnected();
	}

	private void HandleTap() {
		ReleaseGrappleIfConnected();
	}

	private void ConnectGrappleInDirectionIfAble(Vector2 direction) {
		if (!AbleToConnect()) return;
		Anchorable anchorable;
		if (FindAnchorableInDirection(out anchorable, direction)) {
			ConnectGrapple(anchorable);
			currentState = GrapplerStates.Connected;
		}
		else {
			grapplerRope.Misfire(direction);
		}
	}

	private void ConnectGrappleInCircleIfAble() {
		if (!AbleToConnect()) return;
		Anchorable anchorable;
		if (FindAnchorableInCircle(out anchorable)) {
			ConnectGrapple(anchorable);
			WallPush();
			currentState = GrapplerStates.Connected;
		}
	}

	private bool AbleToConnect() {
		return grapplerRope.IsRetracted() && !grapplerRope.IsConnected();
	}

	private void Connected_EnterState() {
		if (SignalEnteredConnectedState != null) SignalEnteredConnectedState();
	}

	private void Disconnected_EnterState() {
		if (SignalEnteredDisconnectedState != null) SignalEnteredDisconnectedState();
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grapplerRope.Connect(anchorable);
		currentState = GrapplerStates.Connected;
	}

	private void ReleaseGrapple() {
		grapplerRope.Release();
	}

	private bool FindAnchorableInDirection(out Anchorable anchorable, Vector2 direction) {
		return anchorableFinder.FindAnchorableInDirection(out anchorable, direction);
	}

	private bool FindAnchorableInCircle(out Anchorable anchorable) {
		return anchorableFinder.FindAnchorableInCircle(out anchorable);
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}

	private void WallPush() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(wallPushStrength, 3), ForceMode2D.Impulse);
	}
}
