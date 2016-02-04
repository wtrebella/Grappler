using UnityEngine;
using System.Collections;

public class FirTreeCollisionHandler : MonoBehaviour {
	private FirTree firTree;

	private void Awake() {
		firTree = GetComponentInParent<FirTree>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (WhitTools.CompareLayers(collision.gameObject.layer, "Player")) firTree.HandleCollision(collision);
	}
}
