using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float maxDistance = 20;
	[SerializeField] [Range(1, 10)] private float incrementAngle = 5;
	[SerializeField] private float initialAngle = 45;
	[SerializeField] private float upwardCheckAmount = 45;
	[SerializeField] private float downwardCheckAmount = 135;

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		var colliders = RaycastAtAllAngles();
		foundAnchorable = null;

		foreach (Collider2D collider in colliders) {
			Anchorable anchorable = collider.GetComponent<Anchorable>();
			if (foundAnchorable != null) {
				if (anchorable.anchorableID > foundAnchorable.anchorableID) foundAnchorable = anchorable;
			}
			else foundAnchorable = anchorable;
		}
		
		return foundAnchorable != null;
	}

	private Collider2D[] RaycastAtAllAngles() {
		float upAngle = initialAngle;
		float downAngle = initialAngle - incrementAngle;
		float maxAngle = initialAngle + upwardCheckAmount;
		float minAngle = initialAngle - downwardCheckAmount;

		var colliders = new List<Collider2D>();

		while (true) {
			if (upAngle <= maxAngle) colliders.AddItems(RaycastAtAngle(upAngle));
			if (downAngle >= minAngle) colliders.AddItems(RaycastAtAngle(downAngle));
			
			upAngle += incrementAngle;
			downAngle -= incrementAngle;
			
			if (upAngle > maxAngle && downAngle < minAngle) break;
		}

		return colliders.ToArray();
	}

	private Collider2D[] RaycastAtAngle(float angle) {
		Vector2 direction = WhitTools.AngleToDirection(angle);
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, layerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, Time.fixedDeltaTime);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
