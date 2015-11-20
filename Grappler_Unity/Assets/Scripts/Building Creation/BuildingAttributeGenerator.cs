using UnityEngine;
using System.Collections;

public class BuildingAttributeGenerator : MonoBehaviour {
	[SerializeField] private float maxHeightDifference = 50;
	[SerializeField] private Vector2 minBuildingBoundsSize = new Vector2(50, 100);
	[SerializeField] private Vector2 maxBuildingBoundsSize = new Vector2(200, 400);

	public BuildingAttributes GetRandomBuildingAttributes(BuildingAttributes previousBuildingAttributes) {
		SkewedRect skewedRect = GetRandomSkewedRect(previousBuildingAttributes);

		BuildingAttributes buildingAttributes = new BuildingAttributes();
		buildingAttributes.skewedRect = skewedRect;
		buildingAttributes.color = new Color(Random.value, Random.value, Random.value);

		return buildingAttributes;
	}

	private SkewedRect GetRandomSkewedRect(BuildingAttributes previousBuildingAttributes) {
		SkewedRect previousBuildingSkewedRect = new SkewedRect();
		if (previousBuildingAttributes != null) previousBuildingSkewedRect = previousBuildingAttributes.skewedRect;

		Vector2 containingRectOrigin = new Vector2();
		containingRectOrigin.x = (previousBuildingSkewedRect.bottomRight.x - previousBuildingSkewedRect.bottomLeft.x) / 2f + previousBuildingSkewedRect.bottomLeft.x;
		containingRectOrigin.y = 0;
		Rect containingRect = new Rect(containingRectOrigin, GetNextBuildingBoundsSize(previousBuildingAttributes));

		Vector2 bottomLeft = new Vector2(Random.Range(containingRect.xMin, containingRect.xMax - minBuildingBoundsSize.x), 0);
		Vector2 topLeft = new Vector2(Random.Range(containingRect.xMin, containingRect.xMax - minBuildingBoundsSize.x), Random.Range(0, maxBuildingBoundsSize.y));
		Vector2 bottomRight = new Vector2(Random.Range(bottomLeft.x, containingRect.xMax), 0);
		Vector2 topRight = new Vector2(Random.Range(topLeft.x, containingRect.xMax), Random.Range(0, maxBuildingBoundsSize.y));

		SkewedRect skewedRect = new SkewedRect(bottomLeft, topLeft, bottomRight, topRight);

		return skewedRect;
	}

	private Vector2 GetNextBuildingBoundsSize(BuildingAttributes previousBuildingAttributes) {
		if (previousBuildingAttributes == null) return new Vector2(Random.Range(minBuildingBoundsSize.x, maxBuildingBoundsSize.x), Random.Range(minBuildingBoundsSize.y, maxBuildingBoundsSize.y));

		float previousBuildingHeight = previousBuildingAttributes.skewedRect.bounds.size.y;
		float minNegativeDelta = Mathf.Max(minBuildingBoundsSize.y - previousBuildingHeight, -maxHeightDifference);
		float maxPositiveDelta = Mathf.Min(maxBuildingBoundsSize.y - previousBuildingHeight, maxHeightDifference);
		float height = previousBuildingHeight + UnityEngine.Random.Range(minNegativeDelta, maxPositiveDelta);

		return new Vector2(Random.Range(minBuildingBoundsSize.x, maxBuildingBoundsSize.y), height);
	}
}
