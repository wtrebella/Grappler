using UnityEngine;
using System.Collections;

public enum ClothingItemType {
	Hat,
	None
}

public enum ClothingSkeletonType {
	Top,
	Bottom,
	None
}

public class ClothingItem : ScriptableObject {
	public tk2dSpriteCollectionData spriteCollectionData;
	public ClothingItemType type = ClothingItemType.None;
	public ClothingSkeletonType skeleton = ClothingSkeletonType.None;

	[SerializeField, HideInInspector] private tk2dSpriteDefinition sprite;

	public tk2dSpriteDefinition GetSprite() {
		return sprite;
	}

	public void SetSprite(tk2dSpriteDefinition sprite) {
		if (spriteCollectionData == null) {
			Debug.LogError("sprite collection data is null!");
			return;
		}
		if (spriteCollectionData.GetSpriteDefinition(sprite.name) == null) {
			Debug.LogError("sprite collection data doesn't contain definition for sprite: " + sprite.name);
			return;
		}

		this.sprite = sprite;
	}

	public void RemoveSprite() {
		this.sprite = null;
	}
}
