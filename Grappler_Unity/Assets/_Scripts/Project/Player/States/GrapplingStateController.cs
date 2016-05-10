using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	public Action SignalGrappleBegan;
	public Action SignalGrappleEnded;

	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private WhitTerrainPair terrainPair;

	private void Awake() {
		base.BaseAwake();
		collisionSignaler.SignalCollision += OnCollision;
		state = Player.PlayerStates.Grappling;
	}

	public override void EnterState() {
		base.EnterState();

		player.rigidbodyAffecterGroup.SetNonKinematic();
		player.playerAnimator.PlayGrapplingAnimations();
		if (SignalGrappleBegan != null) SignalGrappleBegan();
	}

	public override void ExitState() {
		base.ExitState();

		DisconnectPlayer();
	}
	
	public override void TouchUp() {
		base.TouchUp();

	}

	private void EndGrappling() {
		if (SignalGrappleEnded != null) SignalGrappleEnded();
		SetToFallingState();
	}
		
	public override void Swipe(Vector2 direction, float magnitude) {
		base.Swipe(direction, magnitude);
	}

	public override void FixedUpdateState() {
		base.FixedUpdateState();

		if (player.body.transform.position.x > GrapplingManager.instance.GetGrapplePoint().x) {
			Vector2 velocity = player.body.rigid.velocity;
			Vector2 direction = velocity.normalized;
			float slope = WhitTools.DirectionToSlope(direction);
			if (slope > 0.45f) EndGrappling();
		}
	}

	private bool DisconnectPlayer() {
		return player.grapplingManager.Disconnect();
	}

	private void SetToFallingState() {
		player.SetState(Player.PlayerStates.Falling);
	}

	private void OnCollision() {
		if (timeInState > 0.2f) SetToFallingState();
	}
}
