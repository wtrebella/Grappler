using UnityEngine;
using System.Collections;

public class BuildingPieceAttributes {
	public IntVector3 numWindows;
	public IntVector3 chunkCount;
	public Vector3 origin;
	public Vector2 windowSize;
	public Vector2 marginSize;
	public Vector3 buildingPieceDimensions;

	public BuildingPieceAttributes() {

	}

	public Vector3 GetFrontFaceOrigin() {
		return origin;
	}

	public Vector3 GetRightFaceOrigin() {
		Vector3 rightFaceOrigin = origin;
		rightFaceOrigin.x += buildingPieceDimensions.x;
		return rightFaceOrigin;
	}

	public Vector3 GetLeftFaceOrigin() {
		Vector3 leftFaceOrigin = origin;
		leftFaceOrigin.z += buildingPieceDimensions.z;
		return leftFaceOrigin;
	}

	public Vector3 GetFrontFaceNonWindowChunkOrigin(int chunkX, int chunkY, Vector2 squareSize) {
		Vector3 squareOrigin = Vector3.zero;
		Vector3 faceOrigin = GetFrontFaceOrigin();

		if (numWindows.x == 0 && numWindows.y == 0) {
			squareOrigin = faceOrigin;
		}
		else if (numWindows.x == 0) {
			squareOrigin = new Vector3(faceOrigin.x, faceOrigin.y + squareSize.y * chunkY, faceOrigin.z);
		}
		else if (numWindows.y == 0) {
			squareOrigin = new Vector3(faceOrigin.x + squareSize.x * chunkX, faceOrigin.y, faceOrigin.z);
		}

		return squareOrigin;
	}

	public Vector3 GetFrontFaceSquareSize() {
		Vector3 squareSize = Vector3.zero;

		if (numWindows.x == 0 && numWindows.y == 0) {
			squareSize = new Vector3(buildingPieceDimensions.x, buildingPieceDimensions.y, 0);
		}
		else if (numWindows.x == 0) {
			squareSize = new Vector3(buildingPieceDimensions.x, buildingPieceDimensions.y / chunkCount.y, 0);
		}
		else if (numWindows.y == 0) {
			squareSize = new Vector3(buildingPieceDimensions.x / chunkCount.x, buildingPieceDimensions.y, 0);
		}

		return squareSize;
	}

	public Vector3 GetFrontFaceCornerMarginOrigin(int chunkX, int chunkY) {
		Vector3 marginCornerOrigin = GetFrontFaceOrigin();

		marginCornerOrigin.x += chunkX * (windowSize.x + marginSize.x);
		marginCornerOrigin.y += chunkY * (windowSize.y + marginSize.y);

		return marginCornerOrigin;
	}

	public Vector3 GetFrontFaceHorizontalMarginOrigin(int chunkX, int chunkY) {
		Vector3 marginHorizontalOrigin = GetFrontFaceOrigin();

		marginHorizontalOrigin.x += chunkX * (windowSize.x + marginSize.x) + marginSize.x;
		marginHorizontalOrigin.y += chunkY * (windowSize.y + marginSize.y);

		return marginHorizontalOrigin;
	}
	
	public Vector3 GetFrontFaceVerticalMarginOrigin(int chunkX, int chunkY) {
		Vector3 marginVerticalOrigin = GetFrontFaceOrigin();

		marginVerticalOrigin.x += chunkX * (windowSize.x + marginSize.x);
		marginVerticalOrigin.y += chunkY * (windowSize.y + marginSize.y) + marginSize.y;

		return marginVerticalOrigin;
	}

	public Vector3 GetFrontFaceWindowOrigin(int chunkX, int chunkY) {
		Vector3 windowOrigin = GetFrontFaceOrigin();

		windowOrigin.x += chunkX * (windowSize.x + marginSize.x) + marginSize.x;
		windowOrigin.y += chunkY * (windowSize.y + marginSize.y) + marginSize.y;

		return windowOrigin;
	}

	public Vector3 GetFrontFaceCornerMarginSize() {
		return new Vector3(marginSize.x, marginSize.y, 0);
	}

	public Vector3 GetFrontFaceHorizontalMarginSize() {
		return new Vector3(windowSize.x, marginSize.y, 0);
	}

	public Vector3 GetFrontFaceVerticalMarginSize() {
		return new Vector3(marginSize.x, windowSize.y, 0);
	}

	public Vector3 GetFrontFaceWindowSize() {
		return new Vector3(windowSize.x, windowSize.y, 0);
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
}
