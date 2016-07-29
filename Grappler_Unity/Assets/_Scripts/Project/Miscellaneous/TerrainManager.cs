using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {
	public TerrainSet[] terrainSets;
	public TerrainChunkConnector[] connectorPrefabs;

	[SerializeField] private CollisionSignaler exitTriggerSignaler;
	[SerializeField] private int maxChunks = 10;
	[SerializeField] private int setSize = 10;
	[SerializeField] private TerrainSet firstSet;

	private List<TerrainChunk> terrainChunks;
	private List<TerrainChunkConnector> connectors;
	private TerrainSet currentSet;
	private int numChunks = 0;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		connectors = new List<TerrainChunkConnector>();
		currentSet = firstSet;
	}
	
	private void Start() {
		if (exitTriggerSignaler != null) exitTriggerSignaler.SignalCollision += OnExitTrigger;

		for (int i = 0; i < 5; i++) CreateChunk();
	}

	private void OnExitTrigger() {
		CreateChunk();
		while (terrainChunks.Count > maxChunks) RemoveFirstChunk();
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
		numChunks++;
		if (numChunks % setSize == 0) RechooseSet();
	}

	private void RemoveFirstChunk() {
		if (terrainChunks.Count == 0) return;
		TerrainChunk firstChunk = terrainChunks[0];
		terrainChunks.RemoveAt(0);
		Destroy(firstChunk.gameObject);
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
		return currentSet.GetRandomTerrainChunkPrefab();
	}

	private void RechooseSet() {
		TerrainSet newSet;
		do {
			newSet = GetRandomSet();
		} while (newSet == currentSet);

		currentSet = newSet;
	}
}