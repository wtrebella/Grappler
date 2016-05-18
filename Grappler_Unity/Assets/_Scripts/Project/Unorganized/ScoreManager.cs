using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	public Action<int, int> SignalScoreEvent;
	public Action<int> SignalScoreChanged;

	public int score {get; private set;}

	[SerializeField] private CollisionSignaler collisionSignaler;

	private void Awake() {
		instance = this;
		score = 0;
		collisionSignaler.SignalCollision += OnCollision;
	}

	private void Add(int scoreToAdd, int multiplier) {
		if (SignalScoreEvent != null) SignalScoreEvent(scoreToAdd, multiplier);
		score += (scoreToAdd * multiplier);
		if (SignalScoreChanged != null) SignalScoreChanged(score);
	}

	public void ReportJump(float jumpDistance, int flipCount) {
		float jumpDistanceInGameUnits = jumpDistance * WhitTools.UnityUnitsToGameUnits;
		int scoreToAdd = Round(jumpDistanceInGameUnits);
		int multiplier = flipCount + 1;
		Add(scoreToAdd, multiplier);
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
