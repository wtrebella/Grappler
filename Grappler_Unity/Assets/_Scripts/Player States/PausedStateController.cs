using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PausedStateController : PlayerStateController {
	private void Awake() {
		base.BaseAwake();
		playerState = Player.PlayerStates.Paused;
	}

	public override void EnterState() {
		base.EnterState();
		var rigids = GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rigid in rigids) rigid.isKinematic = true;
	}

	public override void ExitState() {
		base.ExitState();
		var rigids = GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rigid in rigids) rigid.isKinematic = false;
	}

	public override void UpdateState() {
		base.UpdateState();
	}

	public override void FixedUpdateState() {
		base.FixedUpdateState();
	}

	public override void HandleLeftSwipe() {
		base.HandleLeftSwipe();
	}

	public override void HandleRightSwipe() {
		base.HandleRightSwipe();
	}

	public override void HandleUpSwipe() {
		base.HandleUpSwipe();
	}

	public override void HandleDownSwipe() {
		base.HandleDownSwipe();
	}

	public override void HandleTap() {
		base.HandleTap();
	}

	public override void HandleTouchUp() {
		base.HandleTouchUp();
	}

	public override void HandleTouchDown() {
		base.HandleTouchDown();
	}

	public override void HandleLeftTouchDown() {
		base.HandleLeftTouchDown();
	}

	public override void HandleLeftTouchUp() {
		base.HandleLeftTouchUp();
	}

	public override void HandleRightTouchDown() {
		base.HandleRightTouchDown();
	}

	public override void HandleRightTouchUp() {
		base.HandleRightTouchUp();
	}
}