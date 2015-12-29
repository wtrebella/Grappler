using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemySlider : StateMachine {
	[SerializeField] private float movementSpeedAvg = 1;
	[SerializeField] private float movementSpeedVar = 1;

	private Enemy enemy;
	private float movementSpeed;
	private enum EnemyStates {Still, MovingUp, MovingDown};
	
	private void Awake() {
		enemy = GetComponent<Enemy>();
		currentState = Random.value < 0.5 ? EnemyStates.MovingUp : EnemyStates.MovingDown;
		movementSpeed = movementSpeedAvg + Random.Range(-movementSpeedVar, movementSpeedVar);
	}
	
	private void MovingUp_FixedUpdateState() {
		float newLinePosition = enemy.linePosition + movementSpeed / 1000.0f;
		if (newLinePosition >= 1.0f) {
			newLinePosition = 1.0f;
			currentState = EnemyStates.MovingDown;
		}
		enemy.SetSmoothedLinePosition(newLinePosition);
	}
	
	private void MovingDown_FixedUpdateState() {
		float newLinePosition = enemy.linePosition - movementSpeed / 1000.0f;
		if (newLinePosition <= 0.0f) {
			newLinePosition = 0.0f;
			currentState = EnemyStates.MovingUp;
		}
		enemy.SetSmoothedLinePosition(newLinePosition);
	}
}
