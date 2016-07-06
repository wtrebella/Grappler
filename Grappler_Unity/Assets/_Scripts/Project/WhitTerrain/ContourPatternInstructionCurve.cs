using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class ContourPatternInstructionCurve : ContourPatternInstruction {
		public float targetSlope = 0.0f;
		public float radius = 30.0f;
		public bool bumpify = true;

		public ContourPatternInstructionCurve(float targetSlope, float radius, bool bumpify) {
			instructionType = ContourPatternInstructionType.Curve;

			this.targetSlope = targetSlope;
			this.radius = radius;
			this.bumpify = bumpify;
		}
	}
}