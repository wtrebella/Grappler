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

	public Bounds GetSpriteBounds() {
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		tk2dSpriteDefinition spriteDefinition = GetSpriteDefinition();
		if (spriteDefinition != null) bounds = spriteDefinition.GetBounds();
		return bounds;
	}

	public Vector2 GetSpriteSize() {
		Bounds bounds = GetSpriteBounds();
		return bounds.size;
	}

	public float GetSpriteScale() {
		Vector2 size = GetSpriteSize();
		float scale = 100.0f / size.y;
		return scale;
	}

	public tk2dSpriteDefinition GetSpriteDefinition() {
		if (string.IsNullOrEmpty(spriteName)) return null;
		return spriteCollectionData.GetSpriteDefinition(spriteName);
	}

	public bool HasValidSpriteName() {
		return GetSpriteDefinition() != null;
	}
}
