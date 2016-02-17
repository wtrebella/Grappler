﻿using UnityEngine;
using System.Collections;

public class TimeScaler : MonoBehaviour {
	private enum ScaleType {
		None,
		ScaleUp,
		ScaleDown
	}

	[SerializeField] private float timeScaleMin = 0.1f;
	[SerializeField] private float timeScaleNormal = 1.0f;
	[SerializeField] private float timeScaleUpDuration = 0.05f;
	[SerializeField] private float timeScaleDownDuration = 0.5f;

	private ScaleType scaleType = ScaleType.None;
	private float scaleDuration;
	private float timeScaleGoal;
	private float fixedDeltaTimeGoal;
	private float totalTimeScaleChange;
	private float timeScaleChangeRate;
	private float totalFixedDeltaTimeChange;
	private float fixedDeltaTimeChangeRate;

	public void ScaleToSlow() {
		if (Time.timeScale <= timeScaleMin) return;
		if (scaleType == ScaleType.ScaleDown) return;

		StopScalingTime();
		ScaleTime(timeScaleMin, timeScaleDownDuration);
		scaleType = ScaleType.ScaleDown;
	}

	public void ScaleToNormal() {
		if (Time.timeScale >= timeScaleNormal) return;
		if (scaleType == ScaleType.ScaleUp) return;

		StopScalingTime();
		ScaleTime(timeScaleNormal, timeScaleUpDuration);
		scaleType = ScaleType.ScaleUp;
	}

	public void ScaleTime(float scale, float duration) {
		scaleDuration = duration;
		timeScaleGoal = scale;
		totalTimeScaleChange = timeScaleGoal - Time.timeScale;
		timeScaleChangeRate = totalTimeScaleChange / scaleDuration;
		totalFixedDeltaTimeChange = fixedDeltaTimeGoal - Time.fixedDeltaTime;
		fixedDeltaTimeChangeRate = totalFixedDeltaTimeChange / scaleDuration;
		fixedDeltaTimeGoal = (1.0f/30.0f) * scale;
	}

	public void StopScalingTime() {
		scaleType = ScaleType.None;
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
		if (scaleType == ScaleType.None) return;

		float newTimeScale = Mathf.Min(timeScaleNormal, Mathf.Max(timeScaleMin, Time.timeScale + timeScaleChangeRate * Time.unscaledDeltaTime));
		float newFixedDeltaTime = Mathf.Min(1.0f/30.0f, Mathf.Max((1.0f/30.0f) * timeScaleMin, Time.fixedDeltaTime + fixedDeltaTimeChangeRate * Time.unscaledDeltaTime));

		Time.timeScale = newTimeScale;
		Time.fixedDeltaTime = newFixedDeltaTime;
	}
}