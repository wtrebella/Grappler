using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using WhitTerrain;

public class GrapplingStateController : PlayerStateController {
	public Action SignalGrappleBegan;
	public Action SignalGrappleEnded;
	public Action<float> SignalGrappleEndedWithSlopeDeviation;

	[SerializeField] private float slopeAddition = 0.15f;
	[SerializeField] private float allowedSlopeDeviation = 0.25f;
	[SerializeField] private Path terrainPair;
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private PlayerTrajectory playerTrajectory;
	[SerializeField] private Trail trail;

	private void Awake() {
		base.BaseAwake();
		collisionSignaler.SignalCollision += OnCollision;
		state = Player.PlayerStates.Grappling;
	}

	public override void EnterState() {
		base.EnterState();

		playerTrajectory.Show();
		player.rigidbodyAffecterGroup.SetNonKinematic();
		player.playerAnimator.PlayGrapplingAnimations();
		if (SignalGrappleBegan != null) SignalGrappleBegan();
	}

	public override void ExitState() {
		base.ExitState();

		OnGrappleEndDeviation();
		playerTrajectory.Hide();
		collisionSignaler.SignalCollision -= OnCollision;
		DisconnectPlayer();
		if (SignalGrappleEnded != null) SignalGrappleEnded();
	}

	private void OnGrappleEndDeviation() {
		float deviation = GetDeviationFromThroughSlope();

//		if (deviation > allowedSlopeDeviation || playerTrajectory.PredictedPointHitsMountain()) {
//			trail.BurstTrailColor(Color.red);
//			ReduceVelocity(deviation);
//		}
//		else {
			trail.BurstTrailColor(Color.green);
//		}
		if (SignalGrappleEndedWithSlopeDeviation != null) SignalGrappleEndedWithSlopeDeviation(deviation);
	}

	private void ReduceVelocity(float throughSlopeDeviation) {
		float absSlopeDeviation = Mathf.Abs(throughSlopeDeviation);
		float reductionMultiplier = Mathf.Max(0, 1.0f - absSlopeDeviation);
		player.rigidbodyAffecterGroup.ReduceVelocity(reductionMultiplier);
	}

	private float GetDeviationFromThroughSlope() {
		Vector2 velocity = player.body.rigid.velocity;
		Vector2 direction = velocity.normalized;
		float slope = WhitTools.DirectionToSlope(direction);
		float throughSlope = terrainPair.GetThroughSlope(player.body.transform.position);
		float diff = slope - throughSlope;
		return diff;
	}

	public override void RightTouchUp() {
		base.RightTouchUp();

		EndGrappling();
	}

	public override void LeftTouchDown() {
		base.LeftTouchDown();
	}

	private void EndGrappling() {
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
