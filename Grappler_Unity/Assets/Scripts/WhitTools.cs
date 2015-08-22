using UnityEngine;
using System.Collections;

public static class WhitTools {
	public const float PixelsToUnits = 1.0f/32.0f;
	public const float UnitsToPixels = 32.0f;

	public static void Assert(bool condition, string errorString = "") {
		if (!condition) throw new UnityException(errorString);
	}

	public static Vector2 AngleToDirection(float angle) {
		Vector2 direction = (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right).ToVector2();
		return direction;
	}

	public static float DirectionToAngle(Vector2 direction) {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return angle;
	}
}
