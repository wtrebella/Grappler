using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClothingItemAssets {

	[MenuItem("Assets/Create/ClothingItemAsset")]
	public static void CreateItemAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItem>();
	}

	[MenuItem("Assets/Create/ClothingItemSetAsset")]
	public static void CreateItemSetAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItemSet>();
	}

	[MenuItem("Assets/Create/EquippedClothingAsset")]
	public static void CreateEquippedClothingAsset() {
		ScriptableObjectUtility.CreateAsset<EquippedClothing>();
	}
}
