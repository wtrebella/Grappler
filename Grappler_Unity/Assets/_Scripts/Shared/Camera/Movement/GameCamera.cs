using UnityEngine;
using System.Collections;
using System;

public enum CameraFollowMode {
	NONE,
	Terrain,
	Player,
	MAX
}

public class GameCamera : MonoBehaviour {
	[SerializeField] private float offset = 10;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform focusObject;
	[SerializeField] private WhitTerrainPair terrainPair;
	[SerializeField] private Camera cam;
	[SerializeField] private float marginSize = 10;

	private CameraFollowMode cameraFollowMode = CameraFollowMode.Terrain;
	private Vector3 smoothVelocity;
	private float smoothTime = 0.1f;

	public void SetUpdateType(WhitUpdateType newUpdateType) {
		updateType = newUpdateType;
	}

	public void UpdateMovementImmediateNow() {
		UpdateMovementImmediate();
	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		UpdateCameraFollowMode();
		UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		UpdateCameraFollowMode();
		UpdateMovement();
	}

	private void UpdateMovement() {
		UpdateMovementSmoothed();
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
		return (Vector2)transform.position - GetFocusObjectPosition();
	}

	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = GetFocusObjectPosition();

		if (cameraFollowMode == CameraFollowMode.Terrain) {
			Vector3 terrainPoint = (Vector3)terrainPair.GetPointAtX(objectPosition.x);
			terrainPoint.z = GetTargetZ();
			return terrainPoint;
		}
		else if (cameraFollowMode == CameraFollowMode.Player) {
			Vector3 newPos = objectPosition;
			newPos.z = transform.position.z;
			return newPos;
		}

		else return transform.position;
	}

	private void UpdateCameraFollowMode() {
		if (cameraFollowMode == CameraFollowMode.Terrain) {
			if (ShouldSwitchToPlayerFollowMode()) cameraFollowMode = CameraFollowMode.Player;
		}
	}

	private bool ShouldSwitchToPlayerFollowMode() {
		return terrainPair.HasEnd() && terrainPair.GetXIsPastEnd(GetFocusObjectPosition().x);
	}

	private float GetTargetZ() {
		float x = transform.position.x;
		float terrainWidth = terrainPair.GetWidthAtX(x);
		float targetFrustumHeight = terrainWidth + marginSize * 2;
		float distance = cam.GetDistanceAtFrustumHeight(targetFrustumHeight);
		return -distance;
	}

	private Vector2 GetFocusObjectPosition() {
		Vector2 position = focusObject.position;
		position.x += offset;
		return position;
	}
}
