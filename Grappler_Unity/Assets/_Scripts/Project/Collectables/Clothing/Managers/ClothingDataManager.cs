using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ClothingDataManager {
	// =========== HATS ============
	private static string itemSetPathHats = "hats/hatsItemSets/";

	private static List<ClothingItemSet> _hats;
	public static List<ClothingItemSet> hats {
		get {
			if (_hats == null) LoadAllHats();
			return _hats;
		}
	}

	private static void LoadAllHats() {
		var hatsArray = Resources.LoadAll(itemSetPathRoot + itemSetPathHats, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
		_hats = new List<ClothingItemSet>();
		foreach (ClothingItemSet hat in hatsArray) _hats.Add(hat);
	}

	private static void LoadEquippedHat() {
		string equippedHatName = WhitPrefs.GetString(ClothingItemSetType.Hat.ToString(), "");
		ClothingItemSet equippedHat = GetHatItemSet(equippedHatName);
		if (equippedHat != null && !_equippedSets.Contains(equippedHat)) _equippedSets.Add(equippedHat);
	}

	private static ClothingItemSet GetHatItemSet(string hatName) {
		foreach (ClothingItemSet itemSet in hats) {
			if (itemSet.itemName == hatName) return itemSet;
		}
		Debug.Log("no hat found with name: " + hatName);
		return null;
	}



	// =========== SHOES ============
	private static string itemSetPathShoes = "shoes/shoesItemSets/";

	private static List<ClothingItemSet> _shoes;
	public static List<ClothingItemSet> shoes {
		get {
			if (_shoes == null) LoadAllShoes();
			return _shoes;
		}
	}

	private static void LoadAllShoes() {
		var shoesArray = Resources.LoadAll(itemSetPathRoot + itemSetPathShoes, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
		_shoes = new List<ClothingItemSet>();
		foreach (ClothingItemSet shoe in shoesArray) _shoes.Add(shoe);
	}

	private static void LoadEquippedShoes() {
		string equippedShoesName = WhitPrefs.GetString(ClothingItemSetType.Shoes.ToString(), "");
		ClothingItemSet equippedShoes = GetShoesItemSet(equippedShoesName);
		if (equippedShoes != null && !_equippedSets.Contains(equippedShoes)) _equippedSets.Add(equippedShoes);
	}

	private static ClothingItemSet GetShoesItemSet(string shoesName) {
		foreach (ClothingItemSet itemSet in shoes) {
			if (itemSet.itemName == shoesName) return itemSet;
		}
		Debug.Log("no shoes found with name: " + shoesName);
		return null;
	}



	// =========== EVERYTHING ELSE ============
	private static string itemSetPathRoot = "clothingItems/";

	private static List<ClothingItemSet> _equippedSets = new List<ClothingItemSet>();
	public static List<ClothingItemSet> equippedSets {
		get {
			if (_equippedSets == null) LoadEquippedItemSets();
			return _equippedSets;
		}
	}

	public static void OnEnable() {
		LoadEquippedItemSets();
	}

	public static void OnDisable() {
		SaveEquippedItemSets();
	}

	public static bool ItemSetTypeIsEquipped(ClothingItemSetType itemSetType) {
		foreach (ClothingItemSet itemSet in equippedSets) {
			if (itemSet.type == itemSetType) return true;
		}
		return false;
	}

	public static ClothingItemSet GetEquippedItemSet(ClothingItemSetType itemSetType) {
		int indexOfEquippedType = IndexOfEquippedType(itemSetType);
		if (indexOfEquippedType < 0) return null;

		return equippedSets[indexOfEquippedType];
	}

	public static List<ClothingItemSet> GetClothingItemSets(ClothingItemSetType type) {
		if (type == ClothingItemSetType.Hat) return hats;
		else if (type == ClothingItemSetType.Shoes) return shoes;
		else {
			Debug.LogError("invalid clothing item set type: " + type.ToString());
			return null;
		}
	}

	public static void RemoveEquippedItemSet(ClothingItemSetType itemSetType) {
		int index = IndexOfEquippedType(itemSetType);
		if (index < 0 || index >= equippedSets.Count) return;
		equippedSets.RemoveAt(index);
	}

	private static int IndexOfEquippedType(ClothingItemSetType itemSetType) {
		for (int i = 0; i < equippedSets.Count; i++) {
			ClothingItemSet itemSet = equippedSets[i];
			if (itemSet.type == itemSetType) return i;
		}
		return -1;
	}

	private static void LoadEquippedItemSets() {
		if (_equippedSets == null) _equippedSets = new List<ClothingItemSet>();
		_equippedSets.Clear();

		LoadEquippedHat();
		LoadEquippedShoes();
	}

	private static void SaveEquippedItemSets() {
		foreach (ClothingItemSet itemSet in equippedSets) WhitPrefs.SetString(itemSet.type.ToString(), itemSet.itemName);
	}
}
