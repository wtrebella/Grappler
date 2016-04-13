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
		CoinPattern coinPattern = CoinPatternGenerator.testPattern;
		GenerateCoinPattern(coinPattern, distRange);
	}

	private void GenerateCoinPattern(CoinPattern coinPattern, FloatRange distRange) {
		for (int i = 0; i < coinPattern.coinPlacements.Count; i++) {
			CoinPlacement coinPlacement = coinPattern.coinPlacements[i];
			float dist = distRange.Lerp(coinPlacement.distPercent);
			GenerateItem(dist, coinPlacement.vertPercent);
		}
	}
}
