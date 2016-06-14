using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//TODO: gotta convert this to a ModularPanel that is a child of an Alert RootPanel
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

//	public void Show() {
//		animator.SetBool("isShowing", true);
//	}
//
//	public void Hide() {
//		animator.SetBool("isShowing", false);
//	}

	public void SetPanelInfo(PanelInfo panelInfo) {
		SetTitle(panelInfo.title);
		SetDescription(panelInfo.description);
		SetHeaderColor(panelInfo.headerColor);
	}
}