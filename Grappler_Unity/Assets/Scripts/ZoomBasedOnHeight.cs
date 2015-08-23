using UnityEngine;
using System.Collections;

public enum ZoomUpdateType {
	Update,
	FixedUpdate
}

public enum ZoomMovementType {
	Smoothdamp,
	Immediate
}

public class ZoomBasedOnHeight : MonoBehaviour {
	[SerializeField] private Transform objectWithDependentHeight;
	[SerializeField] private ZoomUpdateType updateType = ZoomUpdateType.Update;
	[SerializeField] private ZoomMovementType movementType = ZoomMovementType.Immediate;
	[SerializeField] private float smoothDampTime = 0.13f;
	[SerializeField] float maxZoom = -30;
	[SerializeField] float zoomRatio = 1;

	private Vector3 smoothDampVelocity;

	private void Update() {
		if (updateType == ZoomUpdateType.Update) UpdateZoom();
	}

	private void FixedUpdate() {
		if (updateType == ZoomUpdateType.FixedUpdate) UpdateZoom();
	}

	private void UpdateZoom() {
		if (movementType == ZoomMovementType.Immediate) UpdateZoomImmediate();
		else if (movementType == ZoomMovementType.Smoothdamp) UpdateZoomSmoothDamp();
	}

	private void UpdateZoomImmediate() {
		transform.position = GetTargetPosition();
	}

	private void UpdateZoomSmoothDamp() {
		transform.position = GetSmoothedTargetPosition();
	}

	private float GetObjectHeight() {
		return objectWithDependentHeight.position.y;
	}

	private Vector3 GetTargetPosition() {
		Vector3 targetPosition = transform.position;
		float targetZ = maxZoom - zoomRatio * GetObjectHeight();
		targetZ = Mathf.Min(targetZ, maxZoom);
		targetPosition.z = targetZ;
		return targetPosition;
	}

	private Vector3 GetSmoothedTargetPosition() {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref smoothDampVelocity, smoothDampTime);
		return smoothedPosition;
	}
}
