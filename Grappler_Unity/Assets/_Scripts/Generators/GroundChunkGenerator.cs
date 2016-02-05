using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GroundChunkGenerator : Generator {
	public Action<GroundChunk> SignalGroundChunkGenerated;

	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private int maxChunks = 5;

	private void Awake() {
		base.BaseAwake();
		mountainChunkGenerator.SignalMountainChunkGenerated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		GenerateGroundChunk(mountainChunk);
	}

	private void GenerateGroundChunk(MountainChunk mountainChunk) {
		GroundChunk groundChunk = (GroundChunk)GenerateItem();
		GroundChunk previousGroundChunk;
		if (items.Count == 1) previousGroundChunk = null;
		else previousGroundChunk = items[items.Count - 2].To<GroundChunk>();
		groundChunk.Generate(mountainChunk, previousGroundChunk);
		if (SignalGroundChunkGenerated != null) SignalGroundChunkGenerated(groundChunk);
	}

	protected override void HandleItemGenerated(GeneratableItem item) {
		if (items.Count > maxChunks) RecycleFirstItem();
	}
}
