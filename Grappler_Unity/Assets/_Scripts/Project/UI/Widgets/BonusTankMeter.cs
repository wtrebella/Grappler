using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BonusTankMeter : MonoBehaviour {
	[SerializeField] private BonusTank bonusTank;
	[SerializeField] private Slider slider;
	[SerializeField] private Image fill;
	[SerializeField] private Text bonusText;
	[SerializeField] private float smoothTime = 0.3f;

	private float smoothVelocity;
	private bool isDirty = false;

	private void Awake() {
		SetSliderValue(0);
		HideBonusText();
		bonusTank.SignalTankLevelChanged += OnTankLevelChanged;
		bonusTank.SignalTankFull += OnTankFull;
		bonusTank.SignalTankLostFull += OnTankLostFull;
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
		float target = bonusTank.percentFull;
		float smoothedSpeed = Mathf.SmoothDamp(current, target, ref smoothVelocity, smoothTime);
		return smoothedSpeed;
	}

	private void OnTankLevelChanged() {
		isDirty = true;
	}

	private void OnTankFull() {
		ShowBonusText();
	}

	private void OnTankLostFull() {
		HideBonusText();
	}

	private void ShowBonusText() {
		bonusText.enabled = true;
	}

	private void HideBonusText() {
		bonusText.enabled = false;
	}
}