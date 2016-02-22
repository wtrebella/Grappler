using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ClothingItem))]
public class ClothingItemEditor : Editor {
	private string clothingPrefix = "clothing_";

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		ClothingItem clothingItem = target as ClothingItem;
		if (clothingItem.spriteCollectionData == null) {
			EditorGUILayout.LabelField("Set Sprite Collection Data!");
		}
		else {
			tk2dSpriteDefinition currentSprite = clothingItem.GetSprite();
			if (currentSprite == null || currentSprite.name == "") {
				tk2dSpriteDefinition[] allSprites = clothingItem.spriteCollectionData.spriteDefinitions;
				List<tk2dSpriteDefinition> clothingSprites = new List<tk2dSpriteDefinition>();
				foreach (tk2dSpriteDefinition sprite in allSprites) {
					if (sprite.name.Contains(clothingPrefix)) clothingSprites.Add(sprite);
				}

				foreach (tk2dSpriteDefinition sprite in clothingSprites) {
					if (GUILayout.Button(sprite.name)) {
						clothingItem.SetSprite(sprite);
						return;
					}
				}
			}
			else {
				EditorGUILayout.LabelField("Sprite Name: " + clothingItem.GetSprite().name);
				if (GUILayout.Button("Remove Sprite")) clothingItem.RemoveSprite();
			}
		}
	}
}
