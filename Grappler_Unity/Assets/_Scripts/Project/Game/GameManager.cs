using UnityEngine;
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
		if (Input.GetKeyDown(KeyCode.R)) Restart();
	}

	public void Restart() {
		GameSceneManager.instance.GoToGameplayState();
	}
		
	private void OnEnterDeadState() {
		GameOver();
	}

	private void GameOver() {
		if (isGameOver) Debug.LogWarning("game already over!");

		isGameOver = true;
		ShowPostGamePanel();
	}

	private void ShowPostGamePanel() {
		UIManagerGlobal.instance.ShowPostGamePanel();
	}
}
