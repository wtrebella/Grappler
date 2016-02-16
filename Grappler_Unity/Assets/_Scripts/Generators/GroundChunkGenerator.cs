using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GroundChunkGenerator : Generator {
	public Action<GroundChunk> SignalGroundChunkGenerated;

	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		base.BaseAwake();
		mountainChunkGenerator.SignalMountainChunkGenerated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		GenerateGroundChunk(mountainChunk);
	}

	private void GenerateGroundChunk(MountainChunk mountainChunk) {
		GroundChunk groundChunk = (GroundChunk)GenerateItem();
		groundChunk.mountainChunk = mountainChunk;
		GroundChunk previousGroundChunk;
		if (items.Count == 1) previousGroundChunk = null;
		else previousGroundChunk = items[items.Count - 2].To<GroundChunk>();
		groundChunk.Generate(mountainChunk, previousGroundChunk);
		if (SignalGroundChunkGenerated != null) SignalGroundChunkGenerated(groundChunk);
	}
}
