using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class EquippedClothing : ScriptableObjectSingleton<EquippedClothing> {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/EquippedClothingAsset", false, 100)]
	public static void CreateEquippedClothingAsset() {
		ScriptableObjectUtility.CreateAsset<EquippedClothing>("EquippedClothing");
	}
	#endif

	[SerializeField] private List<ClothingItemSet> _equippedSets;

	public List<ClothingItemSet> equippedSets {
		get {
			if (_equippedSets == null) _equippedSets = new List<ClothingItemSet>();
			return _equippedSets;
		}
	}

	public ClothingItemSet GetEquippedItemSet(ClothingItemSetType itemSetType) {
		int indexOfEquippedType = IndexOfEquippedType(itemSetType);
		if (indexOfEquippedType < 0) return null;

		return equippedSets[indexOfEquippedType];
	}

	public bool ItemSetTypeIsEquipped(ClothingItemSetType itemSetType) {
		foreach (ClothingItemSet itemSet in equippedSets) {
			if (itemSet.type == itemSetType) return true;
		}
		return false;
	}

	public void RemoveEquippedItemSet(ClothingItemSetType itemSetType) {
		int index = IndexOfEquippedType(itemSetType);
		if (index < 0 || index >= equippedSets.Count) return;
		equippedSets.RemoveAt(index);
	}

	private int IndexOfEquippedType(ClothingItemSetType itemSetType) {
		for (int i = 0; i < equippedSets.Count; i++) {
			ClothingItemSet itemSet = equippedSets[i];
			if (itemSet.type == itemSetType) return i;
		}
		return -1;
	}
}
