using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	public UnityEventWithFloat OnVoluntaryRelease;

	[SerializeField] private Rigidbody2DVelocityReducer velocityReducer;
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AirTimeTimer airTimeTimer;
	[SerializeField] private GrapplingState grappling;
	[SerializeField] private AnchorableFinder anchorableFinder;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Grappling;
	}

	public void ConnectGrapplerToHighestAnchorable() {
		if (!grappling.ReadyToConnect()) return;
		Anchorable anchorable;
		if (anchorableFinder.FindAnchorableInScreenOverlap(out anchorable)) {
			ConnectGrapplerIfPossible(anchorable);
		}
	}
	
	public bool DisconnectGrapplerIfPossible() {
		if (grappling.ReadyToDisconnect()) {
			grappling.ReleaseGrapple();
			return true;
		}
		else return false;
	}
	
	public bool ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (grappling.ReadyToConnect()) {
			grappling.Connect(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
			return true;
		}
		return false;
	}

	public void FinishCooldown() {
		grappling.FinishCooldown();
	}

	public override void EnterState() {
		base.EnterState();
		airTimeTimer.OnGrapple();
		velocityReducer.Reduce();
		rockSlide.OnGrapple();
		player.kinematicSwitcher.SetNonKinematic();
		player.playerAnimator.PlayGrapplingAnimations();
	}
	
	public override void TouchUp() {
		base.TouchUp();
		if (DisconnectGrapplerIfPossible()) {
			if (OnVoluntaryRelease != null) OnVoluntaryRelease.Invoke(airTimeTimer.lastStreak);
			player.SetState(Player.PlayerStates.Falling);
		}
	}
}
