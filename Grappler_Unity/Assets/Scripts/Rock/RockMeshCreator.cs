using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RockMeshCreator : MonoBehaviour {
	private List<Vector3> verts = new List<Vector3>();
	private List<int> tris = new List<int>();
	private MeshFilter meshFilter;
	
	public void InitMesh(Vector2[] points) {
		CreateMesh(points);
		ApplyToMesh();
	}
	
	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
	}
	
	private void CreateMesh(Vector2[] points) {
		ClearMesh();
		
		Triangulator triangulator = new Triangulator(points);
		int[] triArray = triangulator.Triangulate();
		
		foreach (int vertexNum in triArray) tris.Add(vertexNum);
		foreach (Vector2 point in points) verts.Add(point);
	}
	
	private void ApplyToMesh() {
		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();
		
		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}
	
	private void ClearMesh() {
		verts.Clear();
		tris.Clear();
	}
}
