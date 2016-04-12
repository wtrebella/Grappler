using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectionItemSprite : ScriptableObject {
	[SerializeField] private tk2dSpriteCollectionData spriteCollectionData;

	[SerializeField, HideInInspector] private string _spriteName;
	public string spriteName {get {return _spriteName;}}

	public void SetSprite(string spriteName) {
		if (!HasSpriteCollection()) {
			Debug.LogError("sprite collection data is null!");
			return;
		}
		_spriteName = spriteName;

		if (!HasValidSpriteName()) {
			Debug.LogError("sprite collection data doesn't contain definition for sprite: " + spriteName);
			return;
		}
	}

	public void RemoveSprite() {
		_spriteName = null;
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

	public bool HasSpriteCollection() {
		return spriteCollectionData != null;
	}

	public tk2dSpriteCollectionData GetSpriteCollectionData() {
		return spriteCollectionData;
	}

	public tk2dSpriteDefinition GetSpriteDefinition() {
		if (string.IsNullOrEmpty(_spriteName)) return null;
		return spriteCollectionData.GetSpriteDefinition(_spriteName);
	}

	public bool HasValidSpriteName() {
		return GetSpriteDefinition() != null;
	}
}
