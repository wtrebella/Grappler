using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingAttributeGenerator : MonoBehaviour {
	[SerializeField] private int numBuildingsToCheck = 3;
	[SerializeField] private int baseSortingOrder = 1000;
	[SerializeField] [Range(0, 1)] private float hue = 0.4f;
	[SerializeField] [Range(-1, 1)] private float baseOverlap = 0.5f;
	[SerializeField] [Range(0, 100)] private float maxHeightDifferenceBetweenBuildings = 50;
	[SerializeField] [Range(0, 100)] private float maxTopCornerVerticalOffset = 50;
	[SerializeField] [Range(0, 100)] private float maxHorizontalOffset = 50;
	[SerializeField] [Range(10, 150)] private float minFaceWidth = 30;
	[SerializeField] [Range(70, 200)]private float maxFaceWidth = 100;
	[SerializeField] [Range(100, 500)] private float minAverageHeight = 200;

	public BuildingAttributes GetNewBuildingAttributes(List<Building> buildings) {
		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.quad = GetNewBuildingQuad(buildings);
		buildingAttributes.color = GetNewBuildingColor();
		buildingAttributes.sortingOrder = GetSortingOrder(buildingAttributes.quad, buildings);

		return buildingAttributes;
	}

	private Color GetNewBuildingColor() {
		HSVColor color = new HSVColor(hue, Random.Range(0.4f, 0.6f), Random.Range(0.7f, 0.9f));
		return color.HSVToRGB();
	}

	private Quad GetNewBuildingQuad(List<Building> buildings) {
		Quad newQuad = null;
		int max = Mathf.Min(buildings.Count, numBuildingsToCheck);

		do {
			newQuad = GetNewRawBuildingQuad(buildings);
			for (int i = 1; i <= max; i++) {
				int index = buildings.Count - i;
				Building otherBuilding = buildings[index];
				Quad otherQuad = otherBuilding.attributes.quad;
				if (!QuadsAreValidTogether(newQuad, otherQuad)) {
					newQuad = null;
					break;
				}
			}
		} while (newQuad == null);

		return newQuad;
	}

	private int GetSortingOrder(Quad newQuad, List<Building> buildings) {
		int numberToCheck = Mathf.Min(buildings.Count, numBuildingsToCheck);
		int sortingOrder;
		int? sortingOrderUpperLimit = null;
		int? sortingOrderLowerLimit = null;

		for (int i = 1; i <= numberToCheck; i++) {
			int index = buildings.Count - i;
			Building otherBuilding = buildings[index];
			Quad otherQuad = otherBuilding.attributes.quad;
			if (!Quad.OverlapQuads(newQuad, otherQuad)) continue;
			if (newQuad.ContainsTopPointsFromOtherQuad(otherQuad)) {
				if (sortingOrderUpperLimit != null) sortingOrderUpperLimit = Mathf.Min((int)sortingOrderUpperLimit, otherBuilding.attributes.sortingOrder);
				else sortingOrderUpperLimit = otherBuilding.attributes.sortingOrder;
			}
			else if (otherQuad.ContainsTopPointsFromOtherQuad(newQuad)) {
				if (sortingOrderLowerLimit != null) sortingOrderLowerLimit = Mathf.Max((int)sortingOrderLowerLimit, otherBuilding.attributes.sortingOrder);
				else sortingOrderLowerLimit = otherBuilding.attributes.sortingOrder;
			}
		}

		if (sortingOrderLowerLimit == null && sortingOrderUpperLimit == null) sortingOrder = baseSortingOrder;
		else if (sortingOrderLowerLimit == null) sortingOrder = (int)sortingOrderUpperLimit - 10;
		else if (sortingOrderUpperLimit == null) sortingOrder = (int)sortingOrderLowerLimit + 10;
		else sortingOrder = ((int)sortingOrderUpperLimit + (int)sortingOrderLowerLimit) / 2;

		return sortingOrder;
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

	private bool QuadsAreValidTogether(Quad quad1, Quad quad2) {
		if (quad1.ContainsTopPointsFromOtherQuad(quad2) && quad2.ContainsTopPointsFromOtherQuad(quad1)) return false;
		else return true;
	}
}
