using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreText : MonoBehaviour {
	[SerializeField] private Text label;

	private void Start() {
		ScoreManager.instance.SignalScoreChanged += OnScoreChanged;
	}

	private void OnScoreChanged(int score) {
		label.text = score.ToString();
	}
}
