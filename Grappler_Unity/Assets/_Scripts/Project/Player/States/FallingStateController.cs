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

		Grapple();
	}
}
