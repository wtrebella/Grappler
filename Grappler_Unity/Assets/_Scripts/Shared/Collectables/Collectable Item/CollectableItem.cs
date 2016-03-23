using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectableItem : ScriptableObject {
	public tk2dSpriteCollectionData spriteCollectionData;
	public CollectablePackageType type = CollectablePackageType.None;
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