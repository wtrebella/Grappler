using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	public Action<int> SignalScoreEvent;

	[SerializeField] private CollisionSignaler collisionSignaler;

	private void Awake() {
		instance = this;
		collisionSignaler.SignalCollision += OnCollision;
	}

	public void ReportJumpDistance(float jumpDistance) {
		if (SignalScoreEvent != null) SignalScoreEvent(Round(jumpDistance));
	}

	public void ReportCollision() {

	}

	private void OnCollision() {
		ReportCollision();
	}

	private int Round(float value) {
		return Mathf.RoundToInt(value);
	}
}
