using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSceneManager : MonoBehaviour {
	public static GameSceneManager instance;

	private void Awake() {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}

	public void StartOrRestartGame() {
		StartCoroutine(StartOrRestartGameRoutine());
	}

	private IEnumerator StartOrRestartGameRoutine() {
		UIManagerGlobal.instance.HideCurrentPanel();
		while (UIManagerGlobal.instance.CurrentPanelExists()) yield return null;
		if (GameStateManager.instance.IsInState(GameStateType.Gameplay)) GameStateManager.instance.PopGameState();
		GameStateManager.instance.PushGameState(GameStateType.Gameplay);
	}
}
