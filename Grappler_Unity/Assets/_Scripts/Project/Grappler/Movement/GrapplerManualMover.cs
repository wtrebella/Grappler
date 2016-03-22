using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class GrapplerManualMover : MonoBehaviour {
	[SerializeField] Follow[] followsToUpdate;

	private Rigidbody2D rb;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void InstanlyMoveTo(Vector3 position) {
		rb.isKinematic = true;
		transform.position = position;
		rb.isKinematic = false;

		UpdateFollows();
	}

	private void UpdateFollows() {
		foreach (Follow follow in followsToUpdate) follow.UpdateMovementImmediateNow();
	}
}
