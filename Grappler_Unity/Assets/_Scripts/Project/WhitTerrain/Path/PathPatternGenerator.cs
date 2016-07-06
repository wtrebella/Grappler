using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class PathPatternGenerator {
		public static PathPattern GetEndPattern(float currentSlope, float topLength, float bottomLength, float endSlope, float radius) {
			ContourPatternStepPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
			ContourPatternStepPair curve = GetDivergentCurve(currentSlope, endSlope, -endSlope, radius, radius, true);
			PathPattern endPattern = new PathPattern(straight, curve);
			return endPattern;
		}

		public static PathPattern GetStraightPattern(float currentSlope, float topLength, float bottomLength) {
			ContourPatternStepPair straight = GetStraight(currentSlope, 0, topLength, bottomLength, true);
			PathPattern straightPattern = new PathPattern(straight);
			return straightPattern;
		}

		public static PathPattern GetWidenPattern(float currentSlope, float widenSlope, float radius) {
			ContourPatternStepPair curveOut = GetDivergentCurve(currentSlope, widenSlope, -widenSlope, radius, radius, true);
			ContourPatternStepPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
			PathPattern widenPattern = new PathPattern(curveOut, curveBack);
			return widenPattern;
		}

		public static PathPattern GetNarrowPattern(float currentSlope, float narrowSlope, float radius) {
			ContourPatternStepPair curveOut = GetDivergentCurve(currentSlope, -narrowSlope, narrowSlope, radius, radius, true);
			ContourPatternStepPair curveBack = GetDivergentCurve(currentSlope, 0, 0, radius, radius, true);
			PathPattern narrowPattern = new PathPattern(curveOut, curveBack);
			return narrowPattern;
		}

		public static PathPattern GetBumpPattern(float currentSlope, float bumpSlope, float upLength, float topLength, float downLength, float minRadius, float maxRadius) {
			ContourPatternStepPair turn1 = GetParallelCurve(currentSlope, bumpSlope, minRadius, maxRadius, true);
			ContourPatternStepPair up = GetStraight(currentSlope, bumpSlope, upLength, upLength, true);
			ContourPatternStepPair turn2 = GetParallelCurve(currentSlope, -bumpSlope, maxRadius, minRadius, true);
			ContourPatternStepPair down = GetStraight(currentSlope, -bumpSlope, downLength, downLength, true);
			ContourPatternStepPair turn3 = GetParallelCurve(currentSlope, 0, minRadius, maxRadius, true);
			PathPattern bumpPattern = new PathPattern(turn1, up, turn2, down, turn3);
			return bumpPattern;
		}

		public static PathPattern GetFlatPattern(float length) {
			ContourPatternStepPair flat = GetStraight(0, 0, length, length, true);
			PathPattern flatPattern = new PathPattern(flat);
			return flatPattern;
		}

		private static ContourPatternStepPair GetParallelCurve(float currentSlope, float deltaSlope, float topRadius, float bottomRadius, bool bumpify) {
			ContourPatternStepCurve top = new ContourPatternStepCurve(currentSlope + deltaSlope, topRadius, bumpify);
			ContourPatternStepCurve bottom = new ContourPatternStepCurve(currentSlope + deltaSlope, bottomRadius, bumpify);
			ContourPatternStepPair curve = new ContourPatternStepPair(top, bottom);
			return curve;
		}

		private static ContourPatternStepPair GetDivergentCurve(float currentSlope, float topDeltaSlope, float bottomDeltaSlope, float topRadius, float bottomRadius, bool bumpify) {
			ContourPatternStepCurve top = new ContourPatternStepCurve(currentSlope + topDeltaSlope, topRadius, bumpify);
			ContourPatternStepCurve bottom = new ContourPatternStepCurve(currentSlope + bottomDeltaSlope, bottomRadius, bumpify);
			ContourPatternStepPair curve = new ContourPatternStepPair(top, bottom);
			return curve;
		}

		private static ContourPatternStepPair GetStraight(float currentSlope, float deltaSlope, float topLength, float bottomLength, bool bumpify) {
			ContourPatternStepStraight top = new ContourPatternStepStraight(currentSlope + deltaSlope, topLength, bumpify);
			ContourPatternStepStraight bottom = new ContourPatternStepStraight(currentSlope + deltaSlope, bottomLength, bumpify);
			ContourPatternStepPair straight = new ContourPatternStepPair(top, bottom);
			return straight;
		}
	}
}