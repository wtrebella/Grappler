using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmallIcicle : MonoBehaviour {
	private Rigidbody2D rigid;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		rigid.gravityScale = 1;
		SpriteSlicer2D.ExplodeSprite(gameObject, 3, 10);
	}
}
