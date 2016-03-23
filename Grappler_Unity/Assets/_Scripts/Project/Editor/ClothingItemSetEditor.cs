using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ClothingPackage))]
public class ClothingItemSetEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		ClothingPackage itemSet = target as ClothingPackage;

		if (GUILayout.Button("Clear Locked Data")) itemSet.ClearLockedData();
	}
}
