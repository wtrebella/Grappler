using UnityEngine;
using System.Collections;

public class Rigidbody2DVelocityReducer : Rigidbody2DAffecter {
	[SerializeField] private WhitAxisType axis;
	[SerializeField, Range(0, 1)] private float reductionMultiplier = 0.9f;

	public void Reduce() {
		foreach (Rigidbody2D rigid in rigidbodies) {
			Vector2 velocity = rigid.velocity;
			velocity *= reductionMultiplier;
			if (axis == WhitAxisType.X) velocity.y = rigid.velocity.y;
			else if (axis == WhitAxisType.Y) velocity.x = rigid.velocity.x;
			rigid.velocity = velocity;
		}
	}
}
