using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BuildingMeshCreator : MonoBehaviour {
	[SerializeField] private Material mat1;
	[SerializeField] private Material mat2;
	[SerializeField] private Material mat3;

	[SerializeField] private int maxBuildingPieces = 3;
	[SerializeField] private Vector2 minMaxWindowMargin = new Vector2(2, 10);
	[SerializeField] private Vector2 minWindowDimensions = new Vector2(2, 5);
	[SerializeField] private Vector2 maxWindowDimensions = new Vector2(10, 25);
	[SerializeField] private Vector3 minBuildingPieceSize = new Vector3(20, 50, 20);
	[SerializeField] private Vector3 maxBuildingPieceSize = new Vector3(70, 300, 70);
	[SerializeField] private int maxNumWindowInset = 3;

	private List<Vector3> verts = new List<Vector3>();
	private List<Vector2> uvs = new List<Vector2>();
	private List<int> tris = new List<int>();
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private UVRect windowUVRect;
	private UVRect roofUVRect;
	private UVRect windowMarginUVRect;

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();

		meshRenderer = GetComponent<MeshRenderer>();

		InitUVRects();
		CreateMesh();
		SetRandomMaterial();
	}

	private void SetRandomMaterial() {
		GetComponent<MeshRenderer>().material = GetRandomMaterial();
	}

	private Material GetRandomMaterial() {
		float val = Random.value;
		if (val < 0.333f) return mat1;
		else if (val < 0.666f) return mat2;
		else return mat3;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			DestroyMesh();
			CreateMesh();
			SetRandomMaterial();
		}
	}

	private void InitUVRects() {
		windowUVRect = new UVRect();
		windowUVRect.bottomLeft = new Vector2(0, 0);
		windowUVRect.topLeft = new Vector2(0, 0.5f);
		windowUVRect.topRight = new Vector2(0.25f, 0.5f);
		windowUVRect.bottomRight = new Vector2(0.25f, 0);

		roofUVRect = new UVRect();
		roofUVRect.bottomLeft = new Vector2(0.5f, 0);
		roofUVRect.topLeft = new Vector2(0.5f, 1);
		roofUVRect.topRight = new Vector2(1, 1);
		roofUVRect.bottomRight = new Vector2(1, 0);

		windowMarginUVRect = new UVRect();
		windowMarginUVRect.bottomLeft = new Vector2(0.25f, 0);
		windowMarginUVRect.topLeft = new Vector2(0.25f, 0.5f);
		windowMarginUVRect.topRight = new Vector2(0.5f, 0.5f);
		windowMarginUVRect.bottomRight = new Vector2(0.5f, 0);
	}

	private void CreateMesh() {
		int buildingPieces = Random.Range(1, maxBuildingPieces + 1);
		
		Vector3 previousDimensions = Vector3.zero;
		Vector3 previousOrigin = Vector3.zero;

		Vector2 marginSize = GetRandomMarginSize();
		Vector2 windowSize = GetRandomWindowSize();

		for (int i = 0; i < buildingPieces; i++) {
			Vector3 buildingPieceDimensions = GetBuildingPieceDimensions();
			IntVector3 numWindows = GetNumWindows(buildingPieceDimensions, windowSize, marginSize);
			float inset = GetRandomInset(windowSize, marginSize);

			Vector3 origin = new Vector3(previousOrigin.x + previousDimensions.x, 0, inset);
			Vector3 frontFaceOrigin = origin;
			Vector3 rightFaceOrigin = new Vector3(origin.x + buildingPieceDimensions.x, origin.y, origin.z);
			Vector3 leftFaceOrigin = new Vector3(origin.x, origin.y, origin.z + buildingPieceDimensions.z);
			Vector3 topFaceOrigin = new Vector3(origin.x, origin.y + buildingPieceDimensions.y, origin.z);

			CreateFrontFace(frontFaceOrigin, numWindows, windowSize, marginSize);
//			CreateRightFace(rightFaceOrigin, numWindows, windowSize, windowMargins);
//			CreateLeftFace(leftFaceOrigin, numWindows, windowSize, windowMargins);
//			CreateTopFace(topFaceOrigin, numWindows, windowSize, windowMargins);

			previousDimensions = buildingPieceDimensions;
			previousOrigin = origin;
		}

		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();
		meshFilter.mesh.uv = uvs.ToArray();

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}
	
	private void CreateFrontFace(Vector3 origin, IntVector3 numWindows, Vector2 windowSize, Vector2 marginSize) {
		IntVector3 chunkCount = new IntVector3(
			Mathf.Max(numWindows.x, 1),
			Mathf.Max(numWindows.y, 1),
			Mathf.Max(numWindows.z, 1)
		);

		for (int y = 0; y <= chunkCount.y; y++) {
			for (int x = 0; x <= chunkCount.x; x++) {
				if (chunkCount.x == 1 || chunkCount.y == 1) {

				}
				else {
					int startIndex = verts.Count;
					bool atLastX = x == chunkCount.x;
					bool atLastY = y == chunkCount.y;

					Vector2 marginCornerOrigin = new Vector2(
						origin.x + x * (windowSize.x + marginSize.x),
						origin.y + y * (windowSize.y + marginSize.y)
					);

					Vector2 marginHorizontalOrigin = new Vector2(
						origin.x + x * (windowSize.x + marginSize.x) + marginSize.x,
						origin.y + y * (windowSize.y + marginSize.y)
					);

					Vector2 marginVerticalOrigin = new Vector2(
						origin.x + x * (windowSize.x + marginSize.x),
						origin.y + y * (windowSize.y + marginSize.y) + marginSize.y
					);
	
					Vector2 windowOrigin = new Vector2(
						origin.x + x * (windowSize.x + marginSize.x) + marginSize.x,
						origin.y + y * (windowSize.y + marginSize.y) + marginSize.y
					);

					// corner margin
					verts.Add(new Vector3(
						marginCornerOrigin.x,
						marginCornerOrigin.y,
						origin.z
					));
					
					verts.Add(new Vector3(
						marginCornerOrigin.x,
						marginCornerOrigin.y + marginSize.y,
						origin.z
					));
					
					verts.Add(new Vector3(
						marginCornerOrigin.x + marginSize.x,
						marginCornerOrigin.y + marginSize.y,
						origin.z
					));
					
					verts.Add(new Vector3(
						marginCornerOrigin.x + marginSize.x,
						marginCornerOrigin.y,
						origin.z
					));

					uvs.Add(windowMarginUVRect.bottomLeft);
					uvs.Add(windowMarginUVRect.topLeft);
					uvs.Add(windowMarginUVRect.topRight);
					uvs.Add(windowMarginUVRect.bottomRight);
					
					tris.Add(startIndex + 0);
					tris.Add(startIndex + 1);
					tris.Add(startIndex + 2);
					
					tris.Add(startIndex + 2);
					tris.Add(startIndex + 3);
					tris.Add(startIndex + 0);

					startIndex += 4;

					// horizontal margin
					if (!atLastX) {
						verts.Add(new Vector3(
							marginHorizontalOrigin.x,
							marginHorizontalOrigin.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							marginHorizontalOrigin.x,
							marginHorizontalOrigin.y + marginSize.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							marginHorizontalOrigin.x + windowSize.x,
							marginHorizontalOrigin.y + marginSize.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							marginHorizontalOrigin.x + windowSize.x,
							marginHorizontalOrigin.y,
							origin.z
						));

						uvs.Add(windowMarginUVRect.bottomLeft);
						uvs.Add(windowMarginUVRect.topLeft);
						uvs.Add(windowMarginUVRect.topRight);
						uvs.Add(windowMarginUVRect.bottomRight);
						
						tris.Add(startIndex + 0);
						tris.Add(startIndex + 1);
						tris.Add(startIndex + 2);
						
						tris.Add(startIndex + 2);
						tris.Add(startIndex + 3);
						tris.Add(startIndex + 0);
						
						startIndex += 4;
					}

					// vertical margin
					if (!atLastY) {
						verts.Add(new Vector3(
							marginVerticalOrigin.x,
							marginVerticalOrigin.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							marginVerticalOrigin.x,
							marginVerticalOrigin.y + windowSize.y,
							origin.z
						));
					
						verts.Add(new Vector3(
							marginVerticalOrigin.x + marginSize.x,
							marginVerticalOrigin.y + windowSize.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							marginVerticalOrigin.x + marginSize.x,
							marginVerticalOrigin.y,
							origin.z
						));

						uvs.Add(windowMarginUVRect.bottomLeft);
						uvs.Add(windowMarginUVRect.topLeft);
						uvs.Add(windowMarginUVRect.topRight);
						uvs.Add(windowMarginUVRect.bottomRight);
						
						tris.Add(startIndex + 0);
						tris.Add(startIndex + 1);
						tris.Add(startIndex + 2);
						
						tris.Add(startIndex + 2);
						tris.Add(startIndex + 3);
						tris.Add(startIndex + 0);
						
						startIndex += 4;
					}

					// window
					if (!atLastX && !atLastY) {
						verts.Add(new Vector3(
							windowOrigin.x,
							windowOrigin.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							windowOrigin.x,
							windowOrigin.y + windowSize.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							windowOrigin.x + windowSize.x,
							windowOrigin.y + windowSize.y,
							origin.z
						));
						
						verts.Add(new Vector3(
							windowOrigin.x + windowSize.x,
							windowOrigin.y,
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
		}
	}

	private void CreateRightFace(Vector3 origin, IntVector3 numWindows, Vector2 windowSize, Vector2 windowMargins) {
		Vector3 chunkDimensions = new Vector3(
			numWindows.x > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.x,
			numWindows.y > 0 ? (windowSize.y + windowMargins.y) : minBuildingPieceSize.y,
			numWindows.z > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.z
		);
		
		IntVector3 chunkCount = new IntVector3(
			Mathf.Max(numWindows.x, 1),
			Mathf.Max(numWindows.y, 1),
			Mathf.Max(numWindows.z, 1)
		);

		for (int y = 0; y < chunkCount.y; y++) {
			for (int z = 0; z < chunkCount.z; z++) {
				int startIndex = verts.Count;
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 0) * chunkDimensions.y,
					origin.z + (z + 0) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 1) * chunkDimensions.y,
					origin.z + (z + 0) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 1) * chunkDimensions.y,
					origin.z + (z + 1) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 0) * chunkDimensions.y,
					origin.z + (z + 1) * chunkDimensions.z
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

	private void CreateLeftFace(Vector3 origin, IntVector3 numWindows, Vector2 windowSize, Vector2 windowMargins) {
		Vector3 chunkDimensions = new Vector3(
			numWindows.x > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.x,
			numWindows.y > 0 ? (windowSize.y + windowMargins.y) : minBuildingPieceSize.y,
			numWindows.z > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.z
		);
		
		IntVector3 chunkCount = new IntVector3(
			Mathf.Max(numWindows.x, 1),
			Mathf.Max(numWindows.y, 1),
			Mathf.Max(numWindows.z, 1)
		);

		for (int y = 0; y < chunkCount.y; y++) {
			for (int z = 0; z < chunkCount.z; z++) {
				int startIndex = verts.Count;
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 0) * chunkDimensions.y,
					origin.z - (z + 0) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 1) * chunkDimensions.y,
					origin.z - (z + 0) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 1) * chunkDimensions.y,
					origin.z - (z + 1) * chunkDimensions.z
				));
				
				verts.Add(new Vector3(
					origin.x,
					origin.y + (y + 0) * chunkDimensions.y,
					origin.z - (z + 1) * chunkDimensions.z
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

	private void CreateTopFace(Vector3 origin, IntVector3 numWindows, Vector2 windowSize, Vector2 windowMargins) {
		Vector3 chunkDimensions = new Vector3(
			numWindows.x > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.x,
			numWindows.y > 0 ? (windowSize.y + windowMargins.y) : minBuildingPieceSize.y,
			numWindows.z > 0 ? (windowSize.x + windowMargins.x) : minBuildingPieceSize.z
		);

		IntVector3 chunkCount = new IntVector3(
			Mathf.Max(numWindows.x, 1),
			Mathf.Max(numWindows.y, 1),
			Mathf.Max(numWindows.z, 1)
		);

		int startIndex = verts.Count;
		
		verts.Add(new Vector3(
			origin.x,
			origin.y,
			origin.z
		));
		
		verts.Add(new Vector3(
			origin.x,
			origin.y,
			origin.z + chunkCount.z * chunkDimensions.z
		));
		
		verts.Add(new Vector3(
			origin.x + chunkCount.x * chunkDimensions.x,
			origin.y,
			origin.z + chunkCount.z * chunkDimensions.z
		));
		
		verts.Add(new Vector3(
			origin.x + chunkCount.x * chunkDimensions.x,
			origin.y,
			origin.z
		));
		
		uvs.Add(roofUVRect.bottomLeft);
		uvs.Add(roofUVRect.topLeft);
		uvs.Add(roofUVRect.topRight);
		uvs.Add(roofUVRect.bottomRight);
		
		tris.Add(startIndex + 0);
		tris.Add(startIndex + 1);
		tris.Add(startIndex + 2);
		
		tris.Add(startIndex + 2);
		tris.Add(startIndex + 3);
		tris.Add(startIndex + 0);
	}

	private Vector3 GetBuildingPieceDimensions() {
		Vector3 v = new Vector3();
		v.x = Random.Range(minBuildingPieceSize.x, maxBuildingPieceSize.x);
		v.y = Random.Range(minBuildingPieceSize.y, maxBuildingPieceSize.y);
		v.z = Random.Range(minBuildingPieceSize.z, maxBuildingPieceSize.z);
		return v;
	}

	private Vector2 GetRandomWindowSize() {
		Vector2 windowDimensions = new Vector2(
			Random.Range(minWindowDimensions.x, maxWindowDimensions.x),
			Random.Range(minWindowDimensions.y, maxWindowDimensions.y)
		);
	
		return windowDimensions;
	}

	private Vector2 GetRandomMarginSize() {
		return new Vector2(Random.Range(minMaxWindowMargin.x, minMaxWindowMargin.y), Random.Range(minMaxWindowMargin.x, minMaxWindowMargin.y));
	}

	private IntVector3 GetNumWindows(Vector3 buildingPieceDimensions, Vector2 windowSize, Vector2 windowMargins) {
		IntVector3 numWindows = new IntVector3(
			(buildingPieceDimensions.x - windowMargins.x) / (windowSize.x + windowMargins.x),
			(buildingPieceDimensions.y - windowMargins.y) / (windowSize.y + windowMargins.y),
			(buildingPieceDimensions.z - windowMargins.x) / (windowSize.x + windowMargins.x)
		);

		return numWindows;
	}

	private float GetRandomInset(Vector2 windowSize, Vector2 windowMargins) {
		int numWindowInset = Random.Range(0, maxNumWindowInset + 1);
		float inset = numWindowInset * (windowSize.x + windowMargins.x) + windowMargins.x;
		return inset;	
	}

	private void DestroyMesh() {
		meshFilter.mesh = null;
		verts.Clear();
		uvs.Clear();
		tris.Clear();
	}
}
