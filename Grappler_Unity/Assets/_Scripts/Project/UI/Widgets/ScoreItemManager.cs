using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreItemManager : MonoBehaviour {
	[SerializeField] private ScoreItem scoreItemPrefab;
	[SerializeField] private VerticalLayoutGroup layoutGroup;

	private List<ScoreItem> scoreItems;

	private void Awake() {
		scoreItems = new List<ScoreItem>();
	}

	private void Start() {
		ScoreManager.instance.SignalScoreEvent += OnScoreEvent;
	}

	public void AddScoreItem(int score) {
		ScoreItem scoreItem = Instantiate(scoreItemPrefab);
		scoreItem.transform.SetParent(layoutGroup.transform);
		scoreItem.SetScore(score);
		scoreItems.Add(scoreItem);
		scoreItem.Show();
	}

	public void RemoveScoreItem() {
		if (scoreItems.Count == 0) return;

		int index = Random.Range(0, scoreItems.Count);
		ScoreItem scoreItem = scoreItems[index];
		scoreItem.Hide();
		scoreItems.RemoveAt(index);
		Destroy(scoreItem.gameObject, 1);
	}

	private void OnScoreEvent(int score) {
		AddScoreItem(score);
	}
}
