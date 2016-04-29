using UnityEngine;
using System.Collections;

public class Ice : GeneratableItem {
	[SerializeField] private SpriteRenderer sprite;

	private void Awake() {
		
	}

	public void SetSection(WhitTerrainSection section) {
		SetWidth(section.length);
		Vector2 startPoint = section.GetWorldStartPoint();
		transform.position = new Vector3(startPoint.x, startPoint.y, -0.1f);
	}

	private void SetWidth(float width) {
		float textureWidth = sprite.sprite.textureRect.width * WhitTools.PixelsToUnits;
		float xScale = width / textureWidth;
		Vector3 scale = transform.localScale;
		scale.x = xScale;
		transform.localScale = scale;
		transform.position = Vector3.zero;
	}
}
