using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitDataTypes;

public class SmallIcicle : GeneratableItem {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private FloatRange xScaleRange = new FloatRange(0.3f, 1.0f);
	[SerializeField] private FloatRange yScaleRange = new FloatRange(0.3f, 1.0f);

	private bool hasCollided = false;

	private void Awake() {
		transform.localScale = new Vector3(xScaleRange.GetRandom(), yScaleRange.GetRandom(), 1.0f);
	}

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
}
