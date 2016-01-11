using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class ClimberFollowUpdateSwitcher : MonoBehaviour {
	[SerializeField] private Climber climber;

	private Follow follow;
	
	private void Awake() {
		follow = GetComponent<Follow>();
		climber.SignalEnteredClimbingState += HandleClimberEnteredClimbingState;
		climber.SignalEnteredFallingState += HandleClimberEnteredFallingState;
		climber.SignalEnteredGrapplingState += HandleClimberEnteredGrapplingState;
	}

	private void HandleClimberEnteredClimbingState() {
		SetFollowUpdate();
	}

	private void HandleClimberEnteredFallingState() {
		SetFollowFixedUpdate();
	}

	private void HandleClimberEnteredGrapplingState() {
		SetFollowFixedUpdate();
	}

	private void SetFollowUpdate() {
		follow.updateType = FollowUpdateType.Update;
	}

	private void SetFollowFixedUpdate() {
		follow.updateType = FollowUpdateType.FixedUpdate;
	}
}
