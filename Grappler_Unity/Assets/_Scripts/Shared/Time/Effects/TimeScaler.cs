using UnityEngine;
using System.Collections;

public class TimeScaler : MonoBehaviour {
	public static TimeScaler instance;

	private const float fixedFrameRate = 60.0f;

	private enum ScaleType {
		None,
		ScaleUp,
		ScaleDown
	}

	[SerializeField] private float timeScaleMin = 0.3f;
	[SerializeField] private float timeScaleNormal = 1.0f;
	[SerializeField] private float timeScaleUpDuration = 0.05f;
	[SerializeField] private float timeScaleDownDuration = 0.25f;

	private ScaleType scaleType = ScaleType.None;
	private float scaleDuration;
	private float timeScaleGoal;
	private float fixedDeltaTimeGoal;
	private float totalTimeScaleChange;
	private float timeScaleChangeRate;
	private float totalFixedDeltaTimeChange;
	private float fixedDeltaTimeChangeRate;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
			return;
		}

		Time.timeScale = timeScaleNormal;
	}

	public void ScaleToSlow() {
		if (Time.timeScale <= timeScaleMin) {
			Time.timeScale = timeScaleMin;
			return;
		}
		if (scaleType == ScaleType.ScaleDown) return;

		StopScalingTime();
		ScaleTime(timeScaleMin, timeScaleDownDuration);
		scaleType = ScaleType.ScaleDown;
	}

	public void ScaleToNormal() {
		if (Time.timeScale >= timeScaleNormal) {
			Time.timeScale = timeScaleNormal;
			return;
		}
		if (scaleType == ScaleType.ScaleUp) return;

		StopScalingTime();
		ScaleTime(timeScaleNormal, timeScaleUpDuration);
		scaleType = ScaleType.ScaleUp;
	}

	public void ScaleTime(float scale, float duration) {
		scaleDuration = Mathf.Clamp(duration, 0.3f, duration);
		timeScaleGoal = scale;
		totalTimeScaleChange = timeScaleGoal - Time.timeScale;
		timeScaleChangeRate = totalTimeScaleChange / scaleDuration;
		totalFixedDeltaTimeChange = fixedDeltaTimeGoal - Time.fixedDeltaTime;
		fixedDeltaTimeChangeRate = totalFixedDeltaTimeChange / scaleDuration;
		fixedDeltaTimeGoal = (1.0f/fixedFrameRate) * scale;
	}

	public void StopScalingTime() {
		scaleType = ScaleType.None;
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (scaleType == ScaleType.None) return;

		float newTimeScale = Mathf.Min(timeScaleNormal, Mathf.Max(timeScaleMin, Time.timeScale + timeScaleChangeRate * Time.unscaledDeltaTime));
		float newFixedDeltaTime = Mathf.Min(1.0f/fixedFrameRate, Mathf.Max((1.0f/fixedFrameRate) * timeScaleMin, Time.fixedDeltaTime + fixedDeltaTimeChangeRate * Time.unscaledDeltaTime));

		Time.timeScale = newTimeScale;
		Time.fixedDeltaTime = newFixedDeltaTime;
	}
}
