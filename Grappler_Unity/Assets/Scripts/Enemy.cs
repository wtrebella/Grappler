using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private MountainChunk mountainChunk;
	private float linePosition;
	private Vector3 smoothVelocity;

	public void SetMountainChunk(MountainChunk mountainChunk) {
		this.mountainChunk = mountainChunk;
		SetLinePosition(0f, false);
	}

	private void SetLinePosition(float linePosition, bool smoothDamp = true) {
		this.linePosition = linePosition;
		Vector3 positionAlongLine = mountainChunk.GetPositionAlongLine(linePosition);
		positionAlongLine.z = -0.01f;
		if (smoothDamp) positionAlongLine = Vector3.SmoothDamp(transform.position, positionAlongLine, ref smoothVelocity, 0.13f);
		transform.position = positionAlongLine;
	}

	private void Update() {
		SetLinePosition(linePosition + 0.001f);
	}
}
