using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public abstract class HatPackage : ClothingPackage {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/HatPackageAsset", false, 102)]
	public static void CreateCollectablePackageAsset() {
		ScriptableObjectUtility.CreateAsset<HatPackage>();
	}
	#endif

	public override ClothingPackageType type {get {return ClothingPackageType.Hat;}}
}