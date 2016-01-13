﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GrapplerController))]
[RequireComponent(typeof(ClimberController))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : StateMachine {
	public Action SignalEnteredClimbingState;
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;

	private enum PlayerStates {Falling, Climbing, Grappling}

	private PlayerAnimator playerAnimator;
	private TriggerSwitcher triggerSwitcher;
	private KinematicSwitcher kinematicSwitcher;
	private GrapplerController grapplerController;
	private ClimberController climberController;

	public bool IsFalling() {
		return CurrentStateIs(PlayerStates.Falling);
	}

	public bool IsClimbing() {
		return CurrentStateIs(PlayerStates.Climbing);
	}

	public bool IsGrappling() {
		return CurrentStateIs(PlayerStates.Grappling);
	}

	private void Awake() {
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		triggerSwitcher = GetComponent<TriggerSwitcher>();

		grapplerController = GetComponent<GrapplerController>();
		grapplerController.SignalConnected += HandleGrapplerConnected;
		grapplerController.SignalDisconnected += HandleGrapplerDisconnected;

		climberController = GetComponent<ClimberController>();
	}

	private void Start() {
		SwipeDetector.instance.SignalTap += HandleTap;
		SwipeDetector.instance.SignalLeftSwipe += HandleLeftSwipe;
		SwipeDetector.instance.SignalRightSwipe += HandleRightSwipe;
		SwipeDetector.instance.SignalUpSwipe += HandleUpSwipe;
		SwipeDetector.instance.SignalDownSwipe += HandleDownSwipe;

		currentState = PlayerStates.Climbing;
	}

	private void HandleGrapplerConnected() {
		currentState = PlayerStates.Grappling;
	}

	private void HandleGrapplerDisconnected() {
		currentState = PlayerStates.Falling;
	}

	private void HandleLeftSwipe() {
		grapplerController.HandleLeftSwipe(this);
		climberController.HandleLeftSwipe(this);
	}

	private void HandleRightSwipe() {
		grapplerController.HandleRightSwipe(this);
		climberController.HandleRightSwipe(this);
	}

	private void HandleUpSwipe() {
		grapplerController.HandleUpSwipe(this);
		climberController.HandleUpSwipe(this);
	}

	private void HandleDownSwipe() {
		grapplerController.HandleDownSwipe(this);
		climberController.HandleDownSwipe(this);
	}

	private void HandleTap() {
		grapplerController.HandleTap();
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}

	private void Falling_EnterState() {
		climberController.StopClimbing();
		kinematicSwitcher.SetNonKinematic();
		playerAnimator.PlayFallingAnimations();
		if (SignalEnteredFallingState != null) SignalEnteredFallingState();
	}

	private void Climbing_EnterState() {
		kinematicSwitcher.SetKinematic();
		playerAnimator.PlayClimbingAnimations();
		climberController.StartClimbing();
		if (SignalEnteredClimbingState != null) SignalEnteredClimbingState();
	}

	private void Grappling_EnterState() {
		climberController.StopClimbing();
		kinematicSwitcher.SetNonKinematic();
		playerAnimator.PlayGrapplingAnimations();
		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();
	}
}
