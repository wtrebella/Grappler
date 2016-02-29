using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateGameplay : GameStateBase {
	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	private void Awake() {
		_gameStateType = GameStateType.Gameplay;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}

	public override void OnEnterState() {
		StartCoroutine(OnEnterState_Routine());
	}

	public override IEnumerator OnEnterState_Routine() {
		yield return StartCoroutine(SetupPayload());
		yield break;
	}

	public override void OnUpdateState() {
		
	}

	public override void OnExitState() {
		
	}

	public override void OnPauseState() {
		
	}

	public override void OnUnpauseState() {
		
	}
}
