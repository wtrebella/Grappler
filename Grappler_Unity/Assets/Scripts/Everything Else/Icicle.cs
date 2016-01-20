using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		rigid.gravityScale = 1;
	}
}
