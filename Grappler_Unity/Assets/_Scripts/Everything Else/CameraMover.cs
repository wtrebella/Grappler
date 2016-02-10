using UnityEngine;
using System.Collections;
using System;

public class CameraMover : MonoBehaviour {
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform horizontalMovementObject;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private Vector2 offset;
	[SerializeField] private Vector2 smoothDampTime = new Vector2(0.3f, 0.1f);
	[SerializeField] private float maxDistanceToObject = 9;

	private float sqrMaxDistanceToObject;
	private float initialDistance;
	private Vector3 initialDirection;
	private float smoothDampVelocityX;
	private float smoothDampVelocityY;

	public void SetUpdateType(WhitUpdateType newUpdateType) {
		updateType = newUpdateType;
	}

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
	}

	public Vector2 GetSmoothDampTime() {
		return smoothDampTime;
	}

	public void SetSmoothDampTimeX(float time) {
		smoothDampTime.x = time;
	}

	public void SetSmoothDampTimeY(float time) {
		smoothDampTime.y = time;
	}

	private void Awake() {
		initialDistance = GetObjectToThisDistance();
		initialDirection = GetObjectToThisDirection();
		sqrMaxDistanceToObject = Mathf.Pow(maxDistanceToObject, 2);
	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

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
		Vector3 targetPosition = new Vector3(x + offset.x, y + offset.y, transform.position.z);
		Vector2 vectorToObject = targetPosition.ToVector2() - objectPosition.ToVector2();
		Vector2 directionToObject = vectorToObject.normalized;
		float sqrDistanceToObject = vectorToObject.sqrMagnitude;
		if (sqrDistanceToObject > sqrMaxDistanceToObject) {
			Vector2 targetPosition2D = objectPosition.ToVector2() + directionToObject * maxDistanceToObject;
			targetPosition = targetPosition2D.ToVector3(transform.position.z);
		}

		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 targetPosition = GetTargetPosition();
		float x = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref smoothDampVelocityX, smoothDampTime.x);
		float y = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref smoothDampVelocityY, smoothDampTime.y);
		Vector3 smoothedPosition = new Vector3(x, y, targetPosition.z);
		return smoothedPosition;
	}
}
