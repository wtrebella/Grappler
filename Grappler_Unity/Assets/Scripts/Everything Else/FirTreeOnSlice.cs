using UnityEngine;
using System.Collections;

public class FirTreeOnSlice : MonoBehaviour {
	[SerializeField] private float forceStrength = 1000;
	[SerializeField] private float torque = 100;

	private FirTree firTree;	

	private void Awake() {
		firTree = GetComponentInParent<FirTree>();
	}

	public void OnSpriteSliced(SpriteSlicer2DSliceInfo sliceInfo) {
		firTree.HandleSlice();
		var childObjects = sliceInfo.ChildObjects;
		foreach (GameObject child in childObjects) {
			child.gameObject.layer = LayerMask.NameToLayer("SlicedPiece");
			Rigidbody2D rigid = child.GetComponent<Rigidbody2D>();
			rigid.AddTorque(torque * rigid.mass, ForceMode2D.Impulse);
			rigid.AddForce(new Vector2(forceStrength * rigid.mass, 0), ForceMode2D.Impulse);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		firTree.HandleCollision(collision);
	}
}
