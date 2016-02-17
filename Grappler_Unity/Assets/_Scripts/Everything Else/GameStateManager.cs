using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public enum GameState {
	Gameplay,
	MainMenu,
	Settings
}

public class GameStateManager : MonoBehaviour {
	public UnityEvent OnGameStateSetToGameplay;
	public UnityEvent OnGameStateSetToMainMenu;
	public UnityEvent OnGameStateSetToSettings;

	[SerializeField] GameState defaultState = GameState.Gameplay;

	public GameState state {get; private set;}

	private void Start() {
		SetState(defaultState);
	}

	public void SetState(GameState newState) {
		state = newState;
		if (state == GameState.Gameplay) WhitTools.Invoke(OnGameStateSetToGameplay);
		else if (state == GameState.MainMenu) WhitTools.Invoke(OnGameStateSetToMainMenu);
		else if (state == GameState.Settings) WhitTools.Invoke(OnGameStateSetToSettings);
	}

	public void SetStateGameplay() {
		SetState(GameState.Gameplay);
	}

	public void SetStateMainMenu() {
		SetState(GameState.MainMenu);
	}

	public void SetStateSettings() {
		SetState(GameState.Settings);
	}
}
