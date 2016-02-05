using UnityEngine;
using System.Collections;

public class WaveOffsetMover : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private float initialHorizontalOffset = -100;

	private float horizontalOffset;
	private float speedOffset = 30;

	private void Awake() {
		horizontalOffset = initialHorizontalOffset;
	}
	
	private void FixedUpdate() {
		UpdateHorizontalOffset();
		UpdatePosition();
	}

	private void UpdatePosition() {
		Vector2 position;
		ApplyHorizontalOffset(out position);
		transform.position = position;
	}

	private void UpdateHorizontalOffset() {
		horizontalOffset += speedOffset * Time.deltaTime;
	}

	private void ApplyHorizontalOffset(out Vector2 position) {
		Vector2 appliedPosition;
		appliedPosition.x = player.transform.position.x + horizontalOffset;
		appliedPosition.y = transform.position.y;
		position = appliedPosition;
	}
}
