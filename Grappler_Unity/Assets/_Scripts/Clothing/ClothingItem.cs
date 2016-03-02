using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ClothingItemType {
	None = 0,
	Hat,
	ShoeBack,
	ShoeFront,
	MAX
}

public enum ClothingSkeletonType {
	Top,
	Bottom,
	None
}

[System.Serializable]
public class ClothingItem : ScriptableObject {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ClothingItemAsset", false, 101)]
	public static void CreateItemAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItem>();
	}
	#endif

	public tk2dSpriteCollectionData spriteCollectionData;
	public ClothingItemType type = ClothingItemType.None;
	public ClothingSkeletonType skeleton = ClothingSkeletonType.None;
	[HideInInspector] public string spriteName;

	public void SetSprite(string spriteName) {
		if (spriteCollectionData == null) {
			Debug.LogError("sprite collection data is null!");
			return;
		}

		this.spriteName = spriteName;

		if (!HasValidSpriteName()) {
			Debug.LogError("sprite collection data doesn't contain definition for sprite: " + spriteName);
			return;
		}
	}

	public bool HasValidSpriteName() {
		return !string.IsNullOrEmpty(spriteName) && spriteCollectionData.GetSpriteDefinition(spriteName) != null;
	}
}
