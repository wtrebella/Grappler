using UnityEngine;
using System.Collections;

public class ScoreItem : MonoBehaviour {
	[SerializeField] private Animator animator;

	private void Awake() {
		Show();
	}

	public void Show() {
		animator.SetBool("isShowing", true);
	}

	public void Hide() {
		animator.SetBool("isShowing", false);
	}
}
