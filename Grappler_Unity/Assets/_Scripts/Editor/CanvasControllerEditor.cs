using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CanvasController))]
public class CanvasControllerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		CanvasController canvasController = (CanvasController)target;

		if (GUILayout.Button("Set To Gameplay Canvas")) {
			canvasController.SetGameplayCanvas();
		}

		if (GUILayout.Button("Set To Main Menu Canvas")) {
			canvasController.SetMainMenuCanvas();
		}

		if (GUILayout.Button("Set To Settings Canvas")) {
			canvasController.SetSettingsCanvas();
		}

		if (canvasController.GetCurrentCanvas() != null) {
			EditorGUILayout.LabelField("Current Canvas: " + canvasController.GetCurrentCanvas().name);
		}
	}
}
