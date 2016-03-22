using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ClothingItemSet : ScriptableObject {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ClothingItemSetAsset", false, 102)]
	public static void CreateItemSetAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingItemSet>();
	}
	#endif

	public bool isLocked {get; private set;}

	public bool isLockedByDefault = true;
	public ClothingItem[] items;
	public ClothingItemSetType type = ClothingItemSetType.None;

	public string itemName {
		get {
			string[] nameParts = name.Split('_');
			string justName = nameParts[nameParts.Length - 1];
			return justName;
		}
	}

	public ClothingItem GetFirstClothingItem() {
		if (items == null || items.Length == 0) return null;
		return items[0];
	}

	public void ClearLockedData() {
		string key = GetIsLockedKey();
		WhitPrefs.RemoveObjectForKey(key);
		Debug.Log("key \"" + key + "\" was deleted!");
	}

	private void OnEnable() {
		isLocked = GetIsLockedSave();
	}

	private void OnDisable() {
		WhitPrefs.SetBool(GetIsLockedKey(), PrefsBoolPriority.False, isLocked);
	}

	private bool GetIsLockedSave() {
		return WhitPrefs.GetBool(GetIsLockedKey(), PrefsBoolPriority.False, isLocked, isLockedByDefault);
	}
		
	private string GetIsLockedKey() {
		return itemName + "_isLocked";
	}
}