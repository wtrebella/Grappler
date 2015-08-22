using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	private tk2dSlicedSprite buildingSprite;

	public void SetColor(Color color) {
		buildingSprite.color = color;
	}

	public void SetSize(Vector2 size) {
		float xInPixels = size.x * WhitTools.UnitsToPixels;
		float yInPixels = size.y * WhitTools.UnitsToPixels;
		buildingSprite.dimensions = new Vector2(xInPixels, yInPixels);
	}

	private void Awake() {
		buildingSprite = GetComponentInChildren<tk2dSlicedSprite>();
	}
}
