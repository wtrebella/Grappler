using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	public Action<int> SignalScoreEvent;
	public Action<int> SignalScoreChanged;

	public int score {get; private set;}

	[SerializeField] private CollisionSignaler collisionSignaler;

	private void Awake() {
		instance = this;
		score = 0;
		collisionSignaler.SignalCollision += OnCollision;
	}

	private void Add(int scoreToAdd) {
		if (SignalScoreEvent != null) SignalScoreEvent(scoreToAdd);
		score += scoreToAdd;
		if (SignalScoreChanged != null) SignalScoreChanged(score);
	}

	public void ReportJumpDistance(float jumpDistance) {
		Add(Round(jumpDistance));
	}

	public void ReportFlip() {
		Add(10);
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
