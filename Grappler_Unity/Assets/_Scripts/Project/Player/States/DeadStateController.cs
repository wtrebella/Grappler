using UnityEngine;
using System.Collections;

public class DeadStateController : PlayerStateController {
	[SerializeField] private GameCamera cameraMover;
	[SerializeField] private RockSlide rockSlide;

	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Dead;
	}

	public override void EnterState() {
		base.EnterState();
		player.grapplingManager.Disconnect();
		player.ghostController.EnableGhosting();
	}

	public override void TouchDown() {
		base.TouchDown();
		GameplayManager.instance.RestartGame();
	}
}
