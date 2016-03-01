using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UIManager : Singleton<UIManager> {
	[SerializeField] private Canvas UICanvas;

	private Dictionary<System.Type, List<PanelBase>> panelDictionary = new Dictionary<System.Type, List<PanelBase>>();

	private void Awake() {
		BaseAwake();
	}

	protected void BaseAwake() {
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

	public static T GetPanelOfType<T>() where T : PanelBase {
		return (T)instance.panelDictionary[typeof(T)].FirstOrDefault();
	}

	public static List<T> GetPanelsOfType<T>() where T : PanelBase {
		return instance.panelDictionary[typeof(T)].Cast<T>().ToList();
	}
}
