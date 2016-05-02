using UnityEngine;
using System.Collections;
using System;

public class GameCamera : MonoBehaviour {
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform focusObject;
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private Camera cam;
	[SerializeField] private float marginSize = 10;

	private Vector3 smoothVelocity;
	private float smoothTime = 0.2f;

	public void SetUpdateType(WhitUpdateType newUpdateType) {
		updateType = newUpdateType;
	}

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
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
		UpdateMovementImmediate();
	}
		
	private void UpdateMovementImmediate() {
		transform.position = GetTargetPosition();
	}

	private void UpdateMovementSmoothed() {
		Vector3 position = transform.position;
		position = Vector3.SmoothDamp(position, GetTargetPosition(), ref smoothVelocity, smoothTime);
		transform.position = position;
	}

	private float GetObjectToThisDistance() {
		return GetObjectToThisVector().magnitude;
	}

	private Vector3 GetObjectToThisDirection() {
		return GetObjectToThisVector().normalized;
	}

	private Vector2 GetObjectToThisVector() {
		return transform.position - focusObject.position;
	}

	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = focusObject.position;
		Vector3 terrainPoint = (Vector3)terrainPair.GetPointAtX(objectPosition.x);
		terrainPoint.z = GetTargetZ();
		return terrainPoint;
	}

	private float GetTargetZ() {
		float x = transform.position.x;
		float terrainWidth = terrainPair.GetWidthAtX(x);
		float targetFrustumHeight = terrainWidth + marginSize * 2;
		float distance = cam.GetDistanceAtFrustumHeight(targetFrustumHeight);
		return -distance;
	}
}
