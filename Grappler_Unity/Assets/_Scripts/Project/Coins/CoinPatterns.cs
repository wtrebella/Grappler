using UnityEngine;
using System.Collections;

public class CoinPatterns {
//	public static CoinPattern RightArrow {get {return GenerateCoinPatternData(stringArray_rightArrow);}}
//	private static string[] stringArray_rightArrow = new string[] {
//		"    xx   ",
//		"    x x  ",
//		"xxxxx  x ",
//		"x       x",
//		"xxxxx  x ",
//		"    x x  ",
//		"    xx   "
//	};

	public static CoinPattern Square {get {return GenerateCoinPatternData(stringArray_square);}}
	private static string[] stringArray_square = new string[] {
		"xxxxx",
		"x   x",
		"x   x",
		"x   x",
		"xxxxx"
	};

	public static CoinPattern Circle {get {return GenerateCoinPatternData(stringArray_circle);}}
	private static string[] stringArray_circle = new string[] {
		"  x  ",
		" x x ",
		"x   x",
		" x x ",
		"  x  "
	};

	public static CoinPattern RandomPattern {get {return GetRandomCoinPatternData();}}

	private static CoinPattern GetRandomCoinPatternData() {
		string[] stringArray = (string[])RXRandom.Select(
//			stringArray_rightArrow,
			stringArray_square,
			stringArray_circle
		);

		return GenerateCoinPatternData(stringArray);
	}
		
	private static CoinPattern GenerateCoinPatternData(string[] pattern) {
		CoinPattern coinPattern = new CoinPattern();
		char[][] charArray = StringArrayToCharArray(pattern);
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

	private static char[][] StringArrayToCharArray(string[] stringArray) {
		char[][] charArray = new char[stringArray.Length][];
		for (int i = 0; i < stringArray.Length; i++) charArray[i] = stringArray[i].ToCharArray();
		return charArray;
	}
}
