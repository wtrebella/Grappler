using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BarPanel : ModularPanel {
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

	protected override IEnumerator ShowSubroutine() {
		AddButtonBar();
		AddPlainBar();
		AddButtonBar();

		ShowBars();

		yield return StartCoroutine(WaitForAllBarsToShow());
	}

	protected override IEnumerator HideSubroutine() {
		HideBars();

		yield return StartCoroutine(WaitForAllBarsToHide());
	}

	private IEnumerator WaitForAllBarsToShow() {
		while (!AllBarsAreShowing()) yield return null;
	}

	private IEnumerator WaitForAllBarsToHide() {
		while (!AllBarsAreHidden()) yield return null;
	}

	private void OnBarShown(Bar bar) {

	}

	private void OnBarHidden(Bar bar) {
		RemoveBar(bar);
		Destroy(bar.gameObject);
	}

	private bool AllBarsAreShowing() {
		bool allShowing = true;
		foreach (Bar bar in barList) {
			if (!bar.isShowing) {
				allShowing = false;
				break;
			}
		}
		return allShowing;
	}

	private bool AllBarsAreHidden() {
		bool allHidden = true;
		foreach (Bar bar in barList) {
			if (bar.isShowing) {
				allHidden = false;
				break;
			}
		}
		return allHidden;
	}
}
