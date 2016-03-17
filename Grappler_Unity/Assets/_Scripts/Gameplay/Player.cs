using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(SkeletonGhostController))]
[RequireComponent(typeof(PausedStateController))]
[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(DeadStateController))]
[RequireComponent(typeof(OnGroundStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Trail))]
[RequireComponent(typeof(Rigidbody2DStopper))]
[RequireComponent(typeof(Rigidbody2DVelocityReducer))]
[RequireComponent(typeof(Rigidbody2DForcer))]
[RequireComponent(typeof(TemporaryTriggerSetter))]
[RequireComponent(typeof(TimeScaler))]
public class Player : StateMachine {
	public UnityEvent OnEnteredFallingState;
	public UnityEvent OnEnteredGrapplingState;
	public UnityEvent OnEnteredDeadState;
	public UnityEvent OnEnteredOnGroundState;
	public UnityEvent OnEnteredPausedState;
	public UnityEvent OnEnteredIdleState;

	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredDeadState;
	public Action SignalEnteredOnGroundState;
	public Action SignalEnteredPausedState;
	public Action SignalEnteredIdleState;

	public enum PlayerStates {Idle, Paused, Falling, Grappling, Dead, OnGround}

	public Trail trail;

	[SerializeField] private PlayerBodyPart body;
	[SerializeField] private PlayerBodyPart feet;

