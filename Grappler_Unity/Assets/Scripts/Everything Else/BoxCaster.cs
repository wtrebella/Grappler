using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class BoxCaster : MonoBehaviour {
	[SerializeField] private LayerMask anchorableLayerMask;

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		foundAnchorable = null;
		Collider2D collider = BoxCast();
		if (collider == null) return false;
		foundAnchorable = collider.GetComponent<Anchorable>();
		return foundAnchorable != null;
	}

	private Collider2D BoxCast() {
		float height = 2;
		Vector2 upperLeft = GameScreen.instance.upperLeft;
		Vector2 origin = new Vector2(upperLeft.x, upperLeft.y - height);
		Vector2 size = new Vector2(GameScreen.instance.width, 2);
		float distance = GameScreen.instance.height;
		RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.down, distance, anchorableLayerMask);
		return hit.collider;
	}
}
