using UnityEngine;
using System.Collections;
using System;

public class ClimberMountainCollision : MonoBehaviour {
	public Action<MountainChunk, Vector2> SignalMountainCollision;

	[SerializeField] private LayerMask mountainLayerMask;

	private void OnCollisionEnter2D(Collision2D collision) {
		int collisionLayer = 1 << collision.collider.gameObject.layer;
		if (mountainLayerMask.value != collisionLayer) return;
		MountainChunk mountainChunk = collision.collider.GetComponent<MountainChunk>();
		Vector2 contactPoint = collision.contacts[0].point;
		if (SignalMountainCollision != null) SignalMountainCollision(mountainChunk, contactPoint);
	}
}
