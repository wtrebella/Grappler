using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EquippedClothing))]
public class EquippedClothingEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		EditorGUILayout.Space();
		var types = EquippedClothing.instance.GetEquippedTypes();
		foreach (ClothingItemSetType type in types) {
			ClothingItemSet itemSet = EquippedClothing.instance.GetEquippedItemSet(type);
			EditorGUILayout.LabelField(type.ToString() + ": " + itemSet.name);
		}
		Repaint();
	}
}
