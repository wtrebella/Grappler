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
//		if (WhitTools.IsInLayer(collider.gameObject, "Ice")) {
			if (!player.isSkating) {
			Ice ice = collider.gameObject.GetComponentInParent<Ice>();
				if (ice) StartSkating(ice);
			}
//		}
	}

	private void StartSkating(Ice ice) {
		skatingController.SetIce(ice);
		player.SetState(Player.PlayerStates.Skating);
	}
}
