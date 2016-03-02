﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ClothingManager))]
public class ClothingManagerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if (!Application.isPlaying) return;

		ClothingManager clothing = target as ClothingManager;
		for (int i = 1; i < (int)ClothingItemType.MAX; i++) {
			ClothingItemSetType type = (ClothingItemSetType)i;
			ClothingItemSet[] itemSets = clothing.GetClothingItemSets(type);
			if (itemSets.Length == 0) continue;
				
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField(type.ToString());
			if (clothing.ItemSetIsEquipped(type)) {
				ClothingItemSet equippedSet = clothing.GetEquippedItemSet(type);
				EditorGUILayout.LabelField("Equipped: " + equippedSet.name);
				if (GUILayout.Button("Unequip " + type.ToString())) clothing.UnequipItemSet(type);
			}
			else {
				foreach (ClothingItemSet itemSet in itemSets) {
					if (GUILayout.Button(itemSet.name)) clothing.EquipItemSet(itemSet);
				}
			}
		}
	}
}