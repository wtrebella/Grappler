using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateGameplay : GameStateBase {
	private void Awake() {
		_gameStateType = GameStateType.Gameplay;
	}

	public override void OnEnterState() {
		base.OnEnterState();
	}

	public override IEnumerator OnEnterState_Routine() {
		yield return StartCoroutine(base.OnEnterState_Routine());
		yield break;
	}

	public override void OnUpdateState() {
		base.OnUpdateState();
	}

	public override void OnExitState() {
		base.OnExitState();
	}

	public override void OnPauseState() {
		base.OnPauseState();
	}

	public override void OnUnpauseState() {
		base.OnUnpauseState();	
	}
}
