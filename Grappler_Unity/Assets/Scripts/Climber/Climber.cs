using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ClimberAnimator))]
[RequireComponent(typeof(ClimberMover))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
public class Climber : StateMachine {
	[SerializeField] private Grappler grappler;

	private enum ClimberStates {Climbing, Grappling, Falling};

	private ClimberAnimator climberAnimator;
	private KinematicSwitcher kinematicSwitcher;
	private TriggerSwitcher triggerSwitcher;
	private ClimberMover climberMover;

	public void SetClimbingState() {
		currentState = ClimberStates.Climbing;
	}

	public void SetGrapplingState() {
		currentState = ClimberStates.Grappling;
	}

	public void SetFallingState() {
		currentState = ClimberStates.Falling;
	}

	private void Awake() {
		grappler.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
		grappler.SignalEnteredFallingState += HandleEnteredFallingState;
		triggerSwitcher = GetComponent<TriggerSwitcher>();
		climberAnimator = GetComponent<ClimberAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		climberMover = GetComponent<ClimberMover>();
	}

	private void HandleEnteredGrapplingState() {
		SetGrapplingState();
	}

	private void HandleEnteredFallingState() {
		SetFallingState();
	}

	private void Climbing_EnterState() {
		kinematicSwitcher.SetKinematic();
		triggerSwitcher.SetAsTrigger(0.3f);
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
