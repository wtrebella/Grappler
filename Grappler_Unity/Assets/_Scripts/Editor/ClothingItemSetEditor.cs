using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ClothingItemSet))]
public class ClothingItemSetEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		ClothingItemSet itemSet = target as ClothingItemSet;

		if (GUILayout.Button("Clear Locked Data")) itemSet.ClearLockedData();
	}
}
