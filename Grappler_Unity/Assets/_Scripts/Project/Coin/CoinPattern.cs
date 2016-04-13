using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinPattern {
	public List<CoinPlacement> coinPlacements {get; private set;}

	public CoinPattern() {
		coinPlacements = new List<CoinPlacement>();
	}

	public void AddCoinPlacement(CoinPlacement coinPlacement) {
		coinPlacements.Add(coinPlacement);
	}

	public void AddCoinPlacement(float distPercent, float vertPercent) {
		CoinPlacement coinPlacement = new CoinPlacement(distPercent, vertPercent);
		coinPlacements.Add(coinPlacement);
	}
}
