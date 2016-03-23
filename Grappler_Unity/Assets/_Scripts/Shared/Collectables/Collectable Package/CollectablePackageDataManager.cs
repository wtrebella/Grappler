using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollectablePackageDataManager {
	protected static string rootPath {get {return "CollectableItems/CollectableItemPackages/";}}
//	private static string clothingPath {get {return rootPath + "Clothing/";}}
//	private static string hatsPath {get {return clothingPath + "Hats/";}}
//	private static string shoesPath {get {return clothingPath + "Shoes/";}}

	private static List<CollectablePackage> _allPackages = new List<CollectablePackage>();
	public static List<CollectablePackage> allPackages {
		get {
			if (_allPackages == null) _allPackages = LoadAllPackages();
			return _allPackages;
		}
	}

	protected static List<CollectablePackage> LoadAllPackages() {
		return LoadPackagesAtPath<CollectablePackage>(rootPath);
	}

	protected static List<T> LoadPackagesAtPath<T>(string path) {
		return Resources.LoadAll(path, typeof(T)).Cast<T>().ToArray().ToList();
	}

	protected static T GetPackageFromList<T>(List<T> list, string packageName) where T : CollectablePackage {
		foreach (T package in list) {
			if (package.packageName == packageName) return package;
		}
		Debug.Log("no package found with name: " + packageName);
		return null;
	}




















	private static List<CollectablePackage> _equippedPackages = new List<CollectablePackage>();
	public static List<CollectablePackage> equippedPackages {
		get {
			if (_equippedPackages == null) LoadEquippedPackages();
			return _equippedPackages;
		}
	}





	public void OnEnable() {
		LoadEquippedPackages();
	}

	public void OnDisable() {
		SaveEquippedPackages();
	}

	public bool PackageTypeIsEquipped(CollectablePackageType packageType) {
		foreach (CollectablePackage package in equippedPackages) {
			if (package.type == packageType) return true;
		}
		return false;
	}

	public CollectablePackage GetEquippedPackage(CollectablePackageType packageType) {
		int indexOfEquippedType = IndexOfEquippedType(packageType);
		if (indexOfEquippedType < 0) return null;

		return equippedPackages[indexOfEquippedType];
	}

//	public List<CollectablePackage> GetPackages(CollectablePackageType packageType) {
//		if (type == ClothingItemSetType.Hat) return hats;
//		else if (type == ClothingItemSetType.Shoes) return shoes;
//		else {
//			Debug.LogError("invalid clothing item set type: " + type.ToString());
//			return null;
//		}
//	}

	public void RemoveEquippedPackage(CollectablePackageType packageType) {
		int index = IndexOfEquippedType(packageType);
		if (index < 0 || index >= equippedPackages.Count) return;
		equippedPackages.RemoveAt(index);
	}

	private int IndexOfEquippedType(CollectablePackageType packageType) {
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

	private void SaveEquippedPackages() {
		foreach (CollectablePackage package in equippedPackages) WhitPrefs.SetString(package.type.ToString(), package.packageName);
	}
}
