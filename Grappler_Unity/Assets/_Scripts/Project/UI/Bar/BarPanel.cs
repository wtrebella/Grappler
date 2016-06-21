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

	private Bar CreateBar(BarInfo barInfo) {
		BarType barType = barInfo.GetBarType();
		Bar prefab = GetBarPrefab(barType);
		Bar bar = Instantiate(prefab);
		bar.ApplyBarInfo(barInfo);
		bar.transform.SetParent(barLayoutGroup.transform);
		bar.transform.localScale = new Vector3(1, 1, 1);
		bar.SetColor(WhitTools.GetColorWithRandomHue(0.6f, 1.0f));
		return bar;
	}

	private Bar AddBar(Bar bar) {
		bar.SignalShown += OnBarShown;
		bar.SignalHidden += OnBarHidden;
		barList.Add(bar);
		return bar;
	}

	private Bar GetBarPrefab(BarType barType) {
		if (barType == BarType.PlainBar) return plainBarPrefab;
		else if (barType == BarType.ButtonBar) return buttonBarPrefab;
		else {
			Debug.LogError("invalid bar type: " + barType.ToString());
			return null;
		}
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

	private void CreateBars() {
		var barInfoTypes = BarInfo.GetBarInfoTypesToShow();
		foreach (BarInfoType barInfoType in barInfoTypes) {
			BarInfo barInfo = new BarInfo(barInfoType);
			Bar bar = CreateBar(barInfo);
			AddBar(bar);
		}
	}

	protected override IEnumerator ShowSubroutine() {
		CreateBars();
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
