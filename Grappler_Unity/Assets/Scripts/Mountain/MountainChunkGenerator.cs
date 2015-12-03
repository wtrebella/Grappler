using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MountainChunkGenerator : MonoBehaviour {
	public Action SignalMountainChunkGenerated;
	
	[SerializeField] private MountainChunk mountainChunkPrefab;

	private List<MountainChunk> mountainChunks;

	private void Awake() {
		mountainChunks = new List<MountainChunk>();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) GenerateMountainChunk();
	}

	private void GenerateMountainChunk() {
		MountainChunk mountainChunk = Instantiate(mountainChunkPrefab);

		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero);
		else {
			MountainChunk prevMountainChunk = mountainChunks.GetLastItem();
			mountainChunk.Generate(prevMountainChunk.GetTopRightPoint());
		}

		mountainChunks.Add(mountainChunk);
	}
}
