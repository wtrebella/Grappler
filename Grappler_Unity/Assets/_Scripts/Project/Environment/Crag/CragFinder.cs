using UnityEngine;
using System.Collections;

public class CragFinder : MonoBehaviour {
	[SerializeField] private GameObject body;

	public Crag FindInDirection(Vector2 direction) {
		RaycastHit2D hit = Physics2D.BoxCast(
			body.transform.position,
			new Vector2(1, 3),
			0,
			direction,
			3,
			1 << LayerMask.NameToLayer("Crag")
		);

		if (hit) {
			Crag crag = hit.collider.GetComponentInParent<Crag>();
			return crag;
		}

		return null;
	}
}
