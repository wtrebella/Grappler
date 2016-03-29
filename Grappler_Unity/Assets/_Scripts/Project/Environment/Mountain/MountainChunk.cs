using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : GeneratableItem {
	public Vector2 origin {get; private set;}
	public MountainChunk previousMountainChunk {get; private set;}
	public List<Point> edgePoints {get; private set;}

	[SerializeField] private int numPoints = 10;
	[SerializeField] private float slopeChange = 0.15f;
	[SerializeField] private float maxPerpDist = 0.75f;
	[SerializeField] private float marginSize = 0.01f;
	[SerializeField] private FloatRange slopeRange = new FloatRange(0.0f, 0.35f);
	[SerializeField] private FloatRange pointDistRange = new FloatRange(1.5f, 2.5f);

	private Dictionary<int, float> distances = new Dictionary<int, float>();
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;
	private float slopeVal;



	// ============= PUBLICS ==============
	public void Initialize(Vector2 origin, MountainChunk previousMountainChunk) {
		this.origin = origin;
		this.previousMountainChunk = previousMountainChunk;

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
		Vector2 firstPoint = GetFirstEdgePoint().pointVector;
		Vector2 lastPoint = GetLastEdgePoint().pointVector;
		float width = lastPoint.x - firstPoint.x;
		float xDelta = x - firstPoint.x;
		float percent = xDelta / width;
		Vector2 position = Vector2.Lerp(firstPoint, lastPoint, percent);
		return position.y;
	}



	// ============= GENERATION ==============
	private void Awake () {
		edgePoints = new List<Point>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
	}

	protected override void HandleRecycled() {
		base.HandleRecycled();
		Reset();
	}

	private void Generate() {
		List<Vector2> points = new List<Vector2>();

		Reset();
		CalculateSlope();
		GenerateBasicShape(points);
		RandomizeEdge(points);
		InitMesh(points);
		CalculateDistances();
	}

	private void Reset() {
		edgePoints.Clear();
		distances.Clear();
		polygonCollider.points = new Vector2[0];
		slopeVector = Vector2.zero;
	}

	private void CalculateSlope() {
		float previousSlopeVal;
		if (previousMountainChunk == null) previousSlopeVal = slopeRange.GetRandom();
		else previousSlopeVal = previousMountainChunk.slopeVal;
		float minChange = Mathf.Max(slopeRange.min - previousSlopeVal, -slopeChange);
		float maxChange = Mathf.Min(slopeRange.max - previousSlopeVal, slopeChange);
		slopeVal = Mathf.Clamp(previousSlopeVal + UnityEngine.Random.Range(minChange, maxChange), slopeRange.min, slopeRange.max);
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slopeVal * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slopeVal * Mathf.PI / 2f);
	}

	private void GenerateBasicShape(List<Vector2> points) {
		Vector2 prevPoint = origin;
		points.Add(prevPoint);

		for (int i = 0; i < numPoints; i++) {
			float dist = pointDistRange.GetRandom();
			Vector2 delta = slopeVector * dist;
			Vector2 point = prevPoint + delta;
			points.Add(point);
			prevPoint = point;
		}

		foreach (Vector2 point in points) {
			Point pointObject = new Point(point);
			edgePoints.Add(pointObject);
		}

		int extraHeightOnTop = 30;
		if (SlopeIsPositive()) {
			points.Add(new Vector2(prevPoint.x, prevPoint.y + extraHeightOnTop));
			points.Add(new Vector2(origin.x - marginSize, prevPoint.y + extraHeightOnTop));
			points.Add(new Vector2(origin.x - marginSize, origin.y));
		}
		else {
			points.Add(new Vector2(prevPoint.x, origin.y + extraHeightOnTop));
			points.Add(new Vector2(origin.x - marginSize, origin.y + extraHeightOnTop));
			points.Add(new Vector2(origin.x - marginSize, origin.y));
		}
	}

	private void RandomizeEdge(List<Vector2> points) {
		Vector2 firstPoint = edgePoints[0].pointVector;
		Vector2 lastPoint = edgePoints.GetLastItem().pointVector;
		Vector2 slopeVectorPerp = new Vector2(slopeVector.y, -slopeVector.x);
		float clampVal = 0.5f;

		for (int i = 1; i < edgePoints.Count - 1; i++) {
			Vector2 point = points[i];
			Vector2 tempPoint = point;
			float perpDist = 0;

			if (i == 1) {
				do {
					perpDist = UnityEngine.Random.Range(-maxPerpDist, maxPerpDist);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y < firstPoint.y);
			}
			else if (i == edgePoints.Count - 2) {
				do {
					perpDist = UnityEngine.Random.Range(-maxPerpDist, maxPerpDist);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y > lastPoint.y);
			}
			else perpDist = UnityEngine.Random.Range(-maxPerpDist, maxPerpDist);

			point += slopeVectorPerp * perpDist;

			Vector2 clampPoint;
			if (SlopeIsPositive()) clampPoint = lastPoint;
			else clampPoint = firstPoint;
			if (point.y >= clampPoint.y) point.y = clampPoint.y - clampVal;

			points[i] = point;
			edgePoints[i].pointVector = point;
		}
	}
		
	private void InitMesh(List<Vector2> points) {
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
	}

	private void CalculateDistances() {
		distances.Add(0, 0);
		for (int i = 1; i < edgePoints.Count; i++) {
			float previousDistance = distances[i-1];
			Vector2 pointA = edgePoints[i-1].pointVector;
			Vector2 pointB = edgePoints[i].pointVector;
			float deltaDistance = (pointB - pointA).magnitude;
			float distance = previousDistance + deltaDistance;
			distances.Add(i, distance);
		}
	}


	// ============= HELPERS ==============
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
		return Vector2.Lerp(pointA.pointVector, pointB.pointVector, lerp);
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
