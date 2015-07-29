using UnityEngine;
using System.Collections;

public static class WhitTools {
	public static void Assert(bool condition, string errorString = "") {
		if (!condition) throw new UnityException(errorString);
	}
}
