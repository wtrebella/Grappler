using UnityEngine;
using System.Collections;

public class Forcer : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float power = 5;

	public void ForceInDirection(Vector2 direction) {
		direction.Normalize();
		Vector2 force = direction * power;
		rigid.AddForce(force, ForceMode2D.Impulse);
	}

	public void ForceRight() {
		ForceInDirection(Vector2.right);
	}

	public void ForceLeft() {
		ForceInDirection(Vector2.left);
	}

	public void ForceUp() {
		ForceInDirection(Vector2.up);
	}

	public void ForceDown() {
		ForceInDirection(Vector2.down);
	}
}
