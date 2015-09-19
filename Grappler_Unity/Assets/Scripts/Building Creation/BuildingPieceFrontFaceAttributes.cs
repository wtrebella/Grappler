using UnityEngine;
using System.Collections;

public class BuildingPieceFrontFaceAttributes : AbstractBuildingPieceFaceAttributes {
	public override bool HasWindows() {
		return buildingPieceAttributes.numWindows.x > 0 || buildingPieceAttributes.numWindows.y > 0;
	}

	public override Vector3 GetOrigin() {
		return buildingPieceAttributes.origin;
	}
	
	public override Rect3D GetBlankRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		Vector3 faceOrigin = GetOrigin();

		if (buildingPieceAttributes.numWindows.x == 0 && buildingPieceAttributes.numWindows.y == 0) {
			chunkRect.size = new Vector3(buildingPieceAttributes.dimensions.x, buildingPieceAttributes.dimensions.y, 0);
			chunkRect.origin = faceOrigin;
		}
		else if (buildingPieceAttributes.numWindows.x == 0) {
			chunkRect.size = new Vector3(buildingPieceAttributes.dimensions.x, buildingPieceAttributes.dimensions.y / buildingPieceAttributes.chunkCount.y, 0);
			chunkRect.origin = new Vector3(faceOrigin.x, faceOrigin.y + chunkRect.size.y * chunkCoordinates.y, faceOrigin.z);
		}
		else if (buildingPieceAttributes.numWindows.y == 0) {
			chunkRect.size = new Vector3(buildingPieceAttributes.dimensions.x / buildingPieceAttributes.chunkCount.x, buildingPieceAttributes.dimensions.y, 0);
			chunkRect.origin = new Vector3(faceOrigin.x + chunkRect.size.x * chunkCoordinates.x, faceOrigin.y, faceOrigin.z);
		}
		
		return chunkRect;
	}
	
	public override Rect3D GetCornerMarginRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(buildingPieceAttributes.marginSize.x, buildingPieceAttributes.marginSize.y, 0);
		
		chunkRect.origin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);
		
		return chunkRect;
	}
	
	public override Rect3D GetHorizontalMarginRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(buildingPieceAttributes.windowSize.x, buildingPieceAttributes.marginSize.y, 0);
		
		chunkRect.origin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);
		
		return chunkRect;
	}
	
	public override Rect3D GetVerticalMarginOrigin(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(buildingPieceAttributes.marginSize.x, buildingPieceAttributes.windowSize.y, 0);
		
		chunkRect.origin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return chunkRect;
	}
	
	public override Rect3D GetWindowRect(IntVector2 chunkCoordinates) {
		Rect3D chunkRect = new Rect3D();
		chunkRect.origin = GetOrigin();
		chunkRect.size = new Vector3(buildingPieceAttributes.windowSize.x, buildingPieceAttributes.windowSize.y, 0);
		
		chunkRect.origin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkRect.origin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return chunkRect;
	}
}
