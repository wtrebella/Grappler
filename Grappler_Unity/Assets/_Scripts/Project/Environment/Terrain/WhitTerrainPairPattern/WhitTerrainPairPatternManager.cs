using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPatternManager : MonoBehaviour {
	private List<WhitTerrainPairPattern> patterns;

	private void Awake() {
		InitializePatterns();
	}

	private void InitializePatterns() {
		patterns = new List<WhitTerrainPairPattern>();

		InitializeStraightPattern();
	}

	private void InitializeStraightPattern() {
		WhitTerrainPatternInstructionStraight topStraight = new WhitTerrainPatternInstructionStraight(0.0f, 30.0f);
		WhitTerrainPatternInstructionStraight bottomStraight = new WhitTerrainPatternInstructionStraight(0.0f, 30.0f);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(topStraight, bottomStraight);

		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern();
		straightPattern.AddInstructionPair(straight);
		patterns.Add(straightPattern);
	}

	private void InitializeWidenPattern() {
		float slope = 30.0f;

		WhitTerrainPatternInstructionCurve topCurveOut = new WhitTerrainPatternInstructionCurve(0.5f, slope);
		WhitTerrainPatternInstructionCurve bottomCurveOut = new WhitTerrainPatternInstructionCurve(-0.5f, slope);
		WhitTerrainPatternInstructionPair curveOut = new WhitTerrainPatternInstructionPair(topCurveOut, bottomCurveOut);

		WhitTerrainPatternInstructionCurve topCurveBack = new WhitTerrainPatternInstructionCurve(0.0f, slope);
		WhitTerrainPatternInstructionCurve bottomCurveBack = new WhitTerrainPatternInstructionCurve(0.0f, slope);
		WhitTerrainPatternInstructionPair curveBack = new WhitTerrainPatternInstructionPair(topCurveBack, bottomCurveBack);

		WhitTerrainPairPattern widenPattern = new WhitTerrainPairPattern();
		widenPattern.AddInstructionPair(curveOut);
		widenPattern.AddInstructionPair(curveBack);
		patterns.Add(widenPattern);
	}
}
