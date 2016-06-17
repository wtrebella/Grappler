using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RootPanel : MonoBehaviour {
	public bool isShowing {get; private set;}

	public Action<RootPanel> SignalShown;
	public Action<RootPanel> SignalHidden;

	private List<ModularPanel> modularPanels;
	private Dictionary<Type, ModularPanel> modularPanelsDict;

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
		modularPanelsDict = new Dictionary<Type, ModularPanel>();
		foreach (ModularPanel modularPanel in modularPanels) {
			Type t = modularPanel.GetType();
			if (modularPanelsDict.ContainsKey(t)) Debug.LogWarning("already contains a panel with type " + t.ToString() + ", skipping duplicates");
			else modularPanelsDict.Add(t, modularPanel);
		}
		SetupModularPanelCallbacks();
	}

	private void SetupModularPanelCallbacks() {
		foreach (ModularPanel modularPanel in modularPanels) {
			modularPanel.SignalShown += OnModularPanelShown;
			modularPanel.SignalHidden += OnModularPanelHidden;
		}
	}

	private IEnumerator ShowRoutine() {
//		ShowAllModularPanels();
//		yield return StartCoroutine(WaitForAllModularPanelsToShow());
		yield return null;
		OnShown();
	}

	private IEnumerator HideRoutine() {
		HideAllModularPanels();
		yield return StartCoroutine(WaitForAllModularPanelsToHide());
		OnHidden();
	}

	public T GetModularPanelOfType<T>() where T : ModularPanel {
		if (!modularPanelsDict.ContainsKey(typeof(T))) {
			Debug.LogError("no modular panel of type " + typeof(T).ToString() + " is a child of this root panel");
			return null;
		}
		return (T)modularPanelsDict[typeof(T)];
	}

	public void ShowModularPanel<T>() where T : ModularPanel {
		T panel = GetModularPanelOfType<T>();
		panel.Show();
	}

	public void HideModularPanel<T>() where T : ModularPanel {
		T panel = GetModularPanelOfType<T>();
		panel.Hide();
	}

	public bool ModularPanelIsShowing<T>() where T : ModularPanel {
		T panel = GetModularPanelOfType<T>();
		return panel.isShowing;
	}

	private void ShowAllModularPanels() {
		foreach (ModularPanel modularPanel in modularPanels) modularPanel.Show();
	}

	private void HideAllModularPanels() {
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
