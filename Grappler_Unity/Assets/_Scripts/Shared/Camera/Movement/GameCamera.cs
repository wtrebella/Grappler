using UnityEngine;
using System.Collections;
using System;
using WhitTerrain;

public enum CameraFollowMode {
	NONE,
	ContourAndPlayer,
	JustPlayer,
	MAX
}

public class GameCamera : MonoBehaviour {
	[SerializeField] private float offset = 10;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private Transform focusObject;
	[SerializeField] private Path path;
	[SerializeField] private Camera cam;
	[SerializeField] private float pathMargin = 10;
	[SerializeField] private float playerMargin = 10;
	[SerializeField] private float newTerrainSwitchMargin = -20;

	private CameraFollowMode cameraFollowMode = CameraFollowMode.JustPlayer;
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

	// TODO cleanup
	private Vector3 GetTargetPosition() {
		Vector3 objectPosition = GetFocusObjectPosition();

		if (cameraFollowMode == CameraFollowMode.ContourAndPlayer) {
			Vector2 topContourPoint = path.topContour.GetPointAtX(objectPosition.x);
			Vector2 bottomCamPoint = path.bottomContour.GetPointAtX(objectPosition.x);
			Vector2 topCamPoint = objectPosition.y > topContourPoint.y ? (Vector2)objectPosition : topContourPoint;
			Vector2 camPoint = WhitTools.GetAveragePoint(topCamPoint, bottomCamPoint);

			Vector3 contourCenterPoint = (Vector3)camPoint;
			contourCenterPoint.z = GetTargetZ();
			return contourCenterPoint;
		}
		else if (cameraFollowMode == CameraFollowMode.JustPlayer) {
			Vector3 newPos = objectPosition;
			newPos.z = transform.position.z;
			return newPos;
		}

		else return transform.position;
	}

	// TODO cleanup
	private float GetTargetZ() {
		Vector3 objectPosition = GetFocusObjectPosition();
		Vector2 topContourPoint = path.topContour.GetPointAtX(objectPosition.x);
		Vector2 bottomCamPoint = path.bottomContour.GetPointAtX(objectPosition.x);
		Vector2 topCamPoint = objectPosition.y > topContourPoint.y ? (Vector2)objectPosition : topContourPoint;

		float terrainWidth = topCamPoint.y - bottomCamPoint.y;// path.GetWidthAtX(x);
		float targetFrustumHeight = terrainWidth + pathMargin * 2;
		float distance = cam.GetDistanceAtFrustumHeight(targetFrustumHeight);
		return -distance;
	}

	private void UpdateCameraFollowMode() {
		if (cameraFollowMode == CameraFollowMode.ContourAndPlayer) {
			if (ShouldSwitchToPlayerFollowMode()) cameraFollowMode = CameraFollowMode.JustPlayer;
		}
		else if (cameraFollowMode == CameraFollowMode.JustPlayer) {
			if (ShouldSwitchToTerrainFollowMode()) cameraFollowMode = CameraFollowMode.ContourAndPlayer;
		}
	}

	private bool ShouldSwitchToPlayerFollowMode() {
		return path.HasEnd() && path.GetXIsPastEnd(GetFocusObjectPosition().x);
	}

	private bool ShouldSwitchToTerrainFollowMode() {
		return !path.HasEnd() && path.GetXIsPastStart(GetFocusObjectPosition().x + newTerrainSwitchMargin);
	}
		
	private Vector2 GetFocusObjectPosition() {
		Vector2 position = focusObject.position;
		position.x += offset;
		position.y += playerMargin;
		return position;
	}
}