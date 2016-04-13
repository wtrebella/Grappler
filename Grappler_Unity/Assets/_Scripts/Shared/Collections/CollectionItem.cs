using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectionItem : ScriptableObject {
	public string itemName = "New Item";

	private string ownedKey {get {return "collectionItem_" + itemName + "_owned";}}
	public bool owned {
		get {
			return WhitPrefs.GetBool(ownedKey, PrefsBoolPriority.True, ownedByDefault);
		}
		set {
			WhitPrefs.SetBool(ownedKey, PrefsBoolPriority.True, value);
		}
	}

	public void OnEnable() {
		if (ownedByDefault) owned = ownedByDefault;
	}

	public bool ownedByDefault = false;

	public CollectionItemSprite[] sprites;

	public void RemovePrefsData() {
		WhitPrefs.RemovePrefsKeyData(ownedKey);
	}

	public CollectionItemSprite GetFirstSprite() {
		return GetSprite(0);
	}

	public CollectionItemSprite GetSecondSprite() {
		return GetSprite(1);
	}

	public CollectionItemSprite GetSprite(int spriteIndex) {
		if (!HasSprites()) {
			Debug.LogError("no sprites to get!");
			return null;
		}
		if (GetSpritesCount() <= spriteIndex) {
			Debug.LogError("trying to get sprite at index " + spriteIndex + " but there are only " + GetSpritesCount() + " available!");
			return null;
		}
		return sprites[spriteIndex];
	}

	public bool HasSprites() {
		return sprites != null && sprites.Length > 0;
	}

	public int GetSpritesCount() {
		if (!HasSprites()) return 0;
		return sprites.Length;
	}
}
