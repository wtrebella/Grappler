using UnityEngine;
using System.Collections;

public class GameStateCharacterCustomization : GameStateBase {
	PanelCharacterCustomization _characterCustomizationPanel = null;
	PanelCharacterCustomization characterCustomizationPanel {
		get {
			if (!_characterCustomizationPanel) _characterCustomizationPanel = UIManager.GetPanelOfType<PanelCharacterCustomization>();

			return _characterCustomizationPanel;
		}
	}

	private void Awake() {
		_gameStateType = GameStateType.CharacterCustomization;
	}

	public override void OnEnterState() {
		base.OnEnterState();

		characterCustomizationPanel.gameObject.SetActive(true);
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

		characterCustomizationPanel.gameObject.SetActive(false);
	}

	public override void OnPauseState() {
		base.OnPauseState();
	}

	public override void OnUnpauseState() {
		base.OnUnpauseState();
	}
}
