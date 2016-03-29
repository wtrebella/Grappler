using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(GroundChunkMeshCreator))]
public class GroundChunk : GeneratableItem {
	[HideInInspector, NonSerialized] public MountainChunk mountainChunk;

	[SerializeField] private float maxBumpHeight = 0.3f;
	[SerializeField] private float marginSize = 60.0f;

	private List<Point> linePoints;
	private Dictionary<int, float> distances = new Dictionary<int, float>();
	private PolygonCollider2D polygonCollider;
	private GroundChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	public void Reset() {
		linePoints.Clear();
		distances.Clear();
		polygonCollider.points = new Vector2[0];
		slopeVector = Vector2.zero;
	}

	public int GetRandomPointIndex() {
		return UnityEngine.Random.Range(0, linePoints.Count);
	}

	public int GetPointsCount() {
		return linePoints.Count;
	}

	public Point GetLastLinePoint() {
		if (linePoints.Count == 0) Debug.LogError("no line points!");
		return linePoints.GetLastItem();
	}

	public Point GetFirstLinePoint() {
		return linePoints[0];
	}

	public int GetIndexOfLinePoint(Point point) {
		for (int i = 0; i < linePoints.Count; i++) {
			Point tempPoint = linePoints[i];
			if (tempPoint == point) return i;
		}
		Debug.LogError("couldn't find point!");
		return -1;
	}

	public float GetTotalDistance() {
		return distances[linePoints.Count - 1];
	}

	public Point GetLinePoint(int index) {
		if (index < 0 || index >= linePoints.Count) {
			Debug.LogError("invalid line point");
			return null;
		}

		return linePoints[index];
	}

	public float PlaceToDistance(float place) {
		float totalDistance = GetTotalDistance();
		float placeDistance = totalDistance * place;
		return placeDistance;
	}

	public float DistanceToPlace(float distance) {
		return distance / GetTotalDistance();
	}

	public float PointToPlace(Point point) {
		int index = GetIndexOfLinePoint(point);
		float distance = distances[index];
		float place = DistanceToPlace(distance);
		return place;
	}

	public Vector2 PlaceToPosition(float place) {
		place = Mathf.Clamp01(place);
		float totalDistance = GetTotalDistance();
		float placeDistance = totalDistance * place;
		int firstPointIndex = GetFirstPointIndexAtDistance(placeDistance);
		int secondPointIndex = firstPointIndex+1;
		if (firstPointIndex >= linePoints.Count - 1) Debug.LogError("distance greater than end of cliff line...");
		float thisPointDistance = distances[firstPointIndex];
		float nextPointDistance = distances[secondPointIndex];
		float deltaDistance = placeDistance - thisPointDistance;
		float betweenPointsDistance = nextPointDistance - thisPointDistance;
		float pointLerp = deltaDistance / betweenPointsDistance;
		return GetPositionBetweenLinePoints(firstPointIndex, secondPointIndex, pointLerp);
	}

	public float GetAverageYAtX(float x) {
		Vector2 firstPoint = GetFirstLinePoint().pointVector;
		Vector2 lastPoint = GetLastLinePoint().pointVector;
		float width = lastPoint.x - firstPoint.x;
		float xDelta = x - firstPoint.x;
		float percent = xDelta / width;
		Vector2 position = Vector2.Lerp(firstPoint, lastPoint, percent);
		return position.y;
	}

	public Vector2 GetPositionBetweenLinePoints(int indexA, int indexB, float lerp) {
		lerp = Mathf.Clamp01(lerp);
		Point pointA = GetLinePoint(indexA);
		Point pointB = GetLinePoint(indexB);
		return Vector2.Lerp(pointA.pointVector, pointB.pointVector, lerp);
	}

	public List<Point> GetListOfLinePoints() {
		return linePoints;
	}
	
