using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(DeadStateController))]
[RequireComponent(typeof(OnGroundStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Trail))]
[RequireComponent(typeof(Rigidbody2DStopper))]
[RequireComponent(typeof(Rigidbody2DVelocityReducer))]
public class Player : StateMachine {
	public UnityEvent OnEnteredFallingState;
	public UnityEvent OnEnteredGrapplingState;
	public UnityEvent OnEnteredDeadState;
	public UnityEvent OnEnteredOnGroundState;

	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredDeadState;
	public Action SignalEnteredOnGroundState;

	public enum PlayerStates {Falling, Grappling, Dead, OnGround}

	public Trail trail;
	public Forcer forcer;

	[SerializeField] private PlayerBodyPart body;
	[SerializeField] private PlayerBodyPart feet;

	[HideInInspector] public PlayerAnimator playerAnimator;
	[HideInInspector] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector] public DeadStateController deadController;
	[HideInInspector] public GrapplingStateController grapplingController;
	[HideInInspector] public OnGroundStateController onGroundController;
	[HideInInspector] public FallingStateController fallingController;
	[HideInInspector] public Rigidbody2DStopper rigidbodyStopper;
	[HideInInspector] public Rigidbody2DVelocityReducer rigidbodyVelocityReducer;

	private CollisionHandler[] collisionHandlers;	
	private PlayerStateController[] stateControllers;

	public bool IsFalling() {return CurrentStateIs(PlayerStates.Falling);}
	public bool IsGrappling() {return CurrentStateIs(PlayerStates.Grappling);}
	public bool IsDead() {return CurrentStateIs(PlayerStates.Dead);}
	public bool IsOnGround() {return CurrentStateIs(PlayerStates.OnGround);}

	public void SetState(PlayerStates state) {
		currentState = state;
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

	private void Awake() {
		collisionHandlers = GetComponents<CollisionHandler>();
		stateControllers = GetComponents<PlayerStateController>();
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		fallingController = GetComponent<FallingStateController>();
		grapplingController = GetComponent<GrapplingStateController>();
		onGroundController = GetComponent<OnGroundStateController>();
		deadController = GetComponent<DeadStateController>();
		rigidbodyStopper = GetComponent<Rigidbody2DStopper>();
		rigidbodyVelocityReducer = GetComponent<Rigidbody2DVelocityReducer>();
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
}
