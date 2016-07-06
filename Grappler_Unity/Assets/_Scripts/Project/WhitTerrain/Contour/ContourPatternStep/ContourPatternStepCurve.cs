using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class ContourPatternStepCurve : ContourPatternStep {
		public float targetSlope = 0.0f;
		public float radius = 30.0f;
		public bool bumpify = true;

		public ContourPatternStepCurve(float targetSlope, float radius, bool bumpify) {
			stepType = ContourPatternStepType.Curve;

			this.targetSlope = targetSlope;
			this.radius = radius;
			this.bumpify = bumpify;
		}
	}
}