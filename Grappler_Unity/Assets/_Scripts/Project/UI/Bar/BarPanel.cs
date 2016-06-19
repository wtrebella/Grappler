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
		bar.SetText((string)RXRandom.Select(
			"6/20 Icicles Hit",
			"425/500 Meters Traveled",
			"34/50 Trees Chopped",
			"5/10 Snowmen Destroyed"));
	}

	private void AddButtonBar() {
		ButtonBar bar = (ButtonBar)AddBar(buttonBarPrefab);
		bar.SetText((string)RXRandom.Select(
			"Free Gift!",
			"Try On Hat",
			"325 Coins Until Gift!",
			"Buy Piggy Bank"));
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

	private void AddRandomBar() {
		if (Random.value < 0.5f) AddPlainBar();
		else AddButtonBar();
	}

	protected override IEnumerator ShowSubroutine() {
		AddRandomBar();
		if (Random.value < 0.5f) AddRandomBar();
		if (Random.value < 0.5f) AddRandomBar();

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
