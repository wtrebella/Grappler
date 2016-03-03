using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ClothingItemSetType {
	None = 0,
	Hat,
	Shoes,
	MAX
}

[System.Serializable]
public class ClothingItemSet : ScriptableObject {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ClothingItemSetAsset", false, 102)]
	public static void CreateItemSetAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItemSet>();
	}
	#endif

	public ClothingItem[] items;
	public ClothingItemSetType type = ClothingItemSetType.None;

	public string itemName {
		get {
			string[] nameParts = name.Split('_');
			string justName = nameParts[nameParts.Length - 1];
			return justName;
		}
	}

	public ClothingItem GetFirstClothingItem() {
		if (items == null || items.Length == 0) return null;
		return items[0];
	}
}