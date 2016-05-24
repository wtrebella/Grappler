using UnityEngine;
using System.Collections;

public class BreakableCollisionHandler : MonoBehaviour {
	[SerializeField] private LayerMask layerMask;

	private Breakable breakable;

	private void Awake() {
		breakable = GetComponentInParent<Breakable>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (breakable == null) {
			Debug.LogWarning("no parent object with Breakable.cs attached!");
			return;
		}

		if (WhitTools.IsInLayer(collision.gameObject, layerMask)) breakable.HandleCollision(collision);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (breakable == null) {
			Debug.LogWarning("no parent object with Breakable.cs attached!");
			return;
		}

		if (WhitTools.IsInLayer(collider.gameObject, layerMask)) breakable.HandleTrigger(collider);
	}
}
