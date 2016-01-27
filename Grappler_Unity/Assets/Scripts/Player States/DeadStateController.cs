using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class DeadStateController : PlayerStateController {
	[SerializeField] private Rigidbody2D bodyRigidbody;
	[SerializeField] private Rigidbody2D feetRigidbody;

	private void Awake() {
		playerState = Player.PlayerStates.Dead;
	}

	public override void EnterState() {
		base.EnterState();
		player.grapplingController.DisconnectGrapplerIfPossible();
		StartCoroutine(WaitThenFreezeX());
	}

	private IEnumerator WaitThenFreezeX() {
		yield return new WaitForSeconds(3);
		bodyRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
		feetRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
	}
	
	public override void ExitState() {
		base.ExitState();
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
