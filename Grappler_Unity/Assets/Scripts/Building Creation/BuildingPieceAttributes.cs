using UnityEngine;
using System.Collections;

public class BuildingPieceAttributes {
	public IntVector3 numWindows;
	public IntVector3 chunkCount;
	public Vector3 origin;
	public Vector2 windowSize;
	public Vector2 marginSize;
	public Vector3 dimensions;

	public bool faceAttributesInitialized {get; private set;}

	private BuildingPieceFrontFaceAttributes _frontFaceAttributes;
	public BuildingPieceFrontFaceAttributes frontFaceAttributes {
		get {
			AssertFaceAttributesAreInitialized();
			return _frontFaceAttributes;
		}
	}

	private BuildingPieceRightFaceAttributes _rightFaceAttributes;
	public BuildingPieceRightFaceAttributes rightFaceAttributes {
		get {
			AssertFaceAttributesAreInitialized();
			return _rightFaceAttributes;
		}
	}

	public BuildingPieceAttributes() {
		faceAttributesInitialized = false;
	}

	public void InitializeFaceAttributes() {
		InitializeFrontFaceAttributes();
		InitializeRightFaceAttributes();
		InitializeLeftFaceAttributes();
		faceAttributesInitialized = true;
	}

	public bool IsLastChunkX(int chunkX) {
		return chunkX == chunkCount.x;
	}

	public bool IsLastChunkY(int chunkY) {
		return chunkY == chunkCount.y;
	}

	public bool IsLastChunkZ(int chunkZ) {
		return chunkZ == chunkCount.z;
	}

	private void InitializeFrontFaceAttributes() {
		_frontFaceAttributes = new BuildingPieceFrontFaceAttributes();
		_frontFaceAttributes.buildingPieceAttributes = this;
	}

	private void InitializeRightFaceAttributes() {
		_rightFaceAttributes = new BuildingPieceRightFaceAttributes();
		_rightFaceAttributes.buildingPieceAttributes = this;
	}

	private void InitializeLeftFaceAttributes() {
		
	}

	private void AssertFaceAttributesAreInitialized() {
		if (!faceAttributesInitialized) Debug.LogError("face attributes not initialized! make sure you initialize them after you set the overall building attributes.");
	}
}
