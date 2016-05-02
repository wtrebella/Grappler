using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairAttributes : ScriptableObjectSingleton<WhitTerrainPairAttributes> {
	public FloatRange slopeVariationRange = new FloatRange(-0.2f, 0.2f);
	public FloatRange widthRange = new FloatRange(6.0f, 20.0f);
	public FloatRange iceWidthRange = new FloatRange(100.0f, 200.0f);

	public float drawDistWidth = 200.0f;
	public float playerDistThreshold = 30.0f;
	public float straightLength = 30.0f;
	public float minCurveRadius = 20.0f;

	[SerializeField] private int continuePatternWeight = 10;
	[SerializeField] private int widenPatternWeight = 2;
	[SerializeField] private int narrowPatternWeight = 2;
	[SerializeField] private int bumpPatternWeight = 1;
	[SerializeField] private int flatPatternWeight = 3;

	public WhitTerrainPairPatternType GetRandomPatternType() {
		List<WhitTerrainPairPatternType> patternTypes = new List<WhitTerrainPairPatternType>();
		for (int i = 0; i < continuePatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Straight);
		for (int i = 0; i < widenPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Widen);
		for (int i = 0; i < narrowPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Narrow);
		for (int i = 0; i < bumpPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Bump);
		for (int i = 0; i < flatPatternWeight; i++) patternTypes.Add(WhitTerrainPairPatternType.Flat);
		return patternTypes.Random();
	}
}
