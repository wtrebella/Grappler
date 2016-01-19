using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour {
	[SerializeField] private float forceStrength = 10;

	private void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		rigid.isKinematic = false;
	}
}
