using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(TriangulatedMesh))]
public class MountainChunk : GeneratableItem {
	public Vector2 origin {get; private set;}
	public MountainChunk previousMountainChunk {get; private set;}
	public List<Point> edgePoints {get; private set;}
	public List<Point> finishingPoints {get; private set;}
	public List<Point> allPoints {get; private set;}

	private MountainChunkAttributes attributes;
	private Dictionary<int, float> distances = new Dictionary<int, float>();
	private PolygonCollider2D polygonCollider;
	private TriangulatedMesh mesh;
	private Vector2 slopeVector;
	private float slopeVal;
	private float extraHeightOnTop = 30;



	// ============= PUBLICS ==============
	public void Initialize(Vector2 origin, MountainChunk previousMountainChunk, MountainChunkAttributes attributes) {
		this.origin = origin;
		this.previousMountainChunk = previousMountainChunk;
		this.attributes = attributes;

		Generate();
	}

	public Point GetFirstEdgePoint() {
		return edgePoints[0];
	}

	public Point GetLastEdgePoint() {
		return edgePoints.GetLastItem();
	}

	public Vector2 PlaceToPosition(float place) {
		place = Mathf.Clamp01(place);
		float placeDistance = PlaceToDistance(place);

		int leftPointIndex = GetFirstPointIndexAtDistance(placeDistance);
		int rightPointIndex = leftPointIndex + 1;

		float leftPointDistance = GetDistanceAtIndex(leftPointIndex);
		float segmentDistance = GetDistanceBetweenIndices(leftPointIndex, rightPointIndex);

		float leftPointToPlaceDistance = placeDistance - leftPointDistance;
		float percent = leftPointToPlaceDistance / segmentDistance;
		Vector2 position = GetPositionBetweenEdgePointIndices(leftPointIndex, rightPointIndex, percent);
		return position;
	}

	public float GetAverageYAtX(float x) {
		Vector2 firstPoint = GetFirstEdgePoint().vector;
		Vector2 lastPoint = GetLastEdgePoint().vector;
		float width = lastPoint.x - firstPoint.x;
		float xDelta = x - firstPoint.x;
		float percent = xDelta / width;
		Vector2 position = Vector2.Lerp(firstPoint, lastPoint, percent);
		return position.y;
	}



	// ============= GENERATION ==============
	private void Awake() {
		allPoints = new List<Point>();
		edgePoints = new List<Point>();
		finishingPoints = new List<Point>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		mesh = GetComponent<TriangulatedMesh>();
	}

	protected override void HandleRecycled() {
		base.HandleRecycled();
		Reset();
	}

	private void Generate() {
		Reset();
		CalculateSlope();
		GenerateEdgePoints();
		GenerateFinishingPoints();
		RandomizeEdge();
		FillAllPointsList();
		CalculateDistances();
		InitMesh();
	}

	private void Reset() {
		edgePoints.Clear();
		finishingPoints.Clear();
		allPoints.Clear();
		distances.Clear();
		polygonCollider.points = new Vector2[0];
		slopeVector = Vector2.zero;
	}

