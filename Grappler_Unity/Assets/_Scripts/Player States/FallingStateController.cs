using UnityEngine;
using System.Collections;

public class FallingStateController : PlayerStateController {
	[SerializeField] private AnchorableFinder anchorableFinder;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Falling;
	}

	public override void EnterState() {
		base.EnterState();
		player.playerAnimator.PlayFallingAnimations();
	}

	public override void TouchDown() {
		base.TouchDown();
		player.grapplingStateController.ConnectGrapplerToHighestAnchorable();
	}
}
