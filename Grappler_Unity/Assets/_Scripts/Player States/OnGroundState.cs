using UnityEngine;
using System.Collections;

public class OnGroundState : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private Rigidbody2DStopper rigidStopper;
	[SerializeField] private PlayerAnimator playerAnimator;

	public void StopRigidbody() {
		rigidStopper.StartStoppingProcess();
		playerAnimator.PlayOnGroundAnimations();
		player.grapplingStateController.DisconnectGrapplerIfPossible();
	}

	public void FreeRigidbody() {
		rigidStopper.Cancel();
	}
}