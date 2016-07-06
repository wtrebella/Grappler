using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPatternGenerator {
	public static WhitTerrainPairPattern GetEndPattern(float currentSlope, float topLength, float bottomLength, float endSlope, float radius) {
		WhitTerrainPatternInstructionPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
		WhitTerrainPatternInstructionPair curve = GetDivergentCurve(currentSlope, endSlope, -endSlope, radius, radius, true);
		WhitTerrainPairPattern endPattern = new WhitTerrainPairPattern(straight, curve);
		return endPattern;
	}

	public static WhitTerrainPairPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
		WhitTerrainPatternInstructionPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern(straight);
		return straightPattern;
	}

	public static WhitTerrainPairPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
		WhitTerrainPatternInstructionPair curveOut = GetDivergentCurve(currentSlope, widenSlope, -widenSlope, radius, radius, true);
		WhitTerrainPatternInstructionPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
		WhitTerrainPairPattern widenPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return widenPattern;
	}

	public static WhitTerrainPairPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
		WhitTerrainPatternInstructionPair curveOut = GetDivergentCurve(currentSlope, -narrowSlope, narrowSlope, radius, radius, true);
		WhitTerrainPatternInstructionPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
		WhitTerrainPairPattern narrowPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return narrowPattern;
	}

	public static WhitTerrainPairPattern GetBumpPattern(float currentSlope, float bumpSlope, float upLength, float topLength, float downLength, float minRadius, float maxRadius) {
		WhitTerrainPatternInstructionPair turn1 = GetParallelCurve(currentSlope, bumpSlope, minRadius, maxRadius, true);
		WhitTerrainPatternInstructionPair up = GetStraight(currentSlope, bumpSlope, upLength, upLength, true);
		WhitTerrainPatternInstructionPair turn2 = GetParallelCurve(currentSlope, -bumpSlope, maxRadius, minRadius, true);
		WhitTerrainPatternInstructionPair down = GetStraight(currentSlope, -bumpSlope, downLength, downLength, true);
		WhitTerrainPatternInstructionPair turn3 = GetParallelCurve(currentSlope, 0, minRadius, maxRadius, true);
		WhitTerrainPairPattern bumpPattern = new WhitTerrainPairPattern(turn1, up, turn2, down, turn3);
		return bumpPattern;
	}

	public static WhitTerrainPairPattern GetFlatPattern(float length) {
		WhitTerrainPatternInstructionPair flat = GetStraight(0, 0, length, length, true);
		WhitTerrainPairPattern flatPattern = new WhitTerrainPairPattern(flat);
		return flatPattern;
	}

	private static WhitTerrainPatternInstructionPair GetParallelCurve(float currentSlope, float deltaSlope, float topRadius, float bottomRadius, bool bumpify) {
		WhitTerrainPatternInstructionCurve top = new WhitTerrainPatternInstructionCurve(currentSlope + deltaSlope, topRadius, bumpify);
		WhitTerrainPatternInstructionCurve bottom = new WhitTerrainPatternInstructionCurve(currentSlope + deltaSlope, bottomRadius, bumpify);
		WhitTerrainPatternInstructionPair curve = new WhitTerrainPatternInstructionPair(top, bottom);
		return curve;
	}

	private static WhitTerrainPatternInstructionPair GetDivergentCurve(float currentSlope, float topDeltaSlope, float bottomDeltaSlope, float topRadius, float bottomRadius, bool bumpify) {
		WhitTerrainPatternInstructionCurve top = new WhitTerrainPatternInstructionCurve(currentSlope + topDeltaSlope, topRadius, bumpify);
		WhitTerrainPatternInstructionCurve bottom = new WhitTerrainPatternInstructionCurve(currentSlope + bottomDeltaSlope, bottomRadius, bumpify);
		WhitTerrainPatternInstructionPair curve = new WhitTerrainPatternInstructionPair(top, bottom);
		return curve;
	}

	private static WhitTerrainPatternInstructionPair GetStraight(float currentSlope, float deltaSlope, float topLength, float bottomLength, bool bumpify) {
		WhitTerrainPatternInstructionStraight top = new WhitTerrainPatternInstructionStraight(currentSlope + deltaSlope, topLength, bumpify);
		WhitTerrainPatternInstructionStraight bottom = new WhitTerrainPatternInstructionStraight(currentSlope + deltaSlope, bottomLength, bumpify);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(top, bottom);
		return straight;
	}
}
