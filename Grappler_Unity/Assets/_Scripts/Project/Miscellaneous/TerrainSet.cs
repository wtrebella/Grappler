using UnityEngine;
using System.Collections;

public class TerrainSet : MonoBehaviour {
	public TerrainSetType terrainSetType = TerrainSetType.NONE;

	[SerializeField] private TerrainChunk[] terrainChunkPrefabs;

	public TerrainChunk GetRandomTerrainChunkPrefab() {
		return terrainChunkPrefabs[Random.Range(0, terrainChunkPrefabs.Length)];
	}
}
