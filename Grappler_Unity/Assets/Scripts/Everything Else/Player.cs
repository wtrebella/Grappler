using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(DeadStateController))]
[RequireComponent(typeof(OnGroundStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(GroundCollisionHandler))]
[RequireComponent(typeof(MountainCollisionHandler))]
[RequireComponent(typeof(Trail))]
[RequireComponent(typeof(Rigidbody2DStopper))]
public class Player : StateMachine {
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredKickingState;
	public Action SignalEnteredDeadState;
	public Action SignalEnteredOnGroundState;

	public enum PlayerStates {Falling, Grappling, Kicking, Dead, OnGround}

	public Trail trail;
	public Forcer forcer;

	[SerializeField] private PlayerBodyPart body;
	[SerializeField] private PlayerBodyPart feet;

	[HideInInspector] public MountainCollisionHandler mountainCollisionHandler;
	[HideInInspector] public GroundCollisionHandler groundCollisionHandler;
	[HideInInspector] public PlayerAnimator playerAnimator;
	[HideInInspector] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector] public DeadStateController deadController;
	[HideInInspector] public KickingStateController kickingController;
	[HideInInspector] public GrapplingStateController grapplingController;
	[HideInInspector] public OnGroundStateController onGroundController;
	[HideInInspector] public FallingStateController fallingController;
	[HideInInspector] public Rigidbody2DStopper rigidbodyStopper;

	public bool IsFalling() {return CurrentStateIs(PlayerStates.Falling);}
	public bool IsGrappling() {return CurrentStateIs(PlayerStates.Grappling);}
	public bool IsKicking() {return CurrentStateIs(PlayerStates.Kicking);}
	public bool IsDead() {return CurrentStateIs(PlayerStates.Dead);}
	public bool IsOnGround() {return CurrentStateIs(PlayerStates.OnGround);}

	public void SetState(PlayerStates state) {
		currentState = state;
	}

	public void HandleCollision(Collision2D collision) {
		bool isGround = WhitTools.CompareLayers(collision.gameObject.layer, "Ground");
		if (isGround) groundCollisionHandler.HandleHitGround(collision);

		bool isMountain = WhitTools.CompareLayers(collision.gameObject.layer, "Mountain");
		if (isMountain) mountainCollisionHandler.HandleHitMountain(collision);
	}

	private void Awake() {
		mountainCollisionHandler = GetComponent<MountainCollisionHandler>();
		groundCollisionHandler = GetComponent<GroundCollisionHandler>();
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		kickingController = GetComponent<KickingStateController>();
		fallingController = GetComponent<FallingStateController>();
		grapplingController = GetComponent<GrapplingStateController>();
		onGroundController = GetComponent<OnGroundStateController>();
		deadController = GetComponent<DeadStateController>();
		rigidbodyStopper = GetComponent<Rigidbody2DStopper>();
	}

	private void Start() {
		SetState(PlayerStates.Falling);
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}

	private float GetTimeInCurrentState() {
		PlayerStateController currentController = GetControllerForCurrentState();
		return currentController.timeInState;
	}

	private PlayerStateController GetControllerForCurrentState() {
		if (CurrentStateIs(PlayerStates.Dead)) return deadController;
		else if (CurrentStateIs(PlayerStates.Falling)) return fallingController;
		else if (CurrentStateIs(PlayerStates.Grappling)) return grapplingController;
		else if (CurrentStateIs(PlayerStates.Kicking)) return kickingController;
		else if (CurrentStateIs(PlayerStates.OnGround)) return onGroundController;
		Debug.LogError("no state controller implemented in this method for " + (PlayerStates)currentState);
		return null;
	}

	private void KillIfBelowScreen() {
		if (body.IsBelowScreen()) SetState(PlayerStates.Dead);
	}

	protected override void PreUpdateState() {

	}

	protected override void PostUpdateState() {
		KillIfBelowScreen();
	}





	// falling

	private void Falling_LeftSwipe() {fallingController.HandleLeftSwipe();}
	private void Falling_RightSwipe() {fallingController.HandleRightSwipe();}
	private void Falling_UpSwipe() {fallingController.HandleUpSwipe();}
	private void Falling_DownSwipe() {fallingController.HandleDownSwipe();}
	private void Falling_Tap() {fallingController.HandleTap();}
	private void Falling_TouchUp() {fallingController.HandleTouchUp();}
	private void Falling_TouchDown() {fallingController.HandleTouchDown();}
	private void Falling_ExitState() {fallingController.ExitState();}
	private void Falling_UpdateState() {fallingController.UpdateState();}
	private void Falling_FixedUpdateState() {fallingController.FixedUpdateState();}
	private void Falling_LeftTouchDown() {fallingController.HandleLeftTouchDown();}
	private void Falling_LeftTouchUp() {fallingController.HandleLeftTouchUp();}
	private void Falling_RightTouchDown() {fallingController.HandleRightTouchDown();}
	private void Falling_RightTouchUp() {fallingController.HandleRightTouchUp();}
	private void Falling_EnterState() {
		fallingController.EnterState();
		if (SignalEnteredFallingState != null) SignalEnteredFallingState();
	}


	// on ground
	
	private void OnGround_LeftSwipe() {onGroundController.HandleLeftSwipe();}
	private void OnGround_RightSwipe() {onGroundController.HandleRightSwipe();}
	private void OnGround_UpSwipe() {onGroundController.HandleUpSwipe();}
	private void OnGround_DownSwipe() {onGroundController.HandleDownSwipe();}
	private void OnGround_Tap() {onGroundController.HandleTap();}
	private void OnGround_TouchUp() {onGroundController.HandleTouchUp();}
	private void OnGround_TouchDown() {onGroundController.HandleTouchDown();}
	private void OnGround_ExitState() {onGroundController.ExitState();}
	private void OnGround_UpdateState() {onGroundController.UpdateState();}
	private void OnGround_FixedUpdateState() {onGroundController.FixedUpdateState();}
	private void OnGround_LeftTouchDown() {onGroundController.HandleLeftTouchDown();}
	private void OnGround_LeftTouchUp() {onGroundController.HandleLeftTouchUp();}
	private void OnGround_RightTouchDown() {onGroundController.HandleRightTouchDown();}
	private void OnGround_RightTouchUp() {onGroundController.HandleRightTouchUp();}
	private void OnGround_EnterState() {
		onGroundController.EnterState();
		if (SignalEnteredOnGroundState != null) SignalEnteredOnGroundState();
	}


	// dead
	
	private void Dead_LeftSwipe() {deadController.HandleLeftSwipe();}
	private void Dead_RightSwipe() {deadController.HandleRightSwipe();}
	private void Dead_UpSwipe() {deadController.HandleUpSwipe();}
	private void Dead_DownSwipe() {deadController.HandleDownSwipe();}
	private void Dead_Tap() {deadController.HandleTap();}
	private void Dead_TouchUp() {deadController.HandleTouchUp();}
	private void Dead_TouchDown() {deadController.HandleTouchDown();}	
	private void Dead_ExitState() {deadController.ExitState();}	
	private void Dead_UpdateState() {deadController.UpdateState();}	
	private void Dead_FixedUpdateState() {deadController.FixedUpdateState();}
	private void Dead_LeftTouchDown() {deadController.HandleLeftTouchDown();}	
	private void Dead_LeftTouchUp() {deadController.HandleLeftTouchUp();}	
	private void Dead_RightTouchDown() {deadController.HandleRightTouchDown();}	
	private void Dead_RightTouchUp() {deadController.HandleRightTouchUp();}
	private void Dead_EnterState() {
		deadController.EnterState();
		if (SignalEnteredDeadState != null) SignalEnteredDeadState();
	}


	// kicking
	
	private void Kicking_LeftSwipe() {kickingController.HandleLeftSwipe();}
	private void Kicking_RightSwipe() {kickingController.HandleRightSwipe();}	
	private void Kicking_UpSwipe() {kickingController.HandleUpSwipe();}	
	private void Kicking_DownSwipe() {kickingController.HandleDownSwipe();}	
	private void Kicking_Tap() {kickingController.HandleTap();}	
	private void Kicking_TouchUp() {kickingController.HandleTouchUp();}	
	private void Kicking_TouchDown() {kickingController.HandleTouchDown();}	
	private void Kicking_ExitState() {kickingController.ExitState();}	
	private void Kicking_UpdateState() {kickingController.UpdateState();}	
	private void Kicking_FixedUpdateState() {kickingController.FixedUpdateState();}
	private void Kicking_LeftTouchDown() {kickingController.HandleLeftTouchDown();}	
	private void Kicking_LeftTouchUp() {kickingController.HandleLeftTouchUp();}	
	private void Kicking_RightTouchDown() {kickingController.HandleRightTouchDown();}	
	private void Kicking_RightTouchUp() {kickingController.HandleRightTouchUp();}
	private void Kicking_EnterState() {
		kickingController.EnterState();
		if (SignalEnteredKickingState != null) SignalEnteredKickingState();
	}


	// grappling

	private void Grappling_LeftSwipe() {grapplingController.HandleLeftSwipe();}	
	private void Grappling_RightSwipe() {grapplingController.HandleRightSwipe();}	
	private void Grappling_UpSwipe() {grapplingController.HandleUpSwipe();}
	private void Grappling_DownSwipe() {grapplingController.HandleDownSwipe();}	
	private void Grappling_Tap() {grapplingController.HandleTap();}
	private void Grappling_TouchUp() {grapplingController.HandleTouchUp();}	
	private void Grappling_TouchDown() {grapplingController.HandleTouchDown();}	
	private void Grappling_ExitState() {grapplingController.ExitState();}	
	private void Grappling_UpdateState() {grapplingController.UpdateState();}	
	private void Grappling_FixedUpdateState() {grapplingController.FixedUpdateState();}
	private void Grappling_LeftTouchDown() {grapplingController.HandleLeftTouchDown();}	
	private void Grappling_LeftTouchUp() {grapplingController.HandleLeftTouchUp();}	
	private void Grappling_RightTouchDown() {grapplingController.HandleRightTouchDown();}	
	private void Grappling_RightTouchUp() {grapplingController.HandleRightTouchUp();}
	private void Grappling_EnterState() {
		grapplingController.EnterState();
		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();
	}
}
