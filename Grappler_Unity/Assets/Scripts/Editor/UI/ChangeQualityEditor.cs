using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ChangeQualityEditor : Editor {
	[MenuItem("Quality/SD")]
	static void SD() {
		ChangeQualityOnAllScenes("SD");
	}

	[MenuItem("Quality/HD")]
	static void HD() {
		ChangeQualityOnAllScenes("HD");
	}

	static void ChangeQualityOnAllScenes(string suffix) {
		if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;

		var currentScene = EditorSceneManager.GetActiveScene();

		foreach (var scene in EditorBuildSettings.scenes) {
			EditorSceneManager.OpenScene(scene.path);
			ChangeQuality.SetQuality(suffix);
			EditorSceneManager.SaveOpenScenes();
		}

		EditorSceneManager.OpenScene(currentScene.path);
	}
}
