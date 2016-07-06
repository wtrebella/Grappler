using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhitTerrain {
	public class ContourPatternInstructionStraight : ContourPatternInstruction {
		public float slope = 0.0f;
		public float length = 10.0f;
		public bool bumpify = true;

		public ContourPatternInstructionStraight(float slope, float length, bool bumpify) {
			instructionType = ContourPatternInstructionType.Straight;
			this.slope = slope;
			this.length = length;
			this.bumpify = bumpify;
		}
	}
}