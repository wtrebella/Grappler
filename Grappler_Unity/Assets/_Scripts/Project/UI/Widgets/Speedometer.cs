using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {
	[SerializeField] private SpeedPercentMeasure speedPercentMeasure;
	[SerializeField] private Slider slider;
	[SerializeField] private Image fill;
	[SerializeField] private float smoothTime = 0.3f;

	private float smoothVelocity;

	public void SetValue(float value) {
		value = Mathf.Clamp01(value);
		slider.value = value;
	}

	public void SetColor(Color color) {
		fill.color = color;
	}

	private void FixedUpdate() {
		SetValue(GetSmoothedSpeedPercent());
	}

	private float GetSmoothedSpeedPercent() {
		float current = slider.value;
		float target = GetSpeedPercent();
		float smoothedSpeed = Mathf.SmoothDamp(current, target, ref smoothVelocity, smoothTime);
		return smoothedSpeed;
	}

	private float GetSpeedPercent() {
		return speedPercentMeasure.GetSpeedPercent();
	}
}
