using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EquippedClothing : ScriptableObjectSingleton<EquippedClothing> {
	[SerializeField] private Dictionary<ClothingItemSetType, ClothingItemSet> _equippedSets;

	public Dictionary<ClothingItemSetType, ClothingItemSet> equippedSets {
		get {
			if (_equippedSets == null) _equippedSets = new Dictionary<ClothingItemSetType, ClothingItemSet>();

			return _equippedSets;
		}
	}

	public List<ClothingItemSetType> GetEquippedTypes() {
		return new List<ClothingItemSetType>(equippedSets.Keys);
	}

	public List<ClothingItemSet> GetEquippedItemSets() {
		return new List<ClothingItemSet>(equippedSets.Values);
	}

	public ClothingItemSet GetEquippedItemSet(ClothingItemSetType itemSetType) {
		if (!equippedSets.ContainsKey(itemSetType)) {
			Debug.LogError("item set type " + itemSetType.ToString() + " doesn't exist in dictionary");
			return null;
		}

		return equippedSets[itemSetType];
	}

	private void OnEnable() {
		Debug.Log(equippedSets.Count);
	}
}
