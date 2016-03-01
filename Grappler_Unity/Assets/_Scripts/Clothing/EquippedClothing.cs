using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EquippedClothing : ScriptableObjectSingleton<EquippedClothing> {
	[SerializeField] private Dictionary<ClothingItemSetType, ClothingItemSet> _equippedSets;

	public Dictionary<ClothingItemSetType, ClothingItemSet> equippedSets {
		get {
			if (_equippedSets == null) _equippedSets = new Dictionary<ClothingItemSetType, ClothingItemSet>();

			foreach (ClothingItemSetType key in _equippedSets.Keys) Debug.Log(key);
			Debug.Log("========");

			// the equipped clothes are being serialized here. but i'm not sure how well it's working yet.

			return _equippedSets;
		}
	}
}
