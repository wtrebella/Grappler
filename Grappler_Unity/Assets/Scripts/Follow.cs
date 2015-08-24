using UnityEngine;
using System.Collections;

public enum FollowUpdateType {
	Update,
	FixedUpdate
}

public enum FollowMovementType {
	Smoothdamp,
	Immediate
}

public class Follow : MonoBehaviour {
	[SerializeField] private Transform objectToFollow;
	[SerializeField] private Vector2 objectOffset;
	[SerializeField] private FollowUpdateType updateType;
	[SerializeField] private FollowMovementType movementType;
	[SerializeField] private float smoothDampTime = 0.13f;
	
	private Vector3 smoothDampVelocity;

	private void Update() {
		if (updateType == FollowUpdateType.Update) UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType == FollowUpdateType.FixedUpdate) UpdateMovement();
	}

	private void UpdateMovement() {
		if (movementType == FollowMovementType.Smoothdamp) UpdateMovementSmoothDamp();
		else if (movementType == FollowMovementType.Immediate) UpdateMovementImmediate();
	}

	private void UpdateMovementSmoothDamp() {
		transform.position = GetSmoothedTargetPosition();
	}

	private void UpdateMovementImmediate() {
		transform.position = GetTargetPosition();
	}

	private Vector3 GetTargetPosition() {
		Vector2 objectPosition = objectToFollow.position.ToVector2() + objectOffset;
		Vector3 targetPosition = objectPosition.ToVector3(transform.position.z);
		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
