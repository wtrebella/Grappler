﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	[SerializeField] private DeadStateController deadStateController;

	private bool isGameOver = false;

	private void Awake() {
		deadStateController.SignalEnterDeadState += OnEnterDeadState;
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	private void OnEnterDeadState() {
		GameOver();
	}

	private void GameOver() {
		isGameOver = true;
		ShowBars();
	}

	private void ShowBars() {
		UIManagerGlobal.instance.AddPanelToQueue<BarPanel>();
	}
}
