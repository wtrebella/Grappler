using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour {
	public float value {get; private set;}

	public Action SignalReachedGoalTime;

	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;

	private bool isTiming = false;
	private float goalTime = Mathf.Infinity;

	public void SetGoalTime(float goalTime) {
		this.goalTime = goalTime;
	}

	public void SetUpdateType(WhitUpdateType updateType) {
		this.updateType = updateType;
	}

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

		if (isTiming) UpdateTimer(Time.unscaledDeltaTime);
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		if (isTiming) UpdateTimer(Time.fixedDeltaTime);
	}

	private void UpdateTimer(float deltaTime) {
		value += deltaTime;
		if (value >= goalTime) {
			value = goalTime;
			StopTimer();
			if (SignalReachedGoalTime != null) SignalReachedGoalTime();
		}
	}
}
