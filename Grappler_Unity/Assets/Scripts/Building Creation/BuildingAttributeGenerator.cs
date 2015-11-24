using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingAttributeGenerator : MonoBehaviour {
	[SerializeField] [Range(0, 1)] private float baseOverlap = 0.5f;
	[SerializeField] [Range(10, 100)] private float maxHeightDifferenceBetweenBuildings = 50;
	[SerializeField] [Range(10, 100)] private float maxTopCornerVerticalOffset = 50;
	[SerializeField] [Range(10, 100)] private float maxHorizontalOffset = 50;
	[SerializeField] [Range(10, 50)] private float minFaceWidth = 30;
	[SerializeField] [Range(70, 200)]private float maxFaceWidth = 100;
	[SerializeField] [Range(100, 300)] private float minAverageHeight = 200;

	public BuildingAttributes GetNewBuildingAttributes(List<Building> buildings) {
		Quad quad = GetNewBuildingQuad(buildings);

		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.quad = quad;
		buildingAttributes.color = new Color(Random.value, Random.value, Random.value);
		buildingAttributes.layer = 0;

		return buildingAttributes;
	}

	private Quad GetNewBuildingQuad(List<Building> buildings) {
		return GetNewRawBuildingQuad(buildings);
	}

	private Quad GetNewRawBuildingQuad(List<Building> buildings) {
		Building previousBuilding;
		Quad previousQuad = Quad.zero;
		if (buildings.Count > 0) {
			previousBuilding = buildings.GetLastItem();
			previousQuad = previousBuilding.attributes.quad;
		}
		
		float bottomLeftX = previousQuad.bottomRight.x + Random.Range(-previousQuad.bottomWidth / 2f * baseOverlap, previousQuad.bottomWidth / 2f);
		float bottomFaceWidth = Random.Range(minFaceWidth, maxFaceWidth);
		float topFaceWidth = Random.Range(minFaceWidth, maxFaceWidth);
		float horizontalOffset = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
		float topCornerVerticalOffset = Random.Range(-maxTopCornerVerticalOffset, maxTopCornerVerticalOffset);
		float heightDifferenceBetweenBuildings = Random.Range(-maxHeightDifferenceBetweenBuildings, maxHeightDifferenceBetweenBuildings);
		float averageHeight = Mathf.Max(minAverageHeight, previousQuad.averageHeight + heightDifferenceBetweenBuildings);
		float leftHeight = averageHeight - topCornerVerticalOffset / 2f;
		float rightHeight = averageHeight + topCornerVerticalOffset / 2f;
		
		Vector2 bottomLeft = new Vector2(bottomLeftX, 0);
		Vector2 bottomRight = new Vector2(bottomLeft.x + bottomFaceWidth, 0);
		Vector2 topLeft = new Vector2(bottomLeft.x + horizontalOffset, leftHeight);
		Vector2 topRight = new Vector2(topLeft.x + topFaceWidth, rightHeight);
		
		Quad quad = new Quad(bottomLeft, topLeft, topRight, bottomRight);
		return quad;
	}
}
