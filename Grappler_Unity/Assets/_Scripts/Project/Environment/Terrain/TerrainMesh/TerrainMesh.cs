using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TriangulatedMesh))]
public class TerrainMesh : MonoBehaviour {
	[SerializeField] private TerrainLine terrainLine;

	private TriangulatedMesh mesh;

	private void Awake() {
		mesh = GetComponent<TriangulatedMesh>();
	}

	private void Update() {
//		InitMesh();
	}

	private bool hasInited = false;
	private void InitMesh() {
		if (hasInited) return;
		List<Vector2> points = terrainLine.GetPoints();

		hasInited = true;

		Vector2 firstPoint = terrainLine.GetFirstPoint();
		Vector2 lastPoint = terrainLine.GetLastPoint();

		float amt = 50.0f;

		Vector2 p1 = lastPoint + Vector2.right * amt;
		Vector2 p2 = p1 + Vector2.up * amt;
		Vector2 p3 = new Vector2(firstPoint.x - amt, p2.y);
		Vector2 p4 = new Vector2(p3.x, firstPoint.y);

		points.Add(p1);
		points.Add(p2);
		points.Add(p3);
		points.Add(p4);

		mesh.RedrawMesh(points.ToArray());
	}
}
