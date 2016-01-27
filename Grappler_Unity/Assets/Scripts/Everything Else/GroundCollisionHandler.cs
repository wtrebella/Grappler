using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class GroundCollisionHandler : MonoBehaviour {
	private Player player;

	private bool hasGrappledSinceHittingGround = true;

	public void HandleHitGround(Collision2D collision) {
		if (player.IsOnGround()) return;
		if (!hasGrappledSinceHittingGround) return;

		hasGrappledSinceHittingGround = false;
		ScreenShaker.instance.CollisionShake(collision.relativeVelocity.magnitude);
		player.SetState(Player.PlayerStates.OnGround);
	}

	private void Awake() {
		player = GetComponent<Player>();
		player.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
	}

	private void HandleEnteredGrapplingState() {
		hasGrappledSinceHittingGround = true;
	}
}
