using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class UIManager : MonoBehaviour {
	public Action<RootPanel> SignalPanelShown;
	public Action<RootPanel> SignalPanelHidden;

	[SerializeField] private Canvas UICanvas;

	private RootPanel currentPanel;

	private Dictionary<Type, RootPanel> panelDictionary = new Dictionary<Type, RootPanel>();

	private void Awake() {
		BaseAwake();
	}

	protected void BaseAwake() {
		var childPanels = GetComponentsInChildren<RootPanel>(true);
		foreach (RootPanel panel in childPanels) {
			if(panelDictionary.ContainsKey(panel.GetType())) Debug.LogWarning("panel of type " + panel.GetType().ToString() + " already exists in dictionary. not adding this one.");
			else panelDictionary.Add(panel.GetType(), panel);

			panel.SignalShown += OnPanelShown;
			panel.SignalHidden += OnPanelHidden;

			panel.gameObject.SetActive(false);
		}
	}

	public void ShowPanel<T>() where T : RootPanel {
		T panel = GetPanelOfType<T>();
		panel.Show();
	}

	public void HideCurrentPanel() {
		if (!CurrentPanelExists()) return;
		currentPanel.Hide();
	}

	public T GetPanelOfType<T>() where T : RootPanel {
		return (T)panelDictionary[typeof(T)];
	}

	public RootPanel GetCurrentPanel() {
		return currentPanel;
	}

	public bool CurrentPanelIsOfType(Type type) {
		if (!CurrentPanelExists()) return false;
		return currentPanel.GetType() == type;
	}

	public bool CurrentPanelExists() {
		return currentPanel != null;
	}

	private void RegisterPanel(RootPanel panel) {
		currentPanel = panel;
	}

	private void DeregisterPanel(RootPanel panel) {
		currentPanel = null;
	}

	private void OnPanelShown(RootPanel panel) {
		RegisterPanel(panel);
		if (SignalPanelShown != null) SignalPanelShown(panel);
	}

	private void OnPanelHidden(RootPanel panel) {
		DeregisterPanel(panel);
		if (SignalPanelHidden != null) SignalPanelHidden(panel);
	}
}
