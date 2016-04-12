using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectionItem : ScriptableObject {
	public string itemName = "New Item";

	private string ownedKey {get {return "collectionItem_" + itemName + "_owned";}}
	private bool _owned = false;
	public bool owned {
		get {
			return WhitPrefs.GetBool(ownedKey, PrefsBoolPriority.True, _owned, false);
		}
		set {
			_owned = value;
			WhitPrefs.SetBool(ownedKey, PrefsBoolPriority.True, _owned);
		}
	}

	public CollectionItemSprite[] sprites;

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
