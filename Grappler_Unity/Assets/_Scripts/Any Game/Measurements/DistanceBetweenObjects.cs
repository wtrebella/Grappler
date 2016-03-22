using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

[Serializable] public class DistanceEvent : UnityEvent <float> {}

public class DistanceBetweenObjects : MonoBehaviour {
	[SerializeField] private Transform object1;
	[SerializeField] private Transform object2;
	[SerializeField] private WhitUpdateType updateType;
	[SerializeField] private DistanceEvent SignalDistanceChanged;

	private float previousDistance;

	public float GetDistance() {
		return (object2.position - object1.position).magnitude;
	}

	private void Awake() {
		previousDistance = GetDistance();
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		CheckForDistanceChange();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		CheckForDistanceChange();
	}

	private void CheckForDistanceChange() {
		float distance = GetDistance();
		if (distance != previousDistance) {
			previousDistance = distance;
			if (SignalDistanceChanged != null) SignalDistanceChanged.Invoke(distance);
		}
	}
}
