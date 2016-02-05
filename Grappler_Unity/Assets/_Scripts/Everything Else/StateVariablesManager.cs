using UnityEngine;
using System.Collections;

public class StateVariablesManager : MonoBehaviour {
	private static StateVariablesManager _instance;

	private static StateVariables _stateVariables;
	public static StateVariables stateVariables {
		get {
			if (_stateVariables == null) InitStateVariables();
			if (_instance == null) InitManager();
			return _stateVariables;
		}
	}

	private static void InitManager() {
		_instance = (new GameObject("State Variables Manager", typeof(StateVariablesManager))).GetComponent<StateVariablesManager>();
		_instance.Load();
		DontDestroyOnLoad(_instance.gameObject);
	}

	private static void InitStateVariables() {
		_stateVariables = new StateVariables();
	}

	private void Load() {
		_stateVariables.characterType = (CharacterType)PlayerPrefs.GetInt(StateVariables.characterType_prefsString, 0);
	}
	
	private void Save() {
		PlayerPrefs.SetInt(StateVariables.characterType_prefsString, (int)_stateVariables.characterType);
	}

	private void OnApplicationQuit() {
		Save();
	}
}