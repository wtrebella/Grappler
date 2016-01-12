﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkNeededDetector))]
[RequireComponent(typeof(MountainChunkNeededDetector))]
public class MountainChunkGenerator : MonoBehaviour {
	public int numMountainChunksCreated {get; private set;}

	public Action<MountainChunk> SignalMountainChunkCreated;

	[SerializeField] private MountainChunk mountainChunkPrefab;

	private MountainChunkNeededDetector neededDetector;
	private List<MountainChunk> mountainChunks;

	public MountainChunk GetMountainChunkAtDist(float lerpDist) {
		int index = (int)lerpDist;
		if (index >= numMountainChunksCreated) index = numMountainChunksCreated - 1;
		return mountainChunks[index];
	}

	public int GetMountainChunkNumAtDist(float lerpDist) {
		return (int)lerpDist;
	}

	private void Awake() {
		numMountainChunksCreated = 0;
		neededDetector = GetComponent<MountainChunkNeededDetector>();
		neededDetector = GetComponent<MountainChunkNeededDetector>();
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
		mountainChunk.transform.parent = transform;

		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero);
		else mountainChunk.Generate(mountainChunks.GetLastItem().GetLastLinePoint().pointVector);

		mountainChunks.Add(mountainChunk);

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
