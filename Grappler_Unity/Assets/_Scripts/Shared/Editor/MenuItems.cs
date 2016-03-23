using UnityEngine;
using System.Collections;
using UnityEditor;

public class MenuItems : Editor {
	[MenuItem("Utilities/Clear PlayerPrefs")]
	public static void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
		Debug.Log("PlayerPrefs cleared!");
	}

	[MenuItem("Assets/Create/CollectionItemSpriteDataAsset", false, 102)]
	public static void CreateCollectionItemSpriteDataAsset() {
		ScriptableObjectUtility.CreateAsset<CollectionItemSpriteData>();
	}

	[MenuItem("Assets/Create/CollectionItem", false, 102)]
	public static void CreateCollectionItemAsset() {
		ScriptableObjectUtility.CreateAsset<CollectionItemData>();
	}
}
