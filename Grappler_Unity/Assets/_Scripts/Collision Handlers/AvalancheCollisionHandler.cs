using UnityEngine;
using System.Collections;

public class AvalancheCollisionHandler : CollisionHandler {
	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);
		ScreenShaker.instance.ShakeMax();
		Time.timeScale = 0.25f;
		player.SetState(Player.PlayerStates.Dead);
	}

	private void Awake() {
		BaseAwake();
	}
}
