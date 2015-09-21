using UnityEngine;
using System.Collections;

public class BuildingPieceTopFaceAttributes : AbstractBuildingPieceFaceAttributes {	
	public override bool HasWindows() {
		return false;
	}

	public override Vector3 GetOrigin() {
		Vector3 origin = buildingPieceAttributes.origin;
		origin.y += buildingPieceAttributes.dimensions.y;
		return origin;
	}

	public override Rect3D GetFullBlankRect() {
		Rect3D chunkRect = new Rect3D();
		Vector3 faceOrigin = GetOrigin();

		chunkRect.size = new Vector3(buildingPieceAttributes.dimensions.x, 0, buildingPieceAttributes.dimensions.z);
		chunkRect.origin = faceOrigin;
		
		return chunkRect;
	}

	public override Rect3D GetBlankRect (IntVector2 chunkCoordinates) {
		throw new System.NotImplementedException ();
	}

	public override Rect3D GetCornerMarginRect (IntVector2 chunkCoordinates) {
		throw new System.NotImplementedException ();
	}

	public override Rect3D GetHorizontalMarginRect (IntVector2 chunkCoordinates) {
		throw new System.NotImplementedException ();
	}

	public override Rect3D GetVerticalMarginOrigin (IntVector2 chunkCoordinates) {
		throw new System.NotImplementedException ();
	}

	public override Rect3D GetWindowRect (IntVector2 chunkCoordinates) {
		throw new System.NotImplementedException ();
	}
}
