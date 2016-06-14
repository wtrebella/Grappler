using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RootPanel : MonoBehaviour {
	public bool isShowing {get; private set;}

	public Action<RootPanel> SignalPanelHidden;

	private List<ModularPanel> modularPanels;

	public void Show() {
		if (isShowing) return;
		StartCoroutine(ShowRoutine());
	}

	public void Hide() {
		if (!isShowing) return;
		StartCoroutine(HideRoutine());
	}

	private IEnumerator ShowRoutine() {
		gameObject.SetActive(true);
		ShowModularPanels();
		// call SignalPanelShown (make an OnShown method) when all have been shown
	}

	private IEnumerator HideRoutine() {
		HideModularPanels();
		// call OnHidden when all have been hidden
	}

	private void Awake() {
		isShowing = false;
		modularPanels = GetComponentsInChildren<ModularPanel>();
		SetupModularPanelCallbacks();
	}

	private void SetupModularPanelCallbacks() {
		foreach (ModularPanel modularPanel in modularPanels) {
			modularPanel.SignalShown += OnModularPanelShown;
			modularPanel.SignalHidden += OnModularPanelHidden;
		}
	}

	private void ShowModularPanels() {
		foreach (ModularPanel modularPanel in modularPanels) modularPanel.Show();
	}

	private void HideModularPanels() {
		foreach (ModularPanel modularPanel in modularPanels) modularPanel.Hide();
	}

	private void OnModularPanelShown(ModularPanel modularPanel) {

	}

	private void OnModularPanelHidden(ModularPanel modularPanel) {

	}

	protected virtual void OnHidden() {
		gameObject.SetActive(false);
		if (SignalPanelHidden != null) SignalPanelHidden(this);
	}
}
