using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreText : MonoBehaviour {
	[SerializeField] private Transform objectToMeasure;

	private Text label;
	private float dist;
	private float startOffset;
	private int score = 0;
	private bool isDead = false;

	public void OnDead() {
		isDead = true;
	}

	private void Awake() {
		label = GetComponent<Text>();
		startOffset = -objectToMeasure.position.x;
	}

	private void Update() {
		dist = objectToMeasure.position.x + startOffset;
		if (!isDead) score = Mathf.Max(score, (int)((dist / 20.0f)));
		label.text = score.ToString("N0");
	}
}
