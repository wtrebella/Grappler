using UnityEngine;
using System.Collections;

public class Enemy : StateMachine {
	[SerializeField] private float movementSpeedAvg = 1;
	[SerializeField] private float movementSpeedVar = 1;
	[SerializeField] private float forceStrength = 1;
	[SerializeField] private float torqueStrength = 1;

	private float movementSpeed;
	private MountainChunk mountainChunk;
	private float linePosition;
	private Vector3 smoothVelocity;
	private enum EnemyStates {MovingUp, MovingDown};

	private void Awake() {
		currentState = Random.value < 0.5 ? EnemyStates.MovingUp : EnemyStates.MovingDown;
		movementSpeed = movementSpeedAvg + Random.Range(-movementSpeedVar, movementSpeedVar);
	}

	public float GetForceStrength() {
		return forceStrength;
	}

	public float GetTorqueStrength() {
		return torqueStrength;
	}

	public void SetMountainChunk(MountainChunk mountainChunk) {
		this.mountainChunk = mountainChunk;
		SetLinePosition(Random.value, false);
	}

	private void MovingUp_FixedUpdateState() {
		float newLinePosition = linePosition + movementSpeed / 1000.0f;
		if (newLinePosition >= 1.0f) {
			newLinePosition = 1.0f;
			currentState = EnemyStates.MovingDown;
		}
		SetLinePosition(newLinePosition);
	}

	private void MovingDown_FixedUpdateState() {
		float newLinePosition = linePosition - movementSpeed / 1000.0f;
		if (newLinePosition <= 0.0f) {
			newLinePosition = 0.0f;
			currentState = EnemyStates.MovingUp;
		}
		SetLinePosition(newLinePosition);
	}

	private void SetLinePosition(float linePosition, bool smoothDamp = true) {
		if (mountainChunk == null) return;

		this.linePosition = linePosition;
		Vector3 positionAlongLine = mountainChunk.GetPositionAlongLine(linePosition);
		positionAlongLine.z = transform.position.z;
		if (smoothDamp) positionAlongLine = Vector3.SmoothDamp(transform.position, positionAlongLine, ref smoothVelocity, 0.13f);
		transform.position = positionAlongLine;
	}
}
