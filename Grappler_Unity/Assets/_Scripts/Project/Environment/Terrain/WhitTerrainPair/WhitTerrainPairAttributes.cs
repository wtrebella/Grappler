using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairAttributes : ScriptableObjectSingleton<WhitTerrainPairAttributes> {
	public FloatRange widthRange = new FloatRange(6.0f, 20.0f);

	public int continuePatternWeight = 10;
	public int widenPatternWeight = 2;
	public int narrowPatternWeight = 2;
	public int bumpPatternWeight = 1;

	public WhitTerrainPairPatternType GetRandomPatternType() {
		List<WhitTerrainPairPatternType> patternTypes = new List<WhitTerrainPairPatternType>();
		for (int i = 0; i < continuePatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Continue);
		for (int i = 0; i < widenPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Widen);
		for (int i = 0; i < narrowPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Narrow);
		for (int i = 0; i < bumpPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Bump);
		return patternTypes.Random();
	}
}
