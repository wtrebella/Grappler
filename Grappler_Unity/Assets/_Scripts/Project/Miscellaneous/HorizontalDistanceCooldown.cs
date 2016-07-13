using UnityEngine;
using System.Collections;

public class HorizontalDistanceCooldown : MonoBehaviour {
	[SerializeField] private Transform objectToFollow;
	[SerializeField] private float distance = 200;

	private float distanceTraveled = 0;
	private float previousX = 0;
	private float initialX = 0;

	public void ResetCooldown() {
		distanceTraveled = 0;
		previousX = 0;
		initialX = objectToFollow.position.x;
	}

	public float GetCooldownPercentDone() {
		return Mathf.Clamp01(distanceTraveled / distance);
	}

	public float GetCooldownPercentLeft() {
		return 1.0f - GetCooldownPercentDone();
	}

	private void Update() {
		UpdateCooldown();
		Debug.Log(distanceTraveled);
	}

	private void UpdateCooldown() {
		if (distanceTraveled >= distance) distanceTraveled = distance;
		else distanceTraveled = Mathf.Max(0, objectToFollow.position.x - initialX);
	}
}
