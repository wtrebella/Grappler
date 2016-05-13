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

		if (player.grapplingManager.Connect()) player.SetState(Player.PlayerStates.Grappling);
	}

	public override void Tap() {
		base.Tap();

		Crag crag = cragFinder.FindInDirection(terrainPair.GetThroughDirection(player.body.transform.position));
		if (crag) player.SetState(Player.PlayerStates.Punching);
	}
}
