using UnityEngine;
using System.Collections;
using System;

public class DistanceMeasure : MonoBehaviour {
	public Action<float> SignalDistanceChanged;

	private float initialX;
	private float previousDistance;

	private void Awake() {
		initialX = transform.position.x;
		previousDistance = initialX;
	}

	private void FixedUpdate() {
		UpdateDistance();
	}

	private void UpdateDistance() {
		float currentDistance = GetCurrentDistance();
		if (currentDistance > previousDistance) {
			if (SignalDistanceChanged != null) SignalDistanceChanged(currentDistance);
		}
	}

	private float GetCurrentDistance() {
		return transform.position.x - initialX;
	}
}
