using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public int score {get; private set;}

	[SerializeField] private DistanceMeasure distanceMeasure;
	[SerializeField] private UILabel scoreLabel;

	public void HandleDistanceChanged(float distance) {
		int newScore = GetScoreFromDistance(distance);
		SetScore(newScore);
		SetLabel(newScore);
	}

	private void Awake() {
		distanceMeasure.SignalDistanceChanged += HandleDistanceChanged;
	}

	private int GetScoreFromDistance(float distance) {
		return (int)(distance * WhitTools.UnityUnitsToGameUnits);
	}

	private void SetLabel(int score) {
		scoreLabel.text = score.ToString("000");
	}

	private void SetScore(int newScore) {
		score = newScore;
	}
}
