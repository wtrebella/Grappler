using UnityEngine;
using System.Collections;

public class UIManagerGlobal : UIManager {

	public void ShowMenu() {
		Menu menu = GetPanelOfType<Menu>();
		menu.SetTitle("Fucker!");
		menu.SetDescription("This is the definition of a fucker. Never underestimate them. They will fuck you. Over and over.");
		menu.SetHeaderColor(new Color(0.3f, 0.7f, 1.0f));
		menu.Show();
	}

	public void HideMenu() {
		Menu menu = GetPanelOfType<Menu>();
		menu.Hide();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) ShowMenu();
	}
}
