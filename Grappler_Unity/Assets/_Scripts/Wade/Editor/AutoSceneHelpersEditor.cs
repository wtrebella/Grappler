using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSceneHelpersEditor : Editor
{
	static string loadedLevel = "";

	static AutoSceneHelpersEditor()
	{
		loadedLevel = EditorSceneManager.GetActiveScene().name;

		if(!Application.isPlaying)
			EditorApplication.hierarchyWindowChanged += OnHierarchyChange;
	}

	static void OnHierarchyChange()
	{
		if(EditorSceneManager.GetActiveScene().name != loadedLevel)
		{
			loadedLevel = EditorSceneManager.GetActiveScene().name;
			if(!GameLoader.DoesExist())
			{
				GameLoader.instance.ToString();
				if(!Application.isPlaying)
					EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}

			if(!Workbench.DoesExist())
			{
				Workbench.instance.AddComponent<DestroySelf>();

				if(!Application.isPlaying)
					EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
			}
		}
	}
}