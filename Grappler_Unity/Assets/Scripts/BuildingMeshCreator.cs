using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BuildingMeshCreator : MonoBehaviour {
	[SerializeField] private int maxBuildingPieces = 3;
	[SerializeField] private Vector3 minBuildingPieceDimensions = new Vector3(10, 50, 10);
	[SerializeField] private Vector3 maxBuildingPieceDimensions = new Vector3(50, 300, 50);
	[SerializeField] private float maxBuildingPieceInset = 20;

	private MeshFilter meshFilter;

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();

		CreateMesh();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			DestroyMesh();
			CreateMesh();
		}
	}

	private void CreateMesh() {
		int buildingPieces = Random.Range(1, maxBuildingPieces);

		Vector3[] verts = new Vector3[16 * buildingPieces];
		Vector2[] uvs = new Vector2[16 * buildingPieces];
		int[] tris = new int[24 * buildingPieces]; // no need to draw the back or bottom faces, so 24 instead of 36
		int vertIndex = 0;
		int triIndex = 0;

		Vector3 previousDimensions = Vector3.zero;
		Vector3 previousOrigin = Vector3.zero;
		float previousInset = 0;

		for (int i = 0; i < buildingPieces; i++) {
			Vector3 dimensions = new Vector3(
				Random.Range(minBuildingPieceDimensions.x, maxBuildingPieceDimensions.x),
				Random.Range(minBuildingPieceDimensions.y, maxBuildingPieceDimensions.y),
				Random.Range(minBuildingPieceDimensions.z, maxBuildingPieceDimensions.z)
			);

			float inset = Random.Range(0, maxBuildingPieceInset);

			Vector3 origin = new Vector3(previousOrigin.x + previousDimensions.x, 0, inset);

			// front face
			verts[vertIndex + 0] = new Vector3(origin.x               , origin.y               , origin.z);
			verts[vertIndex + 1] = new Vector3(origin.x               , origin.y + dimensions.y, origin.z);
			verts[vertIndex + 2] = new Vector3(origin.x + dimensions.x, origin.y + dimensions.y, origin.z);
			verts[vertIndex + 3] = new Vector3(origin.x + dimensions.x, origin.y               , origin.z);

			// right face
			verts[vertIndex + 4] = new Vector3(origin.x + dimensions.x, origin.y               , origin.z               );
			verts[vertIndex + 5] = new Vector3(origin.x + dimensions.x, origin.y + dimensions.y, origin.z               );
			verts[vertIndex + 6] = new Vector3(origin.x + dimensions.x, origin.y + dimensions.y, origin.z + dimensions.z);
			verts[vertIndex + 7] = new Vector3(origin.x + dimensions.x, origin.y               , origin.z + dimensions.z);

			// left face
			verts[vertIndex +  8] = new Vector3(origin.x               , origin.y               , origin.z + dimensions.z);
			verts[vertIndex +  9] = new Vector3(origin.x               , origin.y + dimensions.y, origin.z + dimensions.z);
			verts[vertIndex + 10] = new Vector3(origin.x               , origin.y + dimensions.y, origin.z               );
			verts[vertIndex + 11] = new Vector3(origin.x               , origin.y               , origin.z               );

			// top face
			verts[vertIndex + 12] = new Vector3(origin.x               , origin.y + dimensions.y, origin.z               );
			verts[vertIndex + 13] = new Vector3(origin.x               , origin.y + dimensions.y, origin.z + dimensions.z);
			verts[vertIndex + 14] = new Vector3(origin.x + dimensions.x, origin.y + dimensions.y, origin.z + dimensions.z);
			verts[vertIndex + 15] = new Vector3(origin.x + dimensions.x, origin.y + dimensions.y, origin.z               );

			uvs[vertIndex + 0] = new Vector2(0, 0);
			uvs[vertIndex + 1] = new Vector2(0, 1);
			uvs[vertIndex + 2] = new Vector2(1, 1);
			uvs[vertIndex + 3] = new Vector2(1, 0);

			uvs[vertIndex + 4] = new Vector2(0, 0);
			uvs[vertIndex + 5] = new Vector2(0, 1);
			uvs[vertIndex + 6] = new Vector2(1, 1);
			uvs[vertIndex + 7] = new Vector2(1, 0);

			uvs[vertIndex +  8] = new Vector2(0, 0);
			uvs[vertIndex +  9] = new Vector2(0, 1);
			uvs[vertIndex + 10] = new Vector2(1, 1);
			uvs[vertIndex + 11] = new Vector2(1, 0);

			uvs[vertIndex + 12] = new Vector2(0, 0);
			uvs[vertIndex + 13] = new Vector2(0, 1);
			uvs[vertIndex + 14] = new Vector2(1, 1);
			uvs[vertIndex + 15] = new Vector2(1, 0);

			tris[triIndex + 0] = vertIndex + 0;
			tris[triIndex + 1] = vertIndex + 1;
			tris[triIndex + 2] = vertIndex + 2;

			tris[triIndex + 3] = vertIndex + 2;
			tris[triIndex + 4] = vertIndex + 3;
			tris[triIndex + 5] = vertIndex + 0;

			tris[triIndex + 6] = vertIndex + 4;
			tris[triIndex + 7] = vertIndex + 5;
			tris[triIndex + 8] = vertIndex + 6;

			tris[triIndex +  9] = vertIndex + 6;
			tris[triIndex + 10] = vertIndex + 7;
			tris[triIndex + 11] = vertIndex + 4;

			tris[triIndex + 12] = vertIndex +  8;
			tris[triIndex + 13] = vertIndex +  9;
			tris[triIndex + 14] = vertIndex + 10;

			tris[triIndex + 15] = vertIndex + 10;
			tris[triIndex + 16] = vertIndex + 11;
			tris[triIndex + 17] = vertIndex +  8;

			tris[triIndex + 18] = vertIndex + 12;
			tris[triIndex + 19] = vertIndex + 13;
			tris[triIndex + 20] = vertIndex + 14;

			tris[triIndex + 21] = vertIndex + 14;
			tris[triIndex + 22] = vertIndex + 15;
			tris[triIndex + 23] = vertIndex + 12;

			vertIndex += 16;
			triIndex += 24;

			previousDimensions = dimensions;
			previousOrigin = origin;
			previousInset = inset;
		}

		meshFilter.mesh.vertices = verts;
		meshFilter.mesh.triangles = tris;
		meshFilter.mesh.uv = uvs;

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}

	private void DestroyMesh() {
		meshFilter.mesh = null;
	}
}
