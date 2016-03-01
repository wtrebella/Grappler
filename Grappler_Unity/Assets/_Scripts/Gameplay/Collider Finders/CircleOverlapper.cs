using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CircleOverlapper : MonoBehaviour {
	[SerializeField] private bool drawDebugCircle = false;
	[SerializeField] private float radius = 1;
	[SerializeField] private LayerMask anchorableLayerMask;

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		var colliders = CircleOverlap();
		if (colliders.Length == 0) {
			foundAnchorable = null;
			return false;
		}
		Collider2D collider = GetClosest(colliders);
		foundAnchorable = collider.GetComponent<Anchorable>();
		return foundAnchorable != null;
	}

	private Collider2D GetClosest(Collider2D[] colliders) {
		Collider2D closest = null;
		float minDist = Mathf.Infinity;
		foreach (Collider2D collider in colliders) {
			float dist = (collider.transform.position - transform.position).sqrMagnitude;
			if (dist < minDist) {
				minDist = dist;
				closest = collider;
			}
		}
		WhitTools.Assert(closest != null, "couldn't find anchorable");
		return closest;
	}

	private Collider2D[] CircleOverlap() {		
		return Physics2D.OverlapCircleAll(transform.position, radius, anchorableLayerMask);
	}
	
	private void OnDrawGizmos() {
		if (!drawDebugCircle) return;
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
