using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float maxDistance = 20;
	[SerializeField] [Range(1, 10)] private float incrementAngle = 5;
	[SerializeField] private float arcAngle = 30;

	public bool FindAnchorable(out Anchorable foundAnchorable, Vector2 direction) {
		var colliders = RaycastArc(direction);
		foundAnchorable = GetFurthestAnchorableAmongColliders(colliders);
		return foundAnchorable != null;
	}

	private Anchorable GetFurthestAnchorableAmongColliders(Collider2D[] colliders) {
		Anchorable furthestAnchorable = null;

		foreach (Collider2D collider in colliders) {
			Anchorable anchorable = collider.GetComponent<Anchorable>();
			if (furthestAnchorable != null) {
				if (anchorable.anchorableID > furthestAnchorable.anchorableID) furthestAnchorable = anchorable;
			}
			else furthestAnchorable = anchorable;
		}

		return furthestAnchorable;
	}

	private Collider2D[] RaycastArc(Vector2 direction) {
		float initial = WhitTools.DirectionToAngle(direction);
		float upAngle = initial;
		float downAngle = initial - incrementAngle;
		float maxAngle = initial + arcAngle / 2f;
		float minAngle = initial - arcAngle / 2f;
		
		var colliders = new List<Collider2D>();
		
		while (true) {
			if (upAngle <= maxAngle) colliders.AddItems(RaycastAngle(upAngle));
			if (downAngle >= minAngle) colliders.AddItems(RaycastAngle(downAngle));
			
			upAngle += incrementAngle;
			downAngle -= incrementAngle;
			
			if (upAngle > maxAngle && downAngle < minAngle) break;
		}
		
		return colliders.ToArray();
	}

	private Collider2D[] RaycastAngle(float angle) {
		Vector2 direction = WhitTools.AngleToDirection(angle);
		return RaycastDirection(direction);
	}

	private Collider2D[] RaycastDirection(Vector2 direction) {
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, layerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, Time.fixedDeltaTime);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
