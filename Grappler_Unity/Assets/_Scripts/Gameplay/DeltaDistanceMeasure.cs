using UnityEngine;
using System.Collections;
using System;

public class DeltaDistanceMeasure : MonoBehaviour {
	public Action<float> SignalDeltaDistanceChanged;

	[SerializeField] Transform objectToMeasureFrom;

	private float previousDeltaDistance;

	private void Awake() {
		previousDeltaDistance = Mathf.NegativeInfinity;
	}

	private void FixedUpdate() {
		UpdateDeltaDistance();
	}

	private void UpdateDeltaDistance() {
		float currentDeltaDistance = GetCurrentDeltaDistance();
		if (currentDeltaDistance > previousDeltaDistance) {
			if (SignalDeltaDistanceChanged != null) SignalDeltaDistanceChanged(currentDeltaDistance);
		}
	}

	private float GetCurrentDeltaDistance() {
		return transform.position.x - objectToMeasureFrom.position.x;
	}
}
