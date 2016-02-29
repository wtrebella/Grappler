using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class PlayerFollowUpdateSwitcher : MonoBehaviour {
	[SerializeField] private Player player;

	private Follow follow;
	
	private void Awake() {
		follow = GetComponent<Follow>();
		player.SignalEnteredFallingState += HandleClimberEnteredFallingState;
		player.SignalEnteredGrapplingState += HandleClimberEnteredGrapplingState;
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

	private void HandleClimberEnteredClimbingRopeState() {
		SetFollowUpdate();
	}

	private void SetFollowUpdate() {
		follow.updateType = WhitUpdateType.Update;
	}

	private void SetFollowFixedUpdate() {
		follow.updateType = WhitUpdateType.FixedUpdate;
	}
}
