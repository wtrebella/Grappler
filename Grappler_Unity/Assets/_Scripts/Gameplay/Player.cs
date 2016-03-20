using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(SkeletonGhostController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Trail))]
[RequireComponent(typeof(Rigidbody2DStopper))]
[RequireComponent(typeof(Rigidbody2DVelocityReducer))]
[RequireComponent(typeof(Rigidbody2DForcer))]
[RequireComponent(typeof(TemporaryTriggerSetter))]
[RequireComponent(typeof(TimeScaler))]
public class Player : WhitStateMachine {
	public enum PlayerStates {Idle, Paused, Falling, Grappling, Dead, OnGround}

	public Trail trail;

	[SerializeField] private PlayerBodyPart body;
	[SerializeField] private PlayerBodyPart feet;

	[HideInInspector] public PlayerAnimator playerAnimator;
	[HideInInspector] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector] public Rigidbody2DStopper rigidbodyStopper;
	[HideInInspector] public Rigidbody2DVelocityReducer rigidbodyVelocityReducer;
	[HideInInspector] public TemporaryTriggerSetter triggerSetter;
	[HideInInspector] public Rigidbody2DForcer rigidbodyForcer;
	[HideInInspector] public TimeScaler timeScaleChanger;
	[HideInInspector] public SkeletonGhostController ghostController;

	private CollisionHandler[] collisionHandlers;

	public bool isFalling {get {return CurrentStateIs(PlayerStates.Falling);}}
	public bool isGrappling {get {return CurrentStateIs(PlayerStates.Grappling);}}
	public bool isDead {get {return CurrentStateIs(PlayerStates.Dead);}}
	public bool isOnGround {get {return CurrentStateIs(PlayerStates.OnGround);}}
	public bool isPaused {get {return CurrentStateIs(PlayerStates.Paused);}}
	public bool isIdle {get {return CurrentStateIs(PlayerStates.Idle);}}

	public IdleStateController idleStateController {get {return (IdleStateController)GetStateController(PlayerStates.Idle);}}
	public PausedStateController pausedStateController {get {return (PausedStateController)GetStateController(PlayerStates.Paused);}}
	public FallingStateController fallingStateController {get {return (FallingStateController)GetStateController(PlayerStates.Falling);}}
	public OnGroundStateController onGroundStateController {get {return (OnGroundStateController)GetStateController(PlayerStates.OnGround);}}
	public DeadStateController deadStateController {get {return (DeadStateController)GetStateController(PlayerStates.Dead);}}
	public GrapplingStateController grapplingStateController {get {return (GrapplingStateController)GetStateController(PlayerStates.Grappling);}}

	private void Awake() {
		collisionHandlers = GetComponents<CollisionHandler>();
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		rigidbodyStopper = GetComponent<Rigidbody2DStopper>();
		rigidbodyVelocityReducer = GetComponent<Rigidbody2DVelocityReducer>();
		triggerSetter = GetComponent<TemporaryTriggerSetter>();
		rigidbodyForcer = GetComponent<Rigidbody2DForcer>();
		timeScaleChanger = GetComponent<TimeScaler>();
		ghostController = GetComponent<SkeletonGhostController>();
	}

	private void Start() {
		SetStateIdle();
	}

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
		WhitStateController currentController = GetCurrentStateController();
		return currentController.timeInState;
	}
}
