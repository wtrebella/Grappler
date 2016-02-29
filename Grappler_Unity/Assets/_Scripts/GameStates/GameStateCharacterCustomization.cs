using UnityEngine;
using System.Collections;

public class GameStateCharacterCustomization : GameStateBase {
	private void Awake() {
		_gameStateType = GameStateType.CharacterCustomization;
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
