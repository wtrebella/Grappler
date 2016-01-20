using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : MonoBehaviour {
	public Vector2 origin {get; private set;}

	[SerializeField] private int numPoints = 30;
	[SerializeField] private float bumpWidthAvg = 0.01f;
	[SerializeField] private float bumpWidthVar = 0.005f;
	[SerializeField] private float maxBumpHeight = 0.1f;
	[SerializeField] private float avgSlope = 0.65f;
	[SerializeField] private float slopeVar = 0.25f;
	[SerializeField] private float pointDist = 2.5f;
	[SerializeField] private float pointDistVar = 1.2f;
	[SerializeField] private float perpDistVar = 2.8f;
	[SerializeField] private float marginSize = 60.0f;

	private List<Point> macroLinePoints;
	private List<Point> linePoints;
	private Dictionary<int, float> distances;
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	public int GetRandomPointIndex() {
		return UnityEngine.Random.Range(0, linePoints.Count);
	}

	public int GetPointsCount() {
		return linePoints.Count;
	}

	public Point GetLastLinePoint() {
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

	public float GetPlaceAtY(float y) {
		int index = GetIndexOfNearestLinePointBelowY(y);
		Point pointA = linePoints[index];
		Point pointB = linePoints[index+1];
		float placeAtPointA = GetPlaceAtPoint(pointA);
		float placeAtPointB = GetPlaceAtPoint(pointB);
		float segDistY = pointB.y - pointA.y;
		float deltaY = y - pointA.y;
		float lerp = deltaY / segDistY;
		float place = Mathf.Lerp(placeAtPointA, placeAtPointB, lerp);
		return place;
	}

	public float GetDistanceFromPlace(float place) {
		float totalDistance = GetTotalDistance();
		float placeDistance = totalDistance * place;
		return placeDistance;
	}

	public float GetPlaceFromDistance(float distance) {
		return distance / GetTotalDistance();
	}

	public float GetPlaceAtPoint(Point point) {
		int index = GetIndexOfLinePoint(point);
		float distance = distances[index];
		float place = GetPlaceFromDistance(distance);
		return place;
	}

	public Vector2 GetPositionFromPlace(float place) {
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

	public Vector2 GetPositionBetweenLinePoints(int indexA, int indexB, float lerp) {
		lerp = Mathf.Clamp01(lerp);
		Point pointA = GetLinePoint(indexA);
		Point pointB = GetLinePoint(indexB);
		return Vector2.Lerp(pointA.pointVector, pointB.pointVector, lerp);
	}
	
	public void Generate(Vector2 origin) {
		List<Vector2> points = new List<Vector2>();
		GenerateBasicShape(points, origin);
		MacroRandomizeEdges(points);
		MicroRandomizeEdges(points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
		CalculateDistances();
		this.origin = origin;
	}

	public List<Point> GetListOfLinePoints() {
		return linePoints;
	}

	public List<Point> GetListOfMacroLinePoints() {
		return macroLinePoints;
	}

	private void Awake () {
		linePoints = new List<Point>();
		macroLinePoints = new List<Point>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		float slopeVal = avgSlope + UnityEngine.Random.Range(-slopeVar, slopeVar);
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slopeVal * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slopeVal * Mathf.PI / 2f);
	}

	private int GetIndexOfNearestLinePointBelowY(float y) {
		if (y < linePoints[0].y) return 0;
		for (int i = 0; i < linePoints.Count - 1; i++) {
			Point pointA = linePoints[i];
			Point pointB = linePoints[i+1];
			if (y >= pointA.y && y <= pointB.y) return i;
		}
		Debug.LogError("didn't find point below y: " + y);
		return -1;
	}

	private void CalculateDistances() {
		distances = new Dictionary<int, float>();
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

	private void GenerateBasicShape(List<Vector2> points, Vector2 origin) {
		Vector2 prevPoint = origin;
		points.Add(prevPoint);
		
		for (int i = 0; i < numPoints; i++) {
			float dist = pointDist + UnityEngine.Random.Range(-pointDistVar, pointDistVar);
			Vector2 delta = slopeVector * dist;
			Vector2 point = prevPoint + delta;
			points.Add(point);

			prevPoint = point;
		}

		foreach (Vector2 point in points) {
			Point pointObject = new Point(point);
			linePoints.Add(pointObject);
			macroLinePoints.Add(pointObject);
		}

		int add = 10;
		points.Add(new Vector2(prevPoint.x, prevPoint.y + add));
		points.Add(new Vector2(origin.x - marginSize, prevPoint.y + add));
		points.Add(new Vector2(origin.x - marginSize, origin.y));
	}

	private void MacroRandomizeEdges(List<Vector2> points) {
		Vector2 firstPoint = linePoints[0].pointVector;
		Vector2 lastPoint = linePoints.GetLastItem().pointVector;
		Vector2 slopeVectorPerp = new Vector2(slopeVector.y, -slopeVector.x);
		float clampVal = 0.5f;
		for (int i = 1; i < linePoints.Count - 1; i++) {
			Vector2 point = points[i];
			Vector2 tempPoint = point;
			float perpDist;

			if (i == 1) {
				do {
					perpDist = UnityEngine.Random.Range(-perpDistVar, perpDistVar);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y < firstPoint.y);
			}
			else if (i == linePoints.Count - 2) {
				do {
					perpDist = UnityEngine.Random.Range(-perpDistVar, perpDistVar);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y > lastPoint.y);
			}
			else perpDist = UnityEngine.Random.Range(-perpDistVar, perpDistVar);

			point += slopeVectorPerp * perpDist;
			if (point.y >= lastPoint.y) point.y = lastPoint.y - clampVal;
			points[i] = point;
			linePoints[i].pointVector = point;
			macroLinePoints[i].pointVector = point;
		}
	}

	private void MicroRandomizeEdges(List<Vector2> points) {
		Vector2 lastPoint = linePoints.GetLastItem().pointVector;
		float clampVal = 0.5f;

		for (int j = 0; j < linePoints.Count - 1; j++) {
			Vector2 pointA = linePoints[j].pointVector;
			Vector2 pointB = linePoints[j+1].pointVector;
			float segmentMagnitude = (pointB - pointA).magnitude;
			Vector2 segmentDirection = (pointB - pointA).normalized;
			Vector2 segmentDirectionPerp = new Vector2(segmentDirection.y, -segmentDirection.x);
			float bumpWidth = bumpWidthAvg + UnityEngine.Random.Range(-bumpWidthVar, bumpWidthVar);
			float bumpHeight = UnityEngine.Random.Range(-maxBumpHeight, maxBumpHeight);
			int numBumps = (int)(segmentMagnitude / bumpWidth);
			bumpWidth = segmentMagnitude / (numBumps + 1);
			for (int i = 1; i < numBumps; i++) {
				Vector2 bumpPoint = pointA + segmentDirection * (i * bumpWidth);
				bumpPoint += segmentDirectionPerp * bumpHeight;
				if (bumpPoint.y >= lastPoint.y) bumpPoint.y = lastPoint.y - clampVal;
				linePoints.Insert(j+i, new Point(bumpPoint));
				points.Insert(j+i, bumpPoint);
			}
		}
	}
}
