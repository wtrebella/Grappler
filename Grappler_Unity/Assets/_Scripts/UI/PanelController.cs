using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PanelController : MonoBehaviour {
	[SerializeField] private RectTransform gameplayPanel;
	[SerializeField] private RectTransform mainMenuPanel;
	[SerializeField] private RectTransform settingsPanel;

	[SerializeField] private RectTransform defaultPanel;

	[SerializeField] private Vector3 offScreenPosition = new Vector3(0, 100, 0);

	private RectTransform currentPanel;
	private RectTransform[] panels;

	private void OnEnable() {
		panels = new RectTransform[] {gameplayPanel, mainMenuPanel, settingsPanel};

		SetPanel(defaultPanel);
	}
		
	private void SetPanel(RectTransform panel) {
		if (currentPanel == panel) return;

		currentPanel = panel;
		EnableCurrentPanel();
		DisableNonCurrentPanels();
	}

	private void DisableNonCurrentPanels() {
		foreach (RectTransform p in panels) {
			if (p != currentPanel) DisablePanel(p);
		}
	}

	private void EnableCurrentPanel() {
		EnablePanel(currentPanel);
	}

	private void DisablePanel(RectTransform panel) {
		if (Application.isPlaying) {
			Go.to(panel.transform, 0.3f, new GoTweenConfig()
				.localPosition(offScreenPosition)
				.setEaseType(GoEaseType.SineInOut));
		}
		else {
			((RectTransform)panel.transform).localPosition = offScreenPosition;
		}
	}
		
	private void EnablePanel(RectTransform panel) {
		if (Application.isPlaying) {
			Go.to(panel.transform, 0.3f, new GoTweenConfig()
				.localPosition(Vector3.zero)
				.setEaseType(GoEaseType.SineInOut));
		}
		else {
			panel.transform.localPosition = Vector3.zero;
		}
	}

	public RectTransform GetCurrentPanel() {
		return currentPanel;
	}

	public void SetGameplayPanel() {
		SetPanel(gameplayPanel);
	}

	public void SetMainMenuPanel() {
		SetPanel(mainMenuPanel);
	}

	public void SetSettingsPanel() {
		SetPanel(settingsPanel);
	}
}