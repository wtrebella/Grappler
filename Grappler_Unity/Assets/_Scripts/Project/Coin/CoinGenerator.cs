using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CoinGenerator : ItemBetweenTerrainPairGenerator {
	[SerializeField] private FloatRange verticalSpawnRange = new FloatRange(0.2f, 0.8f);
	[SerializeField] private IntRange numPerChunkRange = new IntRange(3, 10);
	[SerializeField] private float probability = 0.5f;

	protected override void OnPatternAdded(FloatRange distRange) {
		if (UnityEngine.Random.value > probability) return;
		GenerateCoins(distRange);
	}

	private void GenerateCoins(FloatRange distRange) {
		int numPerChunk = numPerChunkRange.GetRandom();
		FloatRange horizontalSpawnRange = new FloatRange(0.05f, 0.95f);

		for (int i = 0; i < numPerChunk; i++) {
			float distPercent = horizontalSpawnRange.GetRandom();
			float dist = distRange.Lerp(distPercent);
			float verticalPercent = verticalSpawnRange.GetRandom();
			GenerateItem(dist, verticalPercent);
		}
	}
}
