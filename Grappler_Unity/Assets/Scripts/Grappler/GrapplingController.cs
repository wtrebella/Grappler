using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class GrapplingController : PlayerComponentController {
	[SerializeField] private Grappling grappling;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public void ConnectGrapplerIfPossible() {
		Anchorable anchorable;
		
		if (anchorableFinder.FindAnchorableInCircle(out anchorable)) {
			ConnectGrapplerIfPossible(anchorable);
		}
	}
	
	public void DisconnectGrapplerIfPossible() {
		if (grappling.ReadyToDisconnect()) {
			grappling.ReleaseGrapple();
			player.SetState(Player.PlayerStates.Falling);
		}
	}
	
	public void ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (grappling.ReadyToConnect()) {
			grappling.Connect(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
		}
	}

	public override void EnterState() {
		player.playerAnimator.PlayGrapplingAnimations();
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
		DisconnectGrapplerIfPossible();
	}

	private void OnComplete(AbstractGoTween tween) {
		Debug.Log("fdoifah");
	}
}
