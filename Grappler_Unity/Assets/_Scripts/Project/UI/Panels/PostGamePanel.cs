using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PostGamePanel : RootPanel {
	public void ShowBars() {
		ShowModularPanel<BarPanel>();
	}

	public void HideBars() {
		HideModularPanel<BarPanel>();
	}
		
	public void ShowGameMenu() {
		ShowModularPanel<GameMenuPanel>();
	}

	public void HideGameMenu() {
		HideModularPanel<GameMenuPanel>();
	}

	private void Awake() {

	}
	
	private void Start() {
	
	}
}
