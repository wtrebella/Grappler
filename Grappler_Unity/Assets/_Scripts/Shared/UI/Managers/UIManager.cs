using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class UIManager : MonoBehaviour {
	[SerializeField] private Canvas UICanvas;

	private Queue<PanelInfo> panelQueue;
	private RootPanel currentPanel;

	private Dictionary<System.Type, List<RootPanel>> panelDictionary = new Dictionary<System.Type, List<RootPanel>>();

	private void Awake() {
		BaseAwake();
	}

	protected void BaseAwake() {
		panelQueue = new Queue<PanelInfo>();

		var childPanels = GetComponentsInChildren<RootPanel>(true);
		foreach (RootPanel panel in childPanels) {
			if(panelDictionary.ContainsKey(panel.GetType())) panelDictionary[panel.GetType()].Add(panel);
			else {
				List<RootPanel> newPanelList = new List<RootPanel>();
				newPanelList.Add(panel);
				panelDictionary.Add(panel.GetType(), newPanelList);
			}

			panel.gameObject.SetActive(false);
		}
	}

	public void ShowPanel<T>() where T : RootPanel {
		T panel = GetPanelOfType<T>();
		panel.Show();
	}

	public void HidePanel<T>() where T : RootPanel {
		T panel = GetPanelOfType<T>();
		panel.Hide();
	}

	public T GetPanelOfType<T>() where T : RootPanel {
		return (T)panelDictionary[typeof(T)].FirstOrDefault();
	}

	public List<T> GetPanelsOfType<T>() where T : RootPanel {
		return panelDictionary[typeof(T)].Cast<T>().ToList();
	}

	public RootPanel GetCurrentPanel() {
		return currentPanel;
	}

	public bool CurrentPanelIsOfType(Type type) {
		if (!RegisteredPanelExists()) return false;
		return currentPanel.GetType() == type;
	}

	public void AddPanelToQueue(PanelInfo panelInfo) {
		panelQueue.Enqueue(panelInfo);
	}

	protected void HideCurrentPanel() {
		if (!RegisteredPanelExists()) return;

		currentPanel.Hide();
	}

	private void RegisterPanel(RootPanel panel) {
		currentPanel = panel;
		panel.SignalPanelHidden += OnPanelHidden;
	}

	private void DeregisterPanel(RootPanel panel) {
		currentPanel = null;
		panel.SignalPanelHidden -= OnPanelHidden;
	}

	private void ShowNextPanelInQueue() {
		if (QueueIsEmpty()) return;

		PanelInfo panelInfo = panelQueue.Dequeue();
		RootPanel panel = panelInfo.panel;
		panel.SetPanelInfo(panelInfo);
		RegisterPanel(panel);
		panel.Show();
	}

	private bool RegisteredPanelExists() {
		return currentPanel != null;
	}

	private bool QueueIsEmpty() {
		return panelQueue.Count == 0;
	}

	private bool ShouldShowNextPanelInQueue() {
		return !QueueIsEmpty() && !RegisteredPanelExists();
	}

	private void OnPanelHidden(RootPanel panel) {
		DeregisterPanel(panel);
	}

	private void Update() {
		BaseUpdate();
	}

	protected void BaseUpdate() {
		if (ShouldShowNextPanelInQueue()) ShowNextPanelInQueue();
	}
}
