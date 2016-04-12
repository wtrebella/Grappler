using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpineCollectionItemSprite : CollectionItemSprite {
	[SerializeField] private string spineSpritePathRoot = "";

	public string spineSpritePath {
		get {
			if (HasValidSpriteName()) return spineSpritePathRoot + spriteName;
			else return "";
		}
	}
}
