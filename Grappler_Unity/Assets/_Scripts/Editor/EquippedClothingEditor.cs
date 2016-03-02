using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EquippedClothing))]
public class EquippedClothingEditor : Editor {
	public override void OnInspectorGUI() {
		EditorGUILayout.Space();
		var itemSets = EquippedClothing.instance.equippedSets;
		foreach (ClothingItemSet itemSet in itemSets) {
			EditorGUILayout.LabelField(itemSet.type.ToString() + ": " + itemSet.name);
		}
		Repaint();
	}
}
