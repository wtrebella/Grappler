using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ClothingDataManager {
	// =========== HATS ============
	private static string packagePathHats = "Hats/";

	private static List<ClothingPackage> _hats;
	public static List<ClothingPackage> hats {
		get {
			if (_hats == null) LoadAllHats();
			return _hats;
		}
	}

	private static void LoadAllHats() {
		var hatsArray = Resources.LoadAll(packagePathRoot + packagePathHats, typeof(ClothingPackage)).Cast<ClothingPackage>().ToArray();
		_hats = new List<ClothingPackage>();
		foreach (ClothingPackage hat in hatsArray) _hats.Add(hat);
	}

	private static void LoadEquippedHat() {
		string equippedHatName = WhitPrefs.GetString(ClothingPackageType.Hat.ToString(), "");
		ClothingPackage equippedHat = GetHatItemSet(equippedHatName);
		if (equippedHat != null && !_equippedSets.Contains(equippedHat)) _equippedSets.Add(equippedHat);
	}

	private static ClothingPackage GetHatItemSet(string hatName) {
		foreach (ClothingPackage itemSet in hats) {
			if (itemSet.packageName == hatName) return itemSet;
		}
		Debug.Log("no hat found with name: " + hatName);
		return null;
	}



	// =========== SHOES ============
	private static string packagePathShoes = "Shoes/";

	private static List<ClothingPackage> _shoes;
	public static List<ClothingPackage> shoes {
		get {
			if (_shoes == null) LoadAllShoes();
			return _shoes;
		}
	}

	private static void LoadAllShoes() {
		var shoesArray = Resources.LoadAll(packagePathRoot + packagePathShoes, typeof(ClothingPackage)).Cast<ClothingPackage>().ToArray();
		_shoes = new List<ClothingPackage>();
		foreach (ClothingPackage shoe in shoesArray) _shoes.Add(shoe);
	}

	private static void LoadEquippedShoes() {
		string equippedShoesName = WhitPrefs.GetString(ClothingPackageType.Shoes.ToString(), "");
		ClothingPackage equippedShoes = GetShoesItemSet(equippedShoesName);
		if (equippedShoes != null && !_equippedSets.Contains(equippedShoes)) _equippedSets.Add(equippedShoes);
	}

	private static ClothingPackage GetShoesItemSet(string shoesName) {
		foreach (ClothingPackage itemSet in shoes) {
			if (itemSet.packageName == shoesName) return itemSet;
		}
		Debug.Log("no shoes found with name: " + shoesName);
		return null;
	}



	// =========== EVERYTHING ELSE ============
	private static string packagePathRoot = "CollectableItems/CollectableItemPackages/Clothing/";

	private static List<ClothingPackage> _equippedSets = new List<ClothingPackage>();
	public static List<ClothingPackage> equippedSets {
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

	public static bool ItemSetTypeIsEquipped(ClothingPackageType itemSetType) {
		foreach (ClothingPackage itemSet in equippedSets) {
			if (itemSet.type == itemSetType) return true;
		}
		return false;
	}

	public static ClothingPackage GetEquippedItemSet(ClothingPackageType itemSetType) {
		int indexOfEquippedType = IndexOfEquippedType(itemSetType);
		if (indexOfEquippedType < 0) return null;

		return equippedSets[indexOfEquippedType];
	}

	public static List<ClothingPackage> GetClothingItemSets(ClothingPackageType type) {
		if (type == ClothingPackageType.Hat) return hats;
		else if (type == ClothingPackageType.Shoes) return shoes;
		else {
			Debug.LogError("invalid clothing item set type: " + type.ToString());
			return null;
		}
	}

	public static void RemoveEquippedItemSet(ClothingPackageType itemSetType) {
		int index = IndexOfEquippedType(itemSetType);
		if (index < 0 || index >= equippedSets.Count) return;
		equippedSets.RemoveAt(index);
	}

	private static int IndexOfEquippedType(ClothingPackageType itemSetType) {
		for (int i = 0; i < equippedSets.Count; i++) {
			ClothingPackage itemSet = equippedSets[i];
			if (itemSet.type == itemSetType) return i;
		}
		return -1;
	}

	private static void LoadEquippedItemSets() {
		if (_equippedSets == null) _equippedSets = new List<ClothingPackage>();
		_equippedSets.Clear();

		LoadEquippedHat();
		LoadEquippedShoes();
	}

	private static void SaveEquippedItemSets() {
		foreach (ClothingPackage itemSet in equippedSets) WhitPrefs.SetString(itemSet.type.ToString(), itemSet.packageName);
	}
}
