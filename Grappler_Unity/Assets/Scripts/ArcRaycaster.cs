using UnityEngine;
using System.Collections;

public class ArcRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float maxDistance = 20;
	[SerializeField] [Range(1, 10)] private float incrementAngle = 5;
	[SerializeField] [Range(1, 10)] private float incrementDistance = 5;
	[SerializeField] private float upwardCheckAmount = 45;
	[SerializeField] private float downwardCheckAmount = 135;

	public bool FindCollider(out Collider2D foundCollider, float initialAngle) {
		if (FindAnyCollider(out foundCollider, initialAngle)) return true;
		else return false;
	}

	private bool FindAnyCollider(out Collider2D foundCollider, float initialAngle) {
		float distance = maxDistance;
		
		while (true) {
			if (FindColliderAtInitialAngleAndDistance(out foundCollider, initialAngle, distance)) return true;
			distance -= incrementDistance;
			if (distance < 0) {
				foundCollider = null;
				return false;
			}
		}
	}

	private bool FindColliderAtInitialAngleAndDistance(out Collider2D foundCollider, float initialAngle, float minDistance) {
		float upAngle = initialAngle;
		float downAngle = initialAngle - incrementAngle;
		float maxAngle = initialAngle + upwardCheckAmount;
		float minAngle = initialAngle - downwardCheckAmount;

		while (true) {
			if (upAngle <= maxAngle) {
				if (FindColliderAtAngleAndDistance(out foundCollider, upAngle, minDistance)) return true;
			}

			if (downAngle >= minAngle) {
				if (FindColliderAtAngleAndDistance(out foundCollider, downAngle, minDistance)) return true;
			}

			upAngle += incrementAngle;
			downAngle -= incrementAngle;

			if (upAngle > maxAngle && downAngle < minAngle) {
				foundCollider = null;
				return false;
			}
		}
	}

	private bool FindColliderAtAngleAndDistance(out Collider2D foundCollider, float angle, float minDistance) {
		Collider2D[] colliders = RaycastAtAngle(angle);
		foreach (Collider2D c in colliders) {
			float checkedDist = GetDistance(c.transform);
			if (checkedDist >= minDistance) {
				foundCollider = c;
				return true;
			}
		}
		foundCollider = null;
		return false;
	}

	private float GetDistance(Transform distanceFrom) {
		return (distanceFrom.position - transform.position).magnitude;
	}

	private Collider2D[] RaycastAtAngle(float angle) {
		Vector2 direction = WhitTools.AngleToDirection(angle);
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, layerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, 1);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
