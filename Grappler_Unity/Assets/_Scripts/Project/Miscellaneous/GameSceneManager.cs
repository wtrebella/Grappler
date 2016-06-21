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

	public void GoToGiftState() {
		StartCoroutine(GoToGameState(GameStateType.Gift));
	}

	public void GoToGameplayState() {
		StartCoroutine(GoToGameState(GameStateType.Gameplay));
	}

	private IEnumerator GoToGameState(GameStateType gameStateType) {
		yield return StartCoroutine(CleanUpCurrentScene());
		GameStateManager.instance.PushGameState(gameStateType);
	}

	private IEnumerator CleanUpCurrentScene() {
		UIManagerGlobal.instance.HideCurrentPanel();
		while (UIManagerGlobal.instance.CurrentPanelExists()) yield return null;
		GameStateManager.instance.PopGameState();
	}
}
