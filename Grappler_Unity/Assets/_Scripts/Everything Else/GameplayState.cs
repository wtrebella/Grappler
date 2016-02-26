using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// right now, the game manager just loads the gamescene and the game automatically starts.
// i need to have an abstract game state manager that controls what gamestates will be loaded somehow.

public class GameplayState : GameStateBase {
	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) RestartGame();
	}

	public override void OnEnterState() {
		
	}

	public override IEnumerator OnEnterState_Routine() {yield break;}

	public override void OnUpdateState() {
		
	}

	public override void OnExitState() {
		
	}

	public override void OnPauseState() {
		
	}

	public override void OnUnpauseState() {
		
	}
}
