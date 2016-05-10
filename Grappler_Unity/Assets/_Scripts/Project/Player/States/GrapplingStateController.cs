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
//		EndGrappling();
	}

	private void EndGrappling() {
		if (SignalGrappleEnded != null) SignalGrappleEnded();
		SetToFallingState();
	}

	// i put this here because it's always in this state
	// by the end of the swipe (since the tap now starts the grapple.)
	// switch it so the *end* of the swipe starts the grapple, which
	// should happen in falling and ice skating states.
	public override void Swipe(Vector2 direction, float magnitude) {
		base.Swipe(direction, magnitude);
		Debug.Log(direction + ", " + magnitude);
	}

	public override void FixedUpdateState() {
		base.FixedUpdateState();


		Vector2 velocity = player.body.rigid.velocity;
		Vector2 direction = velocity.normalized;
		float slope = WhitTools.DirectionToSlope(direction);
		if (slope > 0.45f) EndGrappling();
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
