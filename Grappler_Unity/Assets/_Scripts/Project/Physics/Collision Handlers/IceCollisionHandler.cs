using UnityEngine;
using System.Collections;

public class IceCollisionHandler : CollisionHandler {
	[SerializeField] private SkatingController skatingController;

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
		if (player.isFalling) {
			Ice ice = collider.gameObject.GetComponentInParent<Ice>();
			float speed = rigid.velocity.x;
			if (ice && !ice.GetDoneSkating()) StartSkating(ice, speed);
		}
	}

	private void StartSkating(Ice ice, float speed) {
		skatingController.SetIce(ice);
		skatingController.SetSpeed(speed);
		player.SetState(Player.PlayerStates.Skating);
	}
}
