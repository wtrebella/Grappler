using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GroundChunkGenerator : MonoBehaviour {
	[SerializeField] private GroundChunk groundChunkPrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private List<GroundChunk> groundChunks;

	private void Awake() {
		groundChunks = new List<GroundChunk>();
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		GenerateGroundChunk(mountainChunk);
	}

	private void GenerateGroundChunk(MountainChunk mountainChunk) {
		GroundChunk groundChunk = groundChunkPrefab.Spawn();
		groundChunk.transform.parent = transform;
		GroundChunk previousGroundChunk;
		if (groundChunks.Count == 0) previousGroundChunk = null;
		else previousGroundChunk = groundChunks.GetLastItem();
		groundChunks.Add(groundChunk);
		groundChunk.Generate(mountainChunk, previousGroundChunk);
	}
}
