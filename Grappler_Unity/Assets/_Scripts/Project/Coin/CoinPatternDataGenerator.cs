using UnityEngine;
using System.Collections;

public class CoinPatternDataGenerator {
	private static char[][] rightArrowArray = new char[][] {
		new char[] {'o', 'o', 'o', 'o', 'x', 'o', 'o'},
		new char[] {'o', 'o', 'o', 'o', 'x', 'x', 'o'},
		new char[] {'x', 'x', 'x', 'x', 'x', 'x', 'x'},
		new char[] {'x', 'x', 'x', 'x', 'x', 'x', 'x'},
		new char[] {'o', 'o', 'o', 'o', 'x', 'x', 'o'},
		new char[] {'o', 'o', 'o', 'o', 'x', 'o', 'o'}
	};

	public static CoinPatternData GetCoinPatternData(CoinPatternType coinPatternType) {
		switch (coinPatternType) {
		case CoinPatternType.RightArrow: return GenerateCoinPatternData(rightArrowArray);
		default: return null;
		}
	}

	private static CoinPatternData GenerateCoinPatternData(char[][] charArray) {
		CoinPatternData coinPattern = new CoinPatternData();

		Vector2 percentSize = new Vector2(0.3f, 0.3f);

		for (int y = 0; y < charArray.Length; y++) {
			for (int x = 0; x < charArray[y].Length; x++) {
				char c = charArray[y][x];
				if (c == 'x') {
					Vector2 localPosition = GetLocalPosition(charArray, new Vector2(x, charArray.Length - 1 - y));
					coinPattern.AddPoint(localPosition);
				}
			}
		}

		return coinPattern;
	}

	private static Vector2 GetLocalPosition(char[][] charArray, Vector2 charIndex) {
		Vector2 center = GetCenterIndex(charArray);
		return charIndex - center;
	}

	private static Vector2 GetCenterIndex(char[][] charArray) {
		return new Vector2((charArray.Length - 1) / 2f, (charArray[0].Length - 1) / 2f);
	}
}
