using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(DeadStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Trail))]
public class Player : StateMachine {
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredKickingState;
	public Action SignalEnteredDeadState;

	public enum PlayerStates {Falling, Grappling, Kicking, Dead}

	public Trail trail;
	public Forcer forcer;

	[SerializeField] private Transform body;

	[HideInInspector, NonSerialized] public PlayerAnimator playerAnimator;
	[HideInInspector, NonSerialized] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector, NonSerialized] public DeadStateController deadController;
	[HideInInspector, NonSerialized] public KickingStateController kickingController;
	[HideInInspector, NonSerialized] public GrapplingStateController grapplingController;
	[HideInInspector, NonSerialized] public FallingStateController fallingController;

	public void SetState(PlayerStates state) {
		currentState = state;
	}

	public void HandleCollision(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Tree")) {
			FirTree firTree = collision.collider.GetComponentInParent<FirTree>();
			if (!firTree.hasBeenSliced) {
				float speed = body.GetComponent<Rigidbody2D>().velocity.magnitude;
//				if (speed > 10) SetState(PlayerStates.Dead);
			}
		}
	}

	private void Awake() {
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		kickingController = GetComponent<KickingStateController>();
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
		if (IsBelowScreen()) currentState = PlayerStates.Dead;
	}

	private bool IsBelowScreen() {
		float margin = -5;
		float minY = GameScreen.instance.lowerLeft.y + margin;
		return body.position.y < minY;
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






	// kicking
	
	private void Kicking_LeftSwipe() {
		kickingController.HandleLeftSwipe();
	}
	
	private void Kicking_RightSwipe() {
		kickingController.HandleRightSwipe();
	}
	
	private void Kicking_UpSwipe() {
		kickingController.HandleUpSwipe();
	}
	
	private void Kicking_DownSwipe() {
		kickingController.HandleDownSwipe();
	}
	
	private void Kicking_Tap() {
		kickingController.HandleTap();
	}
	
	private void Kicking_TouchUp() {
		kickingController.HandleTouchUp();
	}
	
	private void Kicking_TouchDown() {
		kickingController.HandleTouchDown();
	}
	
	private void Kicking_EnterState() {
		kickingController.EnterState();
		
		if (SignalEnteredKickingState != null) SignalEnteredKickingState();
	}
	
	private void Kicking_ExitState() {
		kickingController.ExitState();
	}
	
	private void Kicking_UpdateState() {
		kickingController.UpdateState();
	}
	
	private void Kicking_FixedUpdateState() {
		kickingController.FixedUpdateState();
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
