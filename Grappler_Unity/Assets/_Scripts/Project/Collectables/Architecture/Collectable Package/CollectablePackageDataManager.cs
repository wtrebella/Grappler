using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class CollectablePackageDataManager {
//	// =========== HATS ============
//	private static string packagePathHats = "Hats/";
//
//	private static List<ClothingItemSet> _hats;
//	public static List<ClothingItemSet> hats {
//		get {
//			if (_hats == null) LoadAllHats();
//			return _hats;
//		}
//	}
//
//	private static void LoadAllHats() {
//		var hatsArray = Resources.LoadAll(packagePathRoot + packagePathHats, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
//		_hats = new List<ClothingItemSet>();
//		foreach (ClothingItemSet hat in hatsArray) _hats.Add(hat);
//	}
//
//	private static void LoadEquippedHat() {
//		string equippedHatName = WhitPrefs.GetString(ClothingItemSetType.Hat.ToString(), "");
//		ClothingItemSet equippedHat = GetHatItemSet(equippedHatName);
//		if (equippedHat != null && !_equippedPackages.Contains(equippedHat)) _equippedPackages.Add(equippedHat);
//	}
//
//	private static ClothingItemSet GetHatItemSet(string hatName) {
//		foreach (ClothingItemSet itemSet in hats) {
//			if (itemSet.itemName == hatName) return itemSet;
//		}
//		Debug.Log("no hat found with name: " + hatName);
//		return null;
//	}
//
//
//
//	// =========== SHOES ============
//	private static string packagePathShoes = "Shoes/";
//
//	private static List<ClothingItemSet> _shoes;
//	public static List<ClothingItemSet> shoes {
//		get {
//			if (_shoes == null) LoadAllShoes();
//			return _shoes;
//		}
//	}
//
//	private static void LoadAllShoes() {
//		var shoesArray = Resources.LoadAll(packagePathRoot + packagePathShoes, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
//		_shoes = new List<ClothingItemSet>();
//		foreach (ClothingItemSet shoe in shoesArray) _shoes.Add(shoe);
//	}
//
//	private static void LoadEquippedShoes() {
//		string equippedShoesName = WhitPrefs.GetString(ClothingItemSetType.Shoes.ToString(), "");
//		ClothingItemSet equippedShoes = GetShoesItemSet(equippedShoesName);
//		if (equippedShoes != null && !_equippedPackages.Contains(equippedShoes)) _equippedPackages.Add(equippedShoes);
//	}
//
//	private static ClothingItemSet GetShoesItemSet(string shoesName) {
//		foreach (ClothingItemSet itemSet in shoes) {
//			if (itemSet.itemName == shoesName) return itemSet;
//		}
//		Debug.Log("no shoes found with name: " + shoesName);
//		return null;
//	}



	// =========== EVERYTHING ELSE ============
	private static string packagePathRoot = "CollectableItems/CollectableItemPackages/Clothing/";

	private static List<CollectablePackage> _equippedPackages = new List<CollectablePackage>();
	public static List<CollectablePackage> equippedPackages {
		get {
			if (_equippedPackages == null) LoadEquippedPackages();
			return _equippedPackages;
		}
	}

	public static void OnEnable() {
		LoadEquippedPackages();
	}

	public static void OnDisable() {
		SaveEquippedPackages();
	}

	public static bool PackageTypeIsEquipped(CollectablePackageType packageType) {
		foreach (CollectablePackage package in equippedPackages) {
			if (package.type == packageType) return true;
		}
		return false;
	}

	public static CollectablePackage GetEquippedPackage(CollectablePackageType packageType) {
		int indexOfEquippedType = IndexOfEquippedType(packageType);
		if (indexOfEquippedType < 0) return null;

		return equippedPackages[indexOfEquippedType];
	}

	public static List<CollectablePackage> GetPackages(CollectablePackageType packageType) {
//		if (type == ClothingItemSetType.Hat) return hats;
//		else if (type == ClothingItemSetType.Shoes) return shoes;
//		else {
//			Debug.LogError("invalid clothing item set type: " + type.ToString());
//			return null;
//		}
	}

	public static void RemoveEquippedPackage(CollectablePackageType packageType) {
		int index = IndexOfEquippedType(packageType);
		if (index < 0 || index >= equippedPackages.Count) return;
		equippedPackages.RemoveAt(index);
	}

	private static int IndexOfEquippedType(CollectablePackageType packageType) {
		for (int i = 0; i < equippedPackages.Count; i++) {
			CollectablePackage itemSet = equippedPackages[i];
			if (itemSet.type == packageType) return i;
		}
		return -1;
	}

	private static void LoadEquippedPackages() {
		if (_equippedPackages == null) _equippedPackages = new List<CollectablePackage>();
		_equippedPackages.Clear();

//		LoadEquippedHat();
//		LoadEquippedShoes();
	}

	private static void SaveEquippedPackages() {
		foreach (CollectablePackage itemSet in equippedPackages) WhitPrefs.SetString(itemSet.type.ToString(), itemSet.itemName);
	}
}
