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
		float padding = 0.1f;

		windowUVRect = new UVRect();
		windowUVRect.bottomLeft = new Vector2(0, 0);
		windowUVRect.topLeft = new Vector2(0, 0.5f);
		windowUVRect.topRight = new Vector2(0.25f, 0.5f);
		windowUVRect.bottomRight = new Vector2(0.25f, 0);
		windowUVRect.SetPadding(padding);

		roofUVRect = new UVRect();
		roofUVRect.bottomLeft = new Vector2(0.5f, 0);
		roofUVRect.topLeft = new Vector2(0.5f, 1);
		roofUVRect.topRight = new Vector2(1, 1);
		roofUVRect.bottomRight = new Vector2(1, 0);
		roofUVRect.SetPadding(padding);

		windowMarginUVRect = new UVRect();
		windowMarginUVRect.bottomLeft = new Vector2(0.25f, 0);
		windowMarginUVRect.topLeft = new Vector2(0.25f, 0.5f);
		windowMarginUVRect.topRight = new Vector2(0.5f, 0.5f);
		windowMarginUVRect.bottomRight = new Vector2(0.5f, 0);
		windowMarginUVRect.SetPadding(padding);
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
			Vector3 rightFaceOrigin = new Vector3(origin.x + buildingPieceDimensions.x, origin.y, origin.z);
			Vector3 leftFaceOrigin = new Vector3(origin.x, origin.y, origin.z + buildingPieceDimensions.z);
			Vector3 topFaceOrigin = new Vector3(origin.x, origin.y + buildingPieceDimensions.y, origin.z);

			IntVector3 chunkCount = new IntVector3(
				Mathf.Max(numWindows.x, 1),
				Mathf.Max(numWindows.y, 1),
				Mathf.Max(numWindows.z, 1)
			);

			BuildingPieceAttributes buildingPieceAttributes = new BuildingPieceAttributes();
			buildingPieceAttributes.origin = origin;
			buildingPieceAttributes.numWindows = numWindows;
			buildingPieceAttributes.windowSize = windowSize;
			buildingPieceAttributes.marginSize = marginSize;
			buildingPieceAttributes.buildingPieceDimensions = buildingPieceDimensions;
			buildingPieceAttributes.chunkCount = chunkCount;	

			CreateFrontFace(buildingPieceAttributes);
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

	private void CreateSquare(Vector3 squareOrigin, Vector3 squareSize, UVRect uvRect) {
		int startIndex = verts.Count;

		verts.Add(new Vector3(
			squareOrigin.x,
			squareOrigin.y,
			squareOrigin.z
		));
		
		verts.Add(new Vector3(
			squareOrigin.x,
			squareOrigin.y + squareSize.y,
			squareOrigin.z
		));
		
		verts.Add(new Vector3(
			squareOrigin.x + squareSize.x,
			squareOrigin.y + squareSize.y,
			squareOrigin.z + squareSize.z
		));
		
		verts.Add(new Vector3(
			squareOrigin.x + squareSize.x,
			squareOrigin.y,
			squareOrigin.z + squareSize.z
		));
		
		uvs.Add(uvRect.bottomLeft);
		uvs.Add(uvRect.topLeft);
		uvs.Add(uvRect.topRight);
		uvs.Add(uvRect.bottomRight);
		
		tris.Add(startIndex + 0);
		tris.Add(startIndex + 1);
		tris.Add(startIndex + 2);
		
		tris.Add(startIndex + 2);
		tris.Add(startIndex + 3);
		tris.Add(startIndex + 0);
	}

	private void CreateFrontFaceChunkWithNoWindow(int chunkX, int chunkY, BuildingPieceAttributes attributes) {
		Vector3 squareSize = attributes.GetFrontFaceSquareSize();
		Vector3 squareOrigin = attributes.GetFrontFaceNonWindowChunkOrigin(chunkX, chunkY, squareSize);
			
		CreateSquare(squareOrigin, squareSize, windowMarginUVRect);
	}

	private void CreateFrontFaceChunkWithWindow(int chunkX, int chunkY, BuildingPieceAttributes attributes) {
		Vector3 cornerMarginOrigin = attributes.GetFrontFaceCornerMarginOrigin(chunkX, chunkY);
		Vector3 horizontalMarginOrigin = attributes.GetFrontFaceHorizontalMarginOrigin(chunkX, chunkY);
		Vector3 verticalMarginOrigin = attributes.GetFrontFaceVerticalMarginOrigin(chunkX, chunkY);
		Vector3 windowOrigin = attributes.GetFrontFaceWindowOrigin(chunkX, chunkY);
		
		CreateSquare(cornerMarginOrigin, attributes.GetFrontFaceCornerMarginSize(), windowMarginUVRect);
		if (!attributes.IsLastChunkX(chunkX)) CreateSquare(horizontalMarginOrigin, attributes.GetFrontFaceHorizontalMarginSize(), windowMarginUVRect);
		if (!attributes.IsLastChunkY(chunkY)) CreateSquare(verticalMarginOrigin, attributes.GetFrontFaceVerticalMarginSize(), windowMarginUVRect);
		if (!attributes.IsLastChunkX(chunkX) && !attributes.IsLastChunkY(chunkY)) CreateSquare(windowOrigin, attributes.GetFrontFaceWindowSize(), windowUVRect);
	}

	private void CreateFrontFace(BuildingPieceAttributes attributes) {
		Vector3 frontFaceOrigin = attributes.GetFrontFaceOrigin();

		for (int y = 0; y <= attributes.chunkCount.y; y++) {
			for (int x = 0; x <= attributes.chunkCount.x; x++) {
				if (attributes.numWindows.x == 0 || attributes.numWindows.y == 0) CreateFrontFaceChunkWithNoWindow(x, y, attributes);
				else CreateFrontFaceChunkWithWindow(x, y, attributes);
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
		float x = Random.Range(minWindowDimensions.x, maxWindowDimensions.x);
		float y = Random.Range(minWindowDimensions.y, maxWindowDimensions.y);
	
		return new Vector2(x, y);
	}

	private Vector2 GetRandomMarginSize() {
		float x = Random.Range(minMaxWindowMargin.x, minMaxWindowMargin.y);
		float y = Random.Range(minMaxWindowMargin.x, minMaxWindowMargin.y);
		return new Vector3(x, y);
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
