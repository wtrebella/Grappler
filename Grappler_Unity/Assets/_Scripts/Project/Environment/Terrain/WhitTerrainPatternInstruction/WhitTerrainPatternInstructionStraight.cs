using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WhitTerrainPatternInstructionStraight : WhitTerrainPatternInstruction {
	public float slope = 0.0f;
	public float length = 10.0f;

	public WhitTerrainPatternInstructionStraight(float slope, float length) {
		instructionType = WhitTerrainPatternInstructionType.Straight;
		this.slope = slope;
		this.length = length;
	}
}