using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour {
	[SerializeField] private float showDuration = 1;
	[SerializeField] private Animator animator;
	[SerializeField] private Text scoreText;

	private float showTime = 0;
	private bool isShowing = false;

	public void SetScore(int score) {
		scoreText.text = score.ToString();
	}

	public void Show() {
		animator.SetBool("isShowing", true);
		isShowing = true;
		showTime = Time.time;
	}

	public void Hide() {
		animator.SetBool("isShowing", false);
		isShowing = false;
	}

	private void Update() {
		if (!isShowing) return;

		if ((Time.time - showTime) > showDuration) Hide();
	}
}