	[HideInInspector] public PausedStateController pausedController;
	[HideInInspector] public PlayerAnimator playerAnimator;
	[HideInInspector] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector] public IdleStateController idleController;
	[HideInInspector] public DeadStateController deadController;
	[HideInInspector] public GrapplingStateController grapplingController;
	[HideInInspector] public OnGroundStateController onGroundController;
	[HideInInspector] public FallingStateController fallingController;
	[HideInInspector] public Rigidbody2DStopper rigidbodyStopper;
	[HideInInspector] public Rigidbody2DVelocityReducer rigidbodyVelocityReducer;
	[HideInInspector] public TemporaryTriggerSetter triggerSetter;
	[HideInInspector] public Rigidbody2DForcer rigidbodyForcer;
	[HideInInspector] public TimeScaler timeScaleChanger;
	[HideInInspector] public SkeletonGhostController ghostController;

	private CollisionHandler[] collisionHandlers;	
	private PlayerStateController[] stateControllers;

	private void Awake() {
		collisionHandlers = GetComponents<CollisionHandler>();
		idleController = GetComponent<IdleStateController>();
		stateControllers = GetComponents<PlayerStateController>();
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		fallingController = GetComponent<FallingStateController>();
		grapplingController = GetComponent<GrapplingStateController>();
		onGroundController = GetComponent<OnGroundStateController>();
		deadController = GetComponent<DeadStateController>();
		rigidbodyStopper = GetComponent<Rigidbody2DStopper>();
		rigidbodyVelocityReducer = GetComponent<Rigidbody2DVelocityReducer>();
		triggerSetter = GetComponent<TemporaryTriggerSetter>();
		rigidbodyForcer = GetComponent<Rigidbody2DForcer>();
		timeScaleChanger = GetComponent<TimeScaler>();
		ghostController = GetComponent<SkeletonGhostController>();
		pausedController = GetComponent<PausedStateController>();
	}

	private void Start() {
		SetState(PlayerStates.Idle);
	}

	public bool IsFalling() {return CurrentStateIs(PlayerStates.Falling);}
	public bool IsGrappling() {return CurrentStateIs(PlayerStates.Grappling);}
	public bool IsDead() {return CurrentStateIs(PlayerStates.Dead);}
	public bool IsOnGround() {return CurrentStateIs(PlayerStates.OnGround);}
	public bool IsPaused() {return CurrentStateIs(PlayerStates.Paused);}
	public bool IsIdle() {return CurrentStateIs(PlayerStates.Idle);}

	public void SetState(PlayerStates state) {
		currentState = state;
	}

	public void SetStateFalling() {
		SetState(PlayerStates.Falling);
	}

	public void SetStateGrappling() {
		SetState(PlayerStates.Grappling);
	}

	public void SetStateIdle() {
		SetState(PlayerStates.Idle);
	}

	public void SetStateDead() {
		SetState(PlayerStates.Dead);
	}

	public void SetStateOnGround() {
		SetState(PlayerStates.OnGround);
	}

	public void SetStatePaused() {
		SetState(PlayerStates.Paused);
	}
	
	public void HandleCollisionEnter(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionEnter(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleCollisionStay(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionStay(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleCollisionExit(PlayerBodyPart bodyPart, Collision2D collision) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collision.gameObject)) {
				handler.HandleCollisionExit(bodyPart.rigid, collision);
			}
		}
	}

	public void HandleTriggerEnter(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerEnter(bodyPart.rigid, collider);
			}
		}
	}

	public void HandleTriggerStay(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerStay(bodyPart.rigid, collider);
			}
		}
	}

	public void HandleTriggerExit(PlayerBodyPart bodyPart, Collider2D collider) {
		foreach (CollisionHandler handler in collisionHandlers) {
			if (handler.HasNoLayer() || handler.ObjectIsInLayer(collider.gameObject)) {
				handler.HandleTriggerExit(bodyPart.rigid, collider);
			}
		}
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}

	private float GetTimeInCurrentState() {
		PlayerStateController currentController = GetControllerForCurrentState();
		return currentController.timeInState;
	}

	private PlayerStateController GetControllerForCurrentState() {
		foreach (PlayerStateController stateController in stateControllers) {
			if (CurrentStateIs(stateController.playerState)) return stateController;
		}
		Debug.LogError("no state controller implemented in this method for " + (PlayerStates)currentState);
		return null;
	}

	protected override void PreUpdateState() {

	}

	protected override void PostUpdateState() {

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
		if (OnEnteredFallingState != null) OnEnteredFallingState.Invoke();
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
		if (OnEnteredOnGroundState != null) OnEnteredOnGroundState.Invoke();
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
		if (OnEnteredDeadState != null) OnEnteredDeadState.Invoke();
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
		if (OnEnteredGrapplingState != null) OnEnteredGrapplingState.Invoke();
	}


	// idle

	private void Idle_LeftSwipe() {idleController.HandleLeftSwipe();}	
	private void Idle_RightSwipe() {idleController.HandleRightSwipe();}	
	private void Idle_UpSwipe() {idleController.HandleUpSwipe();}
	private void Idle_DownSwipe() {idleController.HandleDownSwipe();}	
	private void Idle_Tap() {idleController.HandleTap();}
	private void Idle_TouchUp() {idleController.HandleTouchUp();}	
	private void Idle_TouchDown() {idleController.HandleTouchDown();}	
	private void Idle_ExitState() {idleController.ExitState();}	
	private void Idle_UpdateState() {idleController.UpdateState();}	
	private void Idle_FixedUpdateState() {idleController.FixedUpdateState();}
	private void Idle_LeftTouchDown() {idleController.HandleLeftTouchDown();}	
	private void Idle_LeftTouchUp() {idleController.HandleLeftTouchUp();}	
	private void Idle_RightTouchDown() {idleController.HandleRightTouchDown();}	
	private void Idle_RightTouchUp() {idleController.HandleRightTouchUp();}
	private void Idle_EnterState() {
		idleController.EnterState();
		if (SignalEnteredIdleState != null) SignalEnteredIdleState();
		if (OnEnteredIdleState != null) OnEnteredIdleState.Invoke();
	}


	// paused

	private void Paused_LeftSwipe() {pausedController.HandleLeftSwipe();}	
	private void Paused_RightSwipe() {pausedController.HandleRightSwipe();}	
	private void Paused_UpSwipe() {pausedController.HandleUpSwipe();}
	private void Paused_DownSwipe() {pausedController.HandleDownSwipe();}	
	private void Paused_Tap() {pausedController.HandleTap();}
	private void Paused_TouchUp() {pausedController.HandleTouchUp();}	
	private void Paused_TouchDown() {pausedController.HandleTouchDown();}	
	private void Paused_ExitState() {pausedController.ExitState();}	
	private void Paused_UpdateState() {pausedController.UpdateState();}	
	private void Paused_FixedUpdateState() {pausedController.FixedUpdateState();}
	private void Paused_LeftTouchDown() {pausedController.HandleLeftTouchDown();}	
	private void Paused_LeftTouchUp() {pausedController.HandleLeftTouchUp();}	
	private void Paused_RightTouchDown() {pausedController.HandleRightTouchDown();}	
	private void Paused_RightTouchUp() {pausedController.HandleRightTouchUp();}
	private void Paused_EnterState() {
		pausedController.EnterState();
		if (SignalEnteredPausedState != null) SignalEnteredPausedState();
		if (OnEnteredPausedState != null) OnEnteredPausedState.Invoke();
	}
}
