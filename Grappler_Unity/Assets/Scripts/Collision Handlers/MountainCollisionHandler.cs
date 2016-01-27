using UnityEngine;
using System.Collections;

public class MountainCollisionHandler : CollisionHandler {
	private void Awake() {
		BaseAwake();
	}

	public override void HandleCollision(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollision(rigid, collision);

		ShakeScreen(rigid, collision);
	}
}