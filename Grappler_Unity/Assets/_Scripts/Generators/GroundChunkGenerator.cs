using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GroundChunkGenerator : Generator {
	public Action<GroundChunk> SignalGroundChunkGenerated;

	[SerializeField] private float rangeReductionMultiplier = 0.01f;
	[SerializeField] private FloatRange distanceFromMountainRange = new FloatRange(16, 20);
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		base.BaseAwake();
		mountainChunkGenerator.SignalMountainChunkGenerated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		GenerateGroundChunk(mountainChunk);
	}

	private float num = 0;
	private float GetNextPerlinNoise() {
		float perlin = Mathf.PerlinNoise(num, 0);
		num += 0.1f;
		return perlin;
	}

	private void GenerateGroundChunk(MountainChunk mountainChunk) {
		GroundChunk groundChunk = (GroundChunk)GenerateItem();
		groundChunk.mountainChunk = mountainChunk;
		GroundChunk previousGroundChunk;
		if (items.Count == 1) previousGroundChunk = null;
		else previousGroundChunk = items[items.Count - 2].To<GroundChunk>();

		float distanceFromMountain = GetNextPerlinNoise() * (distanceFromMountainRange.max - distanceFromMountainRange.min) + distanceFromMountainRange.min;
		groundChunk.Generate(mountainChunk, previousGroundChunk, distanceFromMountain);

		distanceFromMountainRange.min = Mathf.Max(0, distanceFromMountainRange.min - rangeReductionMultiplier);

		if (SignalGroundChunkGenerated != null) SignalGroundChunkGenerated(groundChunk);
	}
}
