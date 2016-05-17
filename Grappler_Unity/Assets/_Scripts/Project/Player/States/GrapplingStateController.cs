using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	public Action SignalGrappleBegan;
	public Action SignalGrappleEnded;

	[SerializeField] private CragFinder cragFinder;
	[SerializeField] private float slopeAddition = 0.15f;
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private CollisionSignaler collisionSignaler;

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
		collisionSignaler.SignalCollision -= OnCollision;
		DisconnectPlayer();
	}
	
	public override void TouchUp() {
		base.TouchUp();

		EndGrappling();
	}

	private void EndGrappling() {
		if (SignalGrappleEnded != null) SignalGrappleEnded();
		SetToFallingState();
	}

	private bool PlayerIsPastGrapplePointX() {
		return player.body.transform.position.x > GrapplingManager.instance.GetGrapplePoint().x;
	}

	private bool SlopeIsAboveTarget() {
		Vector2 targetDirection = terrainPair.GetThroughDirection(player.body.transform.position);
		Vector2 velocity = player.body.rigid.velocity;
		Vector2 playerDirection = velocity.normalized;
		float playerSlope = WhitTools.DirectionToSlope(playerDirection);
		float targetSlope = WhitTools.DirectionToSlope(targetDirection);
		targetSlope += slopeAddition;
		return playerSlope > targetSlope;
	}

	private bool DisconnectPlayer() {
		return player.grapplingManager.Disconnect();
	}

	private void OnCollision() {
		if (timeInState > 0.2f) SetToFallingState();
	}
}
