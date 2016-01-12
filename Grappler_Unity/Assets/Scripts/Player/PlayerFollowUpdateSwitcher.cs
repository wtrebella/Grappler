using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class PlayerFollowUpdateSwitcher : MonoBehaviour {
	[SerializeField] private Player player;

	private Follow follow;
	
	private void Awake() {
		follow = GetComponent<Follow>();
		player.SignalEnteredClimbingState += HandleClimberEnteredClimbingState;
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

	private void SetFollowUpdate() {
		follow.updateType = FollowUpdateType.Update;
	}

	private void SetFollowFixedUpdate() {
		follow.updateType = FollowUpdateType.FixedUpdate;
	}
}
