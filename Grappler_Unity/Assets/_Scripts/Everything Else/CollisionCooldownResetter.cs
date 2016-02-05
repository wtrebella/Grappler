using UnityEngine;
using System.Collections;

public class CollisionCooldownResetter : CollisionHandler {
	[SerializeField] private LayerMask[] layerMasksThatCauseCooldownReset;
	[SerializeField] private Cooldown cooldown;

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);
		if (ColliderShouldResetCooldown(collision.collider)) cooldown.ResetCooldown();
	}

	public override void HandleCollisionStay(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionStay(rigid, collision);
		if (ColliderShouldResetCooldown(collision.collider)) cooldown.ResetCooldown();
	}

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);
		if (ColliderShouldResetCooldown(collider)) cooldown.ResetCooldown();
	}

	public override void HandleTriggerStay(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerStay(rigid, collider);
		if (ColliderShouldResetCooldown(collider)) cooldown.ResetCooldown();
	}

	private bool ColliderShouldResetCooldown(Collider2D collider) {
		foreach (LayerMask layerMask in layerMasksThatCauseCooldownReset) {
			if (WhitTools.CompareLayers(layerMask.value, collider.gameObject.layer)) return true;
		}
		return false;
	}
}
