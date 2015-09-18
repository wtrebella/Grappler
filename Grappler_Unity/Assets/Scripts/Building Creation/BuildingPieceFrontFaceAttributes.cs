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
		Vector3 chunkOrigin = Vector3.zero;
		Vector3 chunkSize = Vector3.zero;
		Vector3 faceOrigin = GetOrigin();

		if (buildingPieceAttributes.numWindows.x == 0 && buildingPieceAttributes.numWindows.y == 0) {
			chunkSize = new Vector3(buildingPieceAttributes.dimensions.x, buildingPieceAttributes.dimensions.y, 0);
			chunkOrigin = faceOrigin;
		}
		else if (buildingPieceAttributes.numWindows.x == 0) {
			chunkSize = new Vector3(buildingPieceAttributes.dimensions.x, buildingPieceAttributes.dimensions.y / buildingPieceAttributes.chunkCount.y, 0);
			chunkOrigin = new Vector3(faceOrigin.x, faceOrigin.y + chunkSize.y * chunkCoordinates.y, faceOrigin.z);
		}
		else if (buildingPieceAttributes.numWindows.y == 0) {
			chunkSize = new Vector3(buildingPieceAttributes.dimensions.x / buildingPieceAttributes.chunkCount.x, buildingPieceAttributes.dimensions.y, 0);
			chunkOrigin = new Vector3(faceOrigin.x + chunkSize.x * chunkCoordinates.x, faceOrigin.y, faceOrigin.z);
		}
		
		return new Rect3D(chunkOrigin, chunkSize);
	}
	
	public override Rect3D GetCornerMarginRect(IntVector2 chunkCoordinates) {
		Vector3 chunkOrigin = GetOrigin();
		Vector3 chunkSize = new Vector3(buildingPieceAttributes.marginSize.x, buildingPieceAttributes.marginSize.y, 0);
		
		chunkOrigin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkOrigin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);
		
		return new Rect3D(chunkOrigin, chunkSize);
	}
	
	public override Rect3D GetHorizontalMarginRect(IntVector2 chunkCoordinates) {
		Vector3 chunkOrigin = GetOrigin();
		Vector3 chunkSize = new Vector3(buildingPieceAttributes.windowSize.x, buildingPieceAttributes.marginSize.y, 0);
		
		chunkOrigin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkOrigin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y);
		
		return new Rect3D(chunkOrigin, chunkSize);
	}
	
	public override Rect3D GetVerticalMarginOrigin(IntVector2 chunkCoordinates) {
		Vector3 chunkOrigin = GetOrigin();
		Vector3 chunkSize = new Vector3(buildingPieceAttributes.marginSize.x, buildingPieceAttributes.windowSize.y, 0);

		chunkOrigin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x);
		chunkOrigin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return new Rect3D(chunkOrigin, chunkSize);
	}
	
	public override Rect3D GetWindowRect(IntVector2 chunkCoordinates) {
		Vector3 chunkOrigin = GetOrigin();
		Vector3 chunkSize = new Vector3(buildingPieceAttributes.windowSize.x, buildingPieceAttributes.windowSize.y, 0);

		chunkOrigin.x += chunkCoordinates.x * (buildingPieceAttributes.windowSize.x + buildingPieceAttributes.marginSize.x) + buildingPieceAttributes.marginSize.x;
		chunkOrigin.y += chunkCoordinates.y * (buildingPieceAttributes.windowSize.y + buildingPieceAttributes.marginSize.y) + buildingPieceAttributes.marginSize.y;
		
		return new Rect3D(chunkOrigin, chunkSize);
	}
}
