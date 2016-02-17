using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PanelController))]
public class PanelControllerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PanelController panelController = (PanelController)target;

		if (GUILayout.Button("Set To Gameplay Panel")) {
			panelController.SetGameplayPanel();
		}

		if (GUILayout.Button("Set To Main Menu Panel")) {
			panelController.SetMainMenuPanel();
		}

		if (GUILayout.Button("Set To Settings Panel")) {
			panelController.SetSettingsPanel();
		}

		if (panelController.GetCurrentPanel() != null) {
			EditorGUILayout.LabelField("Current Panel: " + panelController.GetCurrentPanel().name);
		}
	}
}
