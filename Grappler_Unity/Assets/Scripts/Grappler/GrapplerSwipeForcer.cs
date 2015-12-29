using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class GrapplerSwipeForcer : MonoBehaviour {
	[SerializeField] [Range(0, 2)] private float strength = 1;

	private Rigidbody2D rigid;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	public void ApplySwipeForce(Vector2 swipeDirection, float swipeMagnitude) {
		rigid.AddForce(swipeDirection * swipeMagnitude * 0.1f * strength, ForceMode2D.Impulse);
	}
}
