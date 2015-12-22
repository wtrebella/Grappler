using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : MonoBehaviour {
	[SerializeField] private int numPoints = 30;
	[SerializeField] private float avgSlope = 0.65f;
	[SerializeField] private float slopeVar = 0.25f;
	[SerializeField] private float pointDist = 2.5f;
	[SerializeField] private float pointDistVar = 1.2f;
	[SerializeField] private float perpDistVar = 2.8f;
	[SerializeField] private float marginSize = 60.0f;

	private List<Vector2> linePoints;
	private Dictionary<int, float> distances;
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	public int GetRandomPointIndex() {
		return Random.Range(0, linePoints.Count);
	}

	public int GetPointsCount() {
		return linePoints.Count;
	}

	public Vector2 GetLastLinePoint() {
		return linePoints.GetLastItem();
	}

	public Vector2 GetFirstLinePoint() {
		return linePoints[0];
	}

	public float GetTotalDistance() {
		return distances[linePoints.Count - 1];
	}

	public Vector2 GetLinePoint(int index) {
		if (index < 0 || index >= linePoints.Count) {
			Debug.LogError("invalid line point. returning Vector2.zero.");
			return Vector2.zero;
		}

		return linePoints[index];
	}

	public Vector2 GetPositionAlongLine(float lerp) {
		lerp = Mathf.Clamp01(lerp);
		float totalDistance = GetTotalDistance();
		float lerpDistance = totalDistance * lerp;
		int firstPointIndex = GetFirstPointIndexAtDistance(lerpDistance);
		int secondPointIndex = firstPointIndex+1;
		if (firstPointIndex >= linePoints.Count - 1) Debug.LogError("distance greater than end of cliff line...");
		float thisPointDistance = distances[firstPointIndex];
		float nextPointDistance = distances[secondPointIndex];
		float deltaDistance = lerpDistance - thisPointDistance;
		float betweenPointsDistance = nextPointDistance - thisPointDistance;
		float pointLerp = deltaDistance / betweenPointsDistance;
		return GetPositionBetweenLinePoints(firstPointIndex, secondPointIndex, pointLerp);
	}

	public Vector2 GetPositionBetweenLinePoints(int indexA, int indexB, float lerp) {
		lerp = Mathf.Clamp01(lerp);
		Vector2 pointA = GetLinePoint(indexA);
		Vector2 pointB = GetLinePoint(indexB);
		return Vector2.Lerp(pointA, pointB, lerp);
	}
	
	public void Generate(Vector2 origin) {
		List<Vector2> points = new List<Vector2>();
		GenerateBasicShape(ref points, origin);
		RandomizeEdge(ref points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
		CalculateDistances();
	}

	public List<Vector2> GetListOfLinePoints() {
		return linePoints;
	}

	private void Awake () {
		linePoints = new List<Vector2>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		float slopeVal = avgSlope + Random.Range(-slopeVar, slopeVar);
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slopeVal * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slopeVal * Mathf.PI / 2f);
	}

	private void CalculateDistances() {
		distances = new Dictionary<int, float>();
		distances.Add(0, 0);
		for (int i = 1; i < linePoints.Count; i++) {
			float previousDistance = distances[i-1];
			Vector2 pointA = linePoints[i-1];
			Vector2 pointB = linePoints[i];
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

	private void GenerateBasicShape(ref List<Vector2> points, Vector2 origin) {
		Vector2 prevPoint = origin;
		points.Add(prevPoint);
		
		for (int i = 0; i < numPoints; i++) {
			float dist = pointDist + Random.Range(-pointDistVar, pointDistVar);
			Vector2 delta = slopeVector * dist;
			Vector2 point = prevPoint + delta;
			points.Add(point);

			prevPoint = point;
		}

		foreach (Vector2 point in points) linePoints.Add(point);

		points.Add(new Vector2(origin.x - marginSize, prevPoint.y));
		points.Add(new Vector2(origin.x - marginSize, origin.y));
	}

	private void RandomizeEdge(ref List<Vector2> points) {
		Vector2 firstPoint = linePoints[0];
		Vector2 lastPoint = linePoints.GetLastItem();
		Vector2 slopeVectorPerp = new Vector2(slopeVector.y, -slopeVector.x);

		for (int i = 1; i < linePoints.Count - 1; i++) {
			Vector2 point = points[i];
			Vector2 tempPoint = point;
			float perpDist;

			if (i == 1) {
				do {
					perpDist = Random.Range(-perpDistVar, perpDistVar);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y < firstPoint.y);
			}
			else if (i == linePoints.Count - 2) {
				do {
					perpDist = Random.Range(-perpDistVar, perpDistVar);
					tempPoint = point + slopeVectorPerp * perpDist;
				} while (tempPoint.y > lastPoint.y);
			}
			else perpDist = Random.Range(-perpDistVar, perpDistVar);

			point += slopeVectorPerp * perpDist;
			points[i] = point;
			linePoints[i] = point;
		}
	}
}
