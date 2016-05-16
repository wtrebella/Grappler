using UnityEngine;
using System.Collections;

public class FallingStateController : PlayerStateController {
	[SerializeField] private CragFinder cragFinder;
	[SerializeField] private AnchorableFinder anchorableFinder;
	[SerializeField] private WhitTerrainPair terrainPair;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Falling;
	}

	public override void EnterState() {
		base.EnterState();
		player.rigidbodyAffecterGroup.SetNonKinematic();
		player.playerAnimator.PlayFallingAnimations();
	}

	public override void TouchDown() {
		base.TouchDown();

	}

	public override void RightSwipe() {
		base.RightSwipe();

		Grapple();
	}

	public override void LeftTouchDown() {
		base.LeftTouchDown();

		PunchManager.instance.PunchThroughCragIfNear();
	}

	public override void UpdateState() {
		base.UpdateState();

		if (Input.GetKeyDown(KeyCode.LeftArrow)) PunchManager.instance.PunchThroughCragIfNear();
		if (Input.GetKeyDown(KeyCode.RightArrow)) Grapple();
	}

	private void Grapple() {
		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}
}
