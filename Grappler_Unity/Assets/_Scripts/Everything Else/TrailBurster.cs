using UnityEngine;
using System.Collections;

public class TrailBurster : MonoBehaviour {
	[SerializeField] private TrailRenderer trail;

	public void OnPushBack() {
		Pulse();
	}

	public void Pulse() {
		StopPulsing();
		StartCoroutine("PulseCoroutine");
	}

	private void StopPulsing() {
		StopCoroutine("PulseCoroutine");
		Go.killAllTweensWithTarget(trail);
		Go.killAllTweensWithTarget(trail.material);
	}

	private IEnumerator PulseCoroutine() {
		float duration1 = 0.2f;
		float duration2 = 0.5f;

		Go.to(trail.material, duration1, new GoTweenConfig().colorProp("color", new Color(1.0f, 1.0f, 0.1f, 1.0f)).setEaseType(GoEaseType.SineInOut));
		Go.to(trail, duration1, new GoTweenConfig().floatProp("startWidth", 2.0f).setEaseType(GoEaseType.SineInOut));

		yield return new WaitForSeconds(duration1);
		yield return new WaitForEndOfFrame();

		Go.to(trail.material, duration2, new GoTweenConfig().setDelay(duration1).colorProp("color", new Color(1.0f, 1.0f, 1.0f, 0.0f)).setEaseType(GoEaseType.SineInOut));
		Go.to(trail, duration2, new GoTweenConfig().setDelay(duration1).floatProp("startWidth", 0.5f).setEaseType(GoEaseType.SineInOut));
	}

	// TODO: make it so the pulse's size and color change relative to the strength of the push back!
	// this way it will be self-evident what it means, because the user will inevitably do swings at
	// different speeds, and therefore notice the change in trail.

	private void Awake() {

	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
