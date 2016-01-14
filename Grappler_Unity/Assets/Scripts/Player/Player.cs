using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GrapplingController))]
[RequireComponent(typeof(ClimbingController))]
[RequireComponent(typeof(FallingController))]
[RequireComponent(typeof(ForcerController))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : StateMachine {
	public Action SignalEnteredClimbingState;
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;

	public enum PlayerStates {Falling, Climbing, Grappling}

	[HideInInspector, NonSerialized] public ForcerController forcerController;
	[HideInInspector, NonSerialized] public PlayerAnimator playerAnimator;
	[HideInInspector, NonSerialized] public TriggerSwitcher triggerSwitcher;
	[HideInInspector, NonSerialized] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector, NonSerialized] public GrapplingController grapplingController;
	[HideInInspector, NonSerialized] public ClimbingController climbingController;
	[HideInInspector, NonSerialized] public FallingController fallingController;

	public void SetState(PlayerStates state) {
		currentState = state;
	}

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
		forcerController = GetComponent<ForcerController>();
		fallingController = GetComponent<FallingController>();
		grapplingController = GetComponent<GrapplingController>();
		climbingController = GetComponent<ClimbingController>();
	}

	private void Start() {
		SetState(PlayerStates.Climbing);
	}





	private void Falling_LeftSwipe() {
		fallingController.HandleLeftSwipe();
	}

	private void Falling_RightSwipe() {
		fallingController.HandleRightSwipe();
	}

	private void Falling_UpSwipe() {
		fallingController.HandleUpSwipe();
	}
	
	private void Falling_DownSwipe() {
		fallingController.HandleDownSwipe();
	}

	private void Falling_Tap() {
		fallingController.HandleTap();
	}

	private void Falling_EnterState() {
		kinematicSwitcher.SetNonKinematic();
		playerAnimator.PlayFallingAnimations();

		fallingController.EnterState();

		if (SignalEnteredFallingState != null) SignalEnteredFallingState();
	}
	
	private void Falling_ExitState() {
		fallingController.ExitState();
	}
	
	private void Falling_UpdateState() {
		fallingController.UpdateState();
	}
	
	private void Falling_FixedUpdateState() {
		fallingController.FixedUpdateState();
	}





	private void Climbing_LeftSwipe() {
		climbingController.HandleLeftSwipe();
	}
	
	private void Climbing_RightSwipe() {
		climbingController.HandleRightSwipe();
	}
	
	private void Climbing_UpSwipe() {
		climbingController.HandleUpSwipe();
	}
	
	private void Climbing_DownSwipe() {
		climbingController.HandleDownSwipe();
	}
	
	private void Climbing_Tap() {
		climbingController.HandleTap();
	}

	private void Climbing_EnterState() {
		kinematicSwitcher.SetKinematic();
		playerAnimator.PlayClimbingAnimations();

		climbingController.EnterState();

		if (SignalEnteredClimbingState != null) SignalEnteredClimbingState();
	}
	
	private void Climbing_ExitState() {
		climbingController.ExitState();
	}
	
	private void Climbing_UpdateState() {
		climbingController.UpdateState();
	}
	
	private void Climbing_FixedUpdateState() {
		climbingController.FixedUpdateState();
	}





	private void Grappling_LeftSwipe() {
		grapplingController.HandleLeftSwipe();
	}
	
	private void Grappling_RightSwipe() {
		grapplingController.HandleRightSwipe();
	}
	
	private void Grappling_UpSwipe() {
		grapplingController.HandleUpSwipe();
	}
	
	private void Grappling_DownSwipe() {
		grapplingController.HandleDownSwipe();
	}
	
	private void Grappling_Tap() {
		grapplingController.HandleTap();
	}

	private void Grappling_EnterState() {
		kinematicSwitcher.SetNonKinematic();
		playerAnimator.PlayGrapplingAnimations();

		grapplingController.EnterState();

		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();
	}
	
	private void Grappling_ExitState() {
		grapplingController.ExitState();
	}
	
	private void Grappling_UpdateState() {
		grapplingController.UpdateState();
	}
	
	private void Grappling_FixedUpdateState() {
		grapplingController.FixedUpdateState();
	}
	
	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}
}
