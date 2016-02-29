using UnityEngine;
using System.Collections;

public class TrailBurster : MonoBehaviour {
	[SerializeField] private TrailRenderer trail;
	[SerializeField] private Color trailColor;
	[SerializeField] private FloatRange streakRange = new FloatRange(0.0f, 4.0f);
	[SerializeField] private FloatRange widthRange = new FloatRange(0.5f, 2.0f);
	[SerializeField] private float baseDurationHold = 0.3f;
	[SerializeField] private float durationUp = 0.1f;
	[SerializeField] private float durationDown = 0.1f;

	private float currentStreak;
	private float currentDurationHold;

	public void Pulse(float streak) {
		StopPulsing();
		currentStreak = streak;
		currentDurationHold = baseDurationHold * streak;

		StartCoroutine("PulseCoroutine");
	}

	private void StopPulsing() {
		StopCoroutine("PulseCoroutine");
		Go.killAllTweensWithTarget(trail);
		Go.killAllTweensWithTarget(trail.material);
	}

	private IEnumerator PulseCoroutine() {
		float streakPercent = streakRange.GetPercent(currentStreak);
		float alpha = streakPercent;
		Color targetColor = trailColor;
		Color startColor = trailColor;
		targetColor.a = alpha;
		startColor.a = 0;
		trail.material.color = startColor;
		trail.startWidth = widthRange.Lerp(streakPercent);

		Go.to(trail.material, durationUp, new GoTweenConfig().colorProp("color", targetColor).setEaseType(GoEaseType.SineInOut));
//		Go.to(trail, durationUp, new GoTweenConfig().floatProp("startWidth", widthRange.Lerp(streakPercent)).setEaseType(GoEaseType.SineInOut));

		yield return new WaitForSeconds(durationUp + currentDurationHold);
		yield return new WaitForEndOfFrame();

		Go.to(trail.material, durationDown, new GoTweenConfig().setDelay(durationUp).colorProp("color", new Color(1.0f, 1.0f, 1.0f, 0.0f)).setEaseType(GoEaseType.SineInOut));
//		Go.to(trail, durationDown, new GoTweenConfig().setDelay(durationUp).floatProp("startWidth", 0.5f).setEaseType(GoEaseType.SineInOut));
	}

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
