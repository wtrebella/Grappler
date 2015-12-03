using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : MonoBehaviour {
	[SerializeField] private int numPoints = 30;
	[SerializeField] private float avgSlope = 0.7f;
	[SerializeField] private float slopeVar = 0.2f;
	[SerializeField] private float pointDist = 25;
	[SerializeField] private float pointDistVar = 12;
	[SerializeField] private float perpDistVar = 28;
	[SerializeField] private float marginSize = 100;

	private List<Vector2> linePoints;
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	public Vector2 GetLastLinePoint() {
		return linePoints.GetLastItem();
	}
	
	public void Generate(Vector2 origin) {
		List<Vector2> points = new List<Vector2>();
		GenerateBasicShape(ref points, origin);
		RandomizeEdge(ref points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
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
