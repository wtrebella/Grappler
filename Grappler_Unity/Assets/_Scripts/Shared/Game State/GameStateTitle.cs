using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateTitle : GameStateBase {
	private void Awake() {
		_gameStateType = GameStateType.Title;
	}

	public override void OnEnterState() {
		base.OnEnterState();

		InputManager.instance.SignalTouchDown += OnTouchDown;
	}

	public override IEnumerator OnEnterState_Routine() {
		yield return StartCoroutine(base.OnEnterState_Routine());
		yield break;
	}

	private void OnTouchDown() {
		StartGame();
	}

	private void StartGame() {
		GameStateManager.instance.PopGameState();
		GameStateManager.instance.PushGameState(GameStateType.Gameplay);
	}

	public override void OnUpdateState() {
		base.OnUpdateState();

	}

	public override void OnExitState() {
		base.OnExitState();

		InputManager.instance.SignalTouchDown -= OnTouchDown;
	}

	public override void OnPauseState() {
		base.OnPauseState();
	}

	public override void OnUnpauseState() {
		base.OnUnpauseState();	
	}
}
