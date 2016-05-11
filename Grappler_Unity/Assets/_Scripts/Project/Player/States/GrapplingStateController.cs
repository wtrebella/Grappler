using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	public Action SignalGrappleBegan;
	public Action SignalGrappleEnded;

	[SerializeField] private float slopeAddition = 0.15f;
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
		collisionSignaler.SignalCollision -= OnCollision;
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

		float dist = terrainPair.GetDistAtPosition(player.body.transform.position);
		Vector2 distPoint = terrainPair.GetPointAtDist(dist);
		Vector2 endPoint = terrainPair.GetEndPoint();
		Vector2 delta = endPoint - distPoint;
		Vector2 targetDirection = delta.normalized;

		if (player.body.transform.position.x > GrapplingManager.instance.GetGrapplePoint().x) {
			Vector2 velocity = player.body.rigid.velocity;
			Vector2 playerDirection = velocity.normalized;
			float playerSlope = WhitTools.DirectionToSlope(playerDirection);
			float targetSlope = WhitTools.DirectionToSlope(targetDirection);
			targetSlope += slopeAddition;
			if (playerSlope > targetSlope) EndGrappling();
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
