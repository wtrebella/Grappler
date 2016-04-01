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
		InitMesh();
	}

	private bool hasInited = false;
	private void InitMesh() {
		if (hasInited) return;
		List<Vector2> points = terrainLine.GetPoints();
		if (points == null) {
			Debug.LogError("no points!");
			return;
		}
		hasInited = true;

		Vector2 firstPoint = points.GetFirstItem();
		Vector2 lastPoint = points.GetLastItem();

		float amt = 50.0f;

		// above
//		Vector2 p1 = lastPoint + Vector2.right * amt;
//		Vector2 p2 = p1 + Vector2.up * amt;
//		Vector2 p3 = new Vector2(firstPoint.x - amt, p2.y);
//		Vector2 p4 = new Vector2(p3.x, firstPoint.y);

		// below
		Vector2 p1 = lastPoint + Vector2.right * amt;
		Vector2 p2 = p1 + Vector2.down * amt;
		Vector2 p3 = new Vector2(firstPoint.x - amt, p2.y);
		Vector2 p4 = new Vector2(p3.x, firstPoint.y);

		points.Add(p1);
		points.Add(p2);
		points.Add(p3);
		points.Add(p4);

		mesh.RedrawMesh(points.ToArray());
	}
}
