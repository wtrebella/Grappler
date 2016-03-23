using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClothingPackageDataManager : CollectablePackageDataManager {
	private static string clothingPath {get {return rootPath + "Clothing/";}}
	private static string hatsPath {get {return clothingPath + "Hats/";}}
	private static string shoesPath {get {return clothingPath + "Shoes/";}}


	private static List<ClothingPackage> _hats;
	public static List<ClothingPackage> hats {
		get {
			if (_hats == null) _hats = LoadPackagesAtPath<ClothingPackage>(hatsPath);
			return _hats;
		}
	}
		
	private static List<ClothingPackage> _shoes;
	public static List<ClothingPackage> shoes {
		get {
			if (_shoes == null) _shoes = LoadPackagesAtPath<ClothingPackage>(shoesPath);
			return _shoes;
		}
	}

	private static ClothingPackage GetHatPackage(string packageName) {
		return GetPackageFromList<ClothingPackage>(hats, packageName);
	}

	private static ClothingPackage GetShoesPackage(string packageName) {
		return GetPackageFromList<ClothingPackage>(shoes, packageName);
	}
}
