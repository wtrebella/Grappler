﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectionItemData : ScriptableObject {
	private string ownedKey {get {return "collectionItem_" + name + "_owned";}}
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

	public CollectionItemSpriteData[] spriteData;
}
