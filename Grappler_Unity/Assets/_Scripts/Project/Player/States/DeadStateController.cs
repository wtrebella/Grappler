using UnityEngine;
using System.Collections;

public class DeadStateController : PlayerStateController {
	[SerializeField] private CameraMoverHorizontalSmoothDampAdjuster cameraSmoothDampAdjuster;
	[SerializeField] private CameraMover cameraMover;
	[SerializeField] private RockSlide rockSlide;


	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Dead;
	}

	public override void EnterState() {
		base.EnterState();
		player.grapplingManager.Disconnect();
		cameraSmoothDampAdjuster.enabled = false;
		cameraMover.SetSmoothDampTimeX(0.1f);
		player.ghostController.EnableGhosting();
	}

	
	public override void TouchDown() {
		base.TouchDown();
		GameplayManager.instance.RestartGame();
	}
}
