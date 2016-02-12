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
		SaveCollisionTime();
		ReturnTimeToNormalIfDead();
		if (ShouldCarryOutOnGroundEvents()) CarryOutOnGroundEvents(rigid, collision);
	}

	private void Awake() {
		base.BaseAwake();
		player.SignalEnteredGrapplingState += HandleEnteredGrapplingState;
	}

	private void ReturnTimeToNormalIfDead() {
		if (player.IsDead()) player.timeScaleChanger.ScaleToNormal();
	}

	private bool ShouldCarryOutOnGroundEvents() {
		return 
			!player.IsOnGround() && 
			!player.IsDead() &&
			hasGrappledSinceHittingGround && 
			ExitGroundTimeHasElapsed();
	}

	private void SaveCollisionTime() {
		timeLastOnGround = Time.fixedTime;
	}

	private void CarryOutOnGroundEvents(Rigidbody2D rigid, Collision2D collision) {
		ShakeScreen(rigid, collision);
		hasGrappledSinceHittingGround = false;
		player.SetState(Player.PlayerStates.OnGround);
	}

	private void HandleEnteredGrapplingState() {
		hasGrappledSinceHittingGround = true;
	}

	private bool ExitGroundTimeHasElapsed() {
		return GetTimeSinceLastOnGround() >= exitGroundTime;
	}
}
