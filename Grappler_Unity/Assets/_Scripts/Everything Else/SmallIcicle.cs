using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallIcicle : GeneratableItem {
	[SerializeField] private Rigidbody2D rigid;

	private bool hasCollided = false;

	private void Awake() {

	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (hasCollided) return;

		if (WhitTools.CompareLayers(collision.gameObject.layer, "Player")) {
			hasCollided = true;
			transform.parent = null;
			rigid.gravityScale = 1;
			StartCoroutine(DisableClipping(0.2f));
		}
	}

	private IEnumerator DisableClipping(float delay) {
		yield return new WaitForSeconds(delay);
		gameObject.layer = LayerMask.NameToLayer("SlicedPiece");
	}
}
