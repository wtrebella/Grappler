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

		if (currentPanel == null) SetPanelImmediate(defaultPanel);
	}
		
	private void SetPanelAnimated(RectTransform panel) {
		if (currentPanel == panel) return;

		currentPanel = panel;
		EnableCurrentPanelAnimated();
		DisableNonCurrentPanelsAnimated();
	}

	private void SetPanelImmediate(RectTransform panel) {
		if (currentPanel == panel) return;

		currentPanel = panel;
		EnableCurrentPanelImmediate();
		DisableNonCurrentPanelsImmediate();
	}

	private void DisableNonCurrentPanelsAnimated() {
		foreach (RectTransform p in panels) {
			if (p != currentPanel) DisablePanelAnimated(p);
		}
	}

	private void DisableNonCurrentPanelsImmediate() {
		foreach (RectTransform p in panels) {
			if (p != currentPanel) DisablePanelImmediate(p);
		}
	}

	private void EnableCurrentPanelAnimated() {
		EnablePanelAnimated(currentPanel);
	}

	private void EnableCurrentPanelImmediate() {
		EnablePanelImmediate(currentPanel);
	}
		
	private void DisablePanelAnimated(RectTransform panel) {
		Go.to(panel.transform, 0.3f, new GoTweenConfig()
			.localPosition(offScreenPosition)
			.setEaseType(GoEaseType.SineInOut));
	}

	private void DisablePanelImmediate(RectTransform panel) {
		((RectTransform)panel.transform).localPosition = offScreenPosition;
	}

	private void EnablePanelAnimated(RectTransform panel) {
		Go.to(panel.transform, 0.3f, new GoTweenConfig()
			.localPosition(Vector3.zero)
			.setEaseType(GoEaseType.SineInOut));
	}

	private void EnablePanelImmediate(RectTransform panel) {
		((RectTransform)panel.transform).localPosition = Vector3.zero;
	}

	public RectTransform GetCurrentPanel() {
		return currentPanel;
	}

	public void SetGameplayPanel() {
		if (Application.isPlaying) SetPanelAnimated(gameplayPanel);
		else SetPanelImmediate(gameplayPanel);
	}

	public void SetMainMenuPanel() {
		if (Application.isPlaying) SetPanelAnimated(mainMenuPanel);
		else SetPanelImmediate(mainMenuPanel);
	}

	public void SetSettingsPanel() {
		if (Application.isPlaying) SetPanelAnimated(settingsPanel);
		else SetPanelImmediate(settingsPanel);
	}
}