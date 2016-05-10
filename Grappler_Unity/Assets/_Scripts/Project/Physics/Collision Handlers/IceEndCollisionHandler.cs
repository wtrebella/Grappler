using UnityEngine;
using System.Collections;

public class IceEndCollisionHandler : CollisionHandler {
	[SerializeField] private SkatingController skatingController;
	[SerializeField] private float vertical = 10;
	[SerializeField] private float torque = -500;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);
		if (player.isSkating) {
			Ice ice = collider.gameObject.GetComponentInParent<Ice>();
			if (ice) StopSkating(ice);
		}
	}

	private void StopSkating(Ice ice) {
		skatingController.StopSkating();
		player.rigidbodyAffecterGroup.SetVelocity(new Vector2(skatingController.GetSpeed(), vertical));
		player.rigidbodyAffecterGroup.AddTorque(torque);
		player.SetState(Player.PlayerStates.Falling);
	}
}
