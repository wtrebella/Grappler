using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : RootPanel {
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

	public override void Show() {
		base.Show();
		animator.SetBool("isShowing", true);
	}

	public override void Hide() {
		base.Hide();
		animator.SetBool("isShowing", false);
	}

	public override void SetPanelInfo(PanelInfo panelInfo) {
		base.SetPanelInfo(panelInfo);
		SetTitle(panelInfo.title);
		SetDescription(panelInfo.description);
		SetHeaderColor(panelInfo.headerColor);
	}
}