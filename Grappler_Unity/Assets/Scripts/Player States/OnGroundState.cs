using UnityEngine;
using System.Collections;

public class OnGroundState : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2DStopper rigidStopper;
	[SerializeField] private PlayerAnimator playerAnimator;

	public void Stop() {
		rigidStopper.Stop();
		playerAnimator.StopAnimating();
		player.grapplingController.DisconnectGrapplerIfPossible();
	}

	private void SwitchToFallingState() {
		rigidStopper.Cancel();
		player.SetState(Player.PlayerStates.Falling);
	}

	private void Awake() {
		rigidStopper.SignalSlowed += HandleSlowed;
		rigidStopper.SignalStopped += HandleStopped;
	}

	private void HandleSlowed() {

	}

	private void HandleStopped() {
		SwitchToFallingState();
	}
}