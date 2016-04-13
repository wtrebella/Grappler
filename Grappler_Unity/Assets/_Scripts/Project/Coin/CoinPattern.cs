using UnityEngine;
using System.Collections;

public class CoinPattern : GeneratableItem {
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private float multiplier = 2;

	private CoinPatternType coinPatternType;

	public void Initialize(CoinPatternType coinPatternType) {
		this.coinPatternType = coinPatternType;
		GenerateCoins(coinPatternType);
		Vector3 relativeRotation = new Vector3(0, 0, -360);
		Go.to(transform, 3.0f, new GoTweenConfig().setEaseType(GoEaseType.Linear).setIterations(-1).localRotation(relativeRotation, true));
	}

	private void GenerateCoins(CoinPatternType coinPatternType) {
		CoinPatternData coinPatternData = CoinPatternDataGenerator.GetCoinPatternData(coinPatternType);
		if (coinPatternData == null) return;
		foreach (Vector2 point in coinPatternData.points) {
			Coin coin = coinPrefab.Spawn();
			coin.SetCoinPattern(this);
			coin.transform.parent = transform;
			coin.transform.localPosition = point * multiplier;
		}
	}
}
