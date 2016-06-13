using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BarPanel : PanelBase {
	[SerializeField] private Sprite starSprite;

	[SerializeField] private PlainBar plainBarPrefab;
	[SerializeField] private ButtonBar buttonBarPrefab;
	[SerializeField] private VerticalLayoutGroup barLayoutGroup;

	private List<Bar> barList;

	private void AddPlainBar() {
		PlainBar bar = (PlainBar)AddBar(plainBarPrefab);
		bar.SetText("6/20 Babies Melted");
	}

	private void AddButtonBar() {
		ButtonBar bar = (ButtonBar)AddBar(buttonBarPrefab);
		bar.SetText("Free Cocaine!");
		bar.SetIconSprite(starSprite);
	}

	private Bar AddBar(Bar prefab) {
		Bar bar = Instantiate(prefab);
		bar.transform.SetParent(barLayoutGroup.transform);
		bar.transform.localScale = new Vector3(1, 1, 1);
		bar.SetColor(WhitTools.GetColorWithRandomHue(0.6f, 1.0f));
		bar.SignalShown += OnBarShown;
		bar.SignalHidden += OnBarHidden;
		barList.Add(bar);
		return bar;
	}

	private void RemoveBar(Bar bar) {
		bar.SignalShown -= OnBarShown;
		bar.SignalHidden -= OnBarHidden;
		barList.Remove(bar);
	}

	private void Awake() {
		barList = new List<Bar>();
	}

	public override void Show() {
		base.Show();

		AddButtonBar();
		AddPlainBar();
		AddButtonBar();

		ShowBars();
	}

	public override void Hide() {
		base.Hide();

		HideBars();
	}

	private void ShowBars() {
		for (int i = 0; i < barList.Count; i++) {
			Bar bar = barList[i];
			bar.Show(i);
		}
	}

	private void HideBars() {
		for (int i = barList.Count - 1; i >= 0; i--) {
			Bar bar = barList[i];
			bar.Hide(i);
		}
	}

	public override void SetPanelInfo(PanelInfo panelInfo) {
		base.SetPanelInfo(panelInfo);

	}

	private void OnBarShown(Bar bar) {

	}

	private void OnBarHidden(Bar bar) {
		RemoveBar(bar);
		Destroy(bar.gameObject);
		if (barList.Count == 0) {
			OnHidden();
			if (SignalPanelHidden != null) SignalPanelHidden(this);
		}
	}
}
