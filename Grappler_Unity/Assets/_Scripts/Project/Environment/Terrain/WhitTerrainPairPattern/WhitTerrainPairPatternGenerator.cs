using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainPairPatternGenerator {
	public static WhitTerrainPairPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
		WhitTerrainPatternInstructionStraight topStraight = new WhitTerrainPatternInstructionStraight(currentSlope, topLength);
		WhitTerrainPatternInstructionStraight bottomStraight = new WhitTerrainPatternInstructionStraight(currentSlope, bottomLength);
		WhitTerrainPatternInstructionPair straight = new WhitTerrainPatternInstructionPair(topStraight, bottomStraight);

		WhitTerrainPairPattern straightPattern = new WhitTerrainPairPattern(straight);
		return straightPattern;
	}

	public static WhitTerrainPairPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
		WhitTerrainPatternInstructionCurve topCurveOut = new WhitTerrainPatternInstructionCurve(currentSlope + widenSlope, radius);
		WhitTerrainPatternInstructionCurve bottomCurveOut = new WhitTerrainPatternInstructionCurve(currentSlope - widenSlope, radius);
		WhitTerrainPatternInstructionPair curveOut = new WhitTerrainPatternInstructionPair(topCurveOut, bottomCurveOut);

		WhitTerrainPatternInstructionCurve topCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius);
		WhitTerrainPatternInstructionCurve bottomCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius);
		WhitTerrainPatternInstructionPair curveBack = new WhitTerrainPatternInstructionPair(topCurveBack, bottomCurveBack);

		WhitTerrainPairPattern widenPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return widenPattern;
	}

	public static WhitTerrainPairPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
		WhitTerrainPatternInstructionCurve topCurveIn = new WhitTerrainPatternInstructionCurve(currentSlope - narrowSlope, radius);
		WhitTerrainPatternInstructionCurve bottomCurveIn = new WhitTerrainPatternInstructionCurve(currentSlope + narrowSlope, radius);
		WhitTerrainPatternInstructionPair curveOut = new WhitTerrainPatternInstructionPair(topCurveIn, bottomCurveIn);

		WhitTerrainPatternInstructionCurve topCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius);
		WhitTerrainPatternInstructionCurve bottomCurveBack = new WhitTerrainPatternInstructionCurve(currentSlope, radius);
		WhitTerrainPatternInstructionPair curveBack = new WhitTerrainPatternInstructionPair(topCurveBack, bottomCurveBack);

		WhitTerrainPairPattern narrowPattern = new WhitTerrainPairPattern(curveOut, curveBack);
		return narrowPattern;
	}

	public static WhitTerrainPairPattern GetBumpPattern(float currentSlope, float bumpSlope, float minRadius, float maxRadius) {
		WhitTerrainPatternInstructionCurve topTurn1 = new WhitTerrainPatternInstructionCurve(currentSlope + bumpSlope, minRadius);
		WhitTerrainPatternInstructionCurve bottomTurn1 = new WhitTerrainPatternInstructionCurve(currentSlope + bumpSlope, maxRadius);
		WhitTerrainPatternInstructionPair turn1 = new WhitTerrainPatternInstructionPair(topTurn1, bottomTurn1);

		WhitTerrainPatternInstructionCurve topTurn2 = new WhitTerrainPatternInstructionCurve(currentSlope - bumpSlope, maxRadius);
		WhitTerrainPatternInstructionCurve bottomTurn2 = new WhitTerrainPatternInstructionCurve(currentSlope - bumpSlope, minRadius);
		WhitTerrainPatternInstructionPair turn2 = new WhitTerrainPatternInstructionPair(topTurn2, bottomTurn2);

		WhitTerrainPatternInstructionCurve topTurn3 = new WhitTerrainPatternInstructionCurve(currentSlope, minRadius);
		WhitTerrainPatternInstructionCurve bottomTurn3 = new WhitTerrainPatternInstructionCurve(currentSlope, maxRadius);
		WhitTerrainPatternInstructionPair turn3 = new WhitTerrainPatternInstructionPair(topTurn3, bottomTurn3);

		WhitTerrainPairPattern bumpPattern = new WhitTerrainPairPattern(turn1, turn2, turn3);
		return bumpPattern;
	}

	public static WhitTerrainPairPattern GetFlatPattern(float length) {
		WhitTerrainPatternInstructionStraight topFlat = new WhitTerrainPatternInstructionStraight(0, length);
		WhitTerrainPatternInstructionStraight bottomFlat = new WhitTerrainPatternInstructionStraight(0, length);
		WhitTerrainPatternInstructionPair flat = new WhitTerrainPatternInstructionPair(topFlat, bottomFlat);

		WhitTerrainPairPattern flatPattern = new WhitTerrainPairPattern(flat);
		return flatPattern;
	}
}
