using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClothingItemAsset {

	[MenuItem("Assets/Create/ClothingItemAsset")]
	public static void CreateItemAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItem>();
	}

	[MenuItem("Assets/Create/ClothingItemSetAsset")]
	public static void CreateItemCollectionAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItemSet>();
	}
}
