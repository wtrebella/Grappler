using UnityEngine;
using System.Collections;
using System;

public class JumpMeasure : MonoBehaviour {
	[SerializeField] private FlipManager flipManager;
	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private GrapplingStateController grapplingStateController;
	[SerializeField] private Rigidbody2D body;

	private Vector2 pointAtJumpEnd;
	private Vector2 pointAtJumpStart;

	private bool jumpBegan = false;

	private void Awake() {
		collisionSignaler.SignalCollision += OnCollision;
		grapplingStateController.SignalGrappleBegan += OnGrappleBegan;
		grapplingStateController.SignalGrappleEnded += OnGrappleEnded;
	}

	private void OnJumpBegan() {
		jumpBegan = true;
		pointAtJumpStart = GetPosition();
	}

	private void OnJumpEnded() {
		pointAtJumpEnd = GetPosition();
		jumpBegan = false;
		ReportJumpDistance();
	}

	private void OnGrappleBegan() {
		if (jumpBegan) OnJumpEnded();
	}

	private void OnGrappleEnded() {
		OnJumpBegan();
	}

	private void OnCollision() {
		jumpBegan = false;
	}

	private Vector2 GetPosition() {
		return body.transform.position;
	}

	private void ReportJumpDistance() {
		Vector2 delta = pointAtJumpStart - pointAtJumpEnd;
		float jumpDistance = delta.magnitude;
		ScoreManager.instance.ReportJump(jumpDistance, flipManager.flipCount);
		flipManager.ResetFlipCount();
		BonusTankManager.instance.ReportJumpDistance(jumpDistance);
	}
}
