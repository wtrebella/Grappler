using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallIcicle : GeneratableItem {
	[SerializeField] private Rigidbody2D rigid;

	private bool hasCollided = false;

	private void OnCollisionEnter2D(Collision2D collision) {
		if (hasCollided) return;

		if (WhitTools.IsInLayer(collision.gameObject, "Player")) HandleCollision();
	}

	private void HandleCollision() {
		hasCollided = true;
		rigid.gravityScale = 1;
		StartCoroutine(DisableClipping(0.2f));
	}

	private void EnableClipping() {
		gameObject.layer = LayerMask.NameToLayer("SmallIcicle");
	}

	private IEnumerator DisableClipping(float delay) {
		yield return new WaitForSeconds(delay);
		gameObject.layer = LayerMask.NameToLayer("SlicedPiece");
	}

	public override void HandleSpawned(Generator generator) {
		base.HandleSpawned(generator);
		hasCollided = false;
		rigid.gravityScale = 0;
	}

	protected override void HandleRecycled() {
		base.HandleRecycled();
	}
}
