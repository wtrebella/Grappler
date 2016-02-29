﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class SceneEnumAutoGenerator : UnityEditor.AssetModificationProcessor
{
	public static string[] OnWillSaveAssets(string[] paths)
	{
		foreach(string path in paths)
		{
			if(path.ToLower().Contains(".unity"))
				GenerateSceneEnum();
		}

		return paths;
	}

	[MenuItem("Grappler/GenerateEnum/Games Scenes")]
	static void GenerateSceneEnum()
	{
		// Change this path for your project
		string path = Application.dataPath + "/_Scripts/Gameplay/GameScenes.cs";
		var streamWriter = new StreamWriter(path, false);

		string text =
@" // Auto-generated by SceneEnumAutoGenerator.

using UnityEngine;
using System.Collections;

public enum GameScenes {";
		for(int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			if(EditorBuildSettings.scenes[i] == null || EditorBuildSettings.scenes[i].path == "")
				continue;

			text += "\r\n\t";
			text += EditorBuildSettings.scenes[i].path.Split('/').Last().Replace(".unity", "");

			if(i < EditorBuildSettings.scenes.Length - 1)
				text += ",";
		}

		text += "\r\n}";

		streamWriter.Write(text);
		streamWriter.Flush();
		streamWriter.Close();

		AssetDatabase.Refresh();
	}
}
