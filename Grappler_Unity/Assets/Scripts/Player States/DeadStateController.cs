using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class DeadStateController : PlayerStateController {
	[SerializeField] private Rigidbody2D bodyRigidbody;
	[SerializeField] private Rigidbody2D feetRigidbody;

	public override void EnterState() {
		player.grapplingController.DisconnectGrapplerIfPossible();
		StartCoroutine(WaitThenFreezeX());
	}

	private IEnumerator WaitThenFreezeX() {
		yield return new WaitForSeconds(3);
		bodyRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
		feetRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
	}
	
	public override void ExitState() {
		
	}
	
	public override void UpdateState() {

	}
	
	public override void FixedUpdateState() {

	}
	
	public override void HandleLeftSwipe() {

	}
	
	public override void HandleRightSwipe() {

	}
	
	public override void HandleUpSwipe() {

	}
	
	public override void HandleDownSwipe() {
		
	}
	
	public override void HandleTap() {
		
	}

	public override void HandleTouchUp() {
		
	}
	
	public override void HandleTouchDown() {

	}
}
