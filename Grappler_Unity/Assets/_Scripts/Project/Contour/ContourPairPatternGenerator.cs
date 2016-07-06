using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContourPairPatternGenerator {
	public static ContourPairPattern GetEndPattern(float currentSlope, float topLength, float bottomLength, float endSlope, float radius) {
		ContourPatternInstructionPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
		ContourPatternInstructionPair curve = GetDivergentCurve(currentSlope, endSlope, -endSlope, radius, radius, true);
		ContourPairPattern endPattern = new ContourPairPattern(straight, curve);
		return endPattern;
	}

	public static ContourPairPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
		ContourPatternInstructionPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
		ContourPairPattern straightPattern = new ContourPairPattern(straight);
		return straightPattern;
	}

	public static ContourPairPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
		ContourPatternInstructionPair curveOut = GetDivergentCurve(currentSlope, widenSlope, -widenSlope, radius, radius, true);
		ContourPatternInstructionPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
		ContourPairPattern widenPattern = new ContourPairPattern(curveOut, curveBack);
		return widenPattern;
	}

	public static ContourPairPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
		ContourPatternInstructionPair curveOut = GetDivergentCurve(currentSlope, -narrowSlope, narrowSlope, radius, radius, true);
		ContourPatternInstructionPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
		ContourPairPattern narrowPattern = new ContourPairPattern(curveOut, curveBack);
		return narrowPattern;
	}

	public static ContourPairPattern GetBumpPattern(float currentSlope, float bumpSlope, float upLength, float topLength, float downLength, float minRadius, float maxRadius) {
		ContourPatternInstructionPair turn1 = GetParallelCurve(currentSlope, bumpSlope, minRadius, maxRadius, true);
		ContourPatternInstructionPair up = GetStraight(currentSlope, bumpSlope, upLength, upLength, true);
		ContourPatternInstructionPair turn2 = GetParallelCurve(currentSlope, -bumpSlope, maxRadius, minRadius, true);
		ContourPatternInstructionPair down = GetStraight(currentSlope, -bumpSlope, downLength, downLength, true);
		ContourPatternInstructionPair turn3 = GetParallelCurve(currentSlope, 0, minRadius, maxRadius, true);
		ContourPairPattern bumpPattern = new ContourPairPattern(turn1, up, turn2, down, turn3);
		return bumpPattern;
	}

	public static ContourPairPattern GetFlatPattern(float length) {
		ContourPatternInstructionPair flat = GetStraight(0, 0, length, length, true);
		ContourPairPattern flatPattern = new ContourPairPattern(flat);
		return flatPattern;
	}

	private static ContourPatternInstructionPair GetParallelCurve(float currentSlope, float deltaSlope, float topRadius, float bottomRadius, bool bumpify) {
		ContourPatternInstructionCurve top = new ContourPatternInstructionCurve(currentSlope + deltaSlope, topRadius, bumpify);
		ContourPatternInstructionCurve bottom = new ContourPatternInstructionCurve(currentSlope + deltaSlope, bottomRadius, bumpify);
		ContourPatternInstructionPair curve = new ContourPatternInstructionPair(top, bottom);
		return curve;
	}

	private static ContourPatternInstructionPair GetDivergentCurve(float currentSlope, float topDeltaSlope, float bottomDeltaSlope, float topRadius, float bottomRadius, bool bumpify) {
		ContourPatternInstructionCurve top = new ContourPatternInstructionCurve(currentSlope + topDeltaSlope, topRadius, bumpify);
		ContourPatternInstructionCurve bottom = new ContourPatternInstructionCurve(currentSlope + bottomDeltaSlope, bottomRadius, bumpify);
		ContourPatternInstructionPair curve = new ContourPatternInstructionPair(top, bottom);
		return curve;
	}

	private static ContourPatternInstructionPair GetStraight(float currentSlope, float deltaSlope, float topLength, float bottomLength, bool bumpify) {
		ContourPatternInstructionStraight top = new ContourPatternInstructionStraight(currentSlope + deltaSlope, topLength, bumpify);
		ContourPatternInstructionStraight bottom = new ContourPatternInstructionStraight(currentSlope + deltaSlope, bottomLength, bumpify);
		ContourPatternInstructionPair straight = new ContourPatternInstructionPair(top, bottom);
		return straight;
	}
}
