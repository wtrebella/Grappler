using UnityEngine;
using System.Collections;

public class CoinCluster : GeneratableItem {
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private float multiplier = 2;

	public void Initialize(CoinPattern coinPatternData) {
		GenerateCoins(coinPatternData);
		Vector3 relativeRotation = new Vector3(0, 0, -360);
		Go.to(transform, 3.0f, new GoTweenConfig()
			.setEaseType(GoEaseType.Linear)
			.setIterations(-1)
			.localRotation(relativeRotation, true)
		);
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
