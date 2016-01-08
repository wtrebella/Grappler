using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ClimberMover))]
[RequireComponent(typeof(KinematicSwitcher))]
public class Climber : StateMachine {
	[SerializeField] private Grappler grappler;

	private enum ClimberStates {Climbing, Grappling};
	
	private KinematicSwitcher kinematicSwitcher;
	private ClimberMover climberMover;

	public void SetClimbingState() {
		currentState = ClimberStates.Climbing;
	}

	public void SetGrapplingState() {
		currentState = ClimberStates.Grappling;
	}

	private void Awake() {
		grappler.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		climberMover = GetComponent<ClimberMover>();
	}

	private void HandleEnteredGrapplingState() {
		SetGrapplingState();
	}

	private void Climbing_EnterState() {
		kinematicSwitcher.SetKinematic();
		climberMover.StartClimbing();
	}

	private void Grappling_EnterState() {
		kinematicSwitcher.SetNonKinematic();
		climberMover.StopClimbing();
	}
}
