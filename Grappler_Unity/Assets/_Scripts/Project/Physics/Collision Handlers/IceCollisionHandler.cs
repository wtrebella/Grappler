using UnityEngine;
using System.Collections;

public class IceCollisionHandler : CollisionHandler {

	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	public override void HandleTriggerEnter(Rigidbody2D rigid, Collider2D collider) {
		base.HandleTriggerEnter(rigid, collider);

		if (WhitTools.IsInLayer(collider.gameObject, "Ice")) {
			player.SetState(Player.PlayerStates.Skating);
		}
	}
}
