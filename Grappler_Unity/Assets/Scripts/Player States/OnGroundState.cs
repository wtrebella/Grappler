using UnityEngine;
using System.Collections;

public class OnGroundState : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2DStopper rigidStopper;
	[SerializeField] private PlayerAnimator playerAnimator;
	[SerializeField] private float onGroundDuration = 1;

	public void Stop() {
		rigidStopper.Stop();
		playerAnimator.StopAnimating();
		player.grapplingController.DisconnectGrapplerIfPossible();
		StartCoroutine(WaitThenSwitchToFallingState());
	}

	private IEnumerator WaitThenSwitchToFallingState() {
		yield return new WaitForSeconds(onGroundDuration);
		SwitchToFallingState();
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
		SwitchToFallingState();
	}

	private void HandleStopped() {

	}
}