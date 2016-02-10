using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SpeedCutoffChecker : MonoBehaviour {
	private enum SpeedLevel {
		Low,
		Mid,
		High
	}

	[SerializeField] private UnityEvent OnEnteredSpeedLevelLow;
	[SerializeField] private UnityEvent OnEnteredSpeedLevelMid;
	[SerializeField] private UnityEvent OnEnteredSpeedLevelHigh;
	[SerializeField] private float speedCutoff1 = 30;
	[SerializeField] private float speedCutoff2 = 60;

	private Rigidbody2D rigid;
	private SpeedLevel previousSpeedLevel = SpeedLevel.Low;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		RunLowEvent();
	}

	private void FixedUpdate() {
		UpdateSpeedLevel();
	}

	private void UpdateSpeedLevel() {
		SpeedLevel speedLevel = GetSpeedLevel();
		if (speedLevel != previousSpeedLevel) {
			RunEvents(speedLevel);
			previousSpeedLevel = speedLevel;
		}
	}

	private void RunEvents(SpeedLevel speedLevel) {
		if (speedLevel == SpeedLevel.Low) RunLowEvent();
		else if (speedLevel == SpeedLevel.Mid) RunMidEvent();
		else if (speedLevel == SpeedLevel.High) RunHighEvent();
	}

	private void RunLowEvent() {
		if (OnEnteredSpeedLevelLow != null) OnEnteredSpeedLevelLow.Invoke();
	}

	private void RunMidEvent() {
		if (OnEnteredSpeedLevelMid != null) OnEnteredSpeedLevelMid.Invoke();
	}

	private void RunHighEvent() {
		if (OnEnteredSpeedLevelHigh != null) OnEnteredSpeedLevelHigh.Invoke();
	}

	private SpeedLevel GetSpeedLevel() {
		float speed = rigid.velocity.magnitude;
		if (speed < speedCutoff1) return SpeedLevel.Low;
		else if (speed < speedCutoff2) return SpeedLevel.Mid;
		else return SpeedLevel.High;
	}
}
