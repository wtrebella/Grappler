using UnityEngine;
using System.Collections;
using System;

public class ClimberMountainCollision : MonoBehaviour {
	public Action<Vector2> SignalMountainCollision;

	[SerializeField] private LayerMask mountainLayerMask;

	private void OnCollisionEnter2D(Collision2D collision) {
		int collisionLayer = 1 << collision.collider.gameObject.layer;
		if (mountainLayerMask.value != collisionLayer) return;
		 
		if (SignalMountainCollision != null) SignalMountainCollision(collision.contacts[0].point);
	}
}
