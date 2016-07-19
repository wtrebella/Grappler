using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	public TerrainChunk[] terrainChunkPrefabs;
	public Bridge bridgePrefab;

	private List<TerrainChunk> terrainChunks;
	private List<Bridge> bridges;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		bridges = new List<Bridge>();
	}
	
	private void Start() {
		for (int i = 0; i < 10; i++) CreateChunk();
	}
	
	private void Update() {
	
	}

	private void CreateChunk() {
		TerrainChunk terrainChunkPrefab = terrainChunkPrefabs[UnityEngine.Random.Range(0, terrainChunkPrefabs.Length)];
		TerrainChunk newTerrainChunk = Instantiate(terrainChunkPrefab);
		if (terrainChunks.Count == 0) newTerrainChunk.transform.position = Vector3.zero;
		else {
			Bridge bridge = CreateBridge();
			newTerrainChunk.transform.position = bridge.exitPoint.position;
		}
		terrainChunks.Add(newTerrainChunk);
	}

	private Bridge CreateBridge() {
		Bridge bridge = Instantiate(bridgePrefab);
		TerrainChunk terrainChunk = terrainChunks.GetLast();
		bridge.transform.position = terrainChunk.exitPoint.position;
		bridges.Add(bridge);
		return bridge;
	}
}
