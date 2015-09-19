using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BuildingMeshCreator : MonoBehaviour {
	[SerializeField] private Material mat1;
	[SerializeField] private Material mat2;
	[SerializeField] private Material mat3;
	[SerializeField] private Vector2 minMaxWindowMargin = new Vector2(2, 10);
	[SerializeField] private Vector2 minWindowDimensions = new Vector2(10, 10);
	[SerializeField] private Vector2 maxWindowDimensions = new Vector2(80, 80);
	[SerializeField] private int maxBuildingPieces = 3;

	private BuildingPieceAttributeGenerator buildingPieceAttributeGenerator;
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

		buildingPieceAttributeGenerator = GetComponent<BuildingPieceAttributeGenerator>();
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
		int numBuildingPieces = GetRandomNumBuildingPieces();

		Rect3D previousBuildingPieceRect = new Rect3D();
		Vector2 windowSize = GetRandomWindowSize();
		Vector2 marginSize = GetRandomMarginSize();

		for (int i = 0; i < numBuildingPieces; i++) {
			BuildingPieceAttributes pieceAttributes = 
				buildingPieceAttributeGenerator.GetRandomBuildingPieceAttributes(
					previousBuildingPieceRect, 
					windowSize, 
					marginSize);

			CreateFace(pieceAttributes, BuildingFaceType.Front);
			CreateFace(pieceAttributes, BuildingFaceType.Right);

			previousBuildingPieceRect.origin = pieceAttributes.origin;
			previousBuildingPieceRect.size = pieceAttributes.dimensions;
			previousBuildingPieceRect.Log();
		}

		ApplyToMesh();
	}

	private void ApplyToMesh() {
		meshFilter.mesh.vertices = verts.ToArray();
		meshFilter.mesh.triangles = tris.ToArray();
		meshFilter.mesh.uv = uvs.ToArray();
		
		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}

	private void CreateChunk(Rect3D chunkRect, UVRect uvRect) {
		int startIndex = verts.Count;

		verts.Add(new Vector3(chunkRect.origin.x, chunkRect.origin.y, chunkRect.origin.z));
		verts.Add(new Vector3(chunkRect.origin.x, chunkRect.origin.y + chunkRect.size.y, chunkRect.origin.z));
		verts.Add(new Vector3(chunkRect.origin.x + chunkRect.size.x, chunkRect.origin.y + chunkRect.size.y, chunkRect.origin.z + chunkRect.size.z));
		verts.Add(new Vector3(chunkRect.origin.x + chunkRect.size.x, chunkRect.origin.y, chunkRect.origin.z + chunkRect.size.z));
		
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
	
	private void CreateFace(BuildingPieceAttributes pieceAttributes, BuildingFaceType faceType) {
		AbstractBuildingPieceFaceAttributes faceAttributes = GetFaceAttributes(pieceAttributes, faceType);

		for (int y = 0; y <= pieceAttributes.chunkCount.y; y++) {
			for (int x = 0; x <= pieceAttributes.chunkCount.x; x++) {
				IntVector2 coordinates = new IntVector2(x, y);
				if (faceAttributes.HasWindows()) CreateWindowFaceSection(coordinates, pieceAttributes, faceType);
				else CreateBlankFaceSection(coordinates, pieceAttributes, faceType);
			}
		}
	}

	private void CreateBlankFaceSection(IntVector2 chunkCoordinates, BuildingPieceAttributes pieceAttributes, BuildingFaceType faceType) {
		AbstractBuildingPieceFaceAttributes faceAttributes = GetFaceAttributes(pieceAttributes, faceType);
		Rect3D chunkRect = faceAttributes.GetBlankRect(chunkCoordinates);
		CreateChunk(chunkRect, windowMarginUVRect);
	}

	private void CreateWindowFaceSection(IntVector2 chunkCoordinates, BuildingPieceAttributes pieceAttributes, BuildingFaceType faceType) {
		AbstractBuildingPieceFaceAttributes faceAttributes = GetFaceAttributes(pieceAttributes, faceType);

		Rect3D cornerMarginRect = faceAttributes.GetCornerMarginRect(chunkCoordinates);
		Rect3D horizontalMarginRect = faceAttributes.GetHorizontalMarginRect(chunkCoordinates);
		Rect3D verticalMarginRect = faceAttributes.GetVerticalMarginOrigin(chunkCoordinates);
		Rect3D windowRect = faceAttributes.GetWindowRect(chunkCoordinates);

		CreateChunk(cornerMarginRect, windowMarginUVRect);
		if (!pieceAttributes.IsLastChunkX(chunkCoordinates.x)) CreateChunk(horizontalMarginRect, windowMarginUVRect);
		if (!pieceAttributes.IsLastChunkY(chunkCoordinates.y)) CreateChunk(verticalMarginRect, windowMarginUVRect);
		if (!pieceAttributes.IsLastChunkX(chunkCoordinates.x) && !pieceAttributes.IsLastChunkY(chunkCoordinates.y)) CreateChunk(windowRect, windowUVRect);
	}

	private AbstractBuildingPieceFaceAttributes GetFaceAttributes(BuildingPieceAttributes pieceAttributes, BuildingFaceType faceType) {
		AbstractBuildingPieceFaceAttributes faceAttributes = null;
		if (faceType == BuildingFaceType.Front) faceAttributes = pieceAttributes.frontFaceAttributes;
		else if (faceType == BuildingFaceType.Right) faceAttributes = pieceAttributes.rightFaceAttributes;
		return faceAttributes;
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

	private int GetRandomNumBuildingPieces() {
		return Random.Range(1, maxBuildingPieces + 1);
	}

	private void DestroyMesh() {
		meshFilter.mesh = null;
		verts.Clear();
		uvs.Clear();
		tris.Clear();
	}
}
