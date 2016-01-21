using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Icicle : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		rigid.gravityScale = 1;
		SpriteSlicer2D.ExplodeSprite(gameObject, 3, 10);
	}
}
