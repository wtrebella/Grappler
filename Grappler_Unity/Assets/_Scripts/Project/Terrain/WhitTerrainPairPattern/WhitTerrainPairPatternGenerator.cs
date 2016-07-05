using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPatternGenerator {
	public static WhitTerrainPairPattern GetEndPattern(float currentSlope, float topLength, float bottomLength, float endSlope, float radius) {
		WhitTerrainPatternInstructionPair straight = GetStraight(currentSlope, topLength, bottomLength, true);
		WhitTerrainPatternInstructionPair curve = GetCurve(currentSlope + endSlope, currentSlope - endSlope, radius, radius, true);
		WhitTerrainPairPattern endPattern = new WhitTerrainPairPattern(straight, curve);
		return endPattern;
	}

	public static WhitTerrainPairPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
		WhitTerrainPatternInstructionPair straight = GetStraight(currentSlope, topLength, bottomLength, true);
		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern(straight);
		return straightPattern;
	}

	public static WhitTerrainPairPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
		WhitTerrainPatternInstructionPair curveOut = GetCurve(currentSlope + widenSlope, currentSlope - widenSlope, radius, radius, true);
		WhitTerrainPatternInstructionPair curveBack = GetCurve(currentSlope, currentSlope, radius, radius, true);
		WhitTerrainPairPattern widenPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return widenPattern;
	}

	public static WhitTerrainPairPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
		WhitTerrainPatternInstructionPair curveOut = GetCurve(currentSlope - narrowSlope, currentSlope + narrowSlope, radius, radius, true);
		WhitTerrainPatternInstructionPair curveBack = GetCurve(currentSlope, currentSlope, radius, radius, true);
		WhitTerrainPairPattern narrowPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return narrowPattern;
	}

	public static WhitTerrainPairPattern GetBumpPattern(float currentSlope, float bumpSlope, float upLength, float topLength, float downLength, float minRadius, float maxRadius) {
		WhitTerrainPatternInstructionPair turn1 = GetCurve(currentSlope + bumpSlope, currentSlope + bumpSlope, minRadius, maxRadius, true);
		WhitTerrainPatternInstructionPair up = GetStraight(currentSlope + bumpSlope, upLength, upLength, true);
		WhitTerrainPatternInstructionPair turn2 = GetCurve(currentSlope - bumpSlope, currentSlope - bumpSlope, maxRadius, minRadius, true);
		WhitTerrainPatternInstructionPair down = GetStraight(currentSlope - bumpSlope, downLength, downLength, true);
		WhitTerrainPatternInstructionPair turn3 = GetCurve(currentSlope, currentSlope, minRadius, maxRadius, true);
		WhitTerrainPairPattern bumpPattern = new WhitTerrainPairPattern(turn1, up, turn2, down, turn3);
		return bumpPattern;
	}

	public static WhitTerrainPairPattern GetFlatPattern(float length) {
		WhitTerrainPatternInstructionPair flat = GetStraight(0, length, length, true);
		WhitTerrainPairPattern flatPattern = new WhitTerrainPairPattern(flat);
		return flatPattern;
	}

	private static WhitTerrainPatternInstructionPair GetCurve(float topSlope, float bottomSlope, float topRadius, float bottomRadius, bool bumpify) {
		WhitTerrainPatternInstructionCurve top = new WhitTerrainPatternInstructionCurve(topSlope, topRadius, bumpify);
		WhitTerrainPatternInstructionCurve bottom = new WhitTerrainPatternInstructionCurve(bottomSlope, bottomRadius, bumpify);
		WhitTerrainPatternInstructionPair curve = new WhitTerrainPatternInstructionPair(top, bottom);
		return curve;
	}

	private static WhitTerrainPatternInstructionPair GetStraight(float slope, float topLength, float bottomLength, bool bumpify) {
		WhitTerrainPatternInstructionStraight top = new WhitTerrainPatternInstructionStraight(slope, topLength, bumpify);
		WhitTerrainPatternInstructionStraight bottom = new WhitTerrainPatternInstructionStraight(slope, bottomLength, bumpify);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(top, bottom);
		return straight;
	}
}
