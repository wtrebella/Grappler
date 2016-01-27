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
	[SerializeField] private FloatRange slopeRange = new FloatRange(0.08f, 0.32f);
	[SerializeField] private float maxSlopeChange = 0.02f;
	[SerializeField] private float pointDist = 2.5f;
	[SerializeField] private float pointDistVar = 1.2f;
	[SerializeField] private float perpDistVar = 2.8f;
	[SerializeField] private float marginSize = 60.0f;

	private List<Point> macroLinePoints;
	private List<Point> linePoints;
	private Dictionary<int, float> distances = new Dictionary<int, float>();
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;
	private float slopeVal;

	public void Reset() {
		macroLinePoints.Clear();
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

	public int GetMacroPointsCount() {
		return macroLinePoints.Count;
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
		float placeAtPointA = PointToPlace(pointA);
		float placeAtPointB = PointToPlace(pointB);
		float segDistY = pointB.y - pointA.y;
		float deltaY = y - pointA.y;
		float lerp = deltaY / segDistY;
		float place = Mathf.Lerp(placeAtPointA, placeAtPointB, lerp);
		return place;
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

	public float GetSlopeVal() {
		return slopeVal;
	}
	
	public void Generate(Vector2 origin, MountainChunk previousMountainChunk) {
		Reset();
		List<Vector2> points = new List<Vector2>();
		float previousSlopeVal;
		if (previousMountainChunk == null) previousSlopeVal = slopeRange.GetRandom();
		else previousSlopeVal = previousMountainChunk.slopeVal;
		CalculateSlope(previousSlopeVal);
		GenerateBasicShape(points, origin);
		MacroRandomizeEdges(points);
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

	public bool SlopeIsPositive() {
		return slopeVector.y > 0;
	}

	private void OnDisable() {
		RecycleAllIcicles();
		RecycleAllAnchorables();
	}

	private void CalculateSlope(float previousSlopeVal) {
		float minChange = Mathf.Max(slopeRange.min - previousSlopeVal, -maxSlopeChange);
		float maxChange = Mathf.Min(slopeRange.max - previousSlopeVal, maxSlopeChange);
		slopeVal = Mathf.Clamp(previousSlopeVal + UnityEngine.Random.Range(minChange, maxChange), slopeRange.min, slopeRange.max);
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slopeVal * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slopeVal * Mathf.PI / 2f);
	}

	private void Awake () {
		linePoints = new List<Point>();
		macroLinePoints = new List<Point>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
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

	private void RecycleAllIcicles() {
		var icicles = GetComponentsInChildren<SmallIcicle>();
		foreach (SmallIcicle icicle in icicles) icicle.Recycle();
	}

	private void RecycleAllAnchorables() {
		var anchorables = GetComponentsInChildren<Anchorable>();
		foreach (Anchorable anchorable in anchorables) anchorable.Recycle();
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

			Vector2 clampPoint;
			if (SlopeIsPositive()) clampPoint = lastPoint;
			else clampPoint = firstPoint;
			if (point.y >= clampPoint.y) point.y = clampPoint.y - clampVal;

			points[i] = point;
			linePoints[i].pointVector = point;
			macroLinePoints[i].pointVector = point;
		}
	}

	private void MicroRandomizeEdges(List<Vector2> points) {
		Vector2 firstPoint = linePoints[0].pointVector;
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

				Vector2 clampPoint;
				if (SlopeIsPositive()) clampPoint = lastPoint;
				else clampPoint = firstPoint;
				if (bumpPoint.y >= clampPoint.y) bumpPoint.y = clampPoint.y - clampVal;

				linePoints.Insert(j+i, new Point(bumpPoint));
				points.Insert(j+i, bumpPoint);
			}
		}
	}
}
