using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CollectionItemSprite))]
public class CollectionItemSpriteDataEditor : Editor {
	private CollectionItemSprite spriteData;

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		spriteData = target as CollectionItemSprite;

		if (spriteData.HasSpriteCollection()) ShowSpriteCollectionInfo();
		else ShowSpriteCollectionMissingWarning();

		if (GUI.changed) EditorUtility.SetDirty(spriteData);
	}

	private void ShowSpriteCollectionInfo() {
		if (spriteData.HasValidSpriteName()) {
			ShowSpriteInfo();
			ShowRemoveSpriteButton();
		}
		else {
			ShowAllSpriteButtons();
		}
	}

	private void ShowSpriteInfo() {
		EditorGUILayout.LabelField("Sprite Name: " + spriteData.spriteName);
	}

	private void ShowRemoveSpriteButton() {
		if (GUILayout.Button("Remove Sprite")) {
			spriteData.RemoveSprite();
			GUI.changed = true;
		}
	}

	private void ShowAllSpriteButtons() {
		tk2dSpriteCollectionData spriteCollectionData = spriteData.GetSpriteCollectionData();
		tk2dSpriteDefinition[] allSprites = spriteCollectionData.spriteDefinitions;

		foreach (tk2dSpriteDefinition sprite in allSprites) {
			if (GUILayout.Button(sprite.name)) {
				spriteData.SetSprite(sprite.name);
				GUI.changed = true;
			}
		}
	}

	private void ShowSpriteCollectionMissingWarning() {
		EditorGUILayout.LabelField("Set Sprite Collection Data!");
	}
}
