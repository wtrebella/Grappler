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
		CreateBuilding(buildingAttributes.skewedRect);
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

	private void CreateBuilding(SkewedRect skewedRect) {
		int startIndex = verts.Count;

		verts.Add(new Vector3(skewedRect.bottomLeft.x, skewedRect.bottomLeft.y, 0));
		verts.Add(new Vector3(skewedRect.topLeft.x, skewedRect.topLeft.y, 0));
		verts.Add(new Vector3(skewedRect.topRight.x, skewedRect.topRight.y, 0));
		verts.Add(new Vector3(skewedRect.bottomRight.x, skewedRect.bottomRight.y, 0));
		
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

	private void DestroyMesh() {
		meshFilter.mesh = null;
		verts.Clear();
		uvs.Clear();
		tris.Clear();
	}
}