	private void CalculateSlope() {
		float previousSlopeVal;
		if (previousMountainChunk == null) previousSlopeVal = attributes.slopeRange.GetRandom();
		else previousSlopeVal = previousMountainChunk.slopeVal;
		float minChange = Mathf.Max(attributes.slopeRange.min - previousSlopeVal, -attributes.maxSlopeChange);
		float maxChange = Mathf.Min(attributes.slopeRange.max - previousSlopeVal, attributes.maxSlopeChange);
		float unclampedSlopeVal = previousSlopeVal + UnityEngine.Random.Range(minChange, maxChange);
		slopeVal = attributes.slopeRange.Clamp(unclampedSlopeVal);
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slopeVal * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slopeVal * Mathf.PI / 2f);
	}

	private void GenerateEdgePoints() {
		Vector2 prevPoint = origin;
		AddEdgePoint(prevPoint);

		for (int i = 0; i < attributes.numPoints; i++) {
			float dist = attributes.pointDistRange.GetRandom();
			Vector2 delta = slopeVector * dist;
			Vector2 point = prevPoint + delta;
			AddEdgePoint(point);
			prevPoint = point;
		}
	}

	private void GenerateFinishingPoints() {
		if (SlopeIsPositive()) GenerateFinishingPointsPositiveSlope();
		else GenerateFinishingPointsNegativeSlope();
	}

	private void GenerateFinishingPointsPositiveSlope() {
		Vector2 lastEdgePoint = GetLastEdgePoint().vector;

		AddFinishingPoint(new Vector2(lastEdgePoint.x, lastEdgePoint.y + extraHeightOnTop));
		AddFinishingPoint(new Vector2(origin.x - attributes.marginSize, lastEdgePoint.y + extraHeightOnTop));
		AddFinishingPoint(new Vector2(origin.x - attributes.marginSize, origin.y));
	}

	private void GenerateFinishingPointsNegativeSlope() {
		Vector2 lastEdgePoint = GetLastEdgePoint().vector;

		AddFinishingPoint(new Vector2(lastEdgePoint.x, origin.y + extraHeightOnTop));
		AddFinishingPoint(new Vector2(origin.x - attributes.marginSize, origin.y + extraHeightOnTop));
		AddFinishingPoint(new Vector2(origin.x - attributes.marginSize, origin.y));
	}

	private void FillAllPointsList() {
		allPoints.AddAll<Point>(edgePoints);
		allPoints.AddAll<Point>(finishingPoints);
	}

	private void RandomizeEdge() {
		List<Point> innerEdgePoints = GetInnerEdgePoints();

		Point firstEdgePoint = GetFirstEdgePoint();
		Point lastEdgePoint = GetLastEdgePoint();

		Vector2 slopePerpendicular = GetSlopePerpendicular();

		float clampVal = 0.5f;

		foreach (Point point in innerEdgePoints) {
			Vector2 tempVector = Vector2.zero;
			float magnitude = 0;

			do {
				magnitude = UnityEngine.Random.Range(-attributes.maxPerpDist, attributes.maxPerpDist);
				tempVector = point.vector + slopePerpendicular * magnitude;
			} 
			while (tempVector.y < firstEdgePoint.y && tempVector.y > lastEdgePoint.y);

			point.vector += slopePerpendicular * magnitude;

			Point clampPoint;

			if (SlopeIsPositive()) clampPoint = lastEdgePoint;
			else clampPoint = firstEdgePoint;

			if (point.y >= clampPoint.y) point.y = clampPoint.y - clampVal;
		}
	}
		
	private void InitMesh() {
		var pointsArray = allPoints.ToVector2Array();
		polygonCollider.points = pointsArray;
		mesh.RedrawMesh(pointsArray);
	}

	private void CalculateDistances() {
		distances.Add(0, 0);
		for (int i = 1; i < edgePoints.Count; i++) {
			float previousDistance = distances[i-1];
			Vector2 pointA = edgePoints[i-1].vector;
			Vector2 pointB = edgePoints[i].vector;
			float deltaDistance = (pointB - pointA).magnitude;
			float distance = previousDistance + deltaDistance;
			distances.Add(i, distance);
		}
	}

	// ============= HELPERS ==============
	private List<Point> GetInnerEdgePoints() {
		var innerEdgePoints = edgePoints.Copy<Point>();
		innerEdgePoints.RemoveAt(0);
		innerEdgePoints.RemoveAt(innerEdgePoints.Count - 1);
		return innerEdgePoints;
	}

	private Vector2 GetSlopePerpendicular() {
		return new Vector2(slopeVector.y, -slopeVector.x);
	}

	private void AddEdgePoint(Vector2 pointVector) {
		edgePoints.Add(new Point(pointVector));
	}

	private void AddFinishingPoint(Vector2 pointVector) {
		finishingPoints.Add(new Point(pointVector));
	}

	private void AddAllPoint(Vector2 pointVector) {
		allPoints.Add(new Point(pointVector));
	}

	private float GetDistanceAtIndex(int index) {
		float distance;
		if (distances.TryGetValue(index, out distance)) return distance;
		else {
			Debug.LogError("index " + index + " is not in distance dictionary");
			return 0;
		}
	}

	private float GetDistanceBetweenIndices(int indexA, int indexB) {
		float distanceA = GetDistanceAtIndex(indexA);
		float distanceB = GetDistanceAtIndex(indexB);
		float segmentDistance = distanceB - distanceA;
		return segmentDistance;
	}

	private bool SlopeIsPositive() {
		return slopeVector.y > 0;
	}

	private int GetIndexOfEdgePoint(Point point) {
		for (int i = 0; i < edgePoints.Count; i++) {
			Point tempPoint = edgePoints[i];
			if (tempPoint == point) return i;
		}
		Debug.LogError("couldn't find point!");
		return -1;
	}

	private float GetTotalDistance() {
		return distances[edgePoints.Count - 1];
	}

	private Point GetEdgePoint(int index) {
		if (index < 0 || index >= edgePoints.Count) {
			Debug.LogError("invalid edge point");
			return null;
		}

		return edgePoints[index];
	}

	private float PlaceToDistance(float place) {
		float totalDistance = GetTotalDistance();
		float placeDistance = totalDistance * place;
		return placeDistance;
	}

	private float DistanceToPlace(float distance) {
		return distance / GetTotalDistance();
	}

	private float PointToPlace(Point point) {
		int index = GetIndexOfEdgePoint(point);
		float distance = distances[index];
		float place = DistanceToPlace(distance);
		return place;
	}

	private Vector2 GetPositionBetweenEdgePointIndices(int indexA, int indexB, float lerp) {
		lerp = Mathf.Clamp01(lerp);
		Point pointA = GetEdgePoint(indexA);
		Point pointB = GetEdgePoint(indexB);
		return Vector2.Lerp(pointA.vector, pointB.vector, lerp);
	}

	private int GetFirstPointIndexAtDistance(float distance) {
		int index = 0;

		for (int i = 1; i < edgePoints.Count; i++) {
			float tempDistance = distances[i];
			if (tempDistance >= distance) {
				index = i-1;
				break;
			}
		}

		return index;
	}
}
