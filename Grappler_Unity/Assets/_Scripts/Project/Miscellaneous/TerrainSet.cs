using UnityEngine;
using System.Collections;

public class TerrainSet : MonoBehaviour {
	[SerializeField] private TerrainChunk[] terrainChunkPrefabs;

	public TerrainChunk GetRandomTerrainChunkPrefab() {
		return terrainChunkPrefabs[Random.Range(0, terrainChunkPrefabs.Length)];
	}
}
