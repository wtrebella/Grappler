using UnityEngine;
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
		SwipeDetector.instance.SignalSwipe += HandleSwipe;
		SwipeDetector.instance.SignalTap += HandleTap;

		StartCoroutine(Blah ());
	}

	private IEnumerator Blah() {
		yield return new WaitForSeconds(1);

		currentState = PlayerStates.Climbing;
	}

	private void HandleGrapplerConnected() {
		currentState = PlayerStates.Grappling;
	}

	private void HandleGrapplerDisconnected() {
		currentState = PlayerStates.Falling;
	}

	private void HandleSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		if (CurrentStateIs(PlayerStates.Falling)) {
			grapplerController.HandleFallingSwipe(swipeDirection, swipeMagnitude);
			climberController.HandleFallingSwipe(swipeDirection, swipeMagnitude);
		}
		else if (CurrentStateIs(PlayerStates.Climbing)) {
			grapplerController.HandleClimbingSwipe(swipeDirection, swipeMagnitude);
			climberController.HandleClimbingSwipe(swipeDirection, swipeMagnitude);
		}
		else if (CurrentStateIs(PlayerStates.Grappling)) {
			grapplerController.HandleGrapplingSwipe(swipeDirection, swipeMagnitude);
			climberController.HandleClimbingSwipe(swipeDirection, swipeMagnitude);
		}
	}

	private void HandleTap() {
		grapplerController.HandleTap();
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}

	private void Falling_EnterState() {
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
		kinematicSwitcher.SetNonKinematic();
		playerAnimator.PlayGrapplingAnimations();
		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();
	}
}
