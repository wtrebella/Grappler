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
			pointVectors.Add(point.pointVector);
		}
	}

	private static Segment GetMaxXSegment(List<Point> points) {
		Point leftMost = points[0];
		Point rightMost = points[points.Count - 1];
		foreach (Point point in points) {
			leftMost = point.pointVector.x < leftMost.pointVector.x ? point : leftMost;
			rightMost = point.pointVector.x > rightMost.pointVector.x ? point : rightMost;
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
