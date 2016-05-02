using UnityEngine;
using System.Collections;

public class SpeedPercentMeasure : MonoBehaviour {
	[SerializeField] private float maxSpeed = 80;
	[SerializeField] private SpeedMeasure speedMeasure;

	public float GetSpeedPercent() {
		float speed = speedMeasure.GetXSpeed();
		float clampedSpeed = Mathf.Clamp(speed, 0, maxSpeed);
		float percent = clampedSpeed / maxSpeed;
		return percent;
	}
}
