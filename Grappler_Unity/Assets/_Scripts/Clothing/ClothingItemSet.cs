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
}
