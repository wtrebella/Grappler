using UnityEngine;
using System.Collections;

public class AngleRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float maxDistance = 20;

	public bool FindCollider(out Collider2D foundCollider, float angle) {
		foundCollider = RaycastAtAngle(angle);
		if (foundCollider != null) return true;
		else return false;
	}
	
	private Collider2D RaycastAtAngle(float angle) {
		Collider2D[] colliders = RaycastAllAtAngle(angle);
		if (colliders.Length > 0) return colliders[Random.Range(0, colliders.Length - 1)];
		else return null;
	}

	private Collider2D[] RaycastAllAtAngle(float angle) {
		Vector2 direction = WhitTools.AngleToDirection(angle);
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, layerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, 1);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
