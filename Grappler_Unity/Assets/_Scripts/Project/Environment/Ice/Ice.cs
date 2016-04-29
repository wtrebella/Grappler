using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {
	[SerializeField] private SpriteRenderer sprite;

	private void Awake() {
		float textureWidth = sprite.sprite.textureRect.width * WhitTools.PixelsToUnits;
		float targetWidth = 50;
		float xScale = targetWidth / textureWidth;
		Vector3 scale = transform.localScale;
		scale.x = xScale;
		transform.localScale = scale;
		transform.position = Vector3.zero;
	}
}
