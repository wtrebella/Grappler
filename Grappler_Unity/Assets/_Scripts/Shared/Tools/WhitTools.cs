using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class WhitTools {
	public const float PixelsToUnits = 1.0f/32.0f;
	public const float UnitsToPixels = 32.0f;
	public const float GameUnitsToUnityUnits = 50.0f;
	public const float UnityUnitsToGameUnits = 1.0f/50.0f;

	public static bool SpriteDefinitionIsNull(tk2dSpriteDefinition sprite) {
		return sprite == null || sprite.name == "" || sprite.name == "Null";
	}

	public static void Invoke(UnityEvent unityEvent) {
		if (unityEvent != null) unityEvent.Invoke();
	}

	public static void Invoke(UnityEventWithFloat unityEvent, float f) {
		if (unityEvent != null) unityEvent.Invoke(f);
	}

	public static void Invoke(UnityEventWithInt unityEvent, int i) {
		if (unityEvent != null) unityEvent.Invoke(i);
	}

	public static void Invoke<T>(UnityEventWithList<T> unityEvent, List<T> list) {
		if (unityEvent != null) unityEvent.Invoke(list);
	}

	public static bool Assert(bool condition, string errorString = "") {
		if (!condition) Debug.LogError(errorString);
		return condition;
	}

	public static int IncrementWithWrap(int value, IntRange wrapRange) {
		value++;
		if (value >= wrapRange.max) value -= wrapRange.difference;
		return value;
	}

	public static int DecrementWithWrap(int value, IntRange wrapRange) {
		value--;
		if (value <= wrapRange.min) value += wrapRange.difference;
		return value;
	}

	public static Vector2 AngleToDirection(float angle) {
		Vector2 direction = (Vector2)(Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right);
		return direction;
	}

	public static bool IsInLayer(GameObject obj, LayerMask layerMask) {
		int objLayerMask = (1 << obj.layer);
		bool isInLayer = (layerMask.value & objLayerMask) > 0;
		return isInLayer;
	}

	public static bool IsInLayer(GameObject obj, string layerName) {
		bool isInLayer = LayerMask.NameToLayer(layerName) == obj.layer;

		return isInLayer;
	}

	public static bool CompareLayerMasks(LayerMask mask1, LayerMask mask2) {
		bool masksAreTheSame = (mask1.value & mask2.value) > 0;
		return masksAreTheSame;
	}

	public static float DirectionToAngle(Vector2 direction) {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return angle;
	}

	public static Vector2 SlopeToVector2(float slope) {
		Vector2 slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slope * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slope * Mathf.PI / 2f);
		slopeVector.Normalize();
		return slopeVector;
	}

	// based on algorithm found here: http://www.geometrylab.de/applet-29-en#twopeasants
	public static void SortWithTwoPeasantsPolygonAlgorithm(List<Vector2> pointVectors) {
		List<Point> points_preSort = new List<Point>();
		List<Point> points_postSort = new List<Point>();
		foreach (Vector2 pointVector in pointVectors) points_preSort.Add(new Point(pointVector));

		Segment segment = GetMaxXSegment(points_preSort);
		points_preSort.Remove(segment.pointA);
		points_preSort.Remove(segment.pointB);
		points_postSort.Add(segment.pointA);

		Point previousPoint;
		List<Point> pointsAbove;
		List<Point> pointsBelow;

		pointsAbove = GetPointsAboveSegment(points_preSort, segment);
		previousPoint = segment.pointA;
		while (pointsAbove.Count > 0) {
			Point nextPoint = GetPointClosestToPoint(previousPoint, pointsAbove);
			pointsAbove.Remove(nextPoint);
			points_preSort.Remove(nextPoint);
			points_postSort.Add(nextPoint);
			previousPoint = nextPoint;
		}

		points_postSort.Add(segment.pointB);

		pointsBelow = GetPointsBelowSegment(points_preSort, segment);
		previousPoint = segment.pointB;
		while (pointsBelow.Count > 0) {
			Point nextPoint = GetPointClosestToPoint(previousPoint, pointsBelow);
			pointsBelow.Remove(nextPoint);
			points_preSort.Remove(nextPoint);
			points_postSort.Add(nextPoint);
			previousPoint = nextPoint;
		}

		pointVectors.Clear();

		for (int i = 0; i < points_postSort.Count; i++) {
			Point point = points_postSort[i];
			pointVectors.Add(point.vector);
		}
	}

	private static Segment GetMaxXSegment(List<Point> points) {
		Point leftMost = points[0];
		Point rightMost = points[points.Count - 1];
		foreach (Point point in points) {
			leftMost = point.vector.x < leftMost.vector.x ? point : leftMost;
			rightMost = point.vector.x > rightMost.vector.x ? point : rightMost;
		}
		return new Segment(leftMost, rightMost);
	}

	private static List<Point> GetPointsAboveSegment(List<Point> points, Segment segment) {
		var abovePoints = new List<Point>();
		foreach (Point point in points) {
			if (PointIsAboveSegment(point, segment)) abovePoints.Add(point);
		}
		return abovePoints;
	}

	private static List<Point> GetPointsBelowSegment(List<Point> points, Segment segment) {
		var belowPoints = new List<Point>();
		foreach (Point point in points) {
			if (!PointIsAboveSegment(point, segment)) belowPoints.Add(point);
		}
		return belowPoints;
	}

	private static Point GetPointClosestToPoint(Point pointToCheck, List<Point> points) {
		float closestDist = Mathf.Infinity;
		Point closestPoint = null;
		foreach (Point point in points) {
			float dist = Mathf.Abs(pointToCheck.x - point.x);
			if (dist < closestDist) {
				closestDist = dist;
				closestPoint = point;
			}
		}

		return closestPoint;
	}

	private static bool PointIsAboveSegment(Point point, Segment segment) {
		float projectedY = segment.slope * point.x + segment.yIntercept;
		return point.y > projectedY;
	}
}
