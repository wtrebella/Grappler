using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour {
	[SerializeField] private Animator animator;
	[SerializeField] private Text scoreText;

	private float showTime = 0;
	private float showDuration = 0;
	private bool isShowing = false;

	public void SetScore(int score) {
		scoreText.text = score.ToString();
	}

	public void Show(float duration) {
		animator.SetBool("isShowing", true);
		isShowing = true;
		showDuration = duration;
		showTime = Time.time;
	}

	public void Hide() {
		animator.SetBool("isShowing", false);
		isShowing = false;
		Destroy(gameObject, 2);
	}

	private void Update() {
		if (!isShowing) return;

		if ((Time.time - showTime) > showDuration) Hide();
	}
}
