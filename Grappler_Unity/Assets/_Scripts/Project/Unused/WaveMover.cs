using UnityEngine;
using System.Collections;

public class WaveMover : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private float baseSpeed = 365;
	[SerializeField] private float speedReductionTriggerDistance = 50;
	[SerializeField] [Range(0, 1)] private float minSpeedPercent = 0.5f;
	private float speed;

	private void Awake() {
		speed = baseSpeed;
	}

	private void FixedUpdate() {
		UpdateSpeed();
		UpdatePosition();
	}

	private void UpdateSpeed() {
		if (!IsWithinSpeedReductionZone()) return;

		speed = baseSpeed * GetSpeedPercent();
	}

	private float GetDistanceToPlayer() {
		return Mathf.Abs(player.transform.position.x - transform.position.x);
	}

	private bool IsWithinSpeedReductionZone() {
		bool isWithinZone = GetDistanceToPlayer() <= speedReductionTriggerDistance;
		return isWithinZone;
	}

	private float GetSpeedPercent() {
		if (!IsWithinSpeedReductionZone()) return 1;

		float percentOfTheWayToPlayer = 1 - GetDistanceToPlayer() / speedReductionTriggerDistance;
		float maxReductionPercent = 1 - minSpeedPercent;
		float reductionPercent = percentOfTheWayToPlayer * maxReductionPercent;
		float speedPercent = 1 - reductionPercent;

		return speedPercent;
	}

	private void UpdatePosition() {
		Vector3 position = transform.position;
		position.x += speed * Time.deltaTime;
		transform.position = position;
	}
}
