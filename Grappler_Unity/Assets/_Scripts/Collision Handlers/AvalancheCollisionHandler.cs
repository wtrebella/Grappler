using UnityEngine;
using System.Collections;

public class AvalancheCollisionHandler : CollisionHandler {
	private bool hasCollided = false;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (hasCollided) return;
	
		hasCollided = true;
		Time.timeScale = 0.6f;
		ScreenShaker.instance.ShakeMiddle();
		player.SetState(Player.PlayerStates.Dead);
	}

	private void Awake() {
		BaseAwake();
	}
}
