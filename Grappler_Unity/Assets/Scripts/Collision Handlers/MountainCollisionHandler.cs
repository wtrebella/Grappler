using UnityEngine;
using System.Collections;

public class MountainCollisionHandler : CollisionHandler {
	private void Awake() {
		BaseAwake();
	}

	public override void HandleCollision(Collision2D collision) {
		base.HandleCollision(collision);
		Debug.Log("mountain collision");

		ScreenShaker.instance.CollisionShake(collision.relativeVelocity.magnitude);
	}
}