using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameStateManager : MonoBehaviour
{
	// Map of game state types to a corresponding game type object
	Dictionary<GameStateType, GameStateBase> _stateTypeToStateMap = new Dictionary<GameStateType, GameStateBase>();

	Stack<GameStateBase> _activeGameStates = new Stack<GameStateBase>();

	[SerializeField]
	GameStateType _initGameState = GameStateType.CharacterCustomization;

	Transform _gameModeParent = null;

	public GameStateBase CurrentState
	{ get { return _activeGameStates.Peek(); } }

	public void Reinitialize(GameStateType gameStateType)
	{
		_stateTypeToStateMap.Clear();

		InitializeGameStateTypes();
		PushGameState(gameStateType);
	}

	void Update()
	{
		if(_activeGameStates.Count > 0)
			_activeGameStates.Peek().OnUpdateState();
	}

	void InitializeGameStateTypes()
	{
//		// Menus
//        _stateTypeToStateMap.Add(GameStateType.MainMenu, SpawnGameModeGO<GameStateMainMenu>());
//		_stateTypeToStateMap.Add(GameStateType.CharacterSelect, SpawnGameModeGO<GameStateCharacterSelect>());
//		_stateTypeToStateMap.Add(GameStateType.GameOver, SpawnGameModeGO<GameStateGameOver>());
//
//		// Game modes
//		_stateTypeToStateMap.Add(GameStateType.Soccer, SpawnGameModeGO<GameStateSoccer>());
//		_stateTypeToStateMap.Add(GameStateType.CaptureTheFlagTwoTeam, SpawnGameModeGO<GameStateCaptureTheFlagTwoTeam>());
//		_stateTypeToStateMap.Add(GameStateType.CaptureTheFlagMultiTeam, SpawnGameModeGO<GameStateCaptureTheFlagMultiTeam>());
//		_stateTypeToStateMap.Add(GameStateType.HotPotato, SpawnGameModeGO<GameStateHotPotato>());
//		_stateTypeToStateMap.Add(GameStateType.Coinz, SpawnGameModeGO<GameStateCoinz>());
//		_stateTypeToStateMap.Add(GameStateType.Sumo, SpawnGameModeGO<GameStateSumo>());
//		_stateTypeToStateMap.Add(GameStateType.Volleyball, SpawnGameModeGO<GameStateVolleyball>());
	}

	T SpawnGameModeGO<T>() where T : GameStateBase
	{
		T tObj = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
		tObj.transform.SetParent(_gameModeParent);

		return tObj;
	}

	public void PushGameState(GameStateType gameStateType)
	{
		if(_activeGameStates.Count > 0)
			_activeGameStates.Peek().OnPauseState();

		_activeGameStates.Push(_stateTypeToStateMap[gameStateType]);
			_stateTypeToStateMap[gameStateType].OnEnterState();
	}

	public void PopGameState()
	{
		if(_activeGameStates.Count > 0)
			_activeGameStates.Pop().OnExitState();

		if(_activeGameStates.Count > 0)
			_activeGameStates.Peek().OnUnpauseState();
	}

	public void ClearGameStates()
	{
		while(_activeGameStates.Count > 0)
			_activeGameStates.Pop().OnExitState();
	}

	public bool IsInState(GameStateType type)
	{
		return _activeGameStates.Peek() == _stateTypeToStateMap[type];
	}
}
