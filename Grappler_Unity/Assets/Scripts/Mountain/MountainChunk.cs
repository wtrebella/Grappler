using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunk : MonoBehaviour {
	[SerializeField] private int numPoints = 50;
	[SerializeField] private float slope = 0.5f;
	[SerializeField] private float pointDist = 5;
	[SerializeField] private float pointDistVar = 2;
	[SerializeField] private float perpDistVar = 10;

	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;
	private Vector2 slopeVector;

	private void Awake () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		slopeVector = new Vector2();
		slopeVector.x = Mathf.Cos(slope * Mathf.PI / 2f);
		slopeVector.y = Mathf.Sin(slope * Mathf.PI / 2f);
	}

	private void Start() {
		Generate();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) Generate();
	}

	private void Generate() {
		List<Vector2> points = new List<Vector2>();
		GenerateBasicShape(ref points);
		RandomizeEdge(ref points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
	}

	private void GenerateBasicShape(ref List<Vector2> points) {
		Vector2 prevPoint = Vector2.zero;
		points.Add(prevPoint);
		
		for (int i = 0; i < numPoints; i++) {
			float dist = pointDist + Random.Range(-pointDistVar, pointDistVar);
			Vector2 delta = slopeVector * dist;
			Vector2 point = prevPoint + delta;
			if (i == numPoints - 1) point.y += 100;
			points.Add(point);

			prevPoint = point;
		}
		
		points.Add(new Vector2(-100, prevPoint.y));
		points.Add(new Vector2(-100, -100));
	}

	private void RandomizeEdge(ref List<Vector2> points) {
		for (int i = 1; i < points.Count - 3; i++) {
			Vector2 point = points[i];
			Vector2 slopeVectorPerp = new Vector2(slopeVector.y, -slopeVector.x);
			float perpDist = Random.Range(-perpDistVar, perpDistVar);
			point += slopeVectorPerp * perpDist;
			points[i] = point;
		}
	}
}
