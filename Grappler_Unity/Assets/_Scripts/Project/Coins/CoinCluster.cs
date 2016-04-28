using UnityEngine;
using System.Collections;

public class CoinCluster : GeneratableItem {
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private float multiplier = 2;

	public void Initialize(CoinPattern coinPatternData) {
		GenerateCoins(coinPatternData);
	}

	private void GenerateCoins(CoinPattern coinPatternData) {
		if (coinPatternData == null) return;
		foreach (Vector2 point in coinPatternData.points) {
			Coin coin = coinPrefab.Spawn();
			coin.transform.parent = transform;
			coin.transform.localPosition = point * multiplier;
		}
	}
}
