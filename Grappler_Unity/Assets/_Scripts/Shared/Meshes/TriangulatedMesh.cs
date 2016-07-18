using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TriangulatedMesh : MonoBehaviour {
	private List<Vector3> verts = new List<Vector3>();
	private List<int> tris = new List<int>();
	private MeshFilter meshFilter;
	
	public void RedrawMesh(Vector2[] outlinePoints) {
		ClearMesh();
		RegenerateMesh(outlinePoints);
		ApplyPointsToMesh();
	}

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
	}

	private void RegenerateMesh(Vector2[] points) {
		WhitTriangulator triangulator = new WhitTriangulator(points);
		int[] triArray = triangulator.Triangulate();

		foreach (int vertexNum in triArray) tris.Add(vertexNum);
		foreach (Vector2 point in points) verts.Add(point);
	}

	private void ApplyPointsToMesh() {
		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}

	public void ClearMesh() {
		tris.Clear();
		verts.Clear();
		meshFilter.mesh.Clear();
	}
}
