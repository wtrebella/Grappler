using UnityEngine;
using System.Collections;

public class PunchZoneCollisionHandler : CollisionHandler {
	private bool isInPunchZone = false;
	private bool successfullyPunched = false;

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		isInPunchZone = true;
	}

	public override void HandleTriggerExit(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (successfullyPunched) {
			Breakable breakable = collider.GetComponentInParent<Breakable>();
			if (breakable) breakable.Explode();
		}
		else Debug.Log(Time.time + " missed!");

		isInPunchZone = false;
		successfullyPunched = false;
	}

	private void Update() {
		if (isInPunchZone) {
			if (Input.GetKeyDown(KeyCode.P)) successfullyPunched = true;
		}
	}
}
