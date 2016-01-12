using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ClimberAnimator))]
[RequireComponent(typeof(ClimberMover))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
public class Climber : StateMachine {
	public Action SignalEnteredClimbingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredFallingState;

	[SerializeField] private Grappler grappler;
	[SerializeField] private ClimberMountainCollision bodyMountainCollision;
	[SerializeField] private ClimberMountainCollision feetMountainCollision;

	private enum ClimberStates {Climbing, Grappling, Falling};
	
	private ClimberAnimator climberAnimator;
	private KinematicSwitcher kinematicSwitcher;
	private TriggerSwitcher triggerSwitcher;
	private ClimberMover climberMover;

	public void SetClimbingState() {
		currentState = ClimberStates.Climbing;
		if (SignalEnteredClimbingState != null) SignalEnteredClimbingState();
	}

	public void SetGrapplingState() {
		currentState = ClimberStates.Grappling;
		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();
	}

	public void SetFallingState() {
		currentState = ClimberStates.Falling;
		if (SignalEnteredFallingState != null) SignalEnteredFallingState();
	}

	public bool IsClimbing() {
		return (ClimberStates)currentState == ClimberStates.Climbing;
	}

	public bool IsGrappling() {
		return (ClimberStates)currentState == ClimberStates.Grappling;
	}

	public bool IsFalling() {
		return (ClimberStates)currentState == ClimberStates.Falling;
	}

	private void Awake() {
		grappler.SignalEnteredConnectedState += HandleGrapplerEnteredConnectedState;
		grappler.SignalEnteredDisconnectedState += HandleGrapplerEnteredDisconnectedState;
		bodyMountainCollision.SignalMountainCollision += HandleBodyMountainCollision;
		feetMountainCollision.SignalMountainCollision += HandleFeetMountainCollision;
		triggerSwitcher = GetComponent<TriggerSwitcher>();
		climberAnimator = GetComponent<ClimberAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		climberMover = GetComponent<ClimberMover>();
		SetClimbingState();
	}

	private void HandleBodyMountainCollision() {
		if (!IsClimbing()) SetClimbingState();
	}

	private void HandleFeetMountainCollision() {
		if (!IsClimbing()) SetClimbingState();
	}

	private void HandleGrapplerEnteredConnectedState() {
		StopClimbingIfNeeded();
		SetGrapplingState();
	}

	private void HandleGrapplerEnteredDisconnectedState() {
		StopClimbingIfNeeded();
		SetFallingState();
	}

	private void StopClimbingIfNeeded() {
		if (IsClimbing()) climberMover.StopClimbing();
	}

	private void Climbing_EnterState() {
		kinematicSwitcher.SetKinematic();
		triggerSwitcher.SetAsTrigger(0.3f);
		grappler.ReleaseGrappleIfConnected();
		climberMover.StartClimbing();
		climberAnimator.PlayClimbingAnimations();
	}

	private void Grappling_EnterState() {
		triggerSwitcher.SetAsNonTrigger(0.3f);
		kinematicSwitcher.SetNonKinematic();
		climberMover.StopClimbing();
		climberAnimator.PlayGrapplingAnimations();
	}

	private void Falling_EnterState() {
		climberAnimator.PlayFallingAnimations();
	}
}
