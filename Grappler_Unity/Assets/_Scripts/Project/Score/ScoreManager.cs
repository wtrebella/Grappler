using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	public Action<int, int> SignalScoreEvent;
	public Action<int> SignalScoreChanged;

	public int score {get; private set;}

	[SerializeField] private Player player;
	[SerializeField] private CollisionSignaler collisionSignaler;
	[SerializeField] private float distanceToScore = 1.0f / 50.0f;

	private Vector2 initialPosition;

	private void Awake() {
		instance = this;
		initialPosition = player.body.transform.position;
		score = 0;
		collisionSignaler.SignalCollision += OnCollision;
	}

	private void Add(int scoreToAdd, int multiplier) {
		if (SignalScoreEvent != null) SignalScoreEvent(scoreToAdd, multiplier);
		score += (scoreToAdd * multiplier);
		if (SignalScoreChanged != null) SignalScoreChanged(score);
	}

	public void ReportJump(float jumpDistance, int flipCount) {
//		float jumpDistanceInGameUnits = jumpDistance * WhitTools.UnityUnitsToGameUnits;
//		int scoreToAdd = Round(jumpDistanceInGameUnits);
//		int multiplier = flipCount + 1;
//		Add(scoreToAdd, multiplier);
	}

	public void ReportCollision() {

	}

	private void Update() {
		if (player.isDead) return;
		Vector2 curPos = player.body.transform.position;
		Vector2 vector = curPos - initialPosition;
		float distance = vector.x;
		score = (int)(Mathf.Max(score, distance * distanceToScore));
		if (SignalScoreChanged != null) SignalScoreChanged(score);
	}

	private void OnCollision() {
		ReportCollision();
	}

	private int Round(float value) {
		return Mathf.RoundToInt(value);
	}
}
