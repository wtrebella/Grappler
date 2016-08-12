using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Polydraw;

public class TerrainManager : MonoBehaviour {
	public ArrowSign signPrefab;
	public GameObject treePrefab;


	public TerrainSet[] terrainSets;
	public TerrainChunkConnector[] connectorPrefabs;

	[SerializeField] private CollisionSignaler exitTriggerSignaler;
	[SerializeField] private int maxChunks = 10;
	[SerializeField] private int numAheadChunks = 5;
	[SerializeField] private int setSize = 10;
	[SerializeField] private TerrainSet firstSet;

	private List<TerrainChunk> terrainChunks;
	private List<TerrainChunkConnector> connectors;
	private TerrainSet currentSet;
	private TerrainSet nextSet;
	private int numChunks = 0;

	private void Awake() {
		terrainChunks = new List<TerrainChunk>();
		connectors = new List<TerrainChunkConnector>();
		currentSet = firstSet;
		ChooseNextSet();
	}
	
	private void Start() {
		if (exitTriggerSignaler != null) exitTriggerSignaler.SignalCollision += OnExitTrigger;

		for (int i = 0; i < numAheadChunks; i++) AddChunk();
	}

	private void OnExitTrigger() {
		AddChunk();
		while (HasTooManyChunks()) RemoveFirstChunk();
	}

	private void AddChunk() {
		TerrainChunk chunk = CreateChunk();
		if (HasChunks()) ConnectToPreviousChunk(chunk);
		else PlaceChunkAtBeginning(chunk);
		terrainChunks.Add(chunk);
		numChunks++;
		if (IsOnLastChunk()) OnLastChunkInSetAdded(chunk);
		OnChunkAdded(chunk);
	}

	private void OnChunkAdded(TerrainChunk chunk) {
		if (chunk.hasFloor) {
			if (UnityEngine.Random.value < 0.1f) CreateForest(chunk);
		}
	}

	private void CreateForest(TerrainChunk chunk) {
		PolydrawObject floor = chunk.GetFirstFloor();
		List<Vector2> points = floor.GetWorldBorderPoints();
		foreach (Vector2 point in points) {
			GameObject tree = Instantiate(treePrefab);
			tree.transform.SetParent(floor.transform);
			tree.transform.position = point;
		}
	}

	private void OnLastChunkInSetAdded(TerrainChunk chunk) {
		if (chunk.hasFloor) CreateFloorSign(chunk);
		else if (chunk.hasCeiling) CreateCeilingSign(chunk);
		IncrementSet();
	}

	private TerrainChunk CreateChunk() {
		TerrainChunk prefab = GetRandomChunkPrefab();
		TerrainChunk chunk = Instantiate(prefab);
		chunk.transform.SetParent(transform);
		return chunk;
	}

	private bool HasTooManyChunks() {
		return terrainChunks.Count > maxChunks;
	}

	private bool IsOnFirstChunk() {
		return numChunks % setSize == 1;
	}

	private bool IsOnLastChunk() {
		return numChunks % setSize == 0;
	}

	private bool HasChunks() {
		return terrainChunks.Count > 0;
	}

	private void PlaceChunkAtBeginning(TerrainChunk chunk) {
		chunk.transform.localPosition = Vector3.zero;
	}

	private void ConnectToPreviousChunk(TerrainChunk chunk) {
		TerrainChunkConnector bridge = CreateConnector();
		chunk.transform.position = bridge.exitPoint.position;
	}

	private void CreateFloorSign(TerrainChunk chunk) {
		PolydrawObject floor = chunk.GetFirstFloor();
		List<Vector2> points = floor.GetWorldBorderPoints();
		Vector2 lastPoint = points.GetLast();
		ArrowSign sign = Instantiate(signPrefab);
		sign.transform.SetParent(floor.transform);
		sign.transform.position = lastPoint;
		sign.SetAsFloorSign();
		sign.SetArrowDirection(nextSet.terrainSetType);
	}

	private void CreateCeilingSign(TerrainChunk chunk) {
		PolydrawObject ceiling = chunk.GetFirstCeiling();
		List<Vector2> points = ceiling.GetWorldBorderPoints();
		Vector2 lastPoint = points.GetLast();
		ArrowSign sign = Instantiate(signPrefab);
		sign.transform.SetParent(ceiling.transform);
		sign.transform.position = lastPoint;
		sign.SetAsCeilingSign();
		sign.SetArrowDirection(nextSet.terrainSetType);
	}

	private void RemoveFirstChunk() {
		if (terrainChunks.Count == 0) return;
		TerrainChunk firstChunk = terrainChunks[0];
		terrainChunks.RemoveAt(0);
		Destroy(firstChunk.gameObject);
	}

	private TerrainChunkConnector CreateConnector() {
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

	private void IncrementSet() {
		currentSet = nextSet;
		ChooseNextSet();
	}

	private void ChooseNextSet() {
		TerrainSet newSet;
		do {
			newSet = GetRandomSet();
		} while (newSet == currentSet);

		nextSet = newSet;
	}
}