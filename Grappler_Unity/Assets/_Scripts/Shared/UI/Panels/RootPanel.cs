using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RootPanel : MonoBehaviour {
	public bool isShowing {get; private set;}

	public Action<RootPanel> SignalShown;
	public Action<RootPanel> SignalHidden;

	private List<ModularPanel> modularPanels;

	private bool initialized = false;

	public void Show() {
		if (!initialized) Initialize();

		if (isShowing) return;
		gameObject.SetActive(true);
		isShowing = true;
		StartCoroutine(ShowRoutine());
	}

	public void Hide() {
		if (!isShowing) return;
		StartCoroutine(HideRoutine());
	}

	private void Initialize() {
		isShowing = false;
		modularPanels = GetComponentsInChildren<ModularPanel>(true).ToList<ModularPanel>();
		SetupModularPanelCallbacks();
	}

	private void SetupModularPanelCallbacks() {
		foreach (ModularPanel modularPanel in modularPanels) {
			modularPanel.SignalShown += OnModularPanelShown;
			modularPanel.SignalHidden += OnModularPanelHidden;
		}
	}

	private IEnumerator ShowRoutine() {
		ShowModularPanels();
		yield return StartCoroutine(WaitForAllModularPanelsToShow());
		OnShown();
	}

	private IEnumerator HideRoutine() {
		HideModularPanels();
		yield return StartCoroutine(WaitForAllModularPanelsToHide());
		OnHidden();
	}

	private void ShowModularPanels() {
		foreach (ModularPanel modularPanel in modularPanels) modularPanel.Show();
	}

	private void HideModularPanels() {
		foreach (ModularPanel modularPanel in modularPanels) modularPanel.Hide();
	}

	private IEnumerator WaitForAllModularPanelsToShow() {
		while (!AllModularPanelsAreShowing()) yield return null;
	}

	private IEnumerator WaitForAllModularPanelsToHide() {
		while (!AllModularPanelsAreHidden()) yield return null;
	}

	private bool AllModularPanelsAreShowing() {
		bool allShowing = true;

		foreach (ModularPanel modularPanel in modularPanels) {
			if (!modularPanel.isShowing) {
				allShowing = false;
				break;
			}
		}

		return allShowing;
	}

	private bool AllModularPanelsAreHidden() {
		bool allHidden = true;

		foreach (ModularPanel modularPanel in modularPanels) {
			if (modularPanel.isShowing) {
				allHidden = false;
				break;
			}
		}

		return allHidden;
	}

	protected virtual void OnModularPanelShown(ModularPanel modularPanel) {

	}

	protected virtual void OnModularPanelHidden(ModularPanel modularPanel) {

	}

	protected void OnHidden() {
		gameObject.SetActive(false);
		isShowing = false;
		if (SignalHidden != null) SignalHidden(this);
	}

	protected void OnShown() {
		if (SignalShown != null) SignalShown(this);
	}
}
