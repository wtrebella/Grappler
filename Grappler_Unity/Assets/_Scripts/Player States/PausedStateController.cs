using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PausedStateController : PlayerStateController {
	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Paused;
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
		base.UpdateState();
	}

	public override void LeftSwipe() {
		base.LeftSwipe();
	}

	public override void RightSwipe() {
		base.RightSwipe();
	}

	public override void UpSwipe() {
		base.UpSwipe();
	}

	public override void DownSwipe() {
		base.DownSwipe();
	}

	public override void Tap() {
		base.Tap();
	}

	public override void TouchUp() {
		base.TouchUp();
	}

	public override void TouchDown() {
		base.TouchDown();
	}

	public override void LeftTouchDown() {
		base.LeftTouchDown();
	}

	public override void LeftTouchUp() {
		base.LeftTouchUp();
	}

	public override void RightTouchDown() {
		base.RightTouchDown();
	}

	public override void RightTouchUp() {
		base.RightTouchUp();
	}
}