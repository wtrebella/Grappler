using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPatternGenerator {
	public static WhitTerrainPairPattern GetEndPattern(float currentSlope, float length) {
		WhitTerrainPatternInstructionStraight topStraight = new WhitTerrainPatternInstructionStraight(currentSlope, length, true);
		WhitTerrainPatternInstructionStraight bottomStraight = new WhitTerrainPatternInstructionStraight(currentSlope, length, true);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(topStraight, bottomStraight);

		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern(straight);
		return straightPattern;
	}

	public static WhitTerrainPairPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
		WhitTerrainPatternInstructionStraight topStraight = new WhitTerrainPatternInstructionStraight(currentSlope, topLength, true);
		WhitTerrainPatternInstructionStraight bottomStraight = new WhitTerrainPatternInstructionStraight(currentSlope, bottomLength, true);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(topStraight, bottomStraight);

		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern(straight);
		return straightPattern;
	}

	public static WhitTerrainPairPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
		WhitTerrainPatternInstructionCurve topCurveOut = new WhitTerrainPatternInstructionCurve(currentSlope + widenSlope, radius, true);
		WhitTerrainPatternInstructionCurve bottomCurveOut = new WhitTerrainPatternInstructionCurve(currentSlope - widenSlope, radius, true);
		WhitTerrainPatternInstructionPair curveOut = new WhitTerrainPatternInstructionPair(topCurveOut, bottomCurveOut);

		WhitTerrainPatternInstructionCurve topCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius, true);
		WhitTerrainPatternInstructionCurve bottomCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius, true);
		WhitTerrainPatternInstructionPair curveBack = new WhitTerrainPatternInstructionPair(topCurveBack, bottomCurveBack);

		WhitTerrainPairPattern widenPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return widenPattern;
	}

	public static WhitTerrainPairPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
		WhitTerrainPatternInstructionCurve topCurveIn = new WhitTerrainPatternInstructionCurve(currentSlope - narrowSlope, radius, false);
		WhitTerrainPatternInstructionCurve bottomCurveIn = new WhitTerrainPatternInstructionCurve(currentSlope + narrowSlope, radius, false);
		WhitTerrainPatternInstructionPair curveOut = new WhitTerrainPatternInstructionPair(topCurveIn, bottomCurveIn);

		WhitTerrainPatternInstructionCurve topCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius, false);
		WhitTerrainPatternInstructionCurve bottomCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius, false);
		WhitTerrainPatternInstructionPair curveBack = new WhitTerrainPatternInstructionPair(topCurveBack, bottomCurveBack);

		WhitTerrainPairPattern narrowPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return narrowPattern;
	}

	public static WhitTerrainPairPattern GetBumpPattern(float currentSlope, float bumpSlope, float minRadius, float maxRadius) {
		WhitTerrainPatternInstructionCurve topTurn1 = new WhitTerrainPatternInstructionCurve(currentSlope + bumpSlope, minRadius, true);
		WhitTerrainPatternInstructionCurve bottomTurn1 = new WhitTerrainPatternInstructionCurve(currentSlope + bumpSlope, maxRadius, true);
		WhitTerrainPatternInstructionPair turn1 = new WhitTerrainPatternInstructionPair(topTurn1, bottomTurn1);

		WhitTerrainPatternInstructionCurve topTurn2 = new WhitTerrainPatternInstructionCurve(currentSlope - bumpSlope, maxRadius, true);
		WhitTerrainPatternInstructionCurve bottomTurn2 = new WhitTerrainPatternInstructionCurve(currentSlope - bumpSlope, minRadius, true);
		WhitTerrainPatternInstructionPair turn2 = new WhitTerrainPatternInstructionPair(topTurn2, bottomTurn2);

		WhitTerrainPatternInstructionCurve topTurn3 = new WhitTerrainPatternInstructionCurve(currentSlope, minRadius, true);
		WhitTerrainPatternInstructionCurve bottomTurn3 = new WhitTerrainPatternInstructionCurve(currentSlope, maxRadius, true);
		WhitTerrainPatternInstructionPair turn3 = new WhitTerrainPatternInstructionPair(topTurn3, bottomTurn3);

		WhitTerrainPairPattern bumpPattern = new WhitTerrainPairPattern(turn1, turn2, turn3);
		return bumpPattern;
	}

	public static WhitTerrainPairPattern GetFlatPattern(float length) {
		WhitTerrainPatternInstructionStraight topFlat = new WhitTerrainPatternInstructionStraight(0, length, true);
		WhitTerrainPatternInstructionStraight bottomFlat = new WhitTerrainPatternInstructionStraight(0, length, false);
		WhitTerrainPatternInstructionPair flat = new WhitTerrainPatternInstructionPair(topFlat, bottomFlat);

		WhitTerrainPairPattern flatPattern = new WhitTerrainPairPattern(flat);
		return flatPattern;
	}
}
