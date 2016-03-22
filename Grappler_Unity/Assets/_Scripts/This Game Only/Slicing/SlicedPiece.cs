using UnityEngine;
using System.Collections;

public class SlicedPiece : MonoBehaviour {
	[SerializeField] private float forceStrength = 20;
	[SerializeField] private float torque = 20;

	private SliceableObjectHolder sliceableObjectHolder;	

	private void Awake() {
		sliceableObjectHolder = GetComponentInParent<SliceableObjectHolder>();
	}

	public void OnSpriteSliced(SpriteSlicer2DSliceInfo sliceInfo) {
		if (sliceableObjectHolder != null) sliceableObjectHolder.HandleSlice();
		var childObjects = sliceInfo.ChildObjects;
		foreach (GameObject child in childObjects) {
			child.gameObject.layer = LayerMask.NameToLayer("SlicedPiece");
			Rigidbody2D rigid = child.GetComponent<Rigidbody2D>();
			rigid.AddTorque(torque * rigid.mass, ForceMode2D.Impulse);
			rigid.AddForce(new Vector2(forceStrength * rigid.mass, 0), ForceMode2D.Impulse);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (sliceableObjectHolder != null) sliceableObjectHolder.HandleCollision(collision);
	}
}
