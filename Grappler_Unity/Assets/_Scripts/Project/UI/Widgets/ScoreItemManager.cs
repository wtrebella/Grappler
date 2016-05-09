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

	public void AddScoreItem() {
		ScoreItem scoreItem = Instantiate(scoreItemPrefab);
		scoreItem.transform.SetParent(layoutGroup.transform);
		scoreItems.Add(scoreItem);
		scoreItem.Show();
	}

	public void RemoveScoreItem() {
		if (scoreItems.Count == 0) return;

		ScoreItem scoreItem = scoreItems[0];
		scoreItem.Hide();
		scoreItems.RemoveAt(0);
		Destroy(scoreItem.gameObject, 1);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) AddScoreItem();
		if (Input.GetKeyDown(KeyCode.Y)) RemoveScoreItem();
	}
}
