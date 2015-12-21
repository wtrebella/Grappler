using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private MountainChunk mountainChunk;
	private float linePosition;

	public void SetMountainChunk(MountainChunk mountainChunk) {
		this.mountainChunk = mountainChunk;
		SetLinePosition(0f);
	}

	private void SetLinePosition(float linePosition) {
		this.linePosition = linePosition;
		Vector3 positionAlongLine = mountainChunk.GetPositionAlongLine(linePosition);
		positionAlongLine.z = -0.01f;
		transform.position = positionAlongLine;
	}

	private void Update() {
		SetLinePosition(linePosition + 0.001f);
	}
}
