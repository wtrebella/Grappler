using UnityEngine;
using System.Collections;
using WhitDataTypes;

public class RockSlideCollisionHandler : CollisionHandler {
	[SerializeField] private FloatRange hitVelocityRangeX = new FloatRange(100, 200);
	[SerializeField] private FloatRange hitVelocityRangeY = new FloatRange(50, 75);
	[SerializeField] private FloatRange torqueRange = new FloatRange(-100, 100);

	private bool hasCollided = false;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (hasCollided) return;

		hasCollided = true;

		ScreenShaker.instance.ShakeMax();
		player.rigidbodyAffecterGroup.AddVelocity(new Vector2(hitVelocityRangeX.GetRandom(), hitVelocityRangeY.GetRandom()));
		player.rigidbodyAffecterGroup.AddTorque(torqueRange.GetRandom());
		player.SetState(Player.PlayerStates.Dead);
	}

	private void Awake() {
		BaseAwake();
	}
}
