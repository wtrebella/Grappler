using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CoinClusterGenerator : ItemBetweenTerrainPairGenerator {
	[SerializeField] private float probability = 0.5f;

	protected override void OnPatternAdded(FloatRange distRange) {
		if (UnityEngine.Random.value > probability) return;
		GenerateCoinPattern(distRange);
	}

	private void GenerateCoinPattern(FloatRange distRange) {
		CoinCluster coinPattern = GenerateItem<CoinCluster>(distRange.GetRandom(), 0.5f);
		coinPattern.Initialize(GetCoinPatternData());
	}

	private CoinPattern GetCoinPatternData() {
		return CoinPatterns.RandomPattern;
	}
}
