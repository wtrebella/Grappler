using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PostGamePanel : RootPanel {
	[SerializeField] private BarPanel barPanel;
	[SerializeField] private GameMenuPanel gameMenuPanel;

	private void Awake() {

	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	public override void Show() {
		base.Show();

		barPanel.Show();
		gameMenuPanel.Show();
	}

	public override void Hide() {
		base.Hide();

		barPanel.Hide();
		gameMenuPanel.Hide();
	}
}
