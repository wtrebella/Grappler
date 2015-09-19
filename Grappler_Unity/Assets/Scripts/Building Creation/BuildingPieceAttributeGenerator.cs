using UnityEngine;
using System.Collections;

public class BuildingPieceAttributeGenerator : MonoBehaviour {
	[SerializeField] private Vector3 minBuildingPieceSize = new Vector3(90, 110, 90);
	[SerializeField] private Vector3 maxBuildingPieceSize = new Vector3(210, 400, 300);
	[SerializeField] private int maxNumWindowInset = 0;

	public Vector3 GetMinBuildingPieceSize() {
		return minBuildingPieceSize;
	}

	public BuildingPieceAttributes GetRandomBuildingPieceAttributes(Rect3D previousBuildingPieceRect, Vector2 windowSize, Vector2 marginSize) {
		Vector3 buildingPieceDimensions = GetRandomBuildingPieceDimensions();
		IntVector3 numWindows = GetNumWindows(buildingPieceDimensions, windowSize, marginSize);
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
		buildingPieceAttributes.dimensions = buildingPieceDimensions;
		buildingPieceAttributes.chunkCount = chunkCount;	
		buildingPieceAttributes.InitializeFaceAttributes();

		return buildingPieceAttributes;
	}

	private Vector3 GetRandomBuildingPieceDimensions() {
		Vector3 v = new Vector3();
		v.x = Random.Range(minBuildingPieceSize.x, maxBuildingPieceSize.x);
		v.y = Random.Range(minBuildingPieceSize.y, maxBuildingPieceSize.y);
		v.z = Random.Range(minBuildingPieceSize.z, maxBuildingPieceSize.z);
		return v;
	}
	
	private IntVector3 GetNumWindows(Vector3 buildingPieceDimensions, Vector2 windowSize, Vector2 marginSize) {
		IntVector3 numWindows = new IntVector3(
			(buildingPieceDimensions.x - marginSize.x) / (windowSize.x + marginSize.x),
			(buildingPieceDimensions.y - marginSize.y) / (windowSize.y + marginSize.y),
			(buildingPieceDimensions.z - marginSize.x) / (windowSize.x + marginSize.x)
		);
		
		return numWindows;
	}
	
	private float GetRandomInset(Vector2 windowSize, Vector2 windowMargins) {
		int numWindowInset = Random.Range(0, maxNumWindowInset + 1);
		float inset = numWindowInset * (windowSize.x + windowMargins.x) + windowMargins.x;
		return inset;	
	}
}
