using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	private tk2dSlicedSprite buildingSprite;
	private BuildingAttributes attributes;

	public void SetBuildingAttributes(BuildingAttributes buildingAttributes) {
		SetColor(buildingAttributes.color);
		SetSize(buildingAttributes.size);
		transform.position = buildingAttributes.position;
		attributes = buildingAttributes;
	}

	public Vector2[] GetCornerPoints() {
		WhitTools.Assert(attributes != null, "no attributes set!");
		
		Vector2[] points = new Vector2[2];
		points[0] = new Vector2(transform.position.x, transform.position.y + attributes.size.y);
		points[1] = new Vector2(transform.position.x + attributes.size.x, transform.position.y + attributes.size.y);
		return points;
	}
	
	private void SetColor(Color color) {
		buildingSprite.color = color;
	}

	private void SetSize(Vector2 size) {
		float xInPixels = size.x * WhitTools.UnitsToPixels;
		float yInPixels = size.y * WhitTools.UnitsToPixels;
		buildingSprite.dimensions = new Vector2(xInPixels, yInPixels);
	}

	private void Awake() {
		buildingSprite = GetComponentInChildren<tk2dSlicedSprite>();
	}
}
