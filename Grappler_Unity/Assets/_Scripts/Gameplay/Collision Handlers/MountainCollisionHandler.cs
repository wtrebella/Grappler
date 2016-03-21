using UnityEngine;
using System.Collections;

public class MountainCollisionHandler : CollisionHandler {
	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		ShakeScreen(rigid, collision);

		player.rigidbodyAffecterGroup.ReduceVelocity();
	}
}