using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class UIManager : MonoBehaviour {
	[SerializeField] private Canvas UICanvas;

	private Queue<PanelInfo> panelQueue;
	private PanelBase registeredPanel;

	private Dictionary<System.Type, List<PanelBase>> panelDictionary = new Dictionary<System.Type, List<PanelBase>>();

	private void Awake() {
		BaseAwake();
	}

	protected void BaseAwake() {
		panelQueue = new Queue<PanelInfo>();

		var childPanels = GetComponentsInChildren<PanelBase>(true);
		foreach (PanelBase panel in childPanels) {
			if(panelDictionary.ContainsKey(panel.GetType())) panelDictionary[panel.GetType()].Add(panel);
			else {
				List<PanelBase> newPanelList = new List<PanelBase>();
				newPanelList.Add(panel);
				panelDictionary.Add(panel.GetType(), newPanelList);
			}

			panel.gameObject.SetActive(false);
		}
	}

	public void ShowPanel<T>() where T : PanelBase {
		T panel = GetPanelOfType<T>();
		panel.Show();
	}

	public void HidePanel<T>() where T : PanelBase {
		T panel = GetPanelOfType<T>();
		panel.Hide();
	}

	public T GetPanelOfType<T>() where T : PanelBase {
		return (T)panelDictionary[typeof(T)].FirstOrDefault();
	}

	public List<T> GetPanelsOfType<T>() where T : PanelBase {
		return panelDictionary[typeof(T)].Cast<T>().ToList();
	}

	public bool RegisteredPanelIsOfType(Type type) {
		if (!RegisteredPanelExists()) return false;
		return registeredPanel.GetType() == type;
	}

	public void AddPanelToQueue(PanelInfo panelInfo) {
		panelQueue.Enqueue(panelInfo);
	}

	protected void HideRegisteredPanel() {
		if (!RegisteredPanelExists()) return;

		registeredPanel.Hide();
	}

	private void RegisterPanel(PanelBase panel) {
		registeredPanel = panel;
		panel.SignalPanelHidden += OnPanelHidden;
	}

	private void DeregisterPanel(PanelBase panel) {
		registeredPanel = null;
		panel.SignalPanelHidden -= OnPanelHidden;
	}

	private void ShowNextPanelInQueue() {
		if (QueueIsEmpty()) return;

		PanelInfo panelInfo = panelQueue.Dequeue();
		PanelBase panel = panelInfo.panel;
		panel.SetPanelInfo(panelInfo);
		RegisterPanel(panel);
		panel.Show();
	}

	private bool RegisteredPanelExists() {
		return registeredPanel != null;
	}

	private bool QueueIsEmpty() {
		return panelQueue.Count == 0;
	}

	private bool ShouldShowNextPanelInQueue() {
		return !QueueIsEmpty() && !RegisteredPanelExists();
	}

	private void OnPanelHidden(PanelBase panel) {
		DeregisterPanel(panel);
	}

	private void Update() {
		BaseUpdate();
	}

	protected void BaseUpdate() {
		if (ShouldShowNextPanelInQueue()) ShowNextPanelInQueue();
	}
}
