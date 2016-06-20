using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IcicleCollisionHandler : CollisionHandler {

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		OnHitIcicle();
	}

	private void OnHitIcicle() {
		GameStats.instance.HitIcicle();
	}
}
