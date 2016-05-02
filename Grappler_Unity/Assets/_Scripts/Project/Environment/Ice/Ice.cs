using UnityEngine;
using System.Collections;

public class Ice : GeneratableItem {
	[SerializeField] private Transform scaledIceContainer;
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private Collider2D endCollider;

	private void Awake() {
		
	}

	public void SetSection(WhitTerrainSection section) {
		SetWidth(section.length);
		SetEndColliderPosition(section.length);
		Vector2 startPoint = section.GetWorldStartPoint();
		transform.position = new Vector3(startPoint.x, startPoint.y, -0.1f);
	}

	private void SetWidth(float width) {
		float textureWidth = sprite.sprite.textureRect.width * WhitTools.PixelsToUnits;
		float xScale = width / textureWidth;
		Vector3 scale = scaledIceContainer.localScale;
		scale.x = xScale;
		scaledIceContainer.localScale = scale;
		transform.position = Vector3.zero;
	}

	private void SetEndColliderPosition(float width) {
		Vector2 endColliderLocalPosition = new Vector3(width, 0, 0);
		endCollider.transform.localPosition = endColliderLocalPosition;
	}
}
