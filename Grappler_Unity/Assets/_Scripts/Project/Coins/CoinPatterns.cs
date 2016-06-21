using UnityEngine;
using System.Collections;

public class CoinPatterns {
	public static CoinPattern Square {get {return GenerateCoinPattern(stringArray_square);}}
	private static string[] stringArray_square = new string[] {
		"xxx",
		"x x",
		"xxx"
	};

	public static CoinPattern XShape {get {return GenerateCoinPattern(stringArray_x);}}
	private static string[] stringArray_x = new string[] {
		"x x",
		" x ",
		"x x"
	};

	public static CoinPattern RandomPattern {get {return GetRandomCoinPattern();}}

	private static CoinPattern GetRandomCoinPattern() {
		string[] stringArray = (string[])RXRandom.Select(
			stringArray_square,
			stringArray_x
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
