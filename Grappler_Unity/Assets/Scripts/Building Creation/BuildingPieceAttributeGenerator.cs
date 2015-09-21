using UnityEngine;
using System.Collections;

public class BuildingPieceAttributeGenerator : MonoBehaviour {
	[SerializeField] private Vector3 minBuildingPieceSize = new Vector3(90, 110, 90);
	[SerializeField] private Vector3 maxBuildingPieceSize = new Vector3(210, 400, 300);
	[SerializeField] private int maxNumWindowInset = 0;

	public Vector3 GetMinBuildingPieceSize() {
		return minBuildingPieceSize;
	}

	public BuildingPieceAttributes GetRandomBuildingPieceAttributes(Rect3D previousBuildingPieceRect, IntVector2 windowSize, IntVector2 marginSize) {
		Vector3 dimensions = GetRandomRoughBuildingPieceDimensions();
		IntVector3 numWindows = GetNumWindows(dimensions, windowSize, marginSize);
		dimensions = ClampDimensions(dimensions, numWindows, windowSize, marginSize);
		float inset = GetRandomInset(windowSize, marginSize);
		
		Vector3 origin = new Vector3(previousBuildingPieceRect.origin.x + previousBuildingPieceRect.size.x, 0, inset);
		
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
		buildingPieceAttributes.dimensions = dimensions;
		buildingPieceAttributes.chunkCount = chunkCount;	
		buildingPieceAttributes.InitializeFaceAttributes();

		return buildingPieceAttributes;
	}

	private Vector3 GetRandomRoughBuildingPieceDimensions() {
		Vector3 v = new Vector3();
		v.x = Random.Range(minBuildingPieceSize.x, maxBuildingPieceSize.x);
		v.y = Random.Range(minBuildingPieceSize.y, maxBuildingPieceSize.y);
		v.z = v.x;
		return v;
	}

	private Vector3 ClampDimensions(Vector3 roughDimensions, IntVector3 numWindows, IntVector2 windowSize, IntVector2 marginSize) {
		Vector3 dimensions = roughDimensions;
		dimensions.x = (windowSize.x + marginSize.x) * numWindows.x + marginSize.x;
		dimensions.y = (windowSize.y + marginSize.y) * numWindows.y + marginSize.y;
		dimensions.z = dimensions.x;
		return dimensions;
	}
	
	private IntVector3 GetNumWindows(Vector3 buildingPieceDimensions, IntVector2 windowSize, IntVector2 marginSize) {
		IntVector3 numWindows = new IntVector3();
		numWindows.x = (int)((buildingPieceDimensions.x - marginSize.x) / (windowSize.x + marginSize.x));
		numWindows.y = (int)((buildingPieceDimensions.y - marginSize.y) / (windowSize.y + marginSize.y));
		numWindows.z = numWindows.x;
		return numWindows;
	}
	
	private int GetRandomInset(IntVector2 windowSize, IntVector2 marginSize) {
		int numWindowInset = Random.Range(0, maxNumWindowInset + 1);
		int inset = numWindowInset * (windowSize.x + marginSize.x);
		if (numWindowInset > 0) inset += marginSize.x;
		return inset;	
	}
}
