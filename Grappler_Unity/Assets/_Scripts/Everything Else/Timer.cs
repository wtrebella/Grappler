using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public float value {get; private set;}

	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;

	private bool isTiming = false;

	public void StartTimer() {
		isTiming = true;
	}

	public void StopTimer() {
		isTiming = false;
	}

	public void ResetTimer() {
		value = 0;
	}
	
	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		if (isTiming) UpdateTimer(Time.deltaTime);
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		if (isTiming) UpdateTimer(Time.fixedDeltaTime);
	}

	private void UpdateTimer(float deltaTime) {
		value += deltaTime;
	}
}
