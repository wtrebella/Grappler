using UnityEngine;
using System.Collections;

public class BigIcicleOnSlice : MonoBehaviour {
	[SerializeField] private float forceStrength = 20;
	[SerializeField] private float torque = 20;
	
	private BigIcicle bigIcicle;	
	
	private void Awake() {
		bigIcicle = GetComponentInParent<BigIcicle>();
	}
	
	public void OnSpriteSliced(SpriteSlicer2DSliceInfo sliceInfo) {
		bigIcicle.HandleSlice();
		var childObjects = sliceInfo.ChildObjects;
		foreach (GameObject child in childObjects) {
			child.gameObject.layer = LayerMask.NameToLayer("BigIcicle");
			Rigidbody2D rigid = child.GetComponent<Rigidbody2D>();
			rigid.AddTorque(torque * rigid.mass, ForceMode2D.Impulse);
			rigid.AddForce(new Vector2(forceStrength * rigid.mass, 0), ForceMode2D.Impulse);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision) {
		bigIcicle.HandleCollision(collision);
	}
}
