using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PostGamePanel : RootPanel {
	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.B)) {
			if (ModularPanelIsShowing<BarPanel>()) HideModularPanel<BarPanel>();
			else ShowModularPanel<BarPanel>();
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			if (ModularPanelIsShowing<GameMenuPanel>()) HideModularPanel<GameMenuPanel>();
			else ShowModularPanel<GameMenuPanel>();
		}
	}
}
