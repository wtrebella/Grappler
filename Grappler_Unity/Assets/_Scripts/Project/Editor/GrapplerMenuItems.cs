using UnityEngine;
using System.Collections;
using UnityEditor;
using WhitTerrain;

public class GrapplerMenuItems : Editor {
	[MenuItem("Assets/Create/MountainChunkAttributes Asset", false, 102)]
	public static void CreateMountainChunkAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<MountainChunkAttributes>();
	}

	[MenuItem("Assets/Create/WhitTerrainSectionAttributes Asset", false, 102)]
	public static void CreateWhitTerrainSectionAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<ContourSectionAttributes>();
	}

	[MenuItem("Assets/Create/WhitTerrainPairAttributes Asset", false, 102)]
	public static void CreateWhitTerrainPairAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<ContourPairAttributes>();
	}
}
