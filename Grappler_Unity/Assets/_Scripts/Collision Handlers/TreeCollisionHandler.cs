using UnityEngine;
using System.Collections;

public class TreeCollisionHandler : CollisionHandler {
	public override void HandleTrigger(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTrigger(rigid, collider);
		ScreenShaker.instance.ShakeLerp(0.2f);
		player.rigidbodyVelocityReducer.Reduce();
	}

	private void Awake() {
		BaseAwake();
	}
}