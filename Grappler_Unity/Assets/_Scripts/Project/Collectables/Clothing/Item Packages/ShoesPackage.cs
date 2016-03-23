using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public abstract class ShoesPackage : ClothingPackage {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ShoesPackageAsset", false, 102)]
	public static void CreateCollectablePackageAsset() {
		ScriptableObjectUtility.CreateAsset<ShoesPackage>();
	}
	#endif

	public override ClothingPackageType type {get {return ClothingPackageType.Shoes;}}
}