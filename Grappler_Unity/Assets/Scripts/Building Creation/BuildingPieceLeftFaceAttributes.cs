using UnityEngine;
using System.Collections;

public class BuildingPieceLeftFaceAttributes : AbstractBuildingPieceFaceAttributes {	
	public override bool HasWindows() {
		return buildingPieceAttributes.numWindows.z > 0 || buildingPieceAttributes.numWindows.y > 0;
	}

	public override Vector3 GetOrigin() {
		Vector3 origin = buildingPieceAttributes.origin;
		origin.z += buildingPieceAttributes.dimensions.z;
		return origin;
	}

	public override Rect3D GetBlankRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		Vector3 faceOrigin = GetOrigin();

		if (buildingPieceAttributes.numWindows.z == 0 && buildingPieceAttributes.numWindows.y == 0) {
			chunkRect.size = new Vector3(0, buildingPieceAttributes.dimensions.y, -buildingPieceAttributes.dimensions.z);
			chunkRect.origin = faceOrigin;
		}
		else if (buildingPieceAttributes.numWindows.z == 0) {
			chunkRect.size = new Vector3(0, buildingPieceAttributes.dimensions.y / buildingPieceAttributes.chunkCount.y, -buildingPieceAttributes.dimensions.z);
			chunkRect.origin = new Vector3(faceOrigin.x, faceOrigin.y + chunkRect.size.y * chunkCoordinates.y, faceOrigin.z);
		}
		else if (buildingPieceAttributes.numWindows.y == 0) {
			chunkRect.size = new Vector3(0, buildingPieceAttributes.dimensions.y, -buildingPieceAttributes.dimensions.z / buildingPieceAttributes.chunkCount.z);
			chunkRect.origin = new Vector3(faceOrigin.x, faceOrigin.y, faceOrigin.z + chunkRect.size.z * chunkCoordinates.x);
		}
		return chunkRect;
	}
	
	public override Rect3D GetCornerMarginRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(0, buildingPieceAttributes.marginSize.y, -buildingPieceAttributes.marginSize.x);
		
		chunkRect.origin.z -= chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);

		return chunkRect;
	}
	
	public override Rect3D GetHorizontalMarginRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(0, buildingPieceAttributes.marginSize.y, -buildingPieceAttributes.windowSize.x);
		
		chunkRect.origin.z -= chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);
		
		return chunkRect;
	}
	
	public override Rect3D GetVerticalMarginOrigin(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(0, buildingPieceAttributes.windowSize.y, -buildingPieceAttributes.marginSize.x);

		chunkRect.origin.z -= chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return chunkRect;
	}
	
	public override Rect3D GetWindowRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(0, buildingPieceAttributes.windowSize.y, -buildingPieceAttributes.windowSize.x);

		chunkRect.origin.z -= chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return chunkRect;
	}

	public override Rect3D GetFullBlankRect () {
		throw new System.NotImplementedException ();
	}
}
