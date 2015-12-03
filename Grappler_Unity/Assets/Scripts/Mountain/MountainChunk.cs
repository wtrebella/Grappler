using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : MonoBehaviour {
	[SerializeField] private int numPoints = 30;
	[SerializeField] private float slope = 0.7f;
	[SerializeField] private float pointDist = 25;
	[SerializeField] private float pointDistVar = 12;
	[SerializeField] private float perpDistVar = 28;
	[SerializeField] private float marginSize = 100;

	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	public Vector2 GetTopRightPoint() {
		WhitTools.Assert(polygonCollider != null, "polygon collider is null!");

		Vector2 point = polygonCollider.points[polygonCollider.points.Length - 3];
		point.y -= marginSize;
		return point;
	}
	
	public void Generate(Vector2 origin) {
		List<Vector2> points = new List<Vector2>();
		GenerateBasicShape(ref points, origin);
		RandomizeEdge(ref points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
	}

	private void Awake () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slope * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slope * Mathf.PI / 2f);
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

		Vector2 lastPoint = prevPoint;
		lastPoint.y += marginSize;
		points.Add(lastPoint);
		points.Add(new Vector2(origin.x - marginSize, lastPoint.y));
		points.Add(new Vector2(origin.x - marginSize, origin.y - marginSize));
	}

	private void RandomizeEdge(ref List<Vector2> points) {
		for (int i = 1; i < points.Count - 4; i++) {
			Vector2 point = points[i];
			Vector2 slopeVectorPerp = new Vector2(slopeVector.y, -slopeVector.x);
			float perpDist = Random.Range(-perpDistVar, perpDistVar);
			point += slopeVectorPerp * perpDist;
			points[i] = point;
		}
	}
}
