using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class BoxCaster : MonoBehaviour {
	[SerializeField] private LayerMask anchorableLayerMask;
	[SerializeField] private LayerMask mountainLayerMask;
	[SerializeField] private bool drawDebugRays = false;

	public bool FindAnchorable(out Anchorable foundAnchorable) {
		foundAnchorable = null;
		Collider2D collider = BoxCast();
		if (collider == null) return false;
		foundAnchorable = collider.GetComponent<Anchorable>();
		return foundAnchorable != null;
	}

	private Collider2D BoxCast() {
		float height = 1;
		Vector2 center = new Vector2(GameScreen.instance.center.x, GameScreen.instance.maxY - height);
		Vector2 size = new Vector2(GameScreen.instance.width, height);
		Vector2 direction = Vector2.down;
		float distance = GameScreen.instance.height;
		if (drawDebugRays) StartCoroutine(DrawBoxCast(center, size, distance, direction));
		RaycastHit2D[] hits = Physics2D.BoxCastAll(center, size, 0, direction, distance, anchorableLayerMask);
		foreach (RaycastHit2D hit in hits) {
			Anchorable anchorable = hit.collider.GetComponent<Anchorable>();
			if (CanDirectlyReachAnchorable(anchorable)) return hit.collider;
		}
		Debug.Log("can't reach any anchorables directly. " + hits.Length + " anchorables found.");
		return null;
	}
	
	private IEnumerator DrawBoxCast(Vector2 center, Vector2 size, float distance, Vector2 direction) {
		Vector2 position = center;
		Vector2 previousPosition = position;
		float distanceCovered = 0;
		float debugCastMultiplier = 30;
		while (distanceCovered < distance) {
			DrawBox(position, size, 0.5f);
			previousPosition = position;
			position += Time.fixedDeltaTime * direction * debugCastMultiplier;
			distanceCovered += (position - previousPosition).magnitude;
			yield return new WaitForFixedUpdate();
		}
	}

	private void DrawBox(Vector2 center, Vector2 size, float duration) {
		Color color = Color.blue;
		Vector2 lowerLeft = new Vector2(center.x - size.x / 2f, center.y - size.y / 2f);
		Vector2 lowerRight = new Vector2(lowerLeft.x + size.x, lowerLeft.y);
		Vector2 upperRight = new Vector2(lowerLeft.x + size.x, lowerLeft.y + size.y);
		Vector2 upperLeft = new Vector2(lowerLeft.x, lowerLeft.y + size.y);
		Debug.DrawLine(lowerLeft, lowerRight, color, duration);
		Debug.DrawLine(lowerRight, upperRight, color, duration);
		Debug.DrawLine(upperRight, upperLeft, color, duration);
		Debug.DrawLine(upperLeft, lowerLeft, color, duration);
	}

	public bool CanDirectlyReachAnchorable(Anchorable anchorable) {
		Vector2 vector = anchorable.transform.position - transform.position;
		Vector2 direction = vector.normalized;
		float distance = vector.magnitude;
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
