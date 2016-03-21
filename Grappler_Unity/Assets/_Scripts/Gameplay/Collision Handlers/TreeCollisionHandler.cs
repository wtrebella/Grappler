using UnityEngine;
using System.Collections;

public class TreeCollisionHandler : CollisionHandler {
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
		ScreenShaker.instance.ShakeLerp(0.2f);

		Rigidbody2DVelocityReducer velocityReducer = player.rigidbodyAffecterGroup.GetAffecter<Rigidbody2DVelocityReducer>();
		if (velocityReducer != null) velocityReducer.Reduce();
	}
}