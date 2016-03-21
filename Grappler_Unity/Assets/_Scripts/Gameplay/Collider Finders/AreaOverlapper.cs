using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class AreaOverlapper : MonoBehaviour {
	[SerializeField] private float overlapWidth = 5;
	[SerializeField] private LayerMask anchorableLayerMask;
	[SerializeField] private LayerMask mountainLayerMask;
	[SerializeField] private bool drawDebugRays = false;

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		foundAnchorable = ScreenOverlap();
		return foundAnchorable != null;
	}

	private Anchorable ScreenOverlap() {
		float xMargin = 20;
		Vector2 bodyPos = transform.position;
		Vector2 screenLowerLeftInWorldPoints =  GameScreen.instance.lowerLeft;
		Vector2 lowerLeft = new Vector2(bodyPos.x - xMargin, screenLowerLeftInWorldPoints.y - xMargin);
		Vector2 upperRight = new Vector2(lowerLeft.x + overlapWidth + xMargin, lowerLeft.y + GameScreen.instance.height + xMargin * 2);
		Collider2D[] colliders = Physics2D.OverlapAreaAll(lowerLeft, upperRight, anchorableLayerMask);
		List<Anchorable> anchorables = new List<Anchorable>();
		foreach (Collider2D collider in colliders) {
			Anchorable anchorable = collider.GetComponent<Anchorable>();
			if (anchorable) anchorables.Add(anchorable);
		}

		anchorables.Sort(delegate(Anchorable a, Anchorable b) {
			if (a.anchorableID > b.anchorableID) return -1;
			else if (a.anchorableID < b.anchorableID) return 1;
			else return 0;
		});

		foreach (Anchorable anchorable in anchorables) {
			if (CanDirectlyReachAnchorable(anchorable)) return anchorable;
		}

		return null;
	}

	public bool CanDirectlyReachAnchorable(Anchorable anchorable) {
		Vector2 vector = anchorable.transform.position - transform.position;
		float distance = vector.magnitude;

		Vector2 direction = vector.normalized;
		distance -= 0.01f;
		var colliders = RaycastDirectionDirect(direction, distance);
		
		MountainChunk mountain = null;
		foreach (Collider2D collider in colliders) {
			mountain = collider.GetComponent<MountainChunk>();
			if (mountain != null) break;
		}
		
		return mountain == null;
	}

	private Collider2D[] RaycastDirectionDirect(Vector2 direction, float distance) {
		distance -= 0.01f;
		RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, distance, anchorableLayerMask | mountainLayerMask);
		if (drawDebugRays) Debug.DrawRay(transform.position, direction * distance, Color.green, 0.5f);
		Collider2D[] colliders = new Collider2D[raycastHits.Length];
		for (int i = 0; i < raycastHits.Length; i++) colliders[i] = raycastHits[i].collider;
		return colliders;
	}
}
