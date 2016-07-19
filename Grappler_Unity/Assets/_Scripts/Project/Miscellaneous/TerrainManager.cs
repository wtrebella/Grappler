using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	public TerrainChunk[] terrainChunkPrefabs;
	public TerrainChunkConnector[] connectorPrefabs;

	private List<TerrainChunk> terrainChunks;
	private List<TerrainChunkConnector> bridges;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		bridges = new List<TerrainChunkConnector>();
	}
	
	private void Start() {
		for (int i = 0; i < 10; i++) CreateChunk();
	}
	
	private void Update() {
	
	}

	private void CreateChunk() {
		TerrainChunk prefab = GetRandomChunkPrefab();
		TerrainChunk chunk = Instantiate(prefab);
		chunk.transform.SetParent(transform);
		if (terrainChunks.Count == 0) chunk.transform.localPosition = Vector3.zero;
		else {
			TerrainChunkConnector bridge = CreateBridge();
			chunk.transform.position = bridge.exitPoint.position;
		}
		terrainChunks.Add(chunk);
	}

	private TerrainChunkConnector CreateBridge() {
		TerrainChunkConnector prefab = GetRandomConnectorPrefab();
		TerrainChunkConnector connector = Instantiate(prefab);
		TerrainChunk previousChunk = terrainChunks.GetLast();
		connector.transform.SetParent(previousChunk.transform);
		connector.transform.position = previousChunk.exitPoint.position;
		bridges.Add(connector);
		return connector;
	}

	private TerrainChunkConnector GetRandomConnectorPrefab() {
		return connectorPrefabs[UnityEngine.Random.Range(0, connectorPrefabs.Length)];
	}

	private TerrainChunk GetRandomChunkPrefab() {
		return terrainChunkPrefabs[UnityEngine.Random.Range(0, terrainChunkPrefabs.Length)];
	}
}
