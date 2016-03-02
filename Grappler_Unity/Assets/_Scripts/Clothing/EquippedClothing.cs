using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class EquippedClothing : ScriptableObject {
	[SerializeField] private List<ClothingItemSet> _equippedSets;

	private static EquippedClothing _instance = null;
	public static EquippedClothing instance {
		get {
			if (_instance == null) {
				_instance = Resources.Load<EquippedClothing>("EquippedClothing");

				if (_instance == null) {
					string errorString = "No singleton object for " + typeof(EquippedClothing).ToString() + " exists in the resources folder!";
					throw new UnityException(errorString);
				}
			}

			return _instance;
		}
	}

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
