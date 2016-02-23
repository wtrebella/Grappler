using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ClothingItem))]
public class ClothingItemEditor : Editor {
	private string clothingPrefix = "clothing_";

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		bool changed = false;

		ClothingItem clothingItem = target as ClothingItem;
		if (clothingItem.spriteCollectionData == null) {
			EditorGUILayout.LabelField("Set Sprite Collection Data!");
		}
		else {
			if (!clothingItem.HasValidSpriteName()) {
				tk2dSpriteDefinition[] allSprites = clothingItem.spriteCollectionData.spriteDefinitions;
				List<tk2dSpriteDefinition> clothingSprites = new List<tk2dSpriteDefinition>();
				foreach (tk2dSpriteDefinition sprite in allSprites) {
					if (sprite.name.Contains(clothingPrefix)) clothingSprites.Add(sprite);
				}

				foreach (tk2dSpriteDefinition sprite in clothingSprites) {
					if (GUILayout.Button(sprite.name)) {
						clothingItem.SetSprite(sprite.name);
						changed = true;
					}
				}
			}
			else {
				EditorGUILayout.LabelField("Sprite Name: " + clothingItem.spriteName);
				if (GUILayout.Button("Remove Sprite")) {
					clothingItem.spriteName = null;
					changed = true;
				}
			}
		}

		if (GUI.changed || changed) {
			EditorUtility.SetDirty(clothingItem);
		}
	}
}
