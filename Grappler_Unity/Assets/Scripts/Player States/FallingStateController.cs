using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class FallingStateController : PlayerStateController {
	public override void EnterState() {
		player.playerAnimator.PlayFallingAnimations();
	}
	
	public override void ExitState() {
		
	}
	
	public override void UpdateState() {
		
	}
	
	public override void FixedUpdateState() {
		
	}
	
	public override void HandleLeftSwipe() {
		player.SetState(Player.PlayerStates.Climbing);
	}
	
	public override void HandleRightSwipe() {

	}
	
	public override void HandleUpSwipe() {
		
	}
	
	public override void HandleDownSwipe() {
		
	}
	
	public override void HandleTap() {
		
	}
}
