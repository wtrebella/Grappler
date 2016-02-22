using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Clothing))]
public class ClothingEditor : Editor {
	private void OnEnable() {

	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if (!Application.isPlaying) return;

		Clothing clothing = target as Clothing;
		ClothingItemType type = ClothingItemType.Hat;
		ClothingItem[] items = GetItems(type);
		if (items.Length == 0) return;
			
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField(type.ToString());
		if (clothing.ItemTypeIsEquipped(type)) {
			ClothingItem equippedItem = clothing.GetEquippedItem(type);
			EditorGUILayout.LabelField("Equipped: " + equippedItem.GetSprite().name);
			if (GUILayout.Button("Unequip " + type.ToString())) clothing.Unequip(type);
		}
		else {
			foreach (ClothingItem item in items) {
				tk2dSpriteDefinition sprite = item.GetSprite();
				if (sprite == null) continue;
				if (GUILayout.Button(sprite.name)) clothing.Equip(item);
			}
		}
	}

	private ClothingItem[] GetItems(ClothingItemType type) {
		Clothing clothing = target as Clothing;

		if (type == ClothingItemType.Hat) return clothing.GetHats();

		return null;
	}
}
