using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollectablePackageDataManager {
	protected static string rootPath {get {return "CollectableItems/CollectableItemPackages/";}}

	private static List<CollectablePackage> _allPackages = new List<CollectablePackage>();
	public static List<CollectablePackage> allPackages {
		get {
			if (_allPackages == null) _allPackages = LoadAllPackages();
			return _allPackages;
		}
	}

	private static List<CollectablePackage> _equippedPackages = new List<CollectablePackage>();
	public static List<CollectablePackage> equippedPackages {
		get {
			if (_equippedPackages == null) _equippedPackages = LoadEquippedPackages();
			return _equippedPackages;
		}
	}

	protected static List<CollectablePackage> LoadAllPackages() {
		return LoadPackagesAtPath<CollectablePackage>(rootPath);
	}

	protected static List<T> LoadPackagesAtPath<T>(string path) {
		return Resources.LoadAll(path, typeof(T)).Cast<T>().ToArray().ToList();
	}

	public static T GetEquippedPackage<T>() where T : CollectablePackage {
		foreach (T package in equippedPackages) {
			if (package.GetType() != typeof(T)) continue;
			else return package;
		}
		return null;
	}

	public static List<T> GetEquippedPackages<T>() where T : CollectablePackage {
		List<T> list = new List<T>();
		foreach (T package in equippedPackages) {
			if (package.GetType() != typeof(T)) continue;
			else list.Add(package);
		}
		return list;
	}

	public static T GetPackage<T>() where T : CollectablePackage {
		foreach (T package in allPackages) {
			if (package.GetType() != typeof(T)) continue;
			else return package;
		}
		return null;
	}

	public static List<T> GetPackages<T>() where T : CollectablePackage {
		List<T> list = new List<T>();
		foreach (T package in allPackages) {
			if (package.GetType() != typeof(T)) continue;
			else list.Add(package);
		}
		return list;
	}

	protected static List<CollectablePackage> LoadEquippedPackages() {
		List<CollectablePackage> list = new List<CollectablePackage>();
		foreach (CollectablePackage package in allPackages) {
			if (package.GetType() != typeof(CollectablePackage)) continue;
			if (package.isEquipped) list.Add(package);
		}
		return list;
	}

	protected static T GetPackage<T>(string packageName) where T : CollectablePackage {
		foreach (T package in allPackages) {
			if (package.packageName == packageName) return package;
		}
		Debug.Log("no package found with name: " + packageName);
		return null;
	}

//	public bool PackageTypeIsEquipped(CollectablePackageType packageType) {
//		foreach (CollectablePackage package in equippedPackages) {
//			if (package.type == packageType) return true;
//		}
//		return false;
//	}
//
//	public CollectablePackage GetEquippedPackage(CollectablePackageType packageType) {
//		int indexOfEquippedType = IndexOfEquippedType(packageType);
//		if (indexOfEquippedType < 0) return null;
//
//		return equippedPackages[indexOfEquippedType];
//	}
//
//	public void RemoveEquippedPackage(CollectablePackageType packageType) {
//		int index = IndexOfEquippedType(packageType);
//		if (index < 0 || index >= equippedPackages.Count) return;
//		equippedPackages.RemoveAt(index);
//	}
//
//	private int IndexOfEquippedType(CollectablePackageType packageType) {
//		for (int i = 0; i < equippedPackages.Count; i++) {
//			CollectablePackage itemSet = equippedPackages[i];
//			if (itemSet.type == packageType) return i;
//		}
//		return -1;
//	}
}
