using UnityEngine;
using System.Collections;

public class GameplayManager : Singleton<GameplayManager> {
	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}

	public void RestartGame() {
		GameStateManager.instance.PopGameState();
		GameStateManager.instance.PushGameState(GameStateType.Gameplay);
	}
}
