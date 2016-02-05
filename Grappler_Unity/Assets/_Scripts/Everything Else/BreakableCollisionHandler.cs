using UnityEngine;
using System.Collections;

public class BreakableCollisionHandler : MonoBehaviour {
	private Breakable breakable;

	private void Awake() {
		breakable = GetComponentInParent<Breakable>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (breakable == null) {
			Debug.LogError("no parent object with Breakable.cs attached!");
			return;
		}

		if (WhitTools.CompareLayers(collision.gameObject.layer, "Player")) breakable.HandleCollision(collision);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (breakable == null) {
			Debug.LogError("no parent object with Breakable.cs attached!");
			return;
		}

		if (WhitTools.CompareLayers(collider.gameObject.layer, "Player")) breakable.HandleTrigger(collider);
	}
}
