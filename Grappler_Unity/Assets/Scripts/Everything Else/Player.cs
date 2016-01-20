using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(DeadStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : StateMachine {
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredDeadState;

	public enum PlayerStates {Falling, Grappling, Dead}

	public Forcer forcer;

	[SerializeField] private Transform body;
	
	[HideInInspector, NonSerialized] public PlayerAnimator playerAnimator;
	[HideInInspector, NonSerialized] public DeadStateController deadController;
	[HideInInspector, NonSerialized] public GrapplingStateController grapplingController;
	[HideInInspector, NonSerialized] public FallingStateController fallingController;

	public void SetState(PlayerStates state) {
		currentState = state;
	}

	public bool IsFalling() {
		return CurrentStateIs(PlayerStates.Falling);
	}

	public bool IsGrappling() {
		return CurrentStateIs(PlayerStates.Grappling);
	}

	public bool IsDead() {
		return CurrentStateIs(PlayerStates.Dead);
	}

	public Vector3 GetBodyPosition() {
		return body.position;
	}

	private void Awake() {
		playerAnimator = GetComponent<PlayerAnimator>();
		fallingController = GetComponent<FallingStateController>();
		grapplingController = GetComponent<GrapplingStateController>();
		deadController = GetComponent<DeadStateController>();
	}

	private void Start() {
		SetState(PlayerStates.Falling);
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}

	private void KillIfBelowScreen() {
		float minY = GameScreen.instance.lowerLeft.y - 5;
		if (body.position.y < minY) currentState = PlayerStates.Dead;
	}

	protected override void PreUpdateState() {

	}

	protected override void PostUpdateState() {
		KillIfBelowScreen();
	}





	// falling

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

	private void Falling_TouchUp() {
		fallingController.HandleTouchUp();
	}

	private void Falling_TouchDown() {
		fallingController.HandleTouchDown();
	}

	private void Falling_EnterState() {
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






	// dead
	
	private void Dead_LeftSwipe() {
		deadController.HandleLeftSwipe();
	}
	
	private void Dead_RightSwipe() {
		deadController.HandleRightSwipe();
	}
	
	private void Dead_UpSwipe() {
		deadController.HandleUpSwipe();
	}
	
	private void Dead_DownSwipe() {
		deadController.HandleDownSwipe();
	}
	
	private void Dead_Tap() {
		deadController.HandleTap();
	}
	
	private void Dead_TouchUp() {
		deadController.HandleTouchUp();
	}
	
	private void Dead_TouchDown() {
		deadController.HandleTouchDown();
	}
	
	private void Dead_EnterState() {
		deadController.EnterState();
		
		if (SignalEnteredDeadState != null) SignalEnteredDeadState();
	}
	
	private void Dead_ExitState() {
		deadController.ExitState();
	}
	
	private void Dead_UpdateState() {
		deadController.UpdateState();
	}
	
	private void Dead_FixedUpdateState() {
		deadController.FixedUpdateState();
	}









	// grappling

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

	private void Grappling_TouchUp() {
		grapplingController.HandleTouchUp();
	}
	
	private void Grappling_TouchDown() {
		grapplingController.HandleTouchDown();
	}

	private void Grappling_EnterState() {
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
}
