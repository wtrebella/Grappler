using UnityEngine;
using System.Collections;

public class SpeedPercentMeasure : MonoBehaviour {
	[SerializeField] private FloatRange speedRange = new FloatRange(30, 90);
	[SerializeField] private SpeedMeasure speedMeasure;

	public float GetSpeedPercent() {
		float speed = speedMeasure.GetSpeed();
		float clampedSpeed = speedRange.Clamp(speed);
		float percent = speedRange.GetPercent(clampedSpeed);
		return percent;
	}
}
