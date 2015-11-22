using UnityEngine;
using System.Collections;

public class BuildingAttributeGenerator : MonoBehaviour {
	[SerializeField] private float maxHeightDifferenceBetweenBuildings = 5;
	[SerializeField] private float maxTopCornerVerticalOffset = 1;
	[SerializeField] private float maxHorizontalOffset = 20;
	[SerializeField] private float minFaceWidth = 50;
	[SerializeField] private float maxFaceWidth = 200;
	[SerializeField] private float minAverageHeight = 100;

	public BuildingAttributes GetRandomBuildingAttributes(BuildingAttributes previousBuildingAttributes) {
		Quad skewedRect = GetRandomSkewedRect(previousBuildingAttributes);

		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.quad = skewedRect;
		buildingAttributes.color = new Color(Random.value, Random.value, Random.value);

		return buildingAttributes;
	}

	private Quad GetRandomSkewedRect(BuildingAttributes previousAttributes) {
		Quad previousQuad = new Quad();
		if (previousAttributes != null) previousQuad = previousAttributes.quad;

		float bottomLeftX = previousQuad.bottomRight.x + Random.Range(-previousQuad.bottomWidth / 2f, previousQuad.bottomWidth / 2f);
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

		Quad quad = new Quad(bottomLeft, topLeft, bottomRight, topRight);
		Debug.Log(quad.bottomLeft + ", " + quad.topLeft + ", " + quad.topRight + ", " + quad.bottomRight);
		return quad;
	}
}
