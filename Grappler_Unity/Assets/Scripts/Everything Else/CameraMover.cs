using UnityEngine;
using System.Collections;
using System;

public class CameraMover : MonoBehaviour {
	[SerializeField] private Vector2 positiveSlopeOffset;
	[SerializeField] private Vector2 negativeSlopeOffset;
	[SerializeField] private Transform horizontalMovementObject;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float smoothDampTime = 0.13f;
	[SerializeField] private Vector2 min = new Vector2(-10000, -10000);

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

	private void FixedUpdate() {
		UpdateMovement();
	}

	private void UpdateMovement() {
		UpdateMovementSmoothDamp();
	}

	private void UpdateMovementSmoothDamp() {
		transform.position = GetSmoothedTargetPosition();
	}

	private void UpdateMovementImmediate() {
		transform.position = GetTargetPosition();
	}

	private float GetObjectToThisDistance() {
		return (transform.position - horizontalMovementObject.position).magnitude;
	}

	private Vector3 GetObjectToThisDirection() {
		return (transform.position - horizontalMovementObject.position).normalized;
	}

	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = horizontalMovementObject.position;
		Vector3 offsetObjectPosition = objectPosition + initialDirection * initialDistance;
		float x = offsetObjectPosition.x;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtX(x);
		float y = chunk.GetAverageYAtX(x);
		Vector2 offset;
		if (chunk.SlopeIsPositive()) offset = positiveSlopeOffset;
		else offset = negativeSlopeOffset;

		Vector3 targetPosition = new Vector3(x + offset.x, y + offset.y, transform.position.z);

		targetPosition.x = Mathf.Max(min.x, targetPosition.x);
		targetPosition.y = Mathf.Max(min.y, targetPosition.y);

		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
