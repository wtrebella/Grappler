using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WhitTools {
	public const float PixelsToUnits = 1.0f/32.0f;
	public const float UnitsToPixels = 32.0f;
	public const float GameUnitsToUnityUnits = 50.0f;
	public const float UnityUnitsToGameUnits = 1.0f/50.0f;

	public static void Assert(bool condition, string errorString = "") {
		if (!condition) Debug.LogError(errorString);
	}

	public static Vector2 AngleToDirection(float angle) {
		Vector2 direction = (Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right).ToVector2();
		return direction;
	}

	public static float DirectionToAngle(Vector2 direction) {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return angle;
	}

	// based on algorithm found here: http://www.geometrylab.de/applet-29-en#twopeasants
	public static void SortWithTwoPeasantsPolygonAlgorithm(ref List<Vector2> points) {
		List<Point> pointObjects_preSort = new List<Point>();
		List<Point> pointObjects_postSort = new List<Point>();
		foreach (Vector2 point in points) pointObjects_preSort.Add(new Point(point));
		Point leftMost = pointObjects_preSort[0];
		Point rightMost = pointObjects_preSort[pointObjects_preSort.Count - 1];
		foreach (Point point in pointObjects_preSort) {
			leftMost = point.pointVector.x < leftMost.pointVector.x ? point : leftMost;
			rightMost = point.pointVector.x > rightMost.pointVector.x ? point : rightMost;
		}
	}
}
