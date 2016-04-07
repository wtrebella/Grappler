using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrainPatternInstructionCurve : WhitTerrainPatternInstruction {
	public float targetSlope = 0.0f;
	public float radius = 30.0f;

	public WhitTerrainPatternInstructionCurve(float targetSlope, float radius) {
		instructionType = WhitTerrainPatternInstructionType.Curve;

		this.targetSlope = targetSlope;
		this.radius = radius;
	}
}