using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Clothing))]
public class ClothingEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if (!Application.isPlaying) return;

		Clothing clothing = target as Clothing;
		for (int i = 1; i < (int)ClothingItemType.MAX; i++) {
			ClothingItemType type = (ClothingItemType)i;
			ClothingItem[] items = GetItems(type);
			if (items.Length == 0) continue;
				
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField(type.ToString());
			if (clothing.ItemTypeIsEquipped(type)) {
				ClothingItem equippedItem = clothing.GetEquippedItem(type);
				EditorGUILayout.LabelField("Equipped: " + equippedItem.spriteName);
				if (GUILayout.Button("Unequip " + type.ToString())) clothing.Unequip(type);
			}
			else {
				foreach (ClothingItem item in items) {
					if (!item.HasValidSpriteName()) continue;
					if (GUILayout.Button(item.spriteName)) clothing.Equip(item);
				}
			}
		}
	}

	private ClothingItem[] GetItems(ClothingItemType type) {
		Clothing clothing = target as Clothing;

		if (type == ClothingItemType.Hat) return clothing.GetHats();
		else if (type == ClothingItemType.ShoeBack) return clothing.GetShoesBack();
		else if (type == ClothingItemType.ShoeFront) return clothing.GetShoesFront();

		return null;
	}
}
