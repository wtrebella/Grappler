using UnityEngine;
using System.Collections;

public class CoinPatterns {
	public static CoinPattern Diagonal {get {return GenerateCoinPattern(stringArray_diagonal);}}
	private static string[] stringArray_diagonal = new string[] {
		"    x",
		"  x  ",
		"x    "
	};

	public static CoinPattern RandomPattern {get {return GetRandomCoinPattern();}}

	private static CoinPattern GetRandomCoinPattern() {
		string[] stringArray = (string[])RXRandom.Select(
			stringArray_diagonal,
			stringArray_diagonal
		);

		return GenerateCoinPattern(stringArray);
	}
		
	private static CoinPattern GenerateCoinPattern(string[] pattern) {
		CoinPattern coinPattern = new CoinPattern();
		char[][] charArray = StringArrayToCharArray(pattern);

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
		return new Vector2((charArray[0].Length - 1) / 2f, (charArray.Length - 1) / 2f);
	}

	private static char[][] StringArrayToCharArray(string[] stringArray) {
		char[][] charArray = new char[stringArray.Length][];
		for (int i = 0; i < stringArray.Length; i++) charArray[i] = stringArray[i].ToCharArray();
		return charArray;
	}
}
