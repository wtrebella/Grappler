﻿using UnityEngine;
using System.Collections;

public enum FollowUpdateType {
	Update,
	FixedUpdate
}

public enum FollowMovementType {
	Smoothdamp,
	Immediate
}

public enum FollowAxisType {
	X,
	Y,
	XandY
}

public class Follow : MonoBehaviour {
	[SerializeField] private Transform objectToFollow;
	[SerializeField] private Vector2 objectOffset;
	[SerializeField] private FollowUpdateType updateType;
	[SerializeField] private FollowMovementType movementType;
	[SerializeField] private FollowAxisType axisType;
	[SerializeField] private float smoothDampTime = 0.13f;

	private float initialDistance;
	private Vector3 initialDirection;
	private Vector3 smoothDampVelocity;

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
	}

	private void Awake() {
		initialDistance = GetObjectToThisDistance();
		initialDirection = GetObjectToThisDirection();
	}

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

	private float GetObjectToThisDistance() {
		return (transform.position - objectToFollow.position).magnitude;
	}

	private Vector3 GetObjectToThisDirection() {
		return (transform.position - objectToFollow.position).normalized;
	}

	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = objectToFollow.position;
		Vector3 offsetObjectPosition = new Vector3(objectPosition.x + objectOffset.x, objectPosition.y + objectOffset.y, objectPosition.z);
		Vector3 targetPosition = offsetObjectPosition + initialDirection * initialDistance;
		
		if (axisType == FollowAxisType.X) targetPosition.y = transform.position.y;
		else if (axisType == FollowAxisType.Y) targetPosition.x = transform.position.x;

		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}