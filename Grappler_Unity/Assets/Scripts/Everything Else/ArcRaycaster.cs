using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class ArcRaycaster : MonoBehaviour {
	[SerializeField] private bool drawDebugRays = false;
	[FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask anchorableLayerMask;
	[SerializeField] private LayerMask mountainLayerMask;
	[SerializeField] private float maxDistance = 20;
	[SerializeField] [Range(1, 10)] private float incrementAngle = 5;
	[SerializeField] private float arcAngle = 30;

	public bool CanDirectlyReachAnchorable(Anchorable anchorable) {
		Vector2 vector = anchorable.transform.position - transform.position;
		Vector2 direction = vector.normalized;
		float distance = vector.magnitude;
		var colliders = RaycastDirectionDirect(direction, distance);

		MountainChunk mountain = null;
		foreach (Collider2D collider in colliders) {
			mountain = collider.GetComponent<MountainChunk>();
			if (mountain != null) break;
		}

		return mountain == null;
	}

	public bool FindAnchorable(out Anchorable foundAnchorable, Vector2 direction) {
		var colliders = RaycastArc(direction);
		foundAnchorable = GetFurthestReachableAnchorableAmongColliders(colliders);
		return foundAnchorable != null;
	}

	private Anchorable GetFurthestReachableAnchorableAmongColliders(Collider2D[] colliders) {
		Anchorable furthestAnchorable = null;

		foreach (Collider2D collider in colliders) {
			Anchorable anchorable = collider.GetComponent<Anchorable>();
			if (!CanDirectlyReachAnchorable(anchorable)) continue;
			if (!GameScreen.instance.IsOnscreen(collider.transform.position)) continue;
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
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, maxDistance, anchorableLayerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * maxDistance, Color.green, Time.fixedDeltaTime);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}

	private Collider2D[] RaycastDirectionDirect(Vector2 direction, float distance) {
		distance -= 0.01f;
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, distance, anchorableLayerMask | mountainLayerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * distance, Color.blue, 0.5f);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
