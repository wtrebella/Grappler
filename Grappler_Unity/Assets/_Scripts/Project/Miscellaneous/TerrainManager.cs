using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	public TerrainSet[] terrainSets;
	public TerrainChunkConnector[] connectorPrefabs;

	private List<TerrainChunk> terrainChunks;
	private List<TerrainChunkConnector> connectors;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		connectors = new List<TerrainChunkConnector>();
	}
	
	private void Start() {
		for (int i = 0; i < 25; i++) CreateChunk();
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
		connectors.Add(connector);
		return connector;
	}

	private TerrainChunkConnector GetRandomConnectorPrefab() {
		return connectorPrefabs[UnityEngine.Random.Range(0, connectorPrefabs.Length)];
	}

	private TerrainChunk GetRandomChunkPrefab() {
		return terrainSets[0].GetRandomTerrainChunkPrefab();
	}
}