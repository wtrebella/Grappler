using UnityEngine;
using System.Collections;

public class PunchingStateController : PlayerStateController {
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private float duration = 0.3f;
	[SerializeField] private float speed = 10;
	[SerializeField] private Timer timer;

	private Vector2 initialVelocity; 

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Punching;
	}

	public override void EnterState() {
		base.EnterState();

		initialVelocity = player.body.rigid.velocity;
		player.rigidbodyAffecterGroup.SetKinematic();
		timer.ResetTimer();
		timer.StartTimer();
	}

	public override void TouchDown() {
		base.TouchDown();

	}

	public override void RightSwipe() {
		base.RightSwipe();

	}

	public override void FixedUpdateState() {
		UpdatePunch();
	}

	private void UpdatePunch() {
		player.transform.position += (Vector3)GetPunchVelocity() * Time.fixedDeltaTime;
		if (timer.value >= duration) StopPunching();
	}

	private Vector2 GetPunchDirection() {
		return terrainPair.GetThroughDirection(player.body.transform.position);
	}

	private Vector2 GetPunchVelocity() {
		return speed * GetPunchDirection();
	}

	private void StopPunching() {
		timer.StopTimer();
		timer.ResetTimer();
		player.SetState(Player.PlayerStates.Falling);
		player.rigidbodyAffecterGroup.SetNonKinematic();
		player.rigidbodyAffecterGroup.SetVelocity((Vector3)GetPunchVelocity());
	}
}