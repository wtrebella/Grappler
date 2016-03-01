using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateGameplay : GameStateBase {
	PanelGameplay _gameplayPanel = null;
	PanelGameplay gameplayPanel {
		get {
			if (!_gameplayPanel) _gameplayPanel = UIManager.GetPanelOfType<PanelGameplay>();

			return _gameplayPanel;
		}
	}

	public void RestartGame() {
		GameStateManager.instance.PopGameState();
		GameStateManager.instance.PushGameState(GameStateType.Gameplay);
	}

	private void Awake() {
		_gameStateType = GameStateType.Gameplay;
	}

	public override void OnEnterState() {
		base.OnEnterState();

		gameplayPanel.gameObject.SetActive(true);
	}

	public override IEnumerator OnEnterState_Routine() {
		yield return StartCoroutine(base.OnEnterState_Routine());
		yield break;
	}

	public override void OnUpdateState() {
		base.OnUpdateState();
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}

	public override void OnExitState() {
		base.OnExitState();

		gameplayPanel.gameObject.SetActive(false);
	}

	public override void OnPauseState() {
		base.OnPauseState();
	}

	public override void OnUnpauseState() {
		base.OnUnpauseState();	
	}
}
