using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BuildingMeshCreator : MonoBehaviour {
	[SerializeField] private int maxBuildingPieces = 3;
	[SerializeField] private Vector2 minWindowDimensions = new Vector2(2, 5);
	[SerializeField] private Vector2 maxWindowDimensions = new Vector2(10, 25);
	[SerializeField] private Vector3 minBuildingPieceSize = new Vector3(20, 50, 20);
	[SerializeField] private Vector3 maxBuildingPieceSize = new Vector3(70, 300, 70);
	[SerializeField] private float maxBuildingPieceInset = 20;

	private List<Vector3> verts = new List<Vector3>();
	private List<Vector2> uvs = new List<Vector2>();
	private List<int> tris = new List<int>();
	private MeshFilter meshFilter;
	private UVRect windowUVRect;
	private UVRect roofUVRect;

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
		InitUVRects();
		CreateMesh();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			DestroyMesh();
			CreateMesh();
		}
	}

	private void InitUVRects() {
		windowUVRect = new UVRect();
		windowUVRect.bottomLeft = new Vector2(0, 0);
		windowUVRect.topLeft = new Vector2(0, 1);
		windowUVRect.topRight = new Vector2(0.5f, 1);
		windowUVRect.bottomRight = new Vector2(0.5f, 0);

		roofUVRect = new UVRect();
		roofUVRect.bottomLeft = new Vector2(0.5f, 0);
		roofUVRect.topLeft = new Vector2(0.5f, 1);
		roofUVRect.topRight = new Vector2(1, 1);
		roofUVRect.bottomRight = new Vector2(1, 0);
	}

	private void CreateMesh() {
		int buildingPieces = Random.Range(1, maxBuildingPieces);
		
		Vector3 previousDimensions = Vector3.zero;
		Vector3 previousOrigin = Vector3.zero;

		Vector2 windowDimensions = GetRandomWindowDimensions();

		for (int i = 0; i < buildingPieces; i++) {
			IntVector3 numWindows = GetRandomNumWindows(windowDimensions);
			Vector3 buildingPieceDimensions = GetBuildingPieceDimensions(numWindows, windowDimensions);
			float inset = GetRandomInset();

			Vector3 origin = new Vector3(previousOrigin.x + previousDimensions.x, 0, inset);

			Vector3 frontFaceOrigin = origin;
			Vector3 rightFaceOrigin = new Vector3(origin.x + buildingPieceDimensions.x, origin.y, origin.z);
			Vector3 leftFaceOrigin = new Vector3(origin.x, origin.y, origin.z + buildingPieceDimensions.z);
			Vector3 topFaceOrigin = new Vector3(origin.x, origin.y + buildingPieceDimensions.y, origin.z);
			
			CreateFrontFace(frontFaceOrigin, numWindows, windowDimensions);
			CreateRightFace(rightFaceOrigin, numWindows, windowDimensions);
			CreateLeftFace(leftFaceOrigin, numWindows, windowDimensions);
			CreateTopFace(topFaceOrigin, numWindows, windowDimensions);

			previousDimensions = buildingPieceDimensions;
			previousOrigin = origin;
		}

		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();
		meshFilter.mesh.uv = uvs.ToArray();

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}

	private void CreateFrontFace(Vector3 origin, IntVector3 numWindows, Vector2 windowDimensions) {
		for (int yWindow = 0; yWindow < numWindows.y; yWindow++) {
			for (int xWindow = 0; xWindow < numWindows.x; xWindow++) {
				int startIndex = verts.Count;
				
				verts.Add(new Vector3(
					origin.x + (xWindow + 0) * windowDimensions.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z
					));
				
				verts.Add(new Vector3(
					origin.x + (xWindow + 0) * windowDimensions.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z
					));
				
				verts.Add(new Vector3(
					origin.x + (xWindow + 1) * windowDimensions.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z
					));
				
				verts.Add(new Vector3(
					origin.x + (xWindow + 1) * windowDimensions.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z
					));
				
				uvs.Add(windowUVRect.bottomLeft);
				uvs.Add(windowUVRect.topLeft);
				uvs.Add(windowUVRect.topRight);
				uvs.Add(windowUVRect.bottomRight);
				
				tris.Add(startIndex + 0);
				tris.Add(startIndex + 1);
				tris.Add(startIndex + 2);
				
				tris.Add(startIndex + 2);
				tris.Add(startIndex + 3);
				tris.Add(startIndex + 0);
			}
		}
	}

	private void CreateRightFace(Vector3 origin, IntVector3 numWindows, Vector2 windowDimensions) {
		for (int yWindow = 0; yWindow < numWindows.y; yWindow++) {
			for (int xWindow = 0; xWindow < numWindows.x; xWindow++) {
				int startIndex = verts.Count;
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z + (xWindow + 0) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z + (xWindow + 0) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z + (xWindow + 1) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z + (xWindow + 1) * windowDimensions.x
					));
				
				uvs.Add(windowUVRect.bottomLeft);
				uvs.Add(windowUVRect.topLeft);
				uvs.Add(windowUVRect.topRight);
				uvs.Add(windowUVRect.bottomRight);
				
				tris.Add(startIndex + 0);
				tris.Add(startIndex + 1);
				tris.Add(startIndex + 2);
				
				tris.Add(startIndex + 2);
				tris.Add(startIndex + 3);
				tris.Add(startIndex + 0);
			}
		}
	}

	private void CreateLeftFace(Vector3 origin, IntVector3 numWindows, Vector2 windowDimensions) {
		for (int yWindow = 0; yWindow < numWindows.y; yWindow++) {
			for (int xWindow = 0; xWindow < numWindows.x; xWindow++) {
				int startIndex = verts.Count;
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z - (xWindow + 0) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z - (xWindow + 0) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 1) * windowDimensions.y,
					origin.z - (xWindow + 1) * windowDimensions.x
					));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (yWindow + 0) * windowDimensions.y,
					origin.z - (xWindow + 1) * windowDimensions.x
					));

				Debug.Log(origin + ", " + verts[startIndex] + ", " + verts[startIndex+3]);
				
				uvs.Add(windowUVRect.bottomLeft);
				uvs.Add(windowUVRect.topLeft);
				uvs.Add(windowUVRect.topRight);
				uvs.Add(windowUVRect.bottomRight);
				
				tris.Add(startIndex + 0);
				tris.Add(startIndex + 1);
				tris.Add(startIndex + 2);
				
				tris.Add(startIndex + 2);
				tris.Add(startIndex + 3);
				tris.Add(startIndex + 0);
			}
		}
	}

	private void CreateTopFace(Vector3 origin, IntVector3 numWindows, Vector2 windowDimensions) {
		int startIndex = verts.Count;
		
		verts.Add(new Vector3(
			origin.x,
			origin.y,
			origin.z
			));
		
		verts.Add(new Vector3(
			origin.x,
			origin.y,
			origin.z + numWindows.x * windowDimensions.x
			));
		
		verts.Add(new Vector3(
			origin.x + numWindows.x * windowDimensions.x,
			origin.y,
			origin.z + numWindows.x * windowDimensions.x
			));
		
		verts.Add(new Vector3(
			origin.x + numWindows.x * windowDimensions.x,
			origin.y,
			origin.z
			));
		
		uvs.Add(windowUVRect.bottomLeft);
		uvs.Add(windowUVRect.topLeft);
		uvs.Add(windowUVRect.topRight);
		uvs.Add(windowUVRect.bottomRight);
		
		tris.Add(startIndex + 0);
		tris.Add(startIndex + 1);
		tris.Add(startIndex + 2);
		
		tris.Add(startIndex + 2);
		tris.Add(startIndex + 3);
		tris.Add(startIndex + 0);
	}

	private Vector3 GetBuildingPieceDimensions(IntVector3 numWindows, Vector2 windowDimensions) {
		Vector3 v = new Vector3();
		v.x = numWindows.x * windowDimensions.x;
		v.y = numWindows.y * windowDimensions.y;
		v.z = numWindows.z * windowDimensions.x;
		return v;
	}

	private Vector2 GetRandomWindowDimensions() {
		Vector2 windowDimensions = new Vector2(
			Random.Range(minWindowDimensions.x, maxWindowDimensions.x),
			Random.Range(minWindowDimensions.y, maxWindowDimensions.y)
		);
	
		return windowDimensions;
	}

	private IntVector3 GetRandomNumWindows(Vector2 windowDimensions) {
		IntVector3 minNumWindows = new IntVector3(
			minBuildingPieceSize.x / windowDimensions.x,
			minBuildingPieceSize.y / windowDimensions.y,
			minBuildingPieceSize.z / windowDimensions.x
		);

		IntVector3 maxNumWindows = new IntVector3(
			maxBuildingPieceSize.x / windowDimensions.x,
			maxBuildingPieceSize.y / windowDimensions.y,
			maxBuildingPieceSize.z / windowDimensions.x
		);
		
		IntVector3 v = new IntVector3();
		v.x = Random.Range(minNumWindows.x, maxNumWindows.x);
		v.y = Random.Range(minNumWindows.y, maxNumWindows.y);
		v.z = Random.Range(minNumWindows.z, maxNumWindows.z);

		return v;
	}

	private float GetRandomInset() {
		return Random.Range(0, maxBuildingPieceInset);
	}

	private void DestroyMesh() {
		meshFilter.mesh = null;
		verts.Clear();
		uvs.Clear();
		tris.Clear();
	}
}
