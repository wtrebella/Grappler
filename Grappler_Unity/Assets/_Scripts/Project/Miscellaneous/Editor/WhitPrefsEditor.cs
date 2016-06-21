using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(WhitPrefs))]
public class WhitPrefsEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		EditorGUILayout.Space();
		if (GUILayout.Button("Remove All")) WhitPrefs.RemoveAll();
	}
}
