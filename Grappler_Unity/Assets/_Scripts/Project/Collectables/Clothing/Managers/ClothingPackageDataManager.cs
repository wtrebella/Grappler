using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClothingPackageDataManager : CollectablePackageDataManager {
	private static string clothingPath {get {return rootPath + "Clothing/";}}
	private static string hatsPath {get {return clothingPath + "Hats/";}}
	private static string shoesPath {get {return clothingPath + "Shoes/";}}

	private static List<HatPackage> _hats;
	public static List<HatPackage> hats {
		get {
			if (_hats == null) _hats = LoadPackagesAtPath<HatPackage>(hatsPath);
			return _hats;
		}
	}
		
	private static List<ShoesPackage> _shoes;
	public static List<ShoesPackage> shoes {
		get {
			if (_shoes == null) _shoes = LoadPackagesAtPath<ShoesPackage>(shoesPath);
			return _shoes;
		}
	}

	private static HatPackage GetHatPackage(string packageName) {
		return GetPackage<HatPackage>(packageName);
	}

	private static ShoesPackage GetShoesPackage(string packageName) {
		return GetPackage<ShoesPackage>(packageName);
	}
}
