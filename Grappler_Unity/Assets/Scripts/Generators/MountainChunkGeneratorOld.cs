using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkNeededDetector))]
[RequireComponent(typeof(AnchorableGenerator))]
public class MountainChunkGeneratorOld : MonoBehaviour {
	public Action<MountainChunk> SignalMountainChunkCreated;

	public int numMountainChunksCreated {get; private set;}
	
	[SerializeField] private MountainChunk mountainChunkPrefab;
	[SerializeField] private int maxChunks = 5;

	private AnchorableGenerator anchorableGenerator;
	private MountainChunkNeededDetector neededDetector;
	private List<MountainChunk> mountainChunks;

	public MountainChunk GetMountainChunkAtPlace(float place) {
		int index = (int)place;
		if (index >= numMountainChunksCreated) index = numMountainChunksCreated - 1;
		return mountainChunks[index];
	}

	public MountainChunk GetMountainChunkAtX(float x) {
		foreach (MountainChunk chunk in mountainChunks) {
			float lastX = chunk.GetLastLinePoint().pointVector.x;
			if (x < lastX) return chunk;
		}

		return mountainChunks[mountainChunks.Count - 1];
	}

	public MountainChunk GetMountainChunk(int index) {
		if (index < 0 || index > mountainChunks.Count) Debug.LogError("index (" + index + ") out of range!");
		return mountainChunks[index];
	}
	
	public int GetMountainChunkIndexAtY(float y) {
		if (y < GetMountainChunk(0).origin.y) return 0;

		for (int i = 0; i < mountainChunks.Count - 1; i++) {
			MountainChunk chunkA = mountainChunks[i];
			MountainChunk chunkB = mountainChunks[i+1];
			if (y >= chunkA.origin.y && y <= chunkB.origin.y) return i;
		}
		Debug.LogError("couldn't find mountain chunk that includes y: " + y);
		return -1;
	}

	public int GetMountainChunkNumAtPlace(float lerpDist) {
		return (int)lerpDist;
	}

	private void Awake() {
		numMountainChunksCreated = 0;
		neededDetector = GetComponent<MountainChunkNeededDetector>();
		anchorableGenerator = GetComponent<AnchorableGenerator>();
		mountainChunks = new List<MountainChunk>();
	}

	private void RecycleFirstChunk() {
		MountainChunk firstChunk = mountainChunks[0];
		mountainChunks.Remove(firstChunk);
		firstChunk.Recycle();
	}

	private void Start() {
		GenerateMountainChunks(3);
	}

	private void GenerateMountainChunks(int numToGenerate) {
		for (int i = 0; i < numToGenerate; i++) GenerateMountainChunk();
	}

	private void GenerateMountainChunk() {
		numMountainChunksCreated++;

		MountainChunk mountainChunk = mountainChunkPrefab.Spawn();
		mountainChunk.transform.parent = transform;

		if (mountainChunks.Count == 0) mountainChunk.Generate(Vector2.zero, null);
		else {
			MountainChunk lastChunk = mountainChunks.GetLastItem();
			mountainChunk.Generate(lastChunk.GetLastLinePoint().pointVector, lastChunk);
		}

		mountainChunks.Add(mountainChunk);

		anchorableGenerator.GenerateAnchorables(mountainChunk);

		if (SignalMountainChunkCreated != null) SignalMountainChunkCreated(mountainChunk);
	}

	private void FixedUpdate() {
		GenerateMountainChunkIfNeeded();
	}

	private void GenerateMountainChunkIfNeeded() {
		if (mountainChunks.Count == 0) return;
		if (neededDetector.NeedsNewMountainChunk(GetLastMountainChunk())) GenerateMountainChunk();
		if (mountainChunks.Count > maxChunks) RecycleFirstChunk();
	}

	private MountainChunk GetLastMountainChunk() {
		return mountainChunks[mountainChunks.Count - 1];
	}
}
