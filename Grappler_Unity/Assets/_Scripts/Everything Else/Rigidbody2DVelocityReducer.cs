using UnityEngine;
using System.Collections;

public class Rigidbody2DVelocityReducer : MonoBehaviour {
	[SerializeField] private Rigidbody2D[] rigidbodies;
	[SerializeField, Range(0, 1)] private float reductionMultiplier = 0.9f;

	public void Reduce() {
		foreach (Rigidbody2D rigid in rigidbodies) {
			Vector2 velocity = rigid.velocity;
			velocity *= reductionMultiplier;
			rigid.velocity = velocity;
		}
	}
}
