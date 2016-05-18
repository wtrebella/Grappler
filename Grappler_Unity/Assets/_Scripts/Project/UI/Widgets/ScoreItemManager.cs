using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreItemManager : MonoBehaviour {
	[SerializeField] private float showDuration = 1;
	[SerializeField] private ScoreItem scoreItemPrefab;
	[SerializeField] private VerticalLayoutGroup layoutGroup;

	private void Start() {
		ScoreManager.instance.SignalScoreEvent += OnScoreEvent;
	}

	public void AddScoreItem(int score, int multiplier) {
		ScoreItem scoreItem = Instantiate(scoreItemPrefab);
		scoreItem.transform.SetParent(layoutGroup.transform);
		scoreItem.SetScore(score, multiplier);
		scoreItem.Show(showDuration);
	}

	private void OnScoreEvent(int score, int multiplier) {
		AddScoreItem(score, multiplier);
	}
}
