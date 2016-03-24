using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class Player : WhitStateMachine {
	public enum PlayerStates {Idle, Paused, Falling, Grappling, Dead, OnGround}

	public Rigidbody2DAffecterGroup rigidbodyAffecterGroup;
	public GrapplingManager grapplingManager;
	public PlayerAnimator playerAnimator;
	public SkeletonGhostController ghostController;
	public GroundDetector groundDetector;
	public PlayerCollisionHandler collisionHandler;

	[HideInInspector] public PlayerBodyPart body;
	[HideInInspector] public PlayerBodyPart feet;

	public bool isFalling {get {return CurrentStateIs(PlayerStates.Falling);}}
	public bool isGrappling {get {return CurrentStateIs(PlayerStates.Grappling);}}
	public bool isDead {get {return CurrentStateIs(PlayerStates.Dead);}}
	public bool isOnGround {get {return CurrentStateIs(PlayerStates.OnGround);}}
	public bool isPaused {get {return CurrentStateIs(PlayerStates.Paused);}}
	public bool isIdle {get {return CurrentStateIs(PlayerStates.Idle);}}

	private IEnumerator Start() {
		SetState(PlayerStates.Idle);
		yield return StartCoroutine(ClothingManager.instance.WaitForInit());
		ClothingManager.instance.WearSavedOrFirstItems();
	}

	public void SetState(PlayerStates state) {
		currentState = state;
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}
}
