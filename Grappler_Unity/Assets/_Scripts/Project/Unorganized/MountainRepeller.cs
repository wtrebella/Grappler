using UnityEngine;
using System.Collections;

public class MountainRepeller : MonoBehaviour {
	[SerializeField] private float distanceThreshold = 15;
	[SerializeField] private float repelForce = 100;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private Rigidbody2DAffecterGroup rigidAffecter;

	private void FixedUpdate() {
		if (IsCloseToMountain()) Repel();
	}

	private void Repel() {
		Vector2 repelDirection = Vector2.down;
		rigidAffecter.AddForce(repelDirection * repelForce, ForceMode2D.Force);
	}

	private bool IsCloseToMountain() {
		return IsWithinDistanceToMountain(distanceThreshold);
	}

	private float GetDistanceFromMountain() {
		RaycastHit2D hit = GetHit();

		Vector2 position = (Vector2)rigid.transform.position;
		if (hit.collider) return (position - hit.point).magnitude;
		else return 1000;
	}

	private bool IsWithinDistanceToMountain(float distance) {
		RaycastHit2D hit = GetHit(distance);

		return hit.collider != null;
	}

	private RaycastHit2D GetHit(float distance = Mathf.Infinity) {
		return Physics2D.Raycast(
			rigid.transform.position, 
			Vector2.up,
			distance, 
			1 << LayerMask.NameToLayer("Mountain"));
	}
}
