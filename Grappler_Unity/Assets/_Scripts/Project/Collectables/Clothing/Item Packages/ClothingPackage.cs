using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ClothingPackage : CollectablePackage {
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ClothingPackageAsset", false, 102)]
	public static void CreateCollectablePackageAsset() {
		ScriptableObjectUtility.CreateAsset<ClothingPackage>();
	}
	#endif

	public ClothingPackageType type = ClothingPackageType.None;
}