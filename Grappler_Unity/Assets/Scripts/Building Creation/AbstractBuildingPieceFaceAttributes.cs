using UnityEngine;
using System.Collections;

public abstract class AbstractBuildingPieceFaceAttributes {
	public BuildingPieceAttributes buildingPieceAttributes;

	public abstract bool HasWindows();
	public abstract Vector3 GetOrigin();
	public abstract Rect3D GetBlankRect(IntVector2 chunkCoordinates);
	public abstract Rect3D GetCornerMarginRect(IntVector2 chunkCoordinates);
	public abstract Rect3D GetHorizontalMarginRect(IntVector2 chunkCoordinates);
	public abstract Rect3D GetVerticalMarginOrigin(IntVector2 chunkCoordinates);
	public abstract Rect3D GetWindowRect(IntVector2 chunkCoordinates);
}
