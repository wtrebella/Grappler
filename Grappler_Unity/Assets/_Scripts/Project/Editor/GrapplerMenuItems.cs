using UnityEngine;
using System.Collections;
using UnityEditor;

public class GrapplerMenuItems : Editor {
	[MenuItem("Assets/Create/MountainChunkAttributes Asset", false, 102)]
	public static void CreateMountainChunkAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<MountainChunkAttributes>();
	}

	[MenuItem("Assets/Create/TerrainLineSectionAttributes Asset", false, 102)]
	public static void CreateTerrainLineSectionAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<WhitTerrainLineSectionAttributes>();
	}

	[MenuItem("Assets/Create/TerrainLineAttributes Asset", false, 102)]
	public static void CreateTerrainLineAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<WhitTerrainLineAttributes>();
	}
}
