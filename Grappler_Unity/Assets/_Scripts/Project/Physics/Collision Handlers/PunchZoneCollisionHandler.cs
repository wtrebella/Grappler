using UnityEngine;
using System.Collections;

public class PunchZoneCollisionHandler : CollisionHandler {
	private bool isInPunchZone = false;
	private bool successfullyPunched = false;
	private Breakable breakable;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		isInPunchZone = true;
		breakable = collider.GetComponentInParent<Breakable>();
	}

	public override void HandleTriggerExit(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (successfullyPunched) {
			if (breakable) Punch();
		}
		else {
			rigid.velocity = Vector2.zero;
			player.SetState(Player.PlayerStates.Dead);
		}
		breakable = null;
	}

	private void Update() {
		if (isInPunchZone) {
			if (Input.GetKeyDown(KeyCode.P)) successfullyPunched = true;
		}
	}

	private void Punch() {
		successfullyPunched = true;
		isInPunchZone = false;
		if (breakable) breakable.Explode();
	}
}
