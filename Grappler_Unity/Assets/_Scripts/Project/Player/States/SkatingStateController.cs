using UnityEngine;
using System.Collections;

public class SkatingStateController : PlayerStateController {
	[SerializeField] private SkatingController skatingController;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Skating;
	}

	public override void EnterState() {
		base.EnterState();

		player.rigidbodyAffecterGroup.SetKinematic();
		player.playerAnimator.PlaySkatingAnimations();
		skatingController.StartSkating();
	}

	public override void ExitState() {
		base.ExitState();

		skatingController.StopSkating();
	}

	public override void TouchDown() {
		base.TouchDown();

		Grapple();
	}

	protected override void Grapple() {
		base.Grapple();

		player.rigidbodyAffecterGroup.SetXVelocity(skatingController.GetSpeed());
	}
}
