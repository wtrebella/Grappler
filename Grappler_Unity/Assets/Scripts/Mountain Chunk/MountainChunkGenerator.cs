using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkVectorLine))]
[RequireComponent(typeof(MountainChunkNeededDetector))]
public class MountainChunkGenerator : MonoBehaviour {
	public int numMountainChunksCreated {get; private set;}

	public Action<MountainChunk> SignalMountainChunkCreated;

	[SerializeField] private MountainChunk mountainChunkPrefab;
	[SerializeField] private MountainChunkNeededDetector neededDetector;

	private MountainChunkVectorLine mountainChunkVectorLine;
	private List<MountainChunk> mountainChunks;

	private void Awake() {
		numMountainChunksCreated = 0;
		neededDetector = GetComponent<MountainChunkNeededDetector>();
		mountainChunkVectorLine = GetComponent<MountainChunkVectorLine>();
		mountainChunks = new List<MountainChunk>();
	}

	private void Start() {
		GenerateMountainChunks(3);
	}

	private void GenerateMountainChunks(int numToGenerate) {
		for (int i = 0; i < numToGenerate; i++) GenerateMountainChunk();
	}

	private void GenerateMountainChunk() {
		numMountainChunksCreated++;

		MountainChunk mountainChunk = Instantiate(mountainChunkPrefab);

		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero);
		else mountainChunk.Generate(mountainChunks.GetLastItem().GetLastLinePoint());

		mountainChunks.Add(mountainChunk);
		mountainChunkVectorLine.AddToLine(mountainChunk);

		if (SignalMountainChunkCreated != null) SignalMountainChunkCreated(mountainChunk);
	}

	private void FixedUpdate() {
		GenerateMountainChunkIfNeeded();
	}

	private void GenerateMountainChunkIfNeeded() {
		if (mountainChunks.Count == 0) return;
		if (neededDetector.NeedsNewMountainChunk(GetLastMountainChunk())) GenerateMountainChunk();
	}

	private MountainChunk GetLastMountainChunk() {
		return mountainChunks[mountainChunks.Count - 1];
	}
}
