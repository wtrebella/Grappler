using UnityEngine;
using System.Collections;

public class BreakableOnSlice : MonoBehaviour {
	[SerializeField] private float forceStrength = 20;
	[SerializeField] private float torque = 20;
	
	public void OnSpriteSliced(SpriteSlicer2DSliceInfo sliceInfo) {
		var childObjects = sliceInfo.ChildObjects;
		foreach (GameObject child in childObjects) {
			child.gameObject.layer = LayerMask.NameToLayer("SlicedPiece");
			Rigidbody2D rigid = child.GetComponent<Rigidbody2D>();
			rigid.gravityScale = 1;
			rigid.constraints = RigidbodyConstraints2D.None;
			rigid.AddTorque(torque * rigid.mass, ForceMode2D.Impulse);
			rigid.AddForce(new Vector2(forceStrength * rigid.mass, 0), ForceMode2D.Impulse);
		}
	}
}
