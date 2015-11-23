using UnityEngine;
using System.Collections;

public class BuildingAttributeGenerator : MonoBehaviour {
	[SerializeField] [Range(0, 1)] private float baseOverlap = 0.5f;
	[SerializeField] [Range(10, 100)] private float maxHeightDifferenceBetweenBuildings = 50;
	[SerializeField] [Range(10, 100)] private float maxTopCornerVerticalOffset = 50;
	[SerializeField] [Range(10, 100)] private float maxHorizontalOffset = 50;
	[SerializeField] [Range(10, 50)] private float minFaceWidth = 30;
	[SerializeField] [Range(70, 200)]private float maxFaceWidth = 100;
	[SerializeField] [Range(100, 300)] private float minAverageHeight = 200;

	public BuildingAttributes GetRandomBuildingAttributes(BuildingAttributes previousBuildingAttributes) {
		Quad skewedRect = GetRandomSkewedRect(previousBuildingAttributes);

		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.quad = skewedRect;
		buildingAttributes.color = new Color(Random.value, Random.value, Random.value);

		return buildingAttributes;
	}

	private Quad GetRandomSkewedRect(BuildingAttributes previousAttributes) {
		Quad previousQuad = new Quad(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
		if (previousAttributes != null) previousQuad = previousAttributes.quad;

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
		Vector2 topRight = new Vector2(bottomRight.x + horizontalOffset, rightHeight);

		Quad quad = new Quad(bottomLeft, topLeft, topRight, bottomRight);
		return quad;
	}
}
