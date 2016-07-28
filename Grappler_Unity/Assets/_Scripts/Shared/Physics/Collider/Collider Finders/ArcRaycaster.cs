using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public enum ArcType {
	Clockwise,
	CounterClockwise,
	Both
}

public class ArcRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask anchorableLayerMask;
	[SerializeField] private LayerMask mountainLayerMask;
	[SerializeField] private LayerMask groundLayerMask;
	[SerializeField] private float maxDistance = 20;
	[SerializeField] [Range(1, 10)] private float incrementAngle = 5;
	[SerializeField] private float arcAngle = 30;
	[SerializeField] private Vector2 initialDirection = new Vector2(1, 1);
	[SerializeField] private ArcType arcType = ArcType.Both;

	public bool CanDirectlyReachAnchorable(Anchorable anchorable) {
		Vector2 vector = anchorable.transform.position - transform.position;
		Vector2 direction = vector.normalized;
		float distance = vector.magnitude;
		var colliders = RaycastDirectionDirect(direction, distance);

		bool hitSomething = false;
		foreach (Collider2D collider in colliders) {
			Anchorable foundAnchorable = collider.GetComponent<Anchorable>();
			if (foundAnchorable == null) {
				hitSomething = true;
				break;
			}
		}

		return !hitSomething;
	}

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		var colliders = RaycastArc();
		foundAnchorable = GetFurthestReachableAnchorableAmongColliders(colliders);
		return foundAnchorable != null;
	}

	private Anchorable GetFurthestReachableAnchorableAmongColliders(Collider2D[] colliders) {
		Anchorable furthestAnchorable = null;

		foreach (Collider2D collider in colliders) {
			Anchorable anchorable = collider.GetComponent<Anchorable>();
			if (!CanDirectlyReachAnchorable(anchorable)) continue;
//			if (!ScreenUtility.instance.IsOnscreen(collider.transform.position)) continue;
			if (furthestAnchorable != null) {
				if (anchorable.anchorableID > furthestAnchorable.anchorableID) furthestAnchorable = anchorable;
			}
			else furthestAnchorable = anchorable;
		}

		return furthestAnchorable;
	}

	private Collider2D[] RaycastArc() {
		if (arcType == ArcType.Both) return RaycastArcBoth();
		else if (arcType == ArcType.Clockwise) return RaycastArcClockwise();
		else if (arcType == ArcType.CounterClockwise) return RaycastArcCounterClockwise();
		else {
			Debug.LogError("invalid arc type: " + arcType.ToString());
			return null;
		}
	}

	private Collider2D[] RaycastArcBoth() {
		float initial = WhitTools.DirectionToAngle(initialDirection);
		float upAngle = initial;
		float downAngle = initial - incrementAngle;
		float maxAngle = initial + arcAngle / 2f;
		float minAngle = initial - arcAngle / 2f;
		
		var colliders = new List<Collider2D>();
		
		while (true) {
			if (upAngle <= maxAngle) colliders.AddAll(RaycastAngle(upAngle));
			if (downAngle >= minAngle) colliders.AddAll(RaycastAngle(downAngle));
			
			upAngle += incrementAngle;
			downAngle -= incrementAngle;
			
			if (upAngle > maxAngle && downAngle < minAngle) break;
		}
		
		return colliders.ToArray();
	}

	private Collider2D[] RaycastArcClockwise() {
		float initial = WhitTools.DirectionToAngle(initialDirection);
		float downAngle = initial;
		float minAngle = initial - arcAngle;

		var colliders = new List<Collider2D>();

		while (true) {
			if (downAngle >= minAngle) colliders.AddAll(RaycastAngle(downAngle));

			downAngle -= incrementAngle;

			if (downAngle < minAngle) break;
		}

		return colliders.ToArray();
	}

	private Collider2D[] RaycastArcCounterClockwise() {
		float initial = WhitTools.DirectionToAngle(initialDirection);
		float upAngle = initial;
		float maxAngle = initial + arcAngle;

		var colliders = new List<Collider2D>();

		while (true) {
			if (upAngle <= maxAngle) colliders.AddAll(RaycastAngle(upAngle));

			upAngle += incrementAngle;

			if (upAngle > maxAngle) break;
		}

		return colliders.ToArray();
	}

	private Collider2D[] RaycastAngle(float angle) {
		Vector2 direction = WhitTools.AngleToDirection(angle);
		return RaycastDirection(direction);
	}

	private Collider2D[] RaycastDirection(Vector2 direction) {
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, anchorableLayerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, 0.5f);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}

	private Collider2D[] RaycastDirectionDirect(Vector2 direction, float distance) {
		distance -= 0.01f;
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, distance, anchorableLayerMask | mountainLayerMask | groundLayerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * distance, Color.blue, 0.5f);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
