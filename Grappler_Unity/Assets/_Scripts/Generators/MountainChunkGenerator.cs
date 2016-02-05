using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MountainChunkNeededDetector))]
[RequireComponent(typeof(AnchorableGenerator))]
public class MountainChunkGenerator : Generator {
	public Action<MountainChunk> SignalMountainChunkGenerated;

	[SerializeField] private int maxChunks = 5;

	private AnchorableGenerator anchorableGenerator;
	private MountainChunkNeededDetector neededDetector;

	public MountainChunk GetMountainChunkAtPlace(float place) {
		int index = (int)place;
		if (index >= numItemsCreated) index = numItemsCreated - 1;
		return ItemToMountainChunk(items[index]);
	}

	public MountainChunk GetMountainChunkAtX(float x) {
		foreach (GeneratableItem item in items) {
			MountainChunk chunk = ItemToMountainChunk(item);
			float lastX = chunk.GetLastLinePoint().pointVector.x;
			if (x < lastX) return chunk;
		}

		return ItemToMountainChunk(items[items.Count - 1]);
	}

	public MountainChunk GetMountainChunk(int index) {
		if (index < 0 || index > items.Count) Debug.LogError("index (" + index + ") out of range!");
		return ItemToMountainChunk(items[index]);
	}
	
	public int GetMountainChunkIndexAtY(float y) {
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
		anchorableGenerator = GetComponent<AnchorableGenerator>();
	}

	private void Start() {
		GenerateItems(3);
	}

	private void FixedUpdate() {
		GenerateMountainChunkIfNeeded();
	}

	private void GenerateMountainChunkIfNeeded() {
		if (items.Count == 0) return;
		if (neededDetector.NeedsNewMountainChunk(GetLastMountainChunk())) GenerateItem();
		if (items.Count > maxChunks) RecycleFirstItem();
	}

	private MountainChunk GetLastMountainChunk() {
		return ItemToMountainChunk(items[items.Count - 1]);
	}

	protected override void HandleItemGenerated(GeneratableItem item) {
		MountainChunk mountainChunk = ItemToMountainChunk(item);
		MountainChunk previousMountainChunk;
		Vector2 origin;
		if (items.Count == 0) {
			previousMountainChunk = null;
			origin = Vector2.zero;
		}
		else {
			previousMountainChunk = ItemToMountainChunk(items.GetLastItem());
			origin = previousMountainChunk.GetLastLinePoint().pointVector;
		}

		mountainChunk.Generate(origin, previousMountainChunk);
		anchorableGenerator.GenerateAnchorables(mountainChunk);

		if (SignalMountainChunkGenerated != null) SignalMountainChunkGenerated(mountainChunk);
	}

	protected MountainChunk ItemToMountainChunk(GeneratableItem item) {
		return item as MountainChunk;
	}
}
