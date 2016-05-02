using UnityEngine;
using System.Collections;

public class SpeedometerMeasure : MonoBehaviour {
	[SerializeField] private float reductionDeadzoneSize = 0.3f;
	[SerializeField] private SpeedPercentMeasure speedPercentMeasure;

	private float speedometerPercent = 0;
	private float previousSpeedPercent = 0;

	public float GetSpeedometerPercent() {
		return speedometerPercent;
	}

	private void FixedUpdate() {
		float currentSpeedPercent = speedPercentMeasure.GetSpeedPercent();
		float delta = currentSpeedPercent - previousSpeedPercent;
		if (delta < 0) {
			float absDelta = Mathf.Abs(delta);
			if (absDelta > reductionDeadzoneSize) UpdateSpeedometerPercent();
		}
		else UpdateSpeedometerPercent();

		previousSpeedPercent = currentSpeedPercent;
	}

	private void UpdateSpeedometerPercent() {
		speedometerPercent = speedPercentMeasure.GetSpeedPercent();
	}
}
