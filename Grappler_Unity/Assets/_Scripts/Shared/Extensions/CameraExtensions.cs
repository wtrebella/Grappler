using UnityEngine;
using System.Collections;

public static class CameraExtensions {
	public static float GetFrustrumHeight(this Camera camera, float distance) {
		return WhitTools.GetFrustumHeight(camera, distance);
	}

	public static float GetFrustrumWidth(this Camera camera, float distance) {
		return WhitTools.GetFrustumWidth(camera, distance);
	}

	public static float GetDistanceAtFrustumHeight(this Camera camera, float frustumHeight) {
		return WhitTools.GetDistanceAtFrustumHeight(camera, frustumHeight);
	}

	public static float GetDistanceAtFrustumWidth(this Camera camera, float frustumWidth) {
		return WhitTools.GetDistanceAtFrustumWidth(camera, frustumWidth);
	}

	public static float GetFieldOfView(this Camera camera, float distance, float frustumHeight) {
		return WhitTools.GetFieldOfView(camera, distance, frustumHeight);
	}
}
