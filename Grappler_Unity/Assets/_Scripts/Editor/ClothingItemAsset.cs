using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClothingItemAsset {

	[MenuItem("Assets/Create/ClothingItemAsset")]
	public static void Create() {
		ScriptableObjectUtility.CreateAsset<ClothingItem>();
	}
}
