using UnityEngine;
using System.Collections;

public class MountainEnemy : MonoBehaviour {
	public float linePosition {get; private set;}

	[SerializeField] private float forceStrength = 1;
	[SerializeField] private float torqueStrength = 1;

	private MountainChunk mountainChunk;
	private Vector3 smoothVelocity;

	public float GetForceStrength() {
		return forceStrength;
	}

	public float GetTorqueStrength() {
		return torqueStrength;
	}

	public void SetMountainChunk(MountainChunk mountainChunk) {
		this.mountainChunk = mountainChunk;
	}

	public void SetExactLinePosition(float linePosition) {
		if (mountainChunk == null) return;
		this.linePosition = linePosition;
		transform.position = GetExactPositionFromNewLinePosition(linePosition);
	}

	public void SetSmoothedLinePosition(float linePosition) {
		if (mountainChunk == null) return;
		this.linePosition = linePosition;
		transform.position = GetSmoothedPositionFromNewLinePosition(linePosition);
	}

	private Vector3 GetSmoothedPositionFromNewLinePosition(float linePosition) {
		Vector3 positionAlongLine = GetExactPositionFromNewLinePosition(linePosition);
		positionAlongLine = Vector3.SmoothDamp(transform.position, positionAlongLine, ref smoothVelocity, 0.13f);
		return positionAlongLine;
	}

	private Vector3 GetExactPositionFromNewLinePosition(float linePosition) {
		Vector3 positionAlongLine = mountainChunk.GetPositionAlongLine(linePosition);
		positionAlongLine.z = transform.position.z;
		return positionAlongLine;
	}
}