	public void Generate(MountainChunk mountainChunk, GroundChunk previousGroundChunk, float distanceFromMountain) {
		Reset();

		slopeVector = (mountainChunk.GetLastLinePoint().pointVector - mountainChunk.GetFirstLinePoint().pointVector).normalized;
	
		List<Vector2> points = new List<Vector2>();
		GenerateShape(points, mountainChunk, previousGroundChunk, distanceFromMountain);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
		CalculateDistances();
	}

	private void GenerateShape(List<Vector2> points, MountainChunk mountainChunk, GroundChunk previousGroundChunk, float distanceFromMountain) {
		float directDistFromMountain = distanceFromMountain;
		Vector2 mountainVector = mountainChunk.GetLastLinePoint().pointVector - mountainChunk.GetFirstLinePoint().pointVector;
		Vector2 slopePerp = new Vector2(slopeVector.y, -slopeVector.x);
		int numPoints = mountainChunk.GetMacroLinePoints().Count;
		float totalDistance = mountainVector.magnitude;
		float distPerPoint = totalDistance / (numPoints - 1);
		float angleFromMountain = Vector2.Angle(Vector2.down, slopePerp * directDistFromMountain);
		float verticalDistFromMountain = directDistFromMountain / Mathf.Cos(angleFromMountain * Mathf.Deg2Rad);
		Vector2 verticalVectorFromMountain = new Vector2(0, -verticalDistFromMountain);

		Vector2 origin = mountainChunk.GetFirstLinePoint().pointVector + verticalVectorFromMountain;
		Vector2 prevPoint = origin;
		points.Add(prevPoint);

		for (int i = 0; i < numPoints; i++) {
			Vector2 delta = slopeVector * distPerPoint;
			Vector2 point = prevPoint + delta;
			points.Add(point);
			prevPoint = point;
		}

		// make the first point of any new chunk the same as the last point of the previous chunk
		if (previousGroundChunk != null) {
			Vector2 previousFirstPoint = previousGroundChunk.GetLastLinePoint().pointVector;
			Vector2 fourthPoint = points[3];
			Vector2 vector = fourthPoint - previousFirstPoint;
			Vector2 direction = vector.normalized;
			float dist = vector.magnitude;
			float distPer = dist / 3;
			points[0] = previousFirstPoint;
			points[1] = points[0] + direction * distPer;
			points[2] = points[1] + direction * distPer;
		}

		for (int i = 4; i < points.Count; i++) {
			Vector2 point = points[i];
			point.y += UnityEngine.Random.Range(-maxBumpHeight, maxBumpHeight);
			points[i] = point;
		}
		
		foreach (Vector2 point in points) {
			Point pointObject = new Point(point);
			linePoints.Add(pointObject);
		}

		// add one more point, this time with margin
		Vector2 lastPoint = points[points.Count - 1];
		Vector2 lastPointWithMargin = lastPoint;
		lastPointWithMargin.x += marginSize;
		points.Add(lastPointWithMargin);
		prevPoint = lastPoint;

		int extraDepthOnBottom = -30;
		Vector2 first = points[0];
		points.Add(new Vector2(lastPointWithMargin.x, first.y + extraDepthOnBottom));
		points.Add(new Vector2(first.x, first.y + extraDepthOnBottom));
		points.Add(new Vector2(first.x, first.y - 1));
	}

	private void Awake() {
		linePoints = new List<Point>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<GroundChunkMeshCreator>();
	}

	private void CalculateDistances() {
		distances.Add(0, 0);
		for (int i = 1; i < linePoints.Count; i++) {
			float previousDistance = distances[i-1];
			Vector2 pointA = linePoints[i-1].pointVector;
			Vector2 pointB = linePoints[i].pointVector;
			float deltaDistance = (pointB - pointA).magnitude;
			float distance = previousDistance + deltaDistance;
			distances.Add(i, distance);
		}
	}

	private int GetFirstPointIndexAtDistance(float distance) {
		int index = 0;

		for (int i = 1; i < linePoints.Count; i++) {
			float tempDistance = distances[i];
			if (tempDistance >= distance) {
				index = i-1;
				break;
			}
		}

		return index;
	}
}
