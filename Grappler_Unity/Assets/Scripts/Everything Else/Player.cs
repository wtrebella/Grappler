using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GrapplingStateController))]
[RequireComponent(typeof(ClimbingStateController))]
[RequireComponent(typeof(FallingStateController))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(ClimbingRopeStateController))]
public class Player : StateMachine {
	public Action SignalEnteredClimbingState;
	public Action SignalEnteredFallingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredClimbingRopeState;

	public enum PlayerStates {Falling, Climbing, Grappling, ClimbingRope}

	public Forcer forcer;

	[SerializeField] private Transform body;

	[HideInInspector, NonSerialized] public ClimbingRopeStateController climbingRopeController;
	[HideInInspector, NonSerialized] public PlayerAnimator playerAnimator;
	[HideInInspector, NonSerialized] public TriggerSwitcher triggerSwitcher;
	[HideInInspector, NonSerialized] public KinematicSwitcher kinematicSwitcher;
	[HideInInspector, NonSerialized] public GrapplingStateController grapplingController;
	[HideInInspector, NonSerialized] public ClimbingStateController climbingController;
	[HideInInspector, NonSerialized] public FallingStateController fallingController;

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

	public Vector3 GetBodyPosition() {
		return body.position;
	}

	private void Awake() {
		climbingRopeController = GetComponent<ClimbingRopeStateController>();
		playerAnimator = GetComponent<PlayerAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		triggerSwitcher = GetComponent<TriggerSwitcher>();
		fallingController = GetComponent<FallingStateController>();
		grapplingController = GetComponent<GrapplingStateController>();
		climbingController = GetComponent<ClimbingStateController>();
	}

	private void Start() {
		SetState(PlayerStates.Falling);
	}

	private bool CurrentStateIs(PlayerStates playerState) {
		return (PlayerStates)currentState == playerState;
	}





//   _______    ___       __       __       __  .__   __.   _______ 
//	|   ____|  /   \     |  |     |  |     |  | |  \ |  |  /  _____|
//	|  |__    /  ^  \    |  |     |  |     |  | |   \|  | |  |  __  
//	|   __|  /  /_\  \   |  |     |  |     |  | |  . `  | |  | |_ | 
//	|  |    /  _____  \  |  `----.|  `----.|  | |  |\   | |  |__| | 
//	|__|   /__/     \__\ |_______||_______||__| |__| \__|  \______| 
//

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







//    ______  __       __  .___  ___. .______    __  .__   __.   _______ 
//   /      ||  |     |  | |   \/   | |   _  \  |  | |  \ |  |  /  _____|
//  |  ,----'|  |     |  | |  \  /  | |  |_)  | |  | |   \|  | |  |  __  
//  |  |     |  |     |  | |  |\/|  | |   _  <  |  | |  . `  | |  | |_ | 
//  |  `----.|  `----.|  | |  |  |  | |  |_)  | |  | |  |\   | |  |__| | 
//   \______||_______||__| |__|  |__| |______/  |__| |__| \__|  \______| 
//                                                                       

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






//    _______ .______          ___      .______   .______    __       __  .__   __.   _______ 
//   /  _____||   _  \        /   \     |   _  \  |   _  \  |  |     |  | |  \ |  |  /  _____|
//  |  |  __  |  |_)  |      /  ^  \    |  |_)  | |  |_)  | |  |     |  | |   \|  | |  |  __  
//  |  | |_ | |      /      /  /_\  \   |   ___/  |   ___/  |  |     |  | |  . `  | |  | |_ | 
//  |  |__| | |  |\  \----./  _____  \  |  |      |  |      |  `----.|  | |  |\   | |  |__| | 
//   \______| | _| `._____/__/     \__\ | _|      | _|      |_______||__| |__| \__|  \______| 
//

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







//    ______  __       __  .___  ___. .______    __  .__   __.   _______    .______        ______   .______    _______ 
//   /      ||  |     |  | |   \/   | |   _  \  |  | |  \ |  |  /  _____|   |   _  \      /  __  \  |   _  \  |   ____|
//  |  ,----'|  |     |  | |  \  /  | |  |_)  | |  | |   \|  | |  |  __     |  |_)  |    |  |  |  | |  |_)  | |  |__   
//  |  |     |  |     |  | |  |\/|  | |   _  <  |  | |  . `  | |  | |_ |    |      /     |  |  |  | |   ___/  |   __|  
//  |  `----.|  `----.|  | |  |  |  | |  |_)  | |  | |  |\   | |  |__| |    |  |\  \----.|  `--'  | |  |      |  |____ 
//   \______||_______||__| |__|  |__| |______/  |__| |__| \__|  \______|    | _| `._____| \______/  | _|      |_______|
//

	private void ClimbingRope_LeftSwipe() {
		climbingRopeController.HandleLeftSwipe();
	}
	
	private void ClimbingRope_RightSwipe() {
		climbingRopeController.HandleRightSwipe();
	}
	
	private void ClimbingRope_UpSwipe() {
		climbingRopeController.HandleUpSwipe();
	}
	
	private void ClimbingRope_DownSwipe() {
		climbingRopeController.HandleDownSwipe();
	}
	
	private void ClimbingRope_Tap() {
		climbingRopeController.HandleTap();
	}
	
	private void ClimbingRope_EnterState() {
		climbingRopeController.EnterState();
		
		if (SignalEnteredClimbingRopeState != null) SignalEnteredClimbingRopeState();
	}
	
	private void ClimbingRope_ExitState() {
		climbingRopeController.ExitState();
	}
	
	private void ClimbingRope_UpdateState() {
		climbingRopeController.UpdateState();
	}
	
	private void ClimbingRope_FixedUpdateState() {
		climbingRopeController.FixedUpdateState();
	}
}
