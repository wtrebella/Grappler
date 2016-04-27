using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : PanelBase {
	[SerializeField] private Image headerImage;
	[SerializeField] private Text title;
	[SerializeField] private Text description;
	[SerializeField] private Animator animator;

	public void SetHeaderColor(Color color) {
		headerImage.color = color;
	}

	public void SetTitle(string text) {
		title.text = text;
	}

	public void SetDescription(string text) {
		description.text = text;
	}

	public void Show() {
		gameObject.SetActive(true);
		animator.SetBool("isShowing", true);
	}

	public void Hide() {
		animator.SetBool("isShowing", false);
	}

	private void OnHidden() {
		gameObject.SetActive(false);
	}
}