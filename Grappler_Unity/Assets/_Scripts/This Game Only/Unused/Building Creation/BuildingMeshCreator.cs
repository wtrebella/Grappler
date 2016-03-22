using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BuildingMeshCreator : MonoBehaviour {
	private List<Vector3> verts = new List<Vector3>();
	private List<Vector2> uvs = new List<Vector2>();
	private List<int> tris = new List<int>();
	private MeshFilter meshFilter;
	
	public void InitMesh(BuildingAttributes buildingAttributes) {
		CreateBuilding(buildingAttributes.quad);
		ApplyToMesh();
	}

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
	}

	private void ApplyToMesh() {
		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();
		meshFilter.mesh.uv = uvs.ToArray();

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}

	private void CreateBuilding(Quad quad) {
		ClearMesh();

		int startIndex = verts.Count;

		verts.Add(new Vector3(quad.bottomLeft.x, quad.bottomLeft.y, 0));
		verts.Add(new Vector3(quad.topLeft.x, quad.topLeft.y, 0));
		verts.Add(new Vector3(quad.topRight.x, quad.topRight.y, 0));
		verts.Add(new Vector3(quad.bottomRight.x, quad.bottomRight.y, 0));

		uvs.Add(new Vector2(0, 0));
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(1, 1));
		uvs.Add(new Vector2(1, 0));

		tris.Add(startIndex + 0);
		tris.Add(startIndex + 1);
		tris.Add(startIndex + 2);
		
		tris.Add(startIndex + 2);
		tris.Add(startIndex + 3);
		tris.Add(startIndex + 0);
	}

	private void ClearMesh() {
		verts.Clear();
		uvs.Clear();
		tris.Clear();
	}
}
