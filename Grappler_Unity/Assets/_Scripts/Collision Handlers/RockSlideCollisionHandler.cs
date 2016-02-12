using UnityEngine;
using System.Collections;

public class RockSlideCollisionHandler : CollisionHandler {
	[SerializeField] private FloatRange hitVelocityRangeX = new FloatRange(300, 500);
	[SerializeField] private FloatRange hitVelocityRangeY = new FloatRange(200, 400);
	[SerializeField] private FloatRange torqueRange = new FloatRange(50, 150);

	private bool hasCollided = false;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (hasCollided) return;

		hasCollided = true;

		ScreenShaker.instance.ShakeMax();
		player.rigidbodyForcer.AddVelocity(new Vector2(hitVelocityRangeX.GetRandom(), hitVelocityRangeY.GetRandom()));
		player.rigidbodyForcer.AddTorque(torqueRange.GetRandom());
		player.SetState(Player.PlayerStates.Dead);
	}

	private void Awake() {
		BaseAwake();
	}
}
