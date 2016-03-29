using UnityEngine;
using System.Collections;
using UnityEditor;

public class GrapplerMenuItems : Editor {
	[MenuItem("Assets/Create/Mountain Chunk Attributes Asset", false, 102)]
	public static void CreateMountainChunkAttributesAsset() {
		ScriptableObjectUtility.CreateAsset<CollectionItem>();
	}
}
