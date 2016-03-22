using UnityEngine;
using System.Collections;
using System;

public class Follow : MonoBehaviour {
	public WhitUpdateType updateType;

	[HideInInspector, NonSerialized] public float minX = Mathf.NegativeInfinity;
	[HideInInspector, NonSerialized] public float minY = Mathf.NegativeInfinity;

	[SerializeField] private WhitMovementType movementType;
	[SerializeField] private WhitAxisType axisType;
	[SerializeField] private Transform objectToFollow;
	[SerializeField] private float smoothDampTime = 0.13f;
	[SerializeField] private Vector2 objectOffset;

	private float initialDistance;
	private Vector3 initialDirection;
	private Vector3 smoothDampVelocity;

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
	}

	public void SetAxisType(WhitAxisType axisType) {
		this.axisType = axisType;
	}

	public Vector2 GetOffset() {
		return objectOffset;
	}

	public void SetOffset(Vector2 offset) {
		objectOffset = offset;
	}

	public float GetOffsetX() {
		return objectOffset.x;
	}

	public float GetOffsetY() {
		return objectOffset.y;
	}

	public void SetOffsetX(float x) {
		objectOffset.x = x;
	}

	public void SetOffsetY(float y) {
		objectOffset.y = y;
	}

	private void Awake() {
		initialDistance = GetObjectToThisDistance();
		initialDirection = GetObjectToThisDirection();
	}

	private void Update() {
		if (updateType == WhitUpdateType.Update) UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType == WhitUpdateType.FixedUpdate) UpdateMovement();
	}

	private void UpdateMovement() {
		if (movementType == WhitMovementType.Smoothdamp) UpdateMovementSmoothDamp();
		else if (movementType == WhitMovementType.Immediate) UpdateMovementImmediate();
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

		if (axisType == WhitAxisType.X) targetPosition.y = transform.position.y;
		else if (axisType == WhitAxisType.Y) targetPosition.x = transform.position.x;

		targetPosition.x = Mathf.Max(minX, targetPosition.x);
		targetPosition.y = Mathf.Max(minY, targetPosition.y);

		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
