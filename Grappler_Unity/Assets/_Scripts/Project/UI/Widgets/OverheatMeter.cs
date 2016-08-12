using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverheatMeter : MonoBehaviour {
	[SerializeField] private Overheat overheat;
	[SerializeField] private Slider slider;
	[SerializeField] private Image fill;
	[SerializeField] private Text overheatText;
	[SerializeField] private float smoothTime = 0.3f;

	private float smoothVelocity;
	private bool isDirty = false;

	private void Awake() {
		SetSliderValue(0);
		HideOverheatText();
		overheat.SignalChange += OnChange;
		overheat.SignalOverheat += OnOverheat;
		overheat.SignalEmptyAfterOverheat += OnEmptyAfterOverheat;
	}

	public void SetSliderValue(float value) {
		value = Mathf.Clamp01(value);
		slider.value = value;
	}

	public void SetColor(Color color) {
		fill.color = color;
	}

	private void FixedUpdate() {
		if (isDirty) UpdateSliderValue();
	}

	private void UpdateSliderValue() {
		SetSliderValue(GetSmoothedPercentFull());
	}

	private float GetSmoothedPercentFull() {
		float current = slider.value;
		float target = overheat.GetTankValue();
		float smoothedSpeed = Mathf.SmoothDamp(current, target, ref smoothVelocity, smoothTime);
		return smoothedSpeed;
	}

	private void OnChange() {
		isDirty = true;
	}

	private void OnOverheat() {
		ShowOverheatText();
	}

	private void OnEmptyAfterOverheat() {
		HideOverheatText();
	}

	private void ShowOverheatText() {
		overheatText.enabled = true;
	}

	private void HideOverheatText() {
		overheatText.enabled = false;
	}
}