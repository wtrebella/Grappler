using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ClothingSkeletonType {
	Top,
	Bottom,
	None
}

[System.Serializable]
public class ClothingItem : CollectableItem {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ClothingItemAsset", false, 101)]
	public static void CreateItemAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItem>();
	}
	#endif

	public ClothingItemType type = ClothingItemType.None;
	public ClothingSkeletonType skeleton = ClothingSkeletonType.None;
}