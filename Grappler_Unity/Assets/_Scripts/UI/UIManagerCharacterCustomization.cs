using UnityEngine;
using System.Collections;

public class UIManagerCharacterCustomization : UIManager {
	private void Start() {
		GetPanelOfType<PanelCharacterCustomization>().gameObject.SetActive(true);
	}

	public void OnPlayButtonClicked() {
		GameStateManager.instance.PopGameState();
		GameStateManager.instance.PushGameState(GameStateType.Gameplay);
	}
}
