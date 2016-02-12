using UnityEngine;
using System.Collections;

public class GroundCollisionHandler : CollisionHandler {
	[SerializeField] private float exitGroundTime = 0.3f;

	private bool hasGrappledSinceHittingGround = true;
	private float timeLastOnGround = 0;

	public float GetTimeSinceLastOnGround() {
		return Time.fixedTime - timeLastOnGround;
	}

	public override void HandleCollisionEnter(Rigidbody2D rigid, Collision2D collision) {
		base.HandleCollisionEnter(rigid, collision);

		if (player.IsOnGround()) return;
		if (!hasGrappledSinceHittingGround) return;
		if (!HasBeenLongEnough()) return;

		ShakeScreen(rigid, collision);

		hasGrappledSinceHittingGround = false;
		player.SetState(Player.PlayerStates.OnGround);
		timeLastOnGround = Time.fixedTime;
	}

	private void Awake() {
		base.BaseAwake();
		player.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
	}

	private void HandleEnteredGrapplingState() {
		hasGrappledSinceHittingGround = true;
	}

	private bool HasBeenLongEnough() {
		return GetTimeSinceLastOnGround() >= exitGroundTime;
	}
}
