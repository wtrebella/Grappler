using UnityEngine;
using System.Collections;

public class RockSlideCollisionHandler : CollisionHandler {
	private bool hasCollided = false;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (hasCollided) return;
	
		hasCollided = true;
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime *= Time.timeScale;
		ScreenShaker.instance.ShakeMax();
		player.SetState(Player.PlayerStates.Dead);
	}

	private void Awake() {
		BaseAwake();
	}
}
