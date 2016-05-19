using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStateManager : Singleton<GameStateManager> {
	public PayloadBase GetPayloadFromStateType(GameStateType gameStateType) {return _stateTypeToPayloadMap[gameStateType];}
	public GameStateBase CurrentState {get {return _activeGameStates.Peek();}}

	[SerializeField] GameStateType _initGameState = GameStateType.CharacterCustomization;

	private Dictionary<GameStateType, GameStateBase> _stateTypeToStateMap = new Dictionary<GameStateType, GameStateBase>();
	private Dictionary<GameStateType, PayloadBase> _stateTypeToPayloadMap = new Dictionary<GameStateType, PayloadBase>();
	private Stack<GameStateBase> _activeGameStates = new Stack<GameStateBase>();
	private Transform _gameModeParent = null;

	public static void LoadSetupSceneThenGameScene() {
		SceneManager.LoadScene(GameScenes.Root.ToString(), LoadSceneMode.Single);
	}

	private void Awake() {
		_gameModeParent = new GameObject("Game States").transform;
		_gameModeParent.parent = transform;

		InitializeGameStateTypes();
		InitializePayloads();

		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		PushGameState(_initGameState);
	}
		
	private void Update() {
		if(_activeGameStates.Count > 0) _activeGameStates.Peek().OnUpdateState();
	}

	public void Reinitialize(GameStateType gameStateType) {
		_stateTypeToStateMap.Clear();

		InitializeGameStateTypes();
		PushGameState(gameStateType);
	}

	private void InitializeGameStateTypes() {
		_stateTypeToStateMap.Add(GameStateType.CharacterCustomization, SpawnGameState<GameStateCharacterCustomization>());
		_stateTypeToStateMap.Add(GameStateType.Gameplay, SpawnGameState<GameStateGameplay>());
		_stateTypeToStateMap.Add(GameStateType.Title, SpawnGameState<GameStateTitle>());
	}

	private void InitializePayloads() {
		foreach(KeyValuePair<GameStateType, GameStateBase> stateTypeToStatePair in _stateTypeToStateMap) {
			string stateName = stateTypeToStatePair.Key.ToString();
			string payloadPath = "Payloads/" + stateName + "_Payload";
			
			PayloadBase payload = Resources.Load<PayloadBase>(payloadPath);
			if(!_stateTypeToPayloadMap.ContainsKey(stateTypeToStatePair.Key)) {
				_stateTypeToPayloadMap.Add(stateTypeToStatePair.Key, payload);
			}
		}
	}

	private T SpawnGameState<T>() where T : GameStateBase {
		T tObj = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
		tObj.transform.parent = _gameModeParent;

		return tObj;
	}

	public void PushGameState(GameStateType gameStateType) {
		if(_activeGameStates.Count > 0)
			_activeGameStates.Peek().OnPauseState();

		_activeGameStates.Push(_stateTypeToStateMap[gameStateType]);
		_stateTypeToStateMap[gameStateType].OnEnterState();
	}

	public void PopGameState() {
		if(_activeGameStates.Count > 0)
			_activeGameStates.Pop().OnExitState();

		if(_activeGameStates.Count > 0)
			_activeGameStates.Peek().OnUnpauseState();
	}

	public void ClearGameStates() {
		while(_activeGameStates.Count > 0)
			_activeGameStates.Pop().OnExitState();
	}

	public bool IsInState(GameStateType type) {
		return _activeGameStates.Peek() == _stateTypeToStateMap[type];
	}
}
