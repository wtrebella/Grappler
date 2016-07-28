using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	public TerrainSet[] terrainSets;
	public TerrainChunkConnector[] connectorPrefabs;

	[SerializeField] private CollisionSignaler exitTriggerSignaler;
	[SerializeField] private int numChunks = 10;

	private List<TerrainChunk> terrainChunks;
	private List<TerrainChunkConnector> connectors;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		connectors = new List<TerrainChunkConnector>();
		exitTriggerSignaler.SignalCollision += OnExitTrigger;
	}
	
	private void Start() {
		for (int i = 0; i < 5; i++) CreateChunk();
	}

	private void OnExitTrigger() {
		CreateChunk();
		while (terrainChunks.Count > numChunks) RemoveFirstChunk();
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

	private void RemoveFirstChunk() {
		if (terrainChunks.Count == 0) return;
		TerrainChunk firstChunk = terrainChunks[0];
		terrainChunks.RemoveAt(0);
		Destroy(firstChunk);
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

	private TerrainSet GetRandomSet() {
		return terrainSets[UnityEngine.Random.Range(0, terrainSets.Length)];
	}

	private TerrainChunk GetRandomChunkPrefab() {
		return GetRandomSet().GetRandomTerrainChunkPrefab();
	}
}