using UnityEngine;
using System.Collections;
using UnityEditor;

public class MenuItems : Editor {
	[MenuItem("Utilities/Clear PlayerPrefs")]
	public static void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
		Debug.Log("PlayerPrefs cleared!");
	}

	[MenuItem("Assets/Create/CollectionItemSprite Asset", false, 102)]
	public static void CreateCollectionItemSpriteDataAsset() {
		ScriptableObjectUtility.CreateAsset<CollectionItemSprite>();
	}

	[MenuItem("Assets/Create/CollectionItem Asset", false, 102)]
	public static void CreateCollectionItemAsset() {
		ScriptableObjectUtility.CreateAsset<CollectionItem>();
	}

	[MenuItem("Assets/Create/SpineCollectionItemSprite Asset", false, 102)]
	public static void CreateSpineCollectionItemSpriteDataAsset() {
		ScriptableObjectUtility.CreateAsset<SpineCollectionItemSprite>();
	}
}
