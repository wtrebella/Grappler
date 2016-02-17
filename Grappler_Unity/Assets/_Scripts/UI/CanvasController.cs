using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CanvasController : MonoBehaviour {
	[SerializeField] private Canvas gameplayCanvas;
	[SerializeField] private Canvas mainMenuCanvas;
	[SerializeField] private Canvas settingsCanvas;

	[SerializeField] private Canvas defaultCanvas;

	private Canvas currentCanvas;
	private Canvas[] canvases;

	private void OnEnable() {
		canvases = new Canvas[] {gameplayCanvas, mainMenuCanvas, settingsCanvas};

		SetCanvas(defaultCanvas);
	}
		
	private void SetCanvas(Canvas canvas) {
		currentCanvas = canvas;
		EnableCurrentCanvas();
		DisableNonCurrentCanvases();
	}

	private void DisableNonCurrentCanvases() {
		foreach (Canvas c in canvases) {
			if (c != currentCanvas) DisableCanvas(c);
		}
	}

	private void EnableCurrentCanvas() {
		EnableCanvas(currentCanvas);
	}

	private void EnableCanvas(Canvas canvas) {
		canvas.enabled = true;
	}

	private void DisableCanvas(Canvas canvas) {
		canvas.enabled = false;
	}

	public Canvas GetCurrentCanvas() {
		return currentCanvas;
	}

	public void SetGameplayCanvas() {
		SetCanvas(gameplayCanvas);
	}

	public void SetMainMenuCanvas() {
		SetCanvas(mainMenuCanvas);
	}

	public void SetSettingsCanvas() {
		SetCanvas(settingsCanvas);
	}

	// TODO: refactor this part into their own classes
	public void TransitionTo(Canvas canvas) {

	}
}