using UnityEngine;
using System.Collections;

public class UIManagerGlobal : UIManager {
	public static UIManagerGlobal instance;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		else instance = this;

		BaseAwake();
		DontDestroyOnLoad(gameObject);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (CurrentPanelExists()) HideCurrentPanel();
			else ShowPostGamePanel();
		}
	}

	public void ShowPostGamePanel() {
		ShowPanel<PostGamePanel>();
		PostGamePanel panel = GetPanelOfType<PostGamePanel>();
		panel.ShowBars();
		panel.ShowGameMenu();
	}
}
