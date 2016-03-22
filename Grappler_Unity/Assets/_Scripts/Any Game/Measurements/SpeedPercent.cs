using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpeedPercent : MonoBehaviour {
	public UnityEventWithFloat OnSpeedPercentChanged;
	public UnityEventWithFloat OnSpeedChanged;

	[SerializeField] private FloatToPercentConverter converter;
	[SerializeField] private Rigidbody2D rigid;

	private float previousSpeedPercent = 0;
	private float previousSpeed = 0;

	public float GetPercent() {
		return converter.ConvertToPercent(GetSpeed());
	}

	public float GetPercent(float speed) {
		return converter.ConvertToPercent(speed);
	}

	private void FixedUpdate() {
		float speed = GetSpeed();
		float percent = GetPercent(speed);
		if (percent != previousSpeedPercent) {
			previousSpeedPercent = percent;
			if (OnSpeedPercentChanged != null) OnSpeedPercentChanged.Invoke(percent);
		}
		if (speed != previousSpeed) {
			previousSpeed = speed;
			if (OnSpeedChanged != null) OnSpeedChanged.Invoke(speed);
		}
	}

	private float GetSpeed() {
		return rigid.velocity.magnitude;
	}
}
