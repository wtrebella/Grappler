using UnityEngine;
using System.Collections;

public class CoinPatternGenerator {
	private static CoinPattern _testPattern;
	public static CoinPattern testPattern {
		get {
			if (_testPattern == null) _testPattern = GenerateTestPattern();
			return _testPattern;
		}
	}

	private static CoinPattern GenerateTestPattern() {
		CoinPattern coinPattern = new CoinPattern();

		coinPattern.AddCoinPlacement(0.2f, 0.5f);
		coinPattern.AddCoinPlacement(0.3f, 0.5f);
		coinPattern.AddCoinPlacement(0.4f, 0.5f);
		coinPattern.AddCoinPlacement(0.5f, 0.5f);
		coinPattern.AddCoinPlacement(0.6f, 0.5f);

		coinPattern.AddCoinPlacement(0.3f, 0.6f);
		coinPattern.AddCoinPlacement(0.4f, 0.6f);
		coinPattern.AddCoinPlacement(0.5f, 0.6f);

		coinPattern.AddCoinPlacement(0.4f, 0.7f);

		return coinPattern;
	}
}
