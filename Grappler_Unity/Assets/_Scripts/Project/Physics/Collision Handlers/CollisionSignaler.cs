using UnityEngine;
using System.Collections;
using System;

public class CollisionSignaler : CollisionHandler {
	public Action SignalCollision;

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);
		if (SignalCollision != null) SignalCollision();
	}

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);
		if (SignalCollision != null) SignalCollision();
	}
}
