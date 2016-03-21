using UnityEngine;
using System.Collections;

public class DeadStateController : PlayerStateController {
	[SerializeField] private Rigidbody2D bodyRigidbody;
	[SerializeField] private Rigidbody2D feetRigidbody;
	[SerializeField] private Physics2DMaterialSwitcher materialSwitcher;
	[SerializeField] private CameraMoverHorizontalSmoothDampAdjuster cameraSmoothDampAdjuster;
	[SerializeField] private CameraMover cameraMover;
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private SkeletonGhostController ghostController;


	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Dead;
	}

	public override void EnterState() {
		base.EnterState();
		player.grapplingStateController.DisconnectGrapplerIfPossible();
		player.timeScaleChanger.ScaleToSlow();
		materialSwitcher.EnableMaterial2();
		cameraSmoothDampAdjuster.enabled = false;
		cameraMover.SetSmoothDampTimeX(0.1f);
		ghostController.EnableGhosts();
	}

	
	public override void TouchDown() {
		base.TouchDown();
		GameplayManager.instance.RestartGame();
	}
}
