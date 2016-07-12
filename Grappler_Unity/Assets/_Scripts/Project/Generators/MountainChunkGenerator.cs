using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkNeededDetector))]
[RequireComponent(typeof(AnchorableGenerator))]
public class MountainChunkGenerator : Generator {
	public Action<MountainChunk> SignalMountainChunkGenerated;

	[SerializeField] private MountainChunkAttributes normalAttributes;
	[SerializeField] private MountainChunkAttributes crazyAttributes;

//	private AnchorableGenerator anchorableGenerator;
	private MountainChunkNeededDetector neededDetector;

	public MountainChunk GetMountainChunkAtPlace(float place) {
		if (items.Count == 0) return null;
		int index = (int)place;
		if (index >= numItemsCreated) index = numItemsCreated - 1;
		return ItemToMountainChunk(items[index]);
	}

	public MountainChunk GetMountainChunkAtX(float x) {
		if (items.Count == 0) return null;
		foreach (GeneratableItem item in items) {
			MountainChunk chunk = ItemToMountainChunk(item);
			float lastX = chunk.GetLastEdgePoint().vector.x;
			if (x < lastX) return chunk;
		}

		return ItemToMountainChunk(items[items.Count - 1]);
	}

	public MountainChunk GetMountainChunk(int index) {
		if (items.Count == 0) return null;
		if (index < 0 || index > items.Count) Debug.LogError("index (" + index + ") out of range!");
		return ItemToMountainChunk(items[index]);
	}
	
	public int GetMountainChunkIndexAtY(float y) {
		if (items.Count == 0) return 0;
		if (y < GetMountainChunk(0).origin.y) return 0;

		for (int i = 0; i < items.Count - 1; i++) {
			MountainChunk chunkA = ItemToMountainChunk(items[i]);
			MountainChunk chunkB = ItemToMountainChunk(items[i+1]);
			if (y >= chunkA.origin.y && y <= chunkB.origin.y) return i;
		}
		Debug.LogError("couldn't find mountain chunk that includes y: " + y);
		return -1;
	}

	public int GetMountainChunkNumAtPlace(float lerpDist) {
		return (int)lerpDist;
	}

	private void Awake() {
		base.BaseAwake();
		neededDetector = GetComponent<MountainChunkNeededDetector>();
//		anchorableGenerator = GetComponent<AnchorableGenerator>();
	}

	private void Start() {
		GenerateMountainChunks(3);
	}

	float t = 0;
	bool hasUsed = false;
	private void FixedUpdate() {
		t += Time.fixedDeltaTime;
		GenerateMountainChunkIfNeeded();
	}

	private void GenerateMountainChunkIfNeeded() {
		if (neededDetector.NeedsNewMountainChunk(GetLastMountainChunk())) GenerateMountainChunk(); 
	}

	private MountainChunk GetLastMountainChunk() {
		if (items.Count == 0) return null;
		return ItemToMountainChunk(items[items.Count - 1]);
	}

	protected void GenerateMountainChunks(int num) {
		for (int i = 0; i < num; i++) GenerateMountainChunk();
	}

	protected void GenerateMountainChunk() {
		MountainChunk mountainChunk = (MountainChunk)GenerateItem();
		MountainChunk previousMountainChunk;
		Vector2 origin;
		if (items.Count <= 1) {
			previousMountainChunk = null;
			origin = Vector2.zero;
		}
		else {
			previousMountainChunk = ItemToMountainChunk(items[items.Count - 2]);
			origin = previousMountainChunk.GetLastEdgePoint().vector;
		}

		MountainChunkAttributes attributes = t < 10 || hasUsed ? normalAttributes : crazyAttributes;
		if (attributes == crazyAttributes) hasUsed = true;
		mountainChunk.Initialize(origin, previousMountainChunk, attributes);
//		anchorableGenerator.GenerateAnchorables(mountainChunk);

		if (SignalMountainChunkGenerated != null) SignalMountainChunkGenerated(mountainChunk);
	}

	protected MountainChunk ItemToMountainChunk(GeneratableItem item) {
		return item as MountainChunk;
	}
}